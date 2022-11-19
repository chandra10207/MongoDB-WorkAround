using System;
using MongoDB.Bson;
using MongoRepositoryPattern.Interfaces;

namespace MongoRepositoryPattern.Entities
{
	public class Document : IDocument
	{
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}

