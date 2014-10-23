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
using RestuarantApp_API.Dtos;

namespace RestuarantApp_API.Controllers
{
    public class MenusController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET api/Menus
        public IQueryable<Menu> GetMenus()
        {
            var menus = from m in db.Menus
                        select m;
            return menus;
        }

        // GET api/Menus/5
        [ResponseType(typeof(Menu))]
        public async Task<IHttpActionResult> GetMenu(int id)
        {
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }
        // GET api/Menus/5
        [ResponseType(typeof(MenuDto))]
        public IHttpActionResult GetOffers()
        {
            List<MenuDto> menuDto = db.Menus.Include(a => a.MenuItems).Where(m => m.MenuItems.Any()).Select(t => new MenuDto
            {
                Id = t.Id,
                Name = t.Name,
                ImageUrl = t.ImageUrl,
                Price = t.Price,
                Description = t.Description,
                MenuItemDtos = t.MenuItems.Select(i => new MenuItemDto {
                    Id = i.Id,
                    Description = i.Description,
                    ImageUrl = i.ImageUrl,
                    Name = i.Name
                }).ToList()

            }).ToList();
           if (menuDto == null)
            {
                return NotFound(); 
            }

           return Ok(menuDto);
        }
        // PUT api/Menus/5
        public async Task<IHttpActionResult> PutMenu(int id, Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menu.Id)
            {
                return BadRequest();
            }

            db.Entry(menu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST api/Menus
        [ResponseType(typeof(Menu))]
        public async Task<IHttpActionResult> PostMenu(Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Menus.Add(menu);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = menu.Id }, menu);
        }

        // DELETE api/Menus/5
        [ResponseType(typeof(Menu))]
        public async Task<IHttpActionResult> DeleteMenu(int id)
        {
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            db.Menus.Remove(menu);
            await db.SaveChangesAsync();

            return Ok(menu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuExists(int id)
        {
            return db.Menus.Count(e => e.Id == id) > 0;
        }
    }
}