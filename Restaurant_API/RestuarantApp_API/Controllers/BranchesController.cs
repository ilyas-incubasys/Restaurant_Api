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

namespace RestuarantApp_API.Controllers
{
    public class BranchesController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET api/Branches
        public IQueryable<BranchInfo> GetBranches()
        {
            var branches = from b in db.Branches
                           select b;
            return branches;
        }

        // GET api/Branches/5
        [ResponseType(typeof(BranchInfo))]
        public async Task<IHttpActionResult> GetBranchInfo(int id)
        {
            BranchInfo branchinfo = await db.Branches.FindAsync(id);
            if (branchinfo == null)
            {
                return NotFound();
            }

            return Ok(branchinfo);
        }

        // PUT api/Branches/5
        public async Task<IHttpActionResult> PutBranchInfo(int id, BranchInfo branchinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branchinfo.Id)
            {
                return BadRequest();
            }

            db.Entry(branchinfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchInfoExists(id))
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

        // POST api/Branches
        [ResponseType(typeof(BranchInfo))]
        public async Task<IHttpActionResult> PostBranchInfo(BranchInfo branchinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Branches.Add(branchinfo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = branchinfo.Id }, branchinfo);
        }

        // DELETE api/Branches/5
        [ResponseType(typeof(BranchInfo))]
        public async Task<IHttpActionResult> DeleteBranchInfo(int id)
        {
            BranchInfo branchinfo = await db.Branches.FindAsync(id);
            if (branchinfo == null)
            {
                return NotFound();
            }

            db.Branches.Remove(branchinfo);
            await db.SaveChangesAsync();

            return Ok(branchinfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchInfoExists(int id)
        {
            return db.Branches.Count(e => e.Id == id) > 0;
        }
    }
}