namespace RestuarantApp_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HeadOfficeAddress = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Single(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderMenus",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.MenuId })
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Single(nullable: false),
                        DeliveryAddress = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        NumberOfPersons = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SubCategoryCategories",
                c => new
                    {
                        SubCategory_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubCategory_Id, t.Category_Id })
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.SubCategory_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.MenuItemMenus",
                c => new
                    {
                        MenuItem_Id = c.Int(nullable: false),
                        Menu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuItem_Id, t.Menu_Id })
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.Menus", t => t.Menu_Id, cascadeDelete: true)
                .Index(t => t.MenuItem_Id)
                .Index(t => t.Menu_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Menus", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.OrderMenus", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OrderMenus", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.MenuItemMenus", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.MenuItemMenus", "MenuItem_Id", "dbo.MenuItems");
            DropForeignKey("dbo.SubCategoryCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.SubCategoryCategories", "SubCategory_Id", "dbo.SubCategories");
            DropForeignKey("dbo.Branches", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.MenuItemMenus", new[] { "Menu_Id" });
            DropIndex("dbo.MenuItemMenus", new[] { "MenuItem_Id" });
            DropIndex("dbo.SubCategoryCategories", new[] { "Category_Id" });
            DropIndex("dbo.SubCategoryCategories", new[] { "SubCategory_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderMenus", new[] { "MenuId" });
            DropIndex("dbo.OrderMenus", new[] { "OrderId" });
            DropIndex("dbo.Menus", new[] { "SubCategoryId" });
            DropIndex("dbo.Branches", new[] { "RestaurantId" });
            DropTable("dbo.MenuItemMenus");
            DropTable("dbo.SubCategoryCategories");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reservations");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderMenus");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Menus");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Branches");
        }
    }
}
