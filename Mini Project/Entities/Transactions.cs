using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transactions
    {
        [Key]
        public string TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }

        [ForeignKey("CustomerVendor")]
        public string UniqueID { get; set; }

        public virtual CustomerVendor CustomerVendor { get; set; }
    }
}
