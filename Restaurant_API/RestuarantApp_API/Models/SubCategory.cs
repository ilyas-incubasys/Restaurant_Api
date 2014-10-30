using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class SubCategory
    {
        public SubCategory() { CreatedDate = DateTime.Now; }
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { set; get; }
        [Required]
        public string CreatedBy { set; get; }

        //Category Relation
        public virtual ICollection<Category> Categories { get; set; }
        //Menu Relation
        public ICollection<Menu> Menus { set; get; }
 
    }
}