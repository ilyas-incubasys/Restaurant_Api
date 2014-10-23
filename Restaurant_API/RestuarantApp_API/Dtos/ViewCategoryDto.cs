using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Dtos
{
    public class ViewCategoryDto
    {
        //menus
        public int Id { set; get; }
        public string Name { set; get; }
        public float Price { set; get; }
        public string ImageUrl { set; get; }
        public string Description { set; get; }

        //categories
        public string CategoryName { set; get; }
        public int CategoryId { set; get; }
        public string CategoryImageUrl { set; get; }

        //menuItems
        public int? MenuItemId { set; get; }
        public string MenuItemName { set; get; }
        public string MenuItemImageUrl { set; get; }
        public string MenuItemDescription { set; get; }
    }
}