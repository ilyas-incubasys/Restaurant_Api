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
    public class OrderMenusController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET api/OrderMenus
        public IQueryable<OrderMenu> GetOrderDetails()
        {
            var orderDetails = from o in db.OrderDetails
                            select o;
            return orderDetails;
        }

        // GET api/OrderMenus/5
        [ResponseType(typeof(OrderMenu))]
        public async Task<IHttpActionResult> GetOrderMenu(int id)
        {
            OrderMenu ordermenu = await db.OrderDetails.FindAsync(id);
            if (ordermenu == null)
            {
                return NotFound();
            }

            return Ok(ordermenu);
        }

        // PUT api/OrderMenus/5
        public async Task<IHttpActionResult> PutOrderMenu(int id, OrderMenu ordermenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordermenu.OrderId)
            {
                return BadRequest();
            }

            db.Entry(ordermenu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderMenuExists(id))
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

        // POST api/OrderMenus
        [ResponseType(typeof(OrderMenu))]
        public async Task<IHttpActionResult> PostOrderMenu(OrderMenu ordermenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(ordermenu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderMenuExists(ordermenu.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ordermenu.OrderId }, ordermenu);
        }

        // DELETE api/OrderMenus/5
        [ResponseType(typeof(OrderMenu))]
        public async Task<IHttpActionResult> DeleteOrderMenu(int id)
        {
            OrderMenu ordermenu = await db.OrderDetails.FindAsync(id);
            if (ordermenu == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(ordermenu);
            await db.SaveChangesAsync();

            return Ok(ordermenu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderMenuExists(int id)
        {
            return db.OrderDetails.Count(e => e.OrderId == id) > 0;
        }
    }
}