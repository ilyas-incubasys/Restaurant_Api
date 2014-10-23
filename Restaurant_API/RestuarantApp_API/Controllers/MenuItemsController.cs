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
    public class MenuItemsController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET api/MenuItems
        public IQueryable<MenuItem> GetMenuItems()
        {
            var menuItems = from m in db.MenuItems
                            select m;
            return menuItems;
        }

        // GET api/MenuItems/5
        [ResponseType(typeof(MenuItem))]
        public async Task<IHttpActionResult> GetMenuItem(int id)
        {
            MenuItem menuitem = await db.MenuItems.FindAsync(id);
            if (menuitem == null)
            {
                return NotFound();
            }

            return Ok(menuitem);
        }

        // PUT api/MenuItems/5
        public async Task<IHttpActionResult> PutMenuItem(int id, MenuItem menuitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuitem.Id)
            {
                return BadRequest();
            }

            db.Entry(menuitem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
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

        // POST api/MenuItems
        [ResponseType(typeof(MenuItem))]
        public async Task<IHttpActionResult> PostMenuItem(MenuItem menuitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuItems.Add(menuitem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = menuitem.Id }, menuitem);
        }

        // DELETE api/MenuItems/5
        [ResponseType(typeof(MenuItem))]
        public async Task<IHttpActionResult> DeleteMenuItem(int id)
        {
            MenuItem menuitem = await db.MenuItems.FindAsync(id);
            if (menuitem == null)
            {
                return NotFound();
            }

            db.MenuItems.Remove(menuitem);
            await db.SaveChangesAsync();

            return Ok(menuitem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuItemExists(int id)
        {
            return db.MenuItems.Count(e => e.Id == id) > 0;
        }
    }
}