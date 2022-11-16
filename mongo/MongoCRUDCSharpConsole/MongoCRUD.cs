using System;
using MongoCRUDCSharpConsole.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MongoCRUDCSharpConsole
{
	public class MongoCRUD
	{
        private readonly IMongoCollection<Account> _accountCollection;
        private readonly IMongoCollection<Restaurant> _restaurantsCollection;

        public MongoCRUD(IMongoClient client)
		{
            var sampleAnalyticsDatabase = client.GetDatabase("sample_analytics");
            _accountCollection = sampleAnalyticsDatabase.GetCollection<Account>("accounts");
            
            var sampleRestaurantsDatabase = client.GetDatabase("sample_restaurants");
            _restaurantsCollection = sampleRestaurantsDatabase.GetCollection<Restaurant>("restaurants");
        }

        public void Querying()
        {
             //var account = _accountCollection
            //    .Find(a => a.AccountId == 557378)
            //    .FirstOrDefault();

            var accounts = _accountCollection
                .Find(a => a.AccountId > 370000)
                .SortByDescending(a => a.Limit)
                .Skip(5)
                .Limit(20);

            foreach (var aa in accounts.ToList())
            {
                Console.WriteLine(aa.AccountId + " ---- " + aa.Limit);
            }

            //Builders Class for Bson Document
            //var filter = Builders<BsonDocument>
            //    .Filter
            //    .Eq("id", new ObjectId("5ca4bbc7a2dd94ee5816238c"));

            //var document = accountCollection
            //    .Find(filter)
            //    .FirstOrDefault();

            //Console.WriteLine(document);


            //collection.Find(new BsonDocument()).FirstOrDefault();
            //OR
            //    collection.Find(x => x.Id == id).FirstOrDefault();
        }


        //returns UpdateResult Object
        //IsAcknowledge = Boolean
        //MatchedCount = How manu document were found
        //ModifiedCount = How many document were updated

        public async Task Update()
        {

            var filter = Builders<Account>
                .Filter
                .Eq(a => a.AccountId , 371138);

            var update = Builders<Account>
                .Update
                .Set(a => a.Limit, 7777);

            //var result =  _accountCollection.UpdateOne(filter, update);
            var result = await _accountCollection.UpdateOneAsync(filter, update);

            if (result.IsAcknowledged)
            {
                Console.WriteLine($"Matched: {result.MatchedCount}; Modified: {result.ModifiedCount}");
            }
        }


        public async Task UpdateMany()
        {
            var filter = Builders<Restaurant>
                .Filter
                .Eq(r => r.Borough, "Brooklyn");


            var update = Builders<Restaurant>
                .Update
                .Set(r => r.Name, "Brooklyn Chandra test");

            var result = await _restaurantsCollection
                .UpdateManyAsync(filter, update);


            Console.WriteLine($"Matched: {result.MatchedCount}; Modified: {result.ModifiedCount}");


   //             Bson Approach

   //             var filter = Builders<BsonDocument>
   //.Filter
   //.Lt("balance", 500);

   //         var update = Builders<BsonDocument>
   //            .Update
   //            .Inc("balance", 10);

   //         var result = await accountsCollection
   //            .UpdateManyAsync(filter, update);

   //         Console.WriteLine(result.ModifiedCount);


        }


        /*
         * DeleteResult Object
         * IsAcknowledge - Was the delete successful? Boolean
         * DeletedCount - How many documents deleted
         * */
        public void DeleteOne()
        {
            //LINQ Expression
            var accounts = _accountCollection
                .DeleteOne(a => a.AccountId == 198100);

            Console.WriteLine("Deleted Count : " + accounts.DeletedCount);

        }

        public void DeleteMany()
        {
            var accounts = _accountCollection
                .DeleteMany(a => a.AccountId < 198100);
        }

















    }
}

