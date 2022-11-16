using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoCRUDCSharpConsole.Models;
using System.Linq;

namespace MongoCRUDCSharpConsole
{
    
	public class AggregationPipeline
    {
        private IMongoClient _mongoClient;
        private IMongoDatabase _sampleAnalyticsDatabase;
        private IMongoDatabase _sampleRestaurantsDatabase;

        private readonly IMongoCollection<Account> _accountCollection;
        private readonly IMongoCollection<BsonDocument> _accountCollectionBson;
        private readonly IMongoCollection<Restaurant> _restaurantsCollection;
        private readonly IMongoCollection<Transfer> _transferCollection;

        public AggregationPipeline(IMongoClient client)
        {
            _mongoClient = client;
            _sampleAnalyticsDatabase = _mongoClient.GetDatabase("sample_analytics");


            _accountCollection = _sampleAnalyticsDatabase.GetCollection<Account>("accounts");
            _accountCollectionBson = _sampleAnalyticsDatabase.GetCollection<BsonDocument>("accounts");
            _transferCollection = _sampleAnalyticsDatabase.GetCollection<Transfer>("transfers");

            _sampleRestaurantsDatabase = _mongoClient.GetDatabase("sample_restaurants");
            _restaurantsCollection = _sampleRestaurantsDatabase.GetCollection<Restaurant>("restaurants");


        }
       


		public void Match()
		{
            //using LINQ
            /*var matchStage = Builders<Account>.Filter.Lte(ac => ac.Limit, 10000);
            var aggregate = _accountCollection.Aggregate().Match(matchStage);
            var results = aggregate.ToList();

            foreach (var account in results)
            {
                Console.WriteLine(account.Limit);

            }*/

            //using BsonDocument
            var accountCollectionBson = _sampleAnalyticsDatabase.GetCollection<BsonDocument>("accounts");
            var matchStage = Builders<BsonDocument>.Filter.Lte("limit", 10000);
            var aggregate = accountCollectionBson.Aggregate().Match(matchStage);
            var results = aggregate.ToList();

            foreach (var account in results)
            {
                Console.WriteLine(account["limit"]);

            }

        }



        public void Group()
        {
            //var matchStage = Builders<Restaurant>.Filter.Eq("cuisine", "American");
            var matchStage = Builders<Restaurant>.Filter.Eq("cuisine", "American");
            var aggregate = _restaurantsCollection
                .Aggregate()
                .Match(matchStage)
                .Group(
                a => a.Borough,
                r => new
                {
                    borough = r.Key,
                    total = r.Sum(a => 1)
                }

                );

            var restaurants = aggregate.ToList();

            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant);
            }
        }


        /*
         * Project: 
         * Specifies output shape
         * Projection similar to find operations
         * used as the last stage to format the output
         * 
         * Sort:
         * Sort and pass sorted order
         * LINQ: either with SortBy() or SortByDescending()
         * Builder class: Use builder class to define the Sort statement that you pass to the Sort() method.
         */


        public void SortLINQ()
        {
            var matchStage = Builders<Account>.Filter.Lte(acc => acc.Limit , 10000);

            var aggregateResult = _accountCollection.Aggregate()
                .Match(matchStage)
                .SortByDescending(ac => ac.Balance)
                .Limit(100)
                .ToList();

            foreach(var agg in aggregateResult)
            {
                Console.WriteLine(agg.AccountId + " --- "+ agg.Balance);
            }
        }

        public void SortBson()
        {
            var matchStage = Builders<BsonDocument>.Filter.Lte("limit", 10000);
            var sort = Builders<BsonDocument>.Sort.Descending("balance");

            var aggregateResult = _accountCollectionBson.Aggregate()
                .Match(matchStage)
                .Sort(sort)
                .Limit(100)
                .ToList();

            foreach (var agg in aggregateResult)
            {
                Console.WriteLine(agg.ToString());
            }
        }

        /*
         * Projection Stage
         * used to create a new document structure
         * 
         */

        public void Projection()
        {
            var matchLimitStage = Builders<Account>.Filter.Lte("limit", 10000);
            var projectStage = Builders<Account>.Projection.Expression(u =>
            new
            {
                AccountID = u.AccountId,
                LimitCSP = u.Limit,
                Balancecsp = u.Balance,
                GBP = u.Limit * 1.3
            }) ;

            var aggregateResults = _accountCollection.Aggregate()
                .Match(matchLimitStage)
               .SortByDescending(u => u.Balance)
                .Project(projectStage)
                .Limit(100)
                .ToList();


            foreach (var agg in aggregateResults)
            {
                Console.WriteLine(agg.ToString());
            }


        }




















    }
}

