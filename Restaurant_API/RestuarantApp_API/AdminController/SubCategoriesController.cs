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
    public class SubCategoriesController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /SubCategories/
        public async Task<ActionResult> Index()
        {
            return View(await db.SubCategories.ToListAsync());
        }

        // GET: /SubCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = await db.SubCategories.FindAsync(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: /SubCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,ImageUrl,CreatedDate,CreatedBy")] SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subcategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subcategory);
        }

        // GET: /SubCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = await db.SubCategories.FindAsync(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: /SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,ImageUrl,CreatedDate,CreatedBy")] SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subcategory);
        }

        // GET: /SubCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = await db.SubCategories.FindAsync(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: /SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubCategory subcategory = await db.SubCategories.FindAsync(id);
            db.SubCategories.Remove(subcategory);
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
