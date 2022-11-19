using System;
using MongoRepositoryPattern.Interfaces;
namespace MongoRepositoryPattern.Models
{

	[BsonCollection("listingsAndReviews")]
	public class ListingsAndReviews : IDocument
	{
        public string Name { get; set; }
        public string PropertyType { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public Decimal MonthlyPrice { get; set; }

    }
}

