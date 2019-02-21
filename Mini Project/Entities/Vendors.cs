using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Vendors
    {
        [Key]
        public int VendorID { get; set; }

        [Required(ErrorMessage = "Vendor Name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only aplhabets in Vendor Name field")]
        public string VendorName { get; set; }

        [StringLength(50, ErrorMessage = "Max Character range reached")]
        public string VendorAddress { get; set; }

        public ContactComplexType ContactInfo { get; set; }

        [StringLength(15, ErrorMessage = "Max Character range reached")]
        public string GSTIN { get; set; }
        public virtual  ICollection<City> Cities { get; set; }
        public virtual ICollection<VendorCategory> VendorCategories { get; set; }
        public virtual ICollection<CustomerVendor> CustomerVendors { get; set; }

        public Vendors()
        {
            this.ContactInfo = new ContactComplexType();
            this.Cities = new HashSet<City>();
            this.VendorCategories = new HashSet<VendorCategory>();
            this.CustomerVendors = new HashSet<CustomerVendor>();
        }
    }
}
