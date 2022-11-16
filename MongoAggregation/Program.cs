using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

MongoClient client = new MongoClient("mongodb://chandra:d56_G_paEEjEv_6@ac-9jm9qpr-shard-00-00.aopnkdq.mongodb.net:27017,ac-9jm9qpr-shard-00-01.aopnkdq.mongodb.net:27017,ac-9jm9qpr-shard-00-02.aopnkdq.mongodb.net:27017/?ssl=true&replicaSet=atlas-bav118-shard-0&authSource=admin&retryWrites=true&w=majority");

IMongoCollection<BsonDocument> playlistCollection = client.GetDatabase("sample_mflix").GetCollection<BsonDocument>("playlist");




List<BsonDocument> results = playlistCollection.Find(new BsonDocument()).ToList();

foreach(BsonDocument result in results)
{
    Console.WriteLine(result["username"] + ": " + string.Join(", ", result["items"]));

}


var pResults = playlistCollection.Aggregate()
    .Match(new BsonDocument { { "username", "chandra" } })
    .Project(new BsonDocument{
            { "_id", 1 },
            { "username", 1 },
            {
                "items", new BsonDocument{
                    {
                        "$map", new BsonDocument{
                            { "input", "$items" },
                            { "as", "item" },
                            {
                                "in", new BsonDocument{
                                    {
                                        "$convert", new BsonDocument{
                                            { "input", "$$item" },
                                            { "to", "objectId" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        })
    .Lookup("movies", "items", "_id", "movies")
    .Unwind("movies")
    .Group(new BsonDocument{
            { "_id", "$_id" },
            {
                "username", new BsonDocument{
                    { "$first", "$username" }
                }
            },
            {
                "movies", new BsonDocument{
                    { "$addToSet", "$movies" }
                }
            }
        })
    .ToList();

foreach (var pResult in pResults)
{
    Console.WriteLine(pResult);
}




Console.ReadKey();