using System;
using Michaelsoft.BodyGuard.Server.DatabaseModels;
using Michaelsoft.BodyGuard.Server.Settings;
using MongoDB.Driver;
using Michaelsoft.BodyGuard.Server.Exceptions;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class TokenService
    {

        private readonly IMongoCollection<Token> _tokens;

        public TokenService(ITokenStoreDatabaseSettings settings,
                            DatabaseEncryptionService encryptionService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tokens = database.GetCollection<Token>(settings.TokensCollectionName);
            var indexKeysDefinition = Builders<Token>.IndexKeys.Ascending(t => t.ExpireAt);
            _tokens.Indexes.CreateOne
                (
                 new CreateIndexModel<Token>(indexKeysDefinition, new CreateIndexOptions {ExpireAfter = TimeSpan.Zero})
                );
        }

        private Token GetById(string id) => _tokens.Find<Token>(token => token.Id == id).FirstOrDefault();

        private Token GetByTypeAndUserId(string type,
                                         string userId) =>
            _tokens.Find<Token>(t => t.Type == type && t.UserId == userId).FirstOrDefault();

        public Token Create(string type,
                            string value,
                            string userId,
                            int ttlSeconds)
        {
            var token = new Token
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

        public Token Validate(string type,
                              string userId,
                              string value)
        {
            var token = GetByTypeAndUserId(type, userId);
            if (token == null) throw new TokenNotFoundException();
            if (!value.Equals(token.Value)) throw new InvalidTokenException();
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