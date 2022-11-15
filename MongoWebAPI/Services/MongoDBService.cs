using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoWebAPI.Models;
using Microsoft.Extensions.Options;

using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MongoWebAPI.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Playlist> _playlistCollection;


        public MongoDBService( IOptions<MongoDBSettings> mongoDBSetting)
        {
            MongoClient client = new MongoClient(mongoDBSetting.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSetting.Value.DatabaseName);
            _playlistCollection = database.GetCollection<Playlist>(mongoDBSetting.Value.CollectionName);

        }

        public async Task<List<Playlist>> GetAsync()
        {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task CreateAsync(Playlist playlist)
        {
            await _playlistCollection.InsertOneAsync(playlist);
            return;

        }

        public async Task CreateManyAsync(Playlist playlist)
        {
            await _playlistCollection.InsertOneAsync(playlist);
            return;

        }


        public async Task AddToPlaylistAsync(string id, string movieId)
        {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("movieIds", movieId);
            await _playlistCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            await _playlistCollection.DeleteOneAsync(filter);
            return;

        }


    }
}

