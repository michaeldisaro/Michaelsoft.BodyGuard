using System;
using System.Collections.Generic;
using System.Text.Json;
using Michaelsoft.BodyGuard.Server.DatabaseModels;
using Michaelsoft.BodyGuard.Server.Settings;
using MongoDB.Driver;
using BCrypt.Net;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Server.Exceptions;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class UserService
    {

        private readonly IMongoCollection<DbUser> _users;

        private readonly DatabaseEncryptionService _encryptionService;

        public UserService(IUserStoreDatabaseSettings settings,
                           DatabaseEncryptionService encryptionService)
        {
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

        public DbUser Create(string emailAddress,
                             string password,
                             JsonElement? userData = null)
        {
            var user = new DbUser
            {
                HashedEmail = emailAddress.Sha1(),
                HashedPassword = HashPassword(password),
                EncryptedData = userData == null || userData.Value.ValueEquals("")
                                    ? null
                                    : _encryptionService.Encrypt(userData.ToString()),
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            var existing = GetByHashedEmail(user.HashedEmail);
            if (existing != null) return existing;
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
                               object userData)
        {
            var user = GetById(id);
            if (user == null) throw new UserNotFoundException();
            var serializedData = userData.ToString();
            user.EncryptedData = _encryptionService.Encrypt(serializedData);
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }

    }
}