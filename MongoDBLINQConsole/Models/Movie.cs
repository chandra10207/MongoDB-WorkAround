using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBLINQConsole.Models
{
    [BsonIgnoreExtraElements]
    public class Movie
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; }

		[BsonElement("title")]
		public string Title { get; set; }

		[BsonElement("year")]
		public int Year { get; set; }

		[BsonElement("runtime")]
		public int Runtime { get; set; }

		[BsonElement("plot")]
		[BsonIgnoreIfNull]
		public string Plot { get; set; } = null!;

		[BsonElement("cast")]
		[BsonIgnoreIfNull]
		public List<string> Cast { get; set; } = null!;

	}
}

