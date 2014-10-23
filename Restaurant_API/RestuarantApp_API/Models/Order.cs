using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class Order
    {
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Currency)]
        public float Total { set; get; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string DeliveryAddress { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { set; get; }
        [Required]
        public string CreatedBy { set; get; }

        //Customer Relation
        public int CustomerId { set; get; }
        public Customer Customer { set; get; }
        
        //Menu Relation
        public ICollection<OrderMenu> Menus { set; get; }

    }
}