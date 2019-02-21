using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class VendorCategory
    {
       
        //[ForeignKey("Vendors")]
        //public int VendorID { get; set; }
        //[ForeignKey("Categories")]
        //public int CategoryID { get; set; }
        [Key, Column(Order = 0)]
        public int CategoryID { get; set; }

        [Key, Column(Order = 1)]
        public int VendorID { get; set; }
        public virtual Categories Category { get; set; }
        public virtual Vendors Vendor { get; set; }

        [Range(0,10000,ErrorMessage ="Max Amount Reached")]
        [Required(ErrorMessage ="Amount Field is required")]
        public int Amount { get; set; }

        //Validation for vendor name and category name

    }
}
