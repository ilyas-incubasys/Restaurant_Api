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
using Newtonsoft.Json;
using RestuarantApp_API.Dtos;
using RestuarantApp_API.Constants;
using System.IO;

namespace RestuarantApp_API.AdminController
{
    public class MenusController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /Menus/
        public async Task<ActionResult> Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var menus = db.Menus.Include(m => m.SubCategory);
            return View(await menus.ToListAsync());
        }

        // GET: /Menus/Details/5
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

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.MenuItems = db.MenuItems.ToList();
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name");
            return View();
        }

        // POST: /Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price,ImageUrl,Description,CreatedDate,CreatedBy,SubCategoryId")] Menu menu)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            ViewBag.MenuItemIds = menu.MenuItems.Select(t => new MenuItemDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                ImageUrl = t.ImageUrl
            }).ToList();
            ViewBag.MenuItems = db.MenuItems.ToList();
            return View(menu);
        }

        // POST: /Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price,ImageUrl,Description,CreatedDate,CreatedBy,SubCategoryId,MenuItemsIds")] Menu menu)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var menuItems = new List<MenuItem>();
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;

                var ItemsIds = Request["MenuItemsIds"].ToString().Split(',');
                if (ItemsIds.Count() > 0)
                {
                    IEnumerable<int> ids = ItemsIds.Select(str => int.Parse(str));
                    menuItems = db.MenuItems.Where(rec => ids.Contains(rec.Id)).ToList();
                }
                db.Entry(menu).Collection("MenuItems").Load();
                foreach (var item in menu.MenuItems.ToList())
                {
                    menu.MenuItems.Remove(item);
                   
                }
                foreach (var item in menuItems)
                {
                    menu.MenuItems.Add(item);
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", menu.SubCategoryId);
            return View(menu);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<JsonResult> CreateMenu(HttpPostedFileBase[] files)
        {
            if (Session["UserName"] == null)
            {
                return Json("session out");
            }
            Menu menu = JsonConvert.DeserializeObject<Menu>(Request["menu"]);
            foreach (var file in files)
            {
                if (file == null || file.ContentLength == 0)
                {
                    //ModelState.AddModelError("ImageUpload", "This field is required");
                    return Json("ImageUrl is required");
                }
                else if (!AppConstants.validImageTypes.Contains(file.ContentType))
                {
                   // ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                    return Json("Please choose either a GIF, JPG or PNG image.");
                }

                if (file != null && file.ContentLength > 0)
                {
                    const string uploadDir = "~/Images";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), file.FileName);
                    var imageUrl = Path.Combine(uploadDir, file.FileName);
                    if (!System.IO.File.Exists(imagePath))
                    {
                        file.SaveAs(imagePath);
                    }
                    else
                    {
                        ModelState.AddModelError("ImageUpload", "Image already Exist");
                        return Json("Image already Exist");
                    }
                }
            }
            var records = new List<MenuItem>();
            if (menu.MenuItemsIds != string.Empty && menu.MenuItemsIds != null)
            {
                IEnumerable<int> ids = menu.MenuItemsIds.Split(',').Select(str => int.Parse(str));
                records = db.MenuItems.Where(rec => ids.Contains(rec.Id)).ToList();
            }
            foreach (var item in records)
            {
                menu.MenuItems.Add(item);
            }
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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Menu menu = await db.Menus.FindAsync(id);
            db.Menus.Remove(menu);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public void FileUpload(HttpPostedFileBase file)
        {

            if (file != null)
            {
                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/images/" + ImageName);

                    if (!System.IO.File.Exists(physicalPath))
                {
                    // save image in folder
                    file.SaveAs(physicalPath);
                }
            }
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
