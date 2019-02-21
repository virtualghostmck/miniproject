using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AccountDetails
    {
       
        [ForeignKey("Customers"),Key]
        public int CustomerID { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Bank A/C No is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter only numbers in Contact No field")]
        [Range(1000000000, 9999999999, ErrorMessage = "Bank A/C No must be of 10 digits only")]
        public long BankAcNo { get; set; }

        [Required(ErrorMessage = "Bank Name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only aplhabets in Bank Name field")]
        public string BankName{ get; set; }

        [Required(ErrorMessage = "IFSC No is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter only numbers in IFSC field")]
        [Range(10000000000, 99999999999, ErrorMessage = "IFSC No must be of 11 digits only")]
        public long IFSC { get; set; }

        [Required(ErrorMessage = "Bank Name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only aplhabets in Branch Name field")]
        public string BranchName { get; set; }
        public long MICRCode { get; set; }       
        public virtual Customers Customers { get; set; }
    }
}
