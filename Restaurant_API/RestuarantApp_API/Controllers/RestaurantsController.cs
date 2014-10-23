using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestuarantApp_API.Models;
using RestuarantApp_API.Context;

namespace RestuarantApp_API.Controllers
{
    public class RestaurantsController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();
        
        // GET api/Restaurants
        public IQueryable<RestaurantInfo> GetRestaurants()
        {
            var restaurants = from r in db.Restaurants
                              select r;
            return restaurants;
        }

        // GET api/Restaurants/5
        [ResponseType(typeof(RestaurantInfo))]
        public async Task<IHttpActionResult> GetRestaurantInfo(int id)
        {
            RestaurantInfo restaurantinfo = await db.Restaurants.FindAsync(id);
            if (restaurantinfo == null)
            {
                return NotFound();
            }

            return Ok(restaurantinfo);
        }

        // PUT api/Restaurants/5
        public async Task<IHttpActionResult> PutRestaurantInfo(int id, RestaurantInfo restaurantinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurantinfo.Id)
            {
                return BadRequest();
            }

            db.Entry(restaurantinfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Restaurants
        [ResponseType(typeof(RestaurantInfo))]
        public async Task<IHttpActionResult> PostRestaurantInfo(RestaurantInfo restaurantinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurantinfo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = restaurantinfo.Id }, restaurantinfo);
        }

        // DELETE api/Restaurants/5
        [ResponseType(typeof(RestaurantInfo))]
        public async Task<IHttpActionResult> DeleteRestaurantInfo(int id)
        {
            RestaurantInfo restaurantinfo = await db.Restaurants.FindAsync(id);
            if (restaurantinfo == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(restaurantinfo);
            await db.SaveChangesAsync();

            return Ok(restaurantinfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestaurantInfoExists(int id)
        {
            return db.Restaurants.Count(e => e.Id == id) > 0;
        }
    }
}