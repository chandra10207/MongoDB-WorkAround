using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoCRUDCSharpConsole.Models
{
    [BsonIgnoreExtraElements]
    public class Customer
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("usename")]
        public string Username { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("birthdate")]
        public DateTime Birthdate { get; set; }

        [BsonElement("active")]
        public Boolean Active { get; set; }
    }
    
}

