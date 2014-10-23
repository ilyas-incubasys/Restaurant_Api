using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    [Table("Branches")]
    public class BranchInfo
    {
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { set; get; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { set; get; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { set; get; }

        //Relations
        public int RestaurantId { set; get; }
        public RestaurantInfo Restaurant { set; get; }
    }
}