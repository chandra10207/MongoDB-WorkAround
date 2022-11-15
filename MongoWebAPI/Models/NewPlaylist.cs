using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoWebAPI.Models
{
    public class NewPlaylist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Cid { get; set; }


        public string cname { get; set; } = null!;

       
    }
}

