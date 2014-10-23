namespace RestuarantApp_API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RestuarantApp_API.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<RestuarantApp_API.Context.RestaurantContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RestuarantApp_API.Context.RestaurantContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Restaurants.AddOrUpdate(x => x.Id,
                new RestaurantInfo() { Id = 1, Name = "Yasir Broast", HeadOfficeAddress = "Gulberg III near hussain chock Lahore" },
                new RestaurantInfo() { Id = 2, Name = "Bon Fire", HeadOfficeAddress = "Bhaikay mord near campus pull Lahore" }
                );

            context.Branches.AddOrUpdate(x => x.Id,
                 new BranchInfo() { RestaurantId = 1, Name = "Gulberg III Branch", Address = "Gulberg III hussain chock firdos market lahore", ImageUrl = "images/asd.jpg" },
                 new BranchInfo() { RestaurantId = 1, Name = "Gulberg II Branch", Address = "Gulberg II main market market lahore", ImageUrl = "images/dfd.jpg" },
                 new BranchInfo() { RestaurantId = 2, Name = "Wahdat Road Branch", Address = "Wahdat Road market lahore", ImageUrl = "images/asd.jpg" },
                 new BranchInfo() { RestaurantId = 2, Name = "Muslim town Branch", Address = "Muslim town market lahore", ImageUrl = "images/dfd.jpg" }
                );

            //add update Categories along SubCategories
            context.Categories.AddOrUpdate(x => x.Id,
                new Category
            {
                Id = 1,
                Name = "chinese",
                ImageUrl = "images/ppf.jpg",
                CreatedBy = "ilyas khan",
                CreatedDate = DateTime.Now,
                SubCategories = new List<SubCategory>() { new SubCategory {Id = 1, Name = "SOUP", ImageUrl = "images/ppf.jpg", CreatedBy = "ilyas khan", CreatedDate = DateTime.Now }, 
                                                          new SubCategory {Id = 2, Name = "FRIED RICE", ImageUrl = "images/rer.jpg", CreatedBy = "ilyas khan", CreatedDate = DateTime.Now} }
            });

            //add update SubCategories along Categories
            context.SubCategories.AddOrUpdate(x => x.Id,
                new SubCategory()
                {
                    Id = 3,
                    Name = "NIHARI",
                    ImageUrl = "images/err.jpg",
                    CreatedBy = "ilyas khan",
                    CreatedDate = DateTime.Now,
                    Categories = new List<Category>() { new Category { Id = 2, Name = "pakistani", ImageUrl = "images/dd.jpg", CreatedBy = "ilyas khan", CreatedDate = DateTime.Now } }

                });

            //add update Menus
            context.Menus.AddOrUpdate(x => x.Id,
                new Menu()
                {
                    Id = 1,
                    Name = "Egg Drop Soup",
                    Price = 10,
                    ImageUrl = "images/xsd.jgp",
                    Description = "Description about Egg Drop Soup",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                    SubCategoryId = 1,
                    //MenuItems = new List<MenuItem>() 
                    //{ 
                    //   new MenuItem()
                    //    {
                    //        Name = "sub item1",
                    //        ImageUrl = "images/xsd.jgp",
                    //        Description = "Description about sub item1",
                    //        CreatedDate = DateTime.Now,
                    //        CreatedBy = "ilyas khan",
                    //    }
                    //}
                },
                new Menu()
                {
                    Id = 2,
                    Name = "Chicken Rice Soup",
                    Price = 10,
                    ImageUrl = "images/rtrt.jgp",
                    Description = "Description about Chicken Rice Soup",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                    SubCategoryId = 1,

                },
                new Menu()
                {
                    Id = 3,
                    Name = "Vegetable Fried Rice",
                    Price = 10,
                    ImageUrl = "images/fghfg.jgp",
                    Description = "Description about Vegetable Fried Rice",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                    SubCategoryId = 2
                },
                new Menu()
                {
                    Id = 4,
                    Name = "White Meat Chicken Fried Rice",
                    Price = 10,
                    ImageUrl = "images/fhfg.jgp",
                    Description = "Description about White Meat Chicken Fried Rice",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                    SubCategoryId = 2
                },
                new Menu()
                {
                    Id = 5,
                    Name = "MOHAMMADI Nihari",
                    Price = 10,
                    ImageUrl = "images/fhfg.jgp",
                    Description = "Description about MOHAMMADI NIHARI",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                    SubCategoryId = 3
                },
                new Menu()
                {
                    Id = 6,
                    Name = "MOHAMMADI Special Nihari",
                    Price = 10,
                    ImageUrl = "images/fhfg.jgp",
                    Description = "Description about MOHAMMADI Special Nihari",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                    SubCategoryId = 3
                }
                );

            context.MenuItems.AddOrUpdate(x => x.Id,
                new MenuItem()
                {
                    Id = 1,
                    Name = "sub item2",
                    ImageUrl = "images/xsd.jgp",
                    Description = "Description about sub item2",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                });
            context.MenuItems.AddOrUpdate(x => x.Id,
                new MenuItem()
                {
                    Id = 2,
                    Name = "sub item3",
                    ImageUrl = "images/xsd.jgp",
                    Description = "Description about sub item3",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                });
            var menuItem1 = context.MenuItems.Where(c => c.Id == 1).ToList();
            Menu menu = new Menu()
                {
                    Name = "MOHAMMADI Special Nihari Fry",
                    Price = 10,
                    ImageUrl = "images/fhfg.jgp",
                    Description = "Description about MOHAMMADI Special Nihari Fry",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "ilyas khan",
                    SubCategoryId = 3,
                    MenuItems = menuItem1
                };
            Menu menu1 = new Menu()
            {
                Name = "MOHAMMADI Special Nihari Fry1",
                Price = 10,
                ImageUrl = "images/fhfg.jgp",
                Description = "Description about MOHAMMADI Special Nihari Fry1",
                CreatedDate = DateTime.Now,
                CreatedBy = "ilyas khan",
                SubCategoryId = 3,
                MenuItems = menuItem1
            };
            //var menuItem = new MenuItem()
            //            {
            //                Name = "sub item2",
            //                ImageUrl = "images/xsd.jgp",
            //                Description = "Description about sub item2",
            //                CreatedDate = DateTime.Now,
            //                CreatedBy = "ilyas khan",
            //            };
            //menu.MenuItems.Add(menuItem);
            context.Menus.AddOrUpdate(menu);
            context.Menus.AddOrUpdate(menu1);
            context.Customers.AddOrUpdate(x => x.Id,
                new Customer()
                {
                    Id = 1,
                    Name = "ilyas khan",
                    Phone = "03336332455",
                    Password = "123456",
                    Email = "ilyas.khan@incubasys.com",
                    CreatedDate = DateTime.Now
                }
                );
            context.Orders.AddOrUpdate(x => x.Id,
                new Order()
                {
                    Id = 1,
                    Total = 20,
                    DeliveryAddress = "street# 13 house #14 farooq colony lahore",
                    CreatedBy = "ilyas khan",
                    CreatedDate = DateTime.Now,
                    CustomerId = 1
                }
                );
            context.OrderDetails.AddOrUpdate(x => x.OrderId,
                new OrderMenu() { OrderId = 1, MenuId = 2, Quantity = 2 }
                );

            Task<IdentityResult> result = RegisterUser("ilyas", "123456");
        }
        public async Task<IdentityResult> RegisterUser(string userName, string password)
        {
            RestuarantApp_API.Context.RestaurantContext context = new Context.RestaurantContext();
            UserManager<Admin> _userManager;
            _userManager = new UserManager<Admin>(new UserStore<Admin>(context));

            Admin user = new Admin
            {
                Name = userName,
                CreatedBy = "ilyas khan",
                CreatedDate = DateTime.Now,
                UserName = "khan"

            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
            }

            return result;
        }
    }
}
