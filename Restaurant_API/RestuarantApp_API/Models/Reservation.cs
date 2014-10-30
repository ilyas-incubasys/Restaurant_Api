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
        public string ReservationDate { set; get; }
        [Required]
        public string ReservationTime { set; get; }
        [Required]
        public int NumberOfPersons { set; get; }
        [Required]
        public DateTime CreatedDate { set; get; }
        [Required]
        public string CreatedBy { set; get; }

        public string Instructions { set; get; }
        //Relations
        public int CustomerId { set; get; }
        public Customer Customer { set; get; }

        public int BranchInfoId { set; get; }
        public BranchInfo BranchInfo { set; get; }

    }
}