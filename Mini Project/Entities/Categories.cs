using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only aplhabets in Category Name field")]
        public string CategoryName { get; set; }

        [StringLength(50, ErrorMessage = "Max Character range reached")]
        public string Description { get; set; }
        public virtual ICollection<VendorCategory> VendorCategories { get; set; }
        public virtual ICollection<CustomerVendor> CustomerVendors { get; set; }

        public Categories()
        {
            this.CustomerVendors = new HashSet<CustomerVendor>();
            this.VendorCategories = new HashSet<VendorCategory>();
        }
    }
}
