using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Dtos
{
    public class MenuItemDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string ImageUrl { set; get; }
        public string Description { set; get; }
    }
}