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
    public class SubCategoriesController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET api/SubCategories
        public IQueryable<SubCategory> GetSubCategories()
        {
            var subCategories = from s in db.SubCategories
                             select s;
            return subCategories;
        }

        // GET api/SubCategories/5
        [ResponseType(typeof(SubCategory))]
        public async Task<IHttpActionResult> GetSubCategory(int id)
        {
            SubCategory subcategory = await db.SubCategories.FindAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }

            return Ok(subcategory);
        }

        // PUT api/SubCategories/5
        public async Task<IHttpActionResult> PutSubCategory(int id, SubCategory subcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subcategory.Id)
            {
                return BadRequest();
            }

            db.Entry(subcategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
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

        // POST api/SubCategories
        [ResponseType(typeof(SubCategory))]
        public async Task<IHttpActionResult> PostSubCategory(SubCategory subcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubCategories.Add(subcategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = subcategory.Id }, subcategory);
        }

        // DELETE api/SubCategories/5
        [ResponseType(typeof(SubCategory))]
        public async Task<IHttpActionResult> DeleteSubCategory(int id)
        {
            SubCategory subcategory = await db.SubCategories.FindAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }

            db.SubCategories.Remove(subcategory);
            await db.SaveChangesAsync();

            return Ok(subcategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubCategoryExists(int id)
        {
            return db.SubCategories.Count(e => e.Id == id) > 0;
        }
    }
}