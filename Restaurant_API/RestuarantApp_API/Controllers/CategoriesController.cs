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
using System.Data.SqlClient;
using RestuarantApp_API.Dtos;
using System.Collections;

namespace RestuarantApp_API.Controllers
{
    public class CategoriesController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET api/Categories
        public IQueryable<Category> GetCategories()
        {
            var categories = from c in db.Categories
                             select c;
            return categories;
        }

        // GET api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> GetCategory(int id)
        {

            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        // GET api/Categories/5
        [ResponseType(typeof(MenuCategoryDto))]
        public IHttpActionResult GetCategoryMenu()
        {

            //var query = " select m.id,m.name,m.price,m.imageurl,m.description,c.name as CategoryName,c.Id as CategoryId,c.Imageurl as CategoryImageurl from dbo.menus m inner join dbo.SubCategoryCategories scc  on scc.subcategory_id=m.subcategoryid inner join dbo.Categories c on c.id=scc.category_id ";
            var query = "select m.id,m.name,m.price,m.imageurl,m.description, "
                        + "c.name as CategoryName,c.Id as CategoryId,c.Imageurl "
                        + " as CategoryImageurl,mi.id as MenuItemId,mi.name as MenuItemName,mi.ImageUrl "
                        + "as MenuItemImageUrl,mi.Description as MenuItemDescription "
                        + "from menus m "
                        + " inner join CategorySubCategories scc  on scc.subcategory_id=m.subcategoryid "
                        + " inner join Categories c on c.id=scc.category_id "
                        + " left outer join menuitemmenus mm on mm.menu_id=m.id "
                        + " left outer join menuitems mi on mi.id=mm.MenuItem_Id ";
            var viewCategoryDto = db.Database.SqlQuery<ViewCategoryDto>(query).ToList();
            List<MenuCategoryDto> menuCategoryDto=(from v in viewCategoryDto.
                                    Select(t=>new  MenuCategoryDto{CategoryId=t.Id,CategoryImageUrl=t.CategoryImageUrl,CategoryName=t.CategoryName
                                    ,MenuDtos=new List<MenuDto>(){new MenuDto{Id=t.Id,Name=t.Name,Price=t.Price,ImageUrl=t.ImageUrl,Description=t.Description
                                    ,MenuItemDtos=new List<MenuItemDto>(){new MenuItemDto{Id=t.MenuItemId.GetValueOrDefault(),Description=t.MenuItemDescription,Name=t.MenuItemName,ImageUrl=t.MenuItemImageUrl}}
                                    }}}).ToList()
                                    select v).Distinct().ToList();
            var categories = db.Categories.ToList();
            if (menuCategoryDto.Count==0)
            {
                return Ok(menuCategoryDto);
            }
            Hashtable hashtable = new Hashtable();
            foreach (var cat in categories)
            {
                var list = menuCategoryDto.Where(o => o.CategoryName.Equals(cat.Name)).SelectMany(p => p.MenuDtos).GroupBy(p=>p.Id).ToList();
                hashtable.Add(cat.Name, list);
            }

            if (hashtable == null)
            {
                return NotFound();
            }

            return Ok(hashtable);
        }

        // PUT api/Categories/5
        public async Task<IHttpActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST api/Categories
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}