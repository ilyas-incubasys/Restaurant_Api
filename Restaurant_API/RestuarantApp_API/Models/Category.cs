using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class Category
    {
        public Category() { CreatedDate = DateTime.Now; }
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
        [NotMapped]
        public string SubCategoryIds { set; get; }
        //SubCategory Relation
        public virtual ICollection<SubCategory> SubCategories { set; get; }
    }
}