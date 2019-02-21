using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using DataAccessLayer;
using Entities;
using System.Data.Entity;

namespace OnlineBillPaymentSystemTest
{
    class DalAdminTest
    {
        IAdminServices adminService;
        IAppContext context;
        [OneTimeSetUp]
        public void Setup()
        {
            context = Substitute.For<IAppContext>();
            IDbSet<Customers> customerDbSet = Substitute.For<IDbSet<Customers>>();
           
            adminService = new AdminServices(context);
            IQueryable<AccountDetails> accounts = new List<AccountDetails>
            {
                new AccountDetails
            {
                BankAcNo = 652173567123,
                BankName = "test",
                CustomerID = 1,
                BranchName = "test",
                IFSC = 652173567123,
                MICRCode = 652173567123
            },
                new AccountDetails
            {
                BankAcNo = 11111111111,
                BankName = "test1",
                CustomerID = 2,
                BranchName = "test1",
                IFSC = 652173567123,
                MICRCode = 652173567123
            }
            }.AsQueryable();
            IDbSet<AccountDetails> accountDbSet = Substitute.For<IDbSet<AccountDetails>>();
            accountDbSet.Provider.Returns(accounts.Provider);
            accountDbSet.Expression.Returns(accounts.Expression);
            accountDbSet.ElementType.Returns(accounts.ElementType);
            accountDbSet.GetEnumerator().Returns(accounts.GetEnumerator());
            context.AccountDetails.Returns(accountDbSet);

            IQueryable<Categories> cat = new List<Categories>
            {
                 new Categories
                {
                    CategoryID = 1,
                    CategoryName = "Test1",
                    Description = "test description"
                },
                new Categories
                {
                    CategoryID = 2,
                    CategoryName = "Test2",
                    Description = "test description"
                },
                new Categories
                {
                    CategoryID = 3,
                    CategoryName = "Test3",
                    Description = "test description"
                },
                new Categories
                {
                    CategoryID = 4,
                    CategoryName = "Test4",
                    Description = "test description"
                }
            }.AsQueryable();
            IDbSet<Categories> catDbSet = Substitute.For<IDbSet<Categories>>();
            catDbSet.Provider.Returns(cat.Provider);
            catDbSet.Expression.Returns(cat.Expression);
            catDbSet.ElementType.Returns(cat.ElementType);
            catDbSet.GetEnumerator().Returns(cat.GetEnumerator());
            context.Categories.Returns(catDbSet);

            IQueryable<City> cities = new List<City>
            {
                new City
                {
                    CityID = 1,
                    CityName = "test"
                },
                new City
                {
                    CityID = 2,
                    CityName = "test"
                },
                new City
                {
                    CityID = 3,
                    CityName = "test"
                }
            }.AsQueryable();
            IDbSet<City> citiesDbSet = Substitute.For<IDbSet<City>>();
            citiesDbSet.Provider.Returns(cities.Provider);
            citiesDbSet.Expression.Returns(cities.Expression);
            citiesDbSet.ElementType.Returns(cities.ElementType);
            citiesDbSet.GetEnumerator().Returns(cities.GetEnumerator());
            context.City.Returns(citiesDbSet);

            IQueryable<Customers> customers = new List<Customers>
            {
                new Customers() {
                    CityID = 1,
                    ContactInfo = { ContactNo = "8877778899", Email = "abc@fd.com"},
                    CustomerAddress = "TestAddress",
                    CustomerID = 1,
                    CustomerName = "Test",
                    Gender = "female",
                    Password = "Test@123"
                },
                 new Customers() {
                    CityID = 2,
                    ContactInfo = { ContactNo = "8877778899", Email = "abc@xyz.com"},
                    CustomerAddress = "TestAddress",
                    CustomerID = 2,
                    CustomerName = "Test2",
                    Gender = "male",
                    Password = "Test@123"
                }
            }.AsQueryable();
            customerDbSet.Provider.Returns(customers.Provider);
            customerDbSet.Expression.Returns(customers.Expression);
            customerDbSet.ElementType.Returns(customers.ElementType);
            customerDbSet.GetEnumerator().Returns(customers.GetEnumerator());
            context.Customers.Returns(customerDbSet);

            IQueryable<Vendors> vendors = new List<Vendors>
            {
                new Vendors
                {
                    VendorID = 1,
                    VendorName = "Test",
                    VendorAddress = "Test Address",
                    ContactInfo = { ContactNo = "5465488798", Email = "Test@test.com"},
                    GSTIN = "8897788898"
                },
                new Vendors
                {
                    VendorID = 2,
                    VendorName = "Test",
                    VendorAddress = "Test Address",
                    ContactInfo = { ContactNo = "5465488798", Email = "Test@test.com"},
                    GSTIN = "8897788898"
                },
                new Vendors
                {
                    VendorID = 3,
                    VendorName = "Test",
                    VendorAddress = "Test Address",
                    ContactInfo = { ContactNo = "5465488798", Email = "Test@test.com"},
                    GSTIN = "889778889823456"
                }
            }.AsQueryable();
            IDbSet<Vendors> vendorDbSet = Substitute.For<IDbSet<Vendors>>();
            vendorDbSet.Provider.Returns(vendors.Provider);
            vendorDbSet.Expression.Returns(vendors.Expression);
            vendorDbSet.ElementType.Returns(vendors.ElementType);
            vendorDbSet.GetEnumerator().Returns(vendors.GetEnumerator());
            context.Vendors.Returns(vendorDbSet);


            IQueryable<CustomerVendor> cusVendors = new List<CustomerVendor> {

                new CustomerVendor
                {
                    AmountDue = 200,
                    CategoryID = 1,
                    CustomerID = 1,
                    VendorID = 1,
                    UniqueID = "u1"
                },
                new CustomerVendor
                {
                    AmountDue = 200,
                    CategoryID = 2,
                    CustomerID = 2,
                    VendorID = 2,
                    UniqueID = "u1"
                },
                new CustomerVendor
                {
                    AmountDue = 200,
                    CategoryID = 3,
                    CustomerID = 1,
                    VendorID = 3,
                    UniqueID = "u1"
                }

            }.AsQueryable();
            IDbSet<CustomerVendor> cusVendorDbSet = Substitute.For<IDbSet<CustomerVendor>>();
            cusVendorDbSet.Provider.Returns(cusVendors.Provider);
            cusVendorDbSet.Expression.Returns(cusVendors.Expression);
            cusVendorDbSet.ElementType.Returns(cusVendors.ElementType);
            cusVendorDbSet.GetEnumerator().Returns(cusVendors.GetEnumerator());
            context.CustomerVendors.Returns(cusVendorDbSet);

            IQueryable<Transactions> transactions = new List<Transactions>()
            {
                new Transactions { TransactionID="t1", UniqueID="u1", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t2", UniqueID="u2", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t3", UniqueID="u3", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t4", UniqueID="u4", Amount=123, TransactionDate=DateTime.Now},
            }.AsQueryable();
            IDbSet<Transactions> transactionDbSet = Substitute.For<IDbSet<Transactions>>();
            transactionDbSet.Provider.Returns(transactions.Provider);
            transactionDbSet.Expression.Returns(transactions.Expression);
            transactionDbSet.ElementType.Returns(transactions.ElementType);
            transactionDbSet.GetEnumerator().Returns(transactions.GetEnumerator());
            context.Transactions.Returns(transactionDbSet);

            IQueryable<Report> reportList = new List<Report>
            {
                new Report()
                {
                    TransactionID = "t1",
                    UniqueID = "u1",
                    Amount = 123,
                    CategoryName = "test",
                    CustomerName = "test",
                    TransactionDate = DateTime.Now,
                    City = "test",
                    VendorName = "test"
                }
            }.AsQueryable();
        }

        [Test]
        public void AddVendorTest()
        {
            context.SaveChanges().Returns(1);

            int result = adminService.AddVendor(context.Vendors.First());

            Assert.AreEqual(1, result);
        }

        [Test]
        public void UpdateVendorTest()
        {

            context.SaveChanges().Returns(1);

            int result = adminService.UpdateVendor(context.Vendors.First(),2);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void GetAllVendorsTest()
        {
            var result = adminService.GetAllVendors();

            Assert.AreEqual(result.Count, context.Vendors.Count());
            
        }

        [Test]
        public void GetAllVendorDetTest()
        {
            var result = adminService.GetVendorDet(1);

            Assert.AreEqual(result, context.Vendors.First());

        }

        [Test]
        public void AddCategoryTest()
        {
            context.SaveChanges().Returns(1);

            var result = adminService.AddCategory(context.Categories.First());

            Assert.AreEqual(result, 1);

        }

        [Test]
        public void UpdateCategoryTest()
        {
            context.SaveChanges().Returns(1);

            var result = adminService.UpdateCategory(context.Categories.First(),2);

            Assert.AreEqual(result, 1);

        }


        [Test]
        public void GetAllCategoriesTest()
        {
            var result = adminService.GetAllCategories();

            Assert.AreEqual(result.Count, context.Categories.Count());

        }

        [Test]
        public void AddCityTest()
        {
            context.SaveChanges().Returns(1);

            var result = adminService.AddCity(context.City.First());

            Assert.AreEqual(result, 1);

        }

        [Test]
        public void UpdateCityTest()
        {
            context.SaveChanges().Returns(1);

            var result = adminService.UpdateCity(context.City.First(), 2);

            Assert.AreEqual(result, 1);

        }

        [Test]
        public void GetAllCityTest()
        {
            var result = adminService.GetAllCities();

            Assert.AreEqual(result.Count, context.City.Count());

        }

        [Test]
        public void GetAllUsersByListTest()
        {
            var result = adminService.getAllUsersByList();

            Assert.AreEqual(result.Count, context.Customers.Count());

        }

        [Test]
        public void GetPaymentDetailsByListTest()
        {
            var result = adminService.getPaymentDetailsByList();

            Assert.AreEqual(result.Count, context.Transactions.Count());

        }

        [Test]
        public void VendorWisePaymentTest()
        {
            var result = adminService.VendorWisePayment();

            Assert.AreEqual(result.GetType(), typeof(List<Report>));
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void CityWisePaymentTest()
        {
            var result = adminService.CityWisePayment();

            Assert.AreEqual(result.GetType(), typeof(List<Report>));
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void CategoryWisePaymentTest()
        {
            var result = adminService.CategoryWisePayment();

            Assert.AreEqual(result.GetType(), typeof(List<Report>));
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void OverAllPaymentTest()
        {
            var result = adminService.OverallPayments();

            Assert.AreEqual(result.GetType(), typeof(List<Report>));
            Assert.AreEqual(result.Count, 3);
        }
    }
}
