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
    public class BranchesController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: /Branches/
        public async Task<ActionResult> Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var branches = db.Branches.Include(b => b.Restaurant);
            return View(await branches.ToListAsync());
        }

        // GET: /Branches/Details/5
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
            BranchInfo branchinfo = await db.Branches.FindAsync(id);
            if (branchinfo == null)
            {
                return HttpNotFound();
            }
            return View(branchinfo);
        }

        // GET: /Branches/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            return View();
        }

        // POST: /Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewBranches viewBranches)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
          
            if (viewBranches.ImageUpload == null || viewBranches.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!AppConstants.validImageTypes.Contains(viewBranches.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var branchinfo = new BranchInfo
                {
                    Name = viewBranches.Name,
                    Address = viewBranches.Address,
                    RestaurantId = viewBranches.RestaurantId
                };

                if (viewBranches.ImageUpload != null && viewBranches.ImageUpload.ContentLength > 0)
                {
                    const string uploadDir = "~/Images";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), viewBranches.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, viewBranches.ImageUpload.FileName);
                    if (!System.IO.File.Exists(imagePath))
                    {
                        viewBranches.ImageUpload.SaveAs(imagePath);
                        branchinfo.ImageUrl = imageUrl;
                        db.Branches.Add(branchinfo);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("ImageUpload", "Image already Exist");
                    }
                }
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", viewBranches.RestaurantId);
            return View(viewBranches);
        }

        // GET: /Branches/Edit/5
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
            BranchInfo branchinfo = await db.Branches.FindAsync(id);
            if (branchinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", branchinfo.RestaurantId);
            return View(branchinfo);
        }

        // POST: /Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,ImageUrl,RestaurantId")] BranchInfo branchinfo)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(branchinfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", branchinfo.RestaurantId);
            return View(branchinfo);
        }

        // GET: /Branches/Delete/5
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
            BranchInfo branchinfo = await db.Branches.FindAsync(id);
            if (branchinfo == null)
            {
                return HttpNotFound();
            }
            return View(branchinfo);
        }

        // POST: /Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            BranchInfo branchinfo = await db.Branches.FindAsync(id);
            db.Branches.Remove(branchinfo);
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
