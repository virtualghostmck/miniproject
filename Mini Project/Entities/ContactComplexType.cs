using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [ComplexType]
    public class ContactComplexType
    {
       
        [Required(ErrorMessage = "Contact No is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter only numbers in Contact No field")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact No must be of 10 digits only")]
        public string ContactNo { get; set; }
        [EmailAddress(ErrorMessage ="Enter valid Email ID")]
        public string Email { get; set; }
    }
}
