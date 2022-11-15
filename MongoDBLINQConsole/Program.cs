using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

MongoClientSettings setting = MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("M01_ATLAS_URI"));

setting.LinqProvider = LinqProvider.V3;

MongoClient client = new MongoClient(setting);

