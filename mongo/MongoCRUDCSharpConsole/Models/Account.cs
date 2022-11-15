using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoCRUDCSharpConsole.Models
{
	[BsonIgnoreExtraElements]
	public class Account
	{
		[BsonId]
		//[BsonRepresentation(BsonType.ObjectId)]
		public ObjectId Id { get; set; }

		[BsonElement("account_id")]
		public int AccountId { get; set; }

		[BsonElement("limit")]
		public int Limit { get; set; }

		//[BsonElement("products")]
		//public IEnumerable<string> Products {get;set;}
		
	}
}

