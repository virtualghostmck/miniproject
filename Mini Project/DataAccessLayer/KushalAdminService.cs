using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    public class KushalAdminService
    {
        AppContext kushalContext;
        public KushalAdminService()
        {
            kushalContext = new AppContext();
        }

        /// <summary>
        ///  Method 1: (To Get User Details)
        /// </summary>
        /// <returns></returns>
        public List<Customers> getAllUsersByList()
        {
            List<Customers> customerDetails = new List<Customers>();
            customerDetails = kushalContext.Customers.ToList();
            return customerDetails;
        }

        /// <summary>
        ///  Method 2: (To Get User Details)
        /// </summary>
        public List<Customers> getAllUsers()
        {
            var getAllUserDetails =
                (
                    from userDetails in kushalContext.Customers
                    select userDetails
                );

            return getAllUserDetails.Distinct().ToList();
        }

        /// <summary>
        ///  Method 1: (To Get Payment Details)
        /// </summary>
        /// <returns></returns>
        public List<Transactions> getPaymentDetailsByList()
        {
            List<Transactions> paymentDetails = new List<Transactions>();
            paymentDetails = kushalContext.Transactions.ToList();
            return paymentDetails;
        }

        /// <summary>
        ///  Method 2: To Get Payment Details
        /// </summary>
        public List<Transactions> getPaymentDetails()
        {
            var getPaymentDetailsQuery =
                (
                    from paymentDetails in kushalContext.Transactions
                    select paymentDetails
                );

            return getPaymentDetailsQuery.Distinct().ToList();
        }

    }
}
