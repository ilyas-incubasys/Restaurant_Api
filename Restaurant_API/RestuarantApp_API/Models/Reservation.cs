using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class Reservation
    {
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { set; get; }
        [Required]
        public int NumberOfPersons { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { set; get; }
        [Required]
        public string CreatedBy { set; get; }

        //Relations
        public int CustomerId { set; get; }
        public Customer Customer { set; get; }

    }
}