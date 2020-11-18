using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Michaelsoft.BodyGuard.Server.DatabaseModels
{
    public class DbUser
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string HashedEmail { get; set; }

        public string HashedPassword { get; set; }

        public string EncryptedData { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public List<string> Roles { get; set; }

        public Dictionary<string, string> Claims { get; set; }

    }
}