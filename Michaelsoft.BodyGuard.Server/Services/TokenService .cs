using System;
using Michaelsoft.BodyGuard.Server.DatabaseModels;
using Michaelsoft.BodyGuard.Server.Exceptions;
using Michaelsoft.BodyGuard.Server.Settings;
using MongoDB.Driver;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class TokenService
    {

        private readonly IMongoCollection<DbToken> _tokens;

        public TokenService(ITokenStoreDatabaseSettings settings,
                            DatabaseEncryptionService encryptionService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tokens = database.GetCollection<DbToken>(settings.TokensCollectionName);
            var indexKeysDefinition = Builders<DbToken>.IndexKeys.Ascending(t => t.ExpireAt);
            _tokens.Indexes.CreateOne
                (
                 new CreateIndexModel<DbToken>(indexKeysDefinition, new CreateIndexOptions {ExpireAfter = TimeSpan.Zero})
                );
        }

        private DbToken GetById(string id) => _tokens.Find<DbToken>(token => token.Id == id).FirstOrDefault();

        private DbToken GetByTypeAndUserId(string type,
                                         string userId) =>
            _tokens.Find<DbToken>(t => t.Type == type && t.UserId == userId).FirstOrDefault();
        
        private DbToken GetByTypeAndValue(string type,
                                           string value) =>
            _tokens.Find<DbToken>(t => t.Type == type && t.Value == value).FirstOrDefault();

        public DbToken Create(string type,
                            string value,
                            string userId,
                            int ttlSeconds)
        {
            var token = new DbToken
            {
                UserId = userId,
                Type = type,
                Value = value,
                Created = DateTime.Now,
                ExpireAt = DateTime.Now.AddSeconds(ttlSeconds)
            };
            var existing = GetByTypeAndUserId(type, userId);
            if (existing != null) return existing;
            _tokens.InsertOne(token);
            return token;
        }

        public DbToken GetTokenByTypeUserAndValue(string type,
                              string userId,
                              string value)
        {
            var token = GetByTypeAndUserId(type, userId);
            if (token == null) throw new TokenNotFoundException();
            if (!value.Equals(token.Value)) throw new InvalidTokenException();
            return token;
        }
        
        public DbToken GetTokenByTypeAndValue(string type,
                                                string value)
        {
            var token = GetByTypeAndValue(type, value);
            if (token == null) throw new TokenNotFoundException();
            return token;
        }

        public void Delete(string id)
        {
            var token = GetById(id);
            if (token == null) throw new TokenNotFoundException();
            _tokens.DeleteOne(u => u.Id == token.Id);
        }

    }
}