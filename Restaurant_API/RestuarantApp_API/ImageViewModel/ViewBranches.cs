using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.ImageViewModel
{
    public class ViewBranches
    {
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { set; get; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { set; get; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { set; get; }
        //Relations
        public int RestaurantId { set; get; }
    }
}