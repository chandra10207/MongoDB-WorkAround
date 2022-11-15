using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoCRUDCSharpConsole.Models
{
	[BsonIgnoreExtraElements]
	public class Restaurant
	{
		[BsonId]
		//[BsonRepresentation(BsonType.ObjectId)]
		public ObjectId Id { get; set; }

		[BsonElement("restaurant_id")]
		public string RestaurantId { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

        [BsonElement("borough")]
        public string Borough { get; set; }

        [BsonElement("cuisine")]
        public string Cuisine { get;set;}

		[BsonElement("custom_chandra1")]
		public string CustomField { get; set; }

    }
}

