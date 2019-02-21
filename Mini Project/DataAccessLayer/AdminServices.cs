using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{


    public class AdminServices: IAdminServices
    {
        IAppContext context;
        public AdminServices()
        {
            context = new AppContext();
        }
        public AdminServices(IAppContext appContext)
        {
            context = appContext;
        }
         

        public int AddVendor(Vendors vendor)
        {
            try
            {
                int result = 0;
                context.Vendors.Add(vendor);
                result = context.SaveChanges();
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int UpdateVendor(Vendors vendor, int vendorId)
        {
            try
            {
                int result = 0;
                Vendors existingVendor = context.Vendors.FirstOrDefault(ven => ven.VendorID == vendorId);
                if (existingVendor != null)
                {
                    existingVendor.VendorName = vendor.VendorName;
                    existingVendor.VendorAddress = vendor.VendorAddress;
                    existingVendor.ContactInfo.ContactNo = vendor.ContactInfo.ContactNo;
                    existingVendor.ContactInfo.Email = vendor.ContactInfo.Email;
                    existingVendor.GSTIN = vendor.GSTIN;
                    result = context.SaveChanges();
                }
                return result;
            }
            catch (SqlException)
            {
                throw;
            }          
            catch (Exception)
            {
                throw;
            }
        }


        public List<Vendors> GetAllVendors()
        {
            try
            {
                List<Vendors> vendors = context.Vendors.ToList();
                return vendors;
            }
            catch (SqlException)
            {
                throw;
            }
           
            catch (Exception)
            {
                throw;
            }
        }



        public Vendors GetVendorDet(int vendorId)
        {
            try
            {
                Vendors existingVendor = context.Vendors.FirstOrDefault(ven => ven.VendorID == vendorId);
                return existingVendor;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public int AddCategory(Categories category)
        {
            try
            {
                int result = 0;
                context.Categories.Add(category);
                result = context.SaveChanges();
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int UpdateCategory(Categories category, int categoryId)
        {
            try
            {
                int result = 0;
                Categories existingCategory = context.Categories.FirstOrDefault(cat => cat.CategoryID == categoryId);
                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName;
                    result = context.SaveChanges();
                }
                return result;
            }
            catch (SqlException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
        }


        public List<Categories> GetAllCategories()
        {
            try
            {
                List<Categories> categories = context.Categories.ToList();
                return categories;
            }
            catch (SqlException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public int AddCity(City city)
        {
            try
            {
                int result = 0;
                context.City.Add(city);
                result = context.SaveChanges();
                return result;
            }
            catch (SqlException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }

        }

        public int UpdateCity(City city, int cityId)
        {
            try
            {
                int result = 0;
                City existingCity = context.City.FirstOrDefault(ct => ct.CityID == cityId);
                if (existingCity != null)
                {
                    existingCity.CityName = city.CityName;
                    result = context.SaveChanges();
                }
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<City> GetAllCities()
        {
            try
            {
                List<City> cities = context.City.ToList();
                return cities;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public List<Report> VendorWisePayment(DateTime? Start = null, DateTime? End = null)
        {
            if (!Start.HasValue)
            {
                Start = new DateTime(1, 1, 1);
            }
            if (!End.HasValue)
            {
                End = new DateTime(9999, 12, 1);
            }

            List<Report> query = (from Transactions in context.Transactions
                        join CustomerVendors in context.CustomerVendors
                          on Transactions.UniqueID
                              equals CustomerVendors.UniqueID
                        join Categories in context.Categories
                           on CustomerVendors.CategoryID
                           equals Categories.CategoryID
                        join Customers in context.Customers
                         on CustomerVendors.CustomerID
                         equals Customers.CustomerID
                        join vendors in context.Vendors
                        on CustomerVendors.VendorID
                        equals vendors.VendorID
                        join city in context.City
                        on Customers.CityID equals city.CityID
                        where DateTime.Compare(Transactions.TransactionDate,Start.Value) > 0 && DateTime.Compare(Transactions.TransactionDate, End.Value) < 0
                        orderby vendors.VendorID
                        select new Report
                        {

                            TransactionID = Transactions.TransactionID,
                            UniqueID = CustomerVendors.UniqueID,
                            TransactionDate = Transactions.TransactionDate,
                            Amount = Transactions.Amount,
                            VendorName = vendors.VendorName,
                            CategoryName = Categories.CategoryName,
                            CustomerName = Customers.CustomerName,
                            City = city.CityName
                        }).ToList();

            return query;
        }





        //--city wise payment of utility bills
        //SELECT t.transactionID, t.transactionDate, t.amount, v.vendorName, cat.categoryName, cus.customerName, c.cityName
        //FROM transactions t
        //INNER JOIN cusotmer_vendor cv ON t.uniqueID= cv.uniqueID
        //INNER JOIN categories cat ON cv.categoryID= cat.categoryID
        //INNER JOIN customers cus ON cus.customerID= cv.customerID
        //INNER JOIN vendor v ON cv.vendorID= v.vendorID
        //INNER JOIN vendor_city vc ON v.vendorID= vc.vendorID
        //INNER JOIN city c ON vc.cityID= c.cityID
        //order BY c.cityID



        public List<Report> CityWisePayment(DateTime? Start=null,DateTime? End=null)
        {
            if (!Start.HasValue)
            {
                Start = new DateTime(1, 1, 1);
            }
            if (!End.HasValue)
            {
                End = new DateTime(9999, 12, 1);
            }
            var query = from Transactions in context.Transactions
                        join CustomerVendors in context.CustomerVendors
                          on Transactions.UniqueID
                              equals CustomerVendors.UniqueID
                        join Categories in context.Categories
                           on CustomerVendors.CategoryID
                           equals Categories.CategoryID
                        join Customers in context.Customers
                         on CustomerVendors.CustomerID
                         equals Customers.CustomerID
                        join vendors in context.Vendors
                        on CustomerVendors.VendorID
                        equals vendors.VendorID
                        join city in context.City
                        on Customers.CityID equals city.CityID
                        where DateTime.Compare(Transactions.TransactionDate, Start.Value) > 0 && DateTime.Compare(Transactions.TransactionDate, End.Value) < 0
                        orderby city.CityName
                        select new Report
                        {

                            TransactionID = Transactions.TransactionID,
                            UniqueID= CustomerVendors.UniqueID,
                            TransactionDate = Transactions.TransactionDate,
                            Amount = Transactions.Amount,
                            VendorName = vendors.VendorName,
                            CategoryName = Categories.CategoryName,
                            CustomerName = Customers.CustomerName,
                            City=city.CityName

                        };
            return query.ToList();
        }





        //--category wise payment of utility bills
        //SELECT t.transactionID, t.transactionDate, t.amount, v.vendorName, cat.categoryName, cus.customerName
        //FROM transactions t
        //INNER JOIN cusotmer_vendor cv ON t.uniqueID= cv.uniqueID
        //INNER JOIN categories cat ON cv.categoryID= cat.categoryID
        //INNER JOIN customers cus ON cus.customerID= cv.customerID
        //INNER JOIN vendor v ON cv.vendorID= v.vendorID
        //order BY cat.categoryID


        public List<Report> CategoryWisePayment(DateTime? Start = null, DateTime? End = null)
        {
            if (!Start.HasValue)
            {
                Start = new DateTime(1, 1, 1);
            }
            if (!End.HasValue)
            {
                End = new DateTime(9999, 12, 1);
            }
            var query = from Transactions in context.Transactions
                        join CustomerVendors in context.CustomerVendors
                          on Transactions.UniqueID
                              equals CustomerVendors.UniqueID
                        join Categories in context.Categories
                           on CustomerVendors.CategoryID
                           equals Categories.CategoryID
                        join Customers in context.Customers
                         on CustomerVendors.CustomerID
                         equals Customers.CustomerID
                        join vendors in context.Vendors
                        on CustomerVendors.VendorID
                        equals vendors.VendorID
                        join city in context.City
                        on Customers.CityID equals city.CityID
                        where DateTime.Compare(Transactions.TransactionDate, Start.Value) > 0 && DateTime.Compare(Transactions.TransactionDate, End.Value) < 0
                        orderby Categories.CategoryID
                        select new Report
                        {

                            TransactionID = Transactions.TransactionID,
                            TransactionDate = Transactions.TransactionDate,
                            UniqueID = CustomerVendors.UniqueID,
                            Amount = Transactions.Amount,
                            VendorName = vendors.VendorName,
                            CategoryName = Categories.CategoryName,
                            CustomerName = Customers.CustomerName,
                            City = city.CityName
                        };

            return query.ToList();
        }


        public List<Report> OverallPayments(DateTime? Start = null, DateTime? End = null)
        {
            if (!Start.HasValue)
            {
                Start = new DateTime(1, 1, 1);
            }
            if (!End.HasValue)
            {
                End = new DateTime(9999, 12, 1);
            }
            var query = from Transactions in context.Transactions
                        join CustomerVendors in context.CustomerVendors
                          on Transactions.UniqueID
                              equals CustomerVendors.UniqueID
                        join Categories in context.Categories
                           on CustomerVendors.CategoryID
                           equals Categories.CategoryID
                        join Customers in context.Customers
                         on CustomerVendors.CustomerID
                         equals Customers.CustomerID
                        join vendors in context.Vendors
                        on CustomerVendors.VendorID
                        equals vendors.VendorID
                        join city in context.City
                        on Customers.CityID equals city.CityID
                        where DateTime.Compare(Transactions.TransactionDate,Start.Value) > 0 && DateTime.Compare(Transactions.TransactionDate, End.Value) < 0
                        select new Report
                        {

                            TransactionID = Transactions.TransactionID,
                            TransactionDate = Transactions.TransactionDate,
                            UniqueID = CustomerVendors.UniqueID,
                            Amount = Transactions.Amount,
                            VendorName = vendors.VendorName,
                            CategoryName = Categories.CategoryName,
                            CustomerName = Customers.CustomerName,
                            City = city.CityName
                        };

            return query.ToList();
        }


        public List<Customers> getAllUsersByList()
        {
            List<Customers> customerDetails;
            customerDetails = context.Customers.ToList();
            return customerDetails;
        }

        public List<Transactions> getPaymentDetailsByList()
        {
            List<Transactions> paymentDetails = new List<Transactions>();
            paymentDetails = context.Transactions.ToList();
            return paymentDetails;
        }
    }
}
   