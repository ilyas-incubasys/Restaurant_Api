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
using RestuarantApp_API.Dtos;
using Newtonsoft.Json;
using RestuarantApp_API.Constants;
using System.IO;

namespace RestuarantApp_API.AdminController
{
    public class CategoriesController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /Categories/
        public async Task<ActionResult> Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(await db.Categories.ToListAsync());
        }

        // GET: /Categories/Details/5
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
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Categories/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.SubCategories = db.SubCategories.ToList();
            return View();
        }

        // POST: /Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,ImageUrl,CreatedDate,CreatedBy")] Category category)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        //[AcceptVerbs(HttpVerbs.Post)]
        //public async Task<JsonResult> CreateCategories(Category category)
        //{
        //    if (Session["UserName"] == null)
        //    {
        //        return Json("session out");
        //    }
        //    var records = new List<SubCategory>();
        //    if (Session["UserName"] == null)
        //    {
        //        return Json("session out");
        //    }
        //    if (category.SubCategoryIds != string.Empty && category.SubCategoryIds != null)
        //    {
        //        IEnumerable<int> ids = category.SubCategoryIds.Split(',').Select(str => int.Parse(str));
        //        records = db.SubCategories.Where(rec => ids.Contains(rec.Id)).ToList();
        //    }

        //    foreach (var item in records)
        //    {
        //        category.SubCategories.Add(item);
        //    }

        //    string respponse = string.Empty;
        //    if (ModelState.IsValid)
        //    {
        //        db.Categories.Add(category);
        //        await db.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return Json(false);
        //    }

        //    ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", category.SubCategoryId);
        //    return Json(true);
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<JsonResult> CreateCategories(HttpPostedFileBase[] files)
        {
            if (Session["UserName"] == null)
            {
                return Json("session out");
            }
            Category category = JsonConvert.DeserializeObject<Category>(Request["categories"]);
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
            var records = new List<SubCategory>();
            if (category.SubCategoryIds != string.Empty && category.SubCategoryIds != null)
            {
                IEnumerable<int> ids = category.SubCategoryIds.Split(',').Select(str => int.Parse(str));
                records = db.SubCategories.Where(rec => ids.Contains(rec.Id)).ToList();
            }
            foreach (var item in records)
            {
                category.SubCategories.Add(item);
            }
            string respponse = string.Empty;
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
            }
            else
            {
                return Json(false);
            }

           // ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", menu.SubCategoryId);
            return Json(true);
        }
        // GET: /Categories/Edit/5
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
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubCategoryIds = category.SubCategories.Select(t => new SubCategoryDto
            {
                Id = t.Id,
                Name = t.Name,
                ImageUrl = t.ImageUrl
            }).ToList();
            ViewBag.SubCategories = db.SubCategories.ToList();
            return View(category);
        }

        // POST: /Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,ImageUrl,CreatedDate,CreatedBy")] Category category)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var subCategories = new List<SubCategory>();
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;

                var subCatIds = Request["SubCategoryIds"].ToString().Split(',');
                if (subCatIds.Count() > 0)
                {
                    IEnumerable<int> ids = subCatIds.Select(str => int.Parse(str));
                    subCategories = db.SubCategories.Where(rec => ids.Contains(rec.Id)).ToList();
                }
                db.Entry(category).Collection("SubCategories").Load();
                foreach (var item in category.SubCategories.ToList())
                {
                    category.SubCategories.Remove(item);

                }
                foreach (var item in subCategories)
                {
                    category.SubCategories.Add(item);
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Categories/Delete/5
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
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Category category = await db.Categories.FindAsync(id);
            db.Categories.Remove(category);
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
