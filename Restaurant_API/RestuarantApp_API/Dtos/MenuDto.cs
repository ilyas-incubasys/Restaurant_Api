using RestuarantApp_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Dtos
{
    public class MenuDto
    {
        public MenuDto() { MenuItemDtos = null; }

        public int Id { set; get; }
        public string Name { set; get; }
        public float Price { set; get; }
        public string ImageUrl { set; get; }
        public string Description { set; get; }

        // Relation
        public virtual ICollection<MenuItemDto> MenuItemDtos { set; get; }
    }

}