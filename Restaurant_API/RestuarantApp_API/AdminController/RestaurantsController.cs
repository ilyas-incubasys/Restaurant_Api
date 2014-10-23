using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestuarantApp_API.Models;
using RestuarantApp_API.Context;

namespace RestuarantApp_API.AdminController
{
    public class RestaurantsController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /Restaurants/
        public async Task<ActionResult> Index()
        {
            return View(await db.Restaurants.ToListAsync());
        }

        // GET: /Restaurants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantInfo restaurantinfo = await db.Restaurants.FindAsync(id);
            if (restaurantinfo == null)
            {
                return HttpNotFound();
            }
            return View(restaurantinfo);
        }

        // GET: /Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,HeadOfficeAddress,ImageUrl")] RestaurantInfo restaurantinfo)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurantinfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(restaurantinfo);
        }

        // GET: /Restaurants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantInfo restaurantinfo = await db.Restaurants.FindAsync(id);
            if (restaurantinfo == null)
            {
                return HttpNotFound();
            }
            return View(restaurantinfo);
        }

        // POST: /Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,HeadOfficeAddress,ImageUrl")] RestaurantInfo restaurantinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantinfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(restaurantinfo);
        }

        // GET: /Restaurants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantInfo restaurantinfo = await db.Restaurants.FindAsync(id);
            if (restaurantinfo == null)
            {
                return HttpNotFound();
            }
            return View(restaurantinfo);
        }

        // POST: /Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RestaurantInfo restaurantinfo = await db.Restaurants.FindAsync(id);
            db.Restaurants.Remove(restaurantinfo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
