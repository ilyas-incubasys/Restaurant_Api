using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class OrderMenu
    {
        //Composite Key
        [Key, Column(Order = 0)]
        public int OrderId { set; get; }
        [Key, Column(Order = 1)]
        public int MenuId { set; get; }
        
        //Relation Attribute
        public int Quantity { set; get; }

        
        //Relations
        public Order Order { set; get; }
        public Menu Menu { set; get; }
    }
}