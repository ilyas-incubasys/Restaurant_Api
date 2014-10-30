using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class Menu
    {
        public Menu()
        {
            MenuItems = new List<MenuItem>();
            CreatedDate = DateTime.Now;
        }
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { set; get; }
        [Required]
        [DataType(DataType.Currency)]
        public float Price { set; get; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { set; get; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { set; get; }
        [Required]
        public string CreatedBy { set; get; }

        //Category Relation
        public int SubCategoryId { set; get; }
        public SubCategory SubCategory { set; get; }
        [NotMapped]
        public string CategoryName { set; get; }
        [NotMapped]
        public int CategoryId { set; get; }
        [NotMapped]
        public string CategoryImageUrl { set; get; }
        [NotMapped]
        public string MenuItemsIds { set; get; }
         [NotMapped]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { set; get; }
        //Menu Item Relation
        public virtual ICollection<MenuItem> MenuItems { set; get; }
        public ICollection<OrderMenu> Orders { set; get; }
 
    }
}