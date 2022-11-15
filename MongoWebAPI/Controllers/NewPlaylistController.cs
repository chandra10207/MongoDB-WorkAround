using System;
using Microsoft.AspNetCore.Mvc;
using MongoWebAPI.Models;
using MongoWebAPI.Services;

namespace MongoWebAPI.Controllers
{
    
    [Controller]
    [Route("api/[controller])")]
    public class NewPlaylistController
    {
        private readonly MongoDBService _mongoDBService;

        public NewPlaylistController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;

        }

        //[HttpGet]
        //public async Task<List<NewPlaylist>> Get()
        //{
        //    return await _mongoDBService.GetAsync();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Playlist playlist)
        //{
        //    await _mongoDBService.CreateAsync(playlist);
        //    return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);

        //}

       
    }
}

