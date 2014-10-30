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
    public class CustomersController : ApiController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET api/Customers
        public IQueryable<Customer> GetCustomers()
        {
            var customer = from c in db.Customers
                           select c;
            return customer;
        }

        // GET api/Customers/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT api/Customers/5
        public async Task<IHttpActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Ok("Bad Request");
            }

            if (id != customer.Id)
            {
                return Ok("Bad Request");
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(customer.Id);
        }
        // POST api/Customers
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomerLogin(Customer customer)
        {
            var user = db.Customers.Where(c => c.Email == customer.Email && c.Password == customer.Password).SingleOrDefault();
            if (user!=null)
            {
                return Ok(user);
            }
            else
            {
                return Ok("Not Found###");
            }

            //CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // POST api/Customers
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var emailExist = db.Customers.Where(c => c.Email == customer.Email).SingleOrDefault();
            if (emailExist==null)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return Ok(customer.Id);
            }
            else
            {
                return Ok("Email Exist");
            }

           //CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // DELETE api/Customers/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            await db.SaveChangesAsync();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.Id == id) > 0;
        }
    }
}