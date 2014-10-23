﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class Category
    {
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

        //SubCategory Relation
        public ICollection<SubCategory> SubCategories { set; get; }
    }
}