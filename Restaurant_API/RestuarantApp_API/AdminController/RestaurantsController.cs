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
using RestuarantApp_API.ImageViewModel;
using RestuarantApp_API.Constants;
using System.IO;

namespace RestuarantApp_API.AdminController
{
    public class RestaurantsController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /Restaurants/
        public async Task<ActionResult> Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(await db.Restaurants.ToListAsync());
        }

        // GET: /Restaurants/Details/5
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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: /Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,HeadOfficeAddress,ImageUrl")] HttpPostedFileBase file, RestaurantInfo restaurantinfo)
        //{
        //    if (Session["UserName"] == null)
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //    if (file != null)
        //    {
        //        string ImageName = System.IO.Path.GetFileName(file.FileName);
        //        string physicalPath = Server.MapPath("~/images/" + ImageName);
        //        restaurantinfo.ImageUrl = ImageName;
        //        if (!System.IO.File.Exists(physicalPath))
        //        {
        //            // save image in folder
        //            file.SaveAs(physicalPath);
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.Restaurants.Add(restaurantinfo);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(restaurantinfo);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewRestaurants viewRestaurants)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (viewRestaurants.ImageUpload == null || viewRestaurants.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!AppConstants.validImageTypes.Contains(viewRestaurants.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var restaurantInfo = new RestaurantInfo
                {
                    Name = viewRestaurants.Name,
                    HeadOfficeAddress = viewRestaurants.HeadOfficeAddress
                };

                if (viewRestaurants.ImageUpload != null && viewRestaurants.ImageUpload.ContentLength > 0)
                {
                    const string uploadDir = "~/Images";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), viewRestaurants.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, viewRestaurants.ImageUpload.FileName);
                    if (!System.IO.File.Exists(imagePath))
                    {
                        viewRestaurants.ImageUpload.SaveAs(imagePath);
                        restaurantInfo.ImageUrl = imageUrl;
                        db.Restaurants.Add(restaurantInfo);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("ImageUpload", "Image already Exist");
                    }
                }
            }

            return View(viewRestaurants);
        }

        // GET: /Restaurants/Edit/5
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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
