using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Michaelsoft.BodyGuard.Common.Enums;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Common.Models;
using Michaelsoft.BodyGuard.Common.Settings;
using Michaelsoft.BodyGuard.Server.DatabaseModels;
using Michaelsoft.BodyGuard.Server.Exceptions;
using Michaelsoft.BodyGuard.Server.Interfaces;
using Michaelsoft.BodyGuard.Server.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class UserService
    {

        private readonly IMongoCollection<DbUser> _users;

        private readonly DatabaseEncryptionService _encryptionService;

        private readonly IdentitySettings _identitySettings;

        private readonly IRoleService _roleService;

        public UserService(IUserStoreDatabaseSettings settings,
                           IOptions<IdentitySettings> identitySettings,
                           DatabaseEncryptionService encryptionService,
                           IRoleService roleService)
        {
            _roleService = roleService;
            _identitySettings = identitySettings.Value;
            _encryptionService = encryptionService;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<DbUser>(settings.UsersCollectionName);
        }

        public List<DbUser> GetAll() => _users.Find(user => true).ToList();

        private DbUser GetById(string id) => _users.Find<DbUser>(user => user.Id == id).FirstOrDefault();

        public DbUser GetByEmail(string email) => GetByHashedEmail(email.Sha1());

        private DbUser GetByHashedEmail(string hashedEmail) =>
            _users.Find<DbUser>(user => user.HashedEmail == hashedEmail).FirstOrDefault();

        private string HashPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        private bool VerifyPassword(DbUser dbUser,
                                    string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, dbUser.HashedPassword);
        }

        private string EncryptUserData(User userData,
                                       string emailAddress = null)
        {
            if (userData == null) return null;
            if (emailAddress != null)
                userData.EmailAddress = emailAddress;
            var properties = typeof(User).GetProperties();
            foreach (var propertyInfo in properties)
                if (!_identitySettings.EnabledUserDataProperties.Contains(propertyInfo.Name))
                    typeof(User).GetProperty(propertyInfo.Name)?.SetMethod?.Invoke(userData, new object[] {null});

            var serializedData =
                JsonConvert.SerializeObject(userData,
                                            new JsonSerializerSettings
                                            {
                                                NullValueHandling = NullValueHandling.Ignore
                                            });
            return serializedData.IsNullOrEmpty() ? null : _encryptionService.Encrypt(serializedData);
        }

        public DbUser Create(string emailAddress,
                             string password,
                             User userData = null)
        {
            var encryptedData = EncryptUserData(userData, emailAddress);
            var user = new DbUser
            {
                HashedEmail = emailAddress.Sha1(),
                HashedPassword = HashPassword(password),
                EncryptedData = encryptedData,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            var existing = GetByHashedEmail(user.HashedEmail);
            if (existing != null) return existing;
            user.Roles = _users.EstimatedDocumentCount() == 0
                             ? new List<string> {Roles.Root}
                             : new List<string> {Roles.User};
            _users.InsertOne(user);
            return user;
        }

        public void UpdatePassword(string userId,
                                   string newPassword,
                                   string newPasswordConfirm)
        {
            if (newPassword != newPasswordConfirm) throw new PasswordsNotMatchingException();
            var user = GetById(userId);
            if (user == null) throw new UserNotFoundException();
            user.HashedPassword = HashPassword(newPassword);
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public DbUser Access(string emailAddress,
                             string password)
        {
            var hashedEmail = emailAddress.Sha1();
            var user = GetByHashedEmail(hashedEmail);
            if (user == null) throw new UserNotFoundException();
            return VerifyPassword(user, password) ? user : throw new WrongPasswordException();
        }

        public void Delete(string id)
        {
            var user = GetById(id);
            if (user == null) throw new UserNotFoundException();
            _users.DeleteOne(u => u.Id == user.Id);
        }

        public string GetData(string id)
        {
            var user = GetById(id);
            if (user == null) throw new UserNotFoundException();
            return user.EncryptedData == null ? null : _encryptionService.Decrypt(user.EncryptedData);
        }

        public void UpdateData(string id,
                               User userData)
        {
            var user = GetById(id);
            if (user == null) throw new UserNotFoundException();
            user.Updated = DateTime.Now;
            user.EncryptedData = EncryptUserData(userData);
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void AssignRole(string emailAddress,
                               string role)
        {
            if(_roleService[role] == null) throw new RoleNotFoundException();
            var user = GetByEmail(emailAddress);
            if (user == null) throw new UserNotFoundException();
            user.Roles ??= new List<string>();
            if (user.Roles.Contains(role)) return;
            user.Roles.Add(role);
            user.Updated = DateTime.Now;
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void RevokeRole(string emailAddress,
                               string role)
        {
            if(_roleService[role] == null) throw new RoleNotFoundException();
            var user = GetByEmail(emailAddress);
            if (user == null) throw new UserNotFoundException();
            if (user.Roles == null) return;
            user.Roles.Remove(role);
            if (user.Roles.Count == 0) user.Roles = null;
            user.Updated = DateTime.Now;
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void Can(string id,
                        List<string> roles,
                        Dictionary<string, string> claims,
                        bool canAll)
        {
            var user = GetById(id);
            if (user == null) throw new UserNotFoundException();
            if (canAll &&
                (!roles.SequenceEqual(user.Roles) || !claims.SequenceEqual(user.Claims)))
                throw new ForbiddenException();
            if (!roles.Any(r => user.Roles.Contains(r)) &&
                !claims.Any(kvp => user.Claims.ContainsKey(kvp.Key) && user.Claims[kvp.Key] == kvp.Value))
                throw new ForbiddenException();
        }

    }
}