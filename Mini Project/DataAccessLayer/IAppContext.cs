using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IAppContext
    {
        IDbSet<Customers> Customers { get; set; }
        IDbSet<Vendors> Vendors { get; set; }
        IDbSet<CustomerVendor> CustomerVendors { get; set; }
        IDbSet<AccountDetails> AccountDetails { get; set; }
        IDbSet<City> City { get; set; }
        IDbSet<Transactions> Transactions { get; set; }
        IDbSet<VendorCategory> VendorCategory { get; set; }
        IDbSet<Categories> Categories { get; set; }

        int SaveChanges();

    }
}
