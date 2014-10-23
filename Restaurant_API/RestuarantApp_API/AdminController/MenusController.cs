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
    public class MenusController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /Menus/
        public async Task<ActionResult> Index()
        {
            var menus = db.Menus.Include(m => m.SubCategory);
            return View(await menus.ToListAsync());
        }

        // GET: /Menus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: /Menus/Create
        public ActionResult Create()
        {
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name");
            return View();
        }

        // POST: /Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,Price,ImageUrl,Description,CreatedDate,CreatedBy,SubCategoryId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", menu.SubCategoryId);
            return View(menu);
        }

        // GET: /Menus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", menu.SubCategoryId);
            return View(menu);
        }

        // POST: /Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Price,ImageUrl,Description,CreatedDate,CreatedBy,SubCategoryId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", menu.SubCategoryId);
            return View(menu);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<JsonResult> CreateMenu(Menu menu)
        {
            string respponse = string.Empty;
            if (ModelState.IsValid)
            {
                 db.Menus.Add(menu);
                 await db.SaveChangesAsync();
            }
            else
            {
                return Json(false);
            }

            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", menu.SubCategoryId);
            return Json(true);
        }
        // GET: /Menus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: /Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Menu menu = await db.Menus.FindAsync(id);
            db.Menus.Remove(menu);
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
