using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using RestuarantApp_API.Migrations;
using RestuarantApp_API.Models;
using RestuarantApp_API.Dtos;
namespace RestuarantApp_API.Context
{
    public class RestaurantContext : IdentityDbContext<Admin>
    {
        public RestaurantContext() :base("RestaurantConnectionString")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<RestaurantContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RestaurantContext, Configuration>("RestaurantConnectionString"));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuItem> MenuItems { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderMenu> OrderDetails { set; get; }
        public DbSet<Reservation> Reservation { set; get; }
        public DbSet<RestaurantInfo> Restaurants { set; get; }
        public DbSet<BranchInfo> Branches { set; get; }
        public DbSet<SubCategory> SubCategories { set; get; }

   
    }
}