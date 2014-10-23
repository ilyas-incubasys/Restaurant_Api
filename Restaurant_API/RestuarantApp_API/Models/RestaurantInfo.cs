using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace RestuarantApp_API.Models
{
    [Table("Restaurants")]
    public class RestaurantInfo
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string HeadOfficeAddress { set; get; }
        public string ImageUrl { set; get; }


        //Relation
        public ICollection<BranchInfo> Branchs { set; get; }
    }
}