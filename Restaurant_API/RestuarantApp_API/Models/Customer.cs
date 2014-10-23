using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestuarantApp_API.Models
{
    public class Customer
    {
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { set; get; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { set; get; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { set; get; }

        //Relations

        public ICollection<Order> Orders { set; get; }
        public ICollection<Reservation> Reservations { set; get; }

    }
}