using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Customers
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only aplhabets in Customer Name field")]
        public string CustomerName { get; set; }

        [RegularExpression("^male$|^female$")]
        public string Gender { get; set; }

        [StringLength(50, ErrorMessage = "Max Character range reached")]
        public string CustomerAddress { get; set; }
        public ContactComplexType ContactInfo { get; set; }
        public string Password { get; set; }
        [ForeignKey("City")]
        //Validate city as cityname required
        public int CityID { get; set; }
        public virtual AccountDetails AccountDetails { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<CustomerVendor> CustomerVendors { get; set; }
        public Customers()
        {
            this.ContactInfo = new ContactComplexType();
            this.CustomerVendors = new HashSet<CustomerVendor>();
        }

    }
}
