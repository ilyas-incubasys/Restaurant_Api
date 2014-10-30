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
    public class MenuItemsController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /MenuItems/
        public async Task<ActionResult> Index(int? menuId)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Menus = new SelectList(db.Menus, "Id", "Name",menuId);
            if (menuId==null)
            {
                return View(await db.MenuItems.ToListAsync());
            }
            else
            {
                return View(await db.MenuItems.Where(t=>t.Menus.Any(o=>o.Id==menuId)).ToListAsync());    
            }
            
        }
        // GET: /MenuItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuitem = await db.MenuItems.FindAsync(id);
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(menuitem);
        }

        // GET: /MenuItems/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: /MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,ImageUrl,Description,CreatedDate,CreatedBy")] MenuItem menuitem)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuitem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(menuitem);
        }

        // GET: /MenuItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuitem = await db.MenuItems.FindAsync(id);
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(menuitem);
        }

        // POST: /MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,ImageUrl,Description,CreatedDate,CreatedBy")] MenuItem menuitem)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(menuitem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuitem);
        }

        // GET: /MenuItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuitem = await db.MenuItems.FindAsync(id);
            if (menuitem == null)
            {
                return HttpNotFound();
            }
            return View(menuitem);
        }

        // POST: /MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            MenuItem menuitem = await db.MenuItems.FindAsync(id);
            db.MenuItems.Remove(menuitem);
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
