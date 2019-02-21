using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustomerVendor
    {
        [Key]
        public string UniqueID { get; set; }
        public int AmountDue { get; set; }

        //[ForeignKey("Customers")]
        //public int CustomerID { get; set; }
        //[ForeignKey("Vendors")]
        //public int VendorID { get; set; }
        //[ForeignKey("Categories")]
        //public int CategoryID { get; set; }

     //   [Index("UniqueComposite", 2, IsUnique = true)]
        public int? CustomerID { get; set; }
     //   [Index("UniqueComposite", 3, IsUnique = true)]
        public int VendorID { get; set; }
   //     [Index("UniqueComposite", 1, IsUnique = true)]
        public int CategoryID { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Vendors Vendor { get; set; }
        public virtual Categories Category { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public CustomerVendor()
        {
            this.Transactions = new HashSet<Transactions>();
        }
    }
}
