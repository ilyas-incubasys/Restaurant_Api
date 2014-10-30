using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class MenuItem
    {
        public MenuItem()
        {
            CreatedDate = DateTime.Now;

        }
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { set; get; }
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
        [NotMapped]
        public string MenuId { set; get; }

        //Menu Relation
        public virtual ICollection<Menu> Menus { set; get; }

    }
}