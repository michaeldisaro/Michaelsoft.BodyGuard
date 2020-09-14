using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Michaelsoft.BodyGuard.Server.DatabaseModels
{
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string HashedEmail { get; set; }

        public string HashedPassword { get; set; }

        public string EncryptedData { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

    }
}