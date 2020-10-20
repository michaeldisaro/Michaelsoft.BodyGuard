using System;
using System.Collections.Generic;
using System.Text.Json;
using Michaelsoft.BodyGuard.Server.DatabaseModels;
using Michaelsoft.BodyGuard.Server.Settings;
using MongoDB.Driver;
using BCrypt.Net;
using Michaelsoft.BodyGuard.Common.Extensions;
using Michaelsoft.BodyGuard.Server.Exceptions;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class UserService
    {

        private readonly IMongoCollection<User> _users;

        private readonly DatabaseEncryptionService _encryptionService;

        public UserService(IUserStoreDatabaseSettings settings,
                           DatabaseEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> GetAll() => _users.Find(user => true).ToList();

        private User GetById(string id) => _users.Find<User>(user => user.Id == id).FirstOrDefault();

        private User GetByHashedEmail(string hashedEmail) =>
            _users.Find<User>(user => user.HashedEmail == hashedEmail).FirstOrDefault();

        private string HashPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        private bool VerifyPassword(User user,
                                    string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.HashedPassword);
        }

        public User Create(string emailAddress,
                           string password,
                           JsonElement userData)
        {
            var user = new User
            {
                HashedEmail = emailAddress.Sha1(),
                HashedPassword = HashPassword(password),
                EncryptedData = userData.ValueEquals("") ? null : _encryptionService.Encrypt(userData.ToString()),
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            var existing = GetByHashedEmail(user.HashedEmail);
            if (existing != null) return existing;
            _users.InsertOne(user);
            return user;
        }

        public User Access(string emailAddress,
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
                               JsonElement userData)
        {
            var user = GetById(id);
            if (user == null) throw new UserNotFoundException();
            user.EncryptedData = _encryptionService.Encrypt(userData.ToString());
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }

    }
}