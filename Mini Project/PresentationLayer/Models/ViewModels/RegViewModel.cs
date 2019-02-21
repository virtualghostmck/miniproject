using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.ViewModels
{
    public class RegViewModel
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only aplhabets in Customer Name field")]
        [Display(Name ="Name")]
        public string CustomerName { get; set; }

        [RegularExpression("^male$|^female$",ErrorMessage ="Please specify male or female")]
        public string Gender { get; set; }

        [StringLength(50, ErrorMessage = "Max Character range reached")]
        [Display(Name = "Address")]
        public string CustomerAddress { get; set; }
        [Required(ErrorMessage = "Contact No is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter only numbers in Contact No field")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact No must be of 10 digits only")]
        [Display(Name = "Mobile No")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage ="Email ID is required")]
        [EmailAddress(ErrorMessage = "Enter valid Email ID")]
        [Display(Name = "Email ID")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="City is Required")]
        public string City { get; set; }

    }
}