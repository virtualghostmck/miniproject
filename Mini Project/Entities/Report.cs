using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Report
    {
        public string TransactionID{ get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string VendorName { get; set; }
        public string CategoryName{ get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string UniqueID { get; set; }
    }
}
