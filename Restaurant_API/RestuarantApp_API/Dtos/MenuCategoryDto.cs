using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Dtos
{
    public class MenuCategoryDto
    {
       
        public string CategoryName { set; get; }
        public int CategoryId { set; get; }
        public string CategoryImageUrl { set; get; }

        public virtual ICollection<MenuDto> MenuDtos { set; get; }
        
    }
}