using System;
using Microsoft.AspNetCore.Mvc;
using MongoRepositoryPattern.Repositories;
using MongoRepositoryPattern.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MongoRepositoryPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsAndReviewsController : ControllerBase
	{
		private readonly IMongoRepository<ListingsAndReviews> _listingReviewRepository;

		public ListingsAndReviewsController(IMongoRepository<ListingsAndReviews> listingReviewRepository)
		{
            _listingReviewRepository = listingReviewRepository;

        }

        [HttpPost("addNew")]
        public async Task AddListingReview([FromBody] ListingsAndReviews listingReview)
        {

            await _listingReviewRepository.InsertOneAsync(listingReview);
        }


        [HttpGet("getPeopleData")]
        public IEnumerable<string> GetPeopleData()
        {   
            var people = _listingReviewRepository.FilterBy(
                filter => filter.Name != "test",
                projection => { "Name": projection.Name, "Room Type": projection.RoomType}
            );
            return people;
        }


    }
}

