using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace DataAccessLayer
{
    public class AppContext:DbContext, IAppContext
    {
        public AppContext():base("name=Online Bill")
        {

        }

        public IDbSet<Customers> Customers { get; set; }      
        public IDbSet<Vendors> Vendors { get; set; }
        public IDbSet<CustomerVendor> CustomerVendors { get; set; }
        public IDbSet<AccountDetails> AccountDetails { get; set; }
        public IDbSet<City> City { get; set; }
        public IDbSet<Transactions> Transactions { get; set; }
        public IDbSet<VendorCategory> VendorCategory { get; set; }
        public IDbSet<Categories> Categories { get; set; }

    }
}
