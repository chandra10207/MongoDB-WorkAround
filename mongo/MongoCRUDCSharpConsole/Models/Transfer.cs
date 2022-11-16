
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoCRUDCSharpConsole.Models
{
        [BsonIgnoreExtraElements]
        public class Transfer
        {
            [BsonId]
            //[BsonRepresentation(BsonType.ObjectId)]
            public ObjectId Id { get; set; }

            [BsonElement("transfer_id")]
            public string TransferId { get; set; }

            [BsonElement("to_account")]
            public int ToAccount { get; set; }

            [BsonElement("from_account")]
            public int FromAccount { get; set; }

            [BsonElement("amount")]
            public int Amount { get; set; }

        }
}

