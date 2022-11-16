using System;
using MongoCRUDCSharpConsole.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MongoCRUDCSharpConsole
{
	public class MongoCRUD
	{
        private IMongoClient _mongoClient;
        private readonly IMongoCollection<Account> _accountCollection;
        private readonly IMongoCollection<Restaurant> _restaurantsCollection;
        private readonly IMongoCollection<Transfer> _transferCollection;

        public MongoCRUD(IMongoClient client)
		{
            _mongoClient = client;
            var sampleAnalyticsDatabase = _mongoClient.GetDatabase("sample_analytics");


            _accountCollection = sampleAnalyticsDatabase.GetCollection<Account>("accounts");
            _transferCollection = sampleAnalyticsDatabase.GetCollection<Transfer>("transfers");
            
            var sampleRestaurantsDatabase = _mongoClient.GetDatabase("sample_restaurants");
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


        //DB ACID Principle - Atomicity, Consistency, Isolation, Durability
        /*
         * C# Transaction steps
         * MongoDB auto cancel any transaction after 60sec
         * 
         
         */
        public void Transaction()
        {

            //var fromID = 557378;
            //var toID = 674364;
            //var transferAmount = 20;

            //var fromAccount = _accountCollection.Find(a => a.AccountId == fromID).FirstOrDefault();
            //var toAccount = _accountCollection.Find(a => a.AccountId == toID).FirstOrDefault();

            //var toAccountBalance = toAccount.Balance + transferAmount;
            using (var session = _mongoClient.StartSession())
            {

                // Define the sequence of operations to perform inside the transactions
                session.WithTransaction(
                    (s, ct) =>
                    {
                        var fromId = 557378;
                        var toId = 674364;

                        // Create the transfer_id and amount being transfered
                        var transferId = "TR02081994";
                        var transferAmount = 20;

                        // Obtain the account that the money will be coming from
                        var fromAccountResult = _accountCollection.Find(e => e.AccountId == fromId).FirstOrDefault();
                        // Get the balance and id of the account that the money will be coming from
                        var fromAccountBalance = fromAccountResult.Balance - transferAmount;
                        var fromAccountId = fromAccountResult.AccountId;

                        Console.WriteLine(fromAccountBalance.GetType());

                        // Obtain the account that the money will be going to
                        var toAccountResult = _accountCollection.Find(e => e.AccountId == toId).FirstOrDefault();
                        // Get the balance and id of the account that the money will be going to
                        var toAccountBalance = toAccountResult.Balance + transferAmount;
                        var toAccountId = toAccountResult.AccountId;

                        // Create the transfer record
                        var transferDocument = new Transfer
                        {
                            TransferId = transferId,
                            ToAccount = toAccountId,
                            FromAccount = fromAccountId,
                            Amount = transferAmount
                        };

                        // Update the balance and transfer array for each account
                        var fromAccountUpdateBalance = Builders<Account>.Update.Set("balance", fromAccountBalance);
                        var fromAccountFilter = Builders<Account>.Filter.Eq("account_id", fromId);
                        _accountCollection.UpdateOne(fromAccountFilter, fromAccountUpdateBalance);

                        var fromAccountUpdateTransfers = Builders<Account>.Update.Push("transfers_complete", transferId);
                        _accountCollection.UpdateOne(fromAccountFilter, fromAccountUpdateTransfers);

                        var toAccountUpdateBalance = Builders<Account>.Update.Set("balance", toAccountBalance);
                        var toAccountFilter = Builders<Account>.Filter.Eq("account_id", toId);
                        _accountCollection.UpdateOne(toAccountFilter, toAccountUpdateBalance);
                        var toAccountUpdateTransfers = Builders<Account>.Update.Push("transfers_complete", transferId);

                        // Insert transfer doc
                        _transferCollection.InsertOne(transferDocument);
                        Console.WriteLine("Transaction complete!");
                        return "Inserted into collections in different databases";
                    });
            }








        }




















    }
}

