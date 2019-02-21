using System;
using PresentationLayer.Areas.Admin.Controllers;
using PresentationLayer.Areas.Admin.Models;
using BusinessLogicLayer;
using DataAccessLayer;
using NSubstitute;
using Entities;
using System.Web.Mvc;
using NSubstituteAutoMocker;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web;


namespace OnlineBillPaymentSystemTest
{
    [TestFixture]
    public class BusinessTest
    {
        IAdminServices adminService;
        IUserService userService;
        Validation validation;
        List<Categories> categoryList;
        List<Customers> customerList;
        List<City> cityList;
        List<Report> reportList;
        List<Vendors> vendorList;
        AccountDetails accountDetails;
        List<Transactions> transactions;
        string email;

        [OneTimeSetUp]
        public void SetUp()
        {
            email = "abc@xyz.com";
            transactions = new List<Transactions>()
            {
                new Transactions { TransactionID="t1", UniqueID="u1", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t2", UniqueID="u2", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t3", UniqueID="u3", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t4", UniqueID="u4", Amount=123, TransactionDate=DateTime.Now},
            };
            accountDetails = new AccountDetails
            {
                BankAcNo = 652173567123,
                BankName = "test",
                CustomerID = 1,
                BranchName = "test",
                IFSC = 652173567123,
                MICRCode = 652173567123
            };

            vendorList = new List<Vendors>
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
            };


            cityList = new List<City>
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
            };

            reportList = new List<Report>
            {
                new Report()
                {
                    TransactionID = "1",
                    UniqueID = "1",
                    Amount = 1,
                    CategoryName = "test",
                    CustomerName = "test",
                    TransactionDate = DateTime.Now,
                    City = "test",
                    VendorName = "test"
                }
            };

            categoryList = new List<Categories>
            {
                new Categories
                {
                    CategoryID = 1,
                    CategoryName = "Test",
                    Description = "test description"
                },
                new Categories
                {
                    CategoryID = 2,
                    CategoryName = "Test",
                    Description = "test description"
                },
                new Categories
                {
                    CategoryID = 3,
                    CategoryName = "Test",
                    Description = "test description"
                },
                new Categories
                {
                    CategoryID = 4,
                    CategoryName = "Test",
                    Description = "test description"
                }
            };
            customerList = new List<Customers>
            {
                new Customers() {
                    CityID = 1,
                    ContactInfo = { ContactNo = "8877778899", Email = "abc@fd.com"},
                    CustomerAddress = "TestAddress",
                    CustomerID = 1,
                    CustomerName = "Test",
                    Gender = "female",
                    Password = "Test@123"
                }
            };
            
            adminService = Substitute.For<IAdminServices>();
            userService = Substitute.For<IUserService>();
            validation = new Validation(adminService, userService);
        }

        [Test]
        public void GetAllCategoriesTest()
        {
            adminService.GetAllCategories().Returns(categoryList);

            Assert.That(validation.GetAllValidCategories(), Is.EqualTo(categoryList) ); 
        }


        [Test]
        public void GetUserByEmailTest()
        {
            userService.GetUserByEmail("test").Returns(customerList[0]);

            Assert.That(validation.ValidateGetUserByEmail("test"), Is.EqualTo(customerList[0]));
        }

        //[TestCase(1)]
        //[Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedException(typeof(Exception))]
        //public void GetUserByEmailTest(int num)
        //{

        //    userService.GetUserByEmail("test").When
        //    //userService = null;
        //    validation.ValidateGetUserByEmail("test");
        //}

        [Test]
        public void AddCategoryTest()
        {
            adminService.AddCategory(categoryList[0]).Returns(1);

            Assert.That(validation.AddValidCategory(categoryList[0]), Is.EqualTo(1));
        }

        [Test]
        public void UpdateCategoryTest()
        {
            adminService.UpdateCategory(categoryList[1],1).Returns(1);

            Assert.That(validation.UpdateValidCategory(categoryList[1],1), Is.EqualTo(1));
        }

        [Test]
        public void GetAllCitiesTest()
        {
            adminService.GetAllCities().Returns(cityList);

            Assert.That(validation.GetAllValidCities(), Is.EqualTo(cityList));
        }


        [Test]
        public void AddValidCityTest()
        {
            adminService.AddCity(cityList[0]).Returns(1);

            Assert.That(validation.AddValidCity(cityList[0]), Is.EqualTo(1));
        }


        [Test]
        public void UpdateCityTest()
        {
            adminService.UpdateCity(cityList[1],1).Returns(1);

            Assert.That(validation.UpdateValidCity(cityList[1],1), Is.EqualTo(1));
        }

        [Test]
        public void GetAllUsersTest()
        {
            adminService.getAllUsersByList().Returns(customerList);

            Assert.That(validation.getAllValidUsersByList(), Is.EqualTo(customerList));
        }

        [Test]
        public void OverallValidPaymentsTest()
        {
            adminService.OverallPayments().Returns(reportList);

            Assert.That(validation.OverallValidPayments(), Is.EqualTo(reportList));
        }


        [Test]
        public void CategoryWisePaymentTest()
        {
            adminService.CategoryWisePayment().Returns(reportList);

            Assert.That(validation.ValidCategoryWisePayment(), Is.EqualTo(reportList));
        }

        [Test]
        public void CityWisePaymentTest()
        {
            adminService.CityWisePayment().Returns(reportList);

            Assert.That(validation.ValidCityWisePayment(), Is.EqualTo(reportList));
        }

        [Test]
        public void VendorWisePaymentTest()
        {
            adminService.VendorWisePayment().Returns(reportList);

            Assert.That(validation.ValidVendorWisePayment(), Is.EqualTo(reportList));
        }

        [Test]
        public void GetVendorsTest()
        {
            userService.GetVendors(1).Returns(vendorList);

            Assert.That(validation.GetVendors(1), Is.EqualTo(vendorList));
        }


        [Test]
        public void GetAllVendorsTest()
        {
            adminService.GetAllVendors().Returns(vendorList);

            Assert.That(validation.GetAllValidVendors(), Is.EqualTo(vendorList));
        }

        [Test]
        public void AddVendorTest()
        {
            adminService.AddVendor(vendorList[0]).Returns(1);

            Assert.That(validation.AddValidVendor(vendorList[0]), Is.EqualTo(1));
        }

        [Test]
        public void AddBankDetailsTest()
        {
            userService.AddBankDetails(accountDetails,1).Returns(true);

            Assert.That(validation.AddBankDetails(accountDetails,1), Is.EqualTo(true));
        }

        [Test]
        public void UpdateVendorTest()
        {
            adminService.UpdateVendor(vendorList[0], 1).Returns(1);

            Assert.That(validation.UpdateValidVendor(vendorList[0], 1), Is.EqualTo(1));
        }


        [Test]
        public void GetBankDetailsTest()
        {
            userService.GetBankDetails(1).Returns(accountDetails);

            Assert.That(validation.GetBankDetails(1), Is.EqualTo(accountDetails));
        }

        #region testcases

        [Test]
        public void GetUser()
        {
            userService.GetUserByEmail(Arg.Any<string>()).Returns(customerList[0]);

            Customers checkCustomer = validation.GetValidUserByEmail(email);

            Assert.That(checkCustomer, Is.EqualTo(customerList[0]));

        }

        [Test]
        public void TransactionsByCustomerTest()
        {

            userService.GetTransactions(Arg.Any<int>()).Returns(transactions);

            List<Transactions> checkTransactions = validation.GetTransactions(customerList[0].CustomerID);

            Assert.That(checkTransactions, Is.EqualTo(transactions));
        }

        [Test]
        public void GetCategoriesTest()
        {

            userService.GetCategories().Returns(categoryList);

            List<Categories> checkCategories = validation.GetCategories();

            Assert.That(checkCategories, Is.EqualTo(categoryList));
        }

        [Test]
        public void GetVendorTest()
        {

            adminService.GetAllVendors().Returns(vendorList);

            List<Vendors> checkVendorList = validation.GetAllVendors();

            Assert.That(checkVendorList, Is.EqualTo(vendorList));
        }

        [Test]
        public void UpdateUserTest()
        {
            userService.UpdateUser(Arg.Any<Customers>(), Arg.Any<int>()).Returns(1);

            int result = validation.UpdateValidUser(customerList[0], 1234);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void UserVendorsTest()
        {
            userService.GetVendors(Arg.Any<int>()).Returns(vendorList);

            List<Vendors> chkVendorList = validation.GetValidVendors(123);

            Assert.That(chkVendorList, Is.EqualTo(vendorList));
        }

        [Test]
        public void AddCustomerTest()
        {
            userService.AddCustomer(Arg.Any<Customers>()).Returns(true);

            bool result = validation.AddCustomer(customerList[0]);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void AddBankTest()
        {
            userService.AddBankDetails(Arg.Any<AccountDetails>(), Arg.Any<int>()).Returns(true);

            bool result = validation.AddValidBankDetails(accountDetails, 123);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void GetBankTest()
        {
            userService.GetBankDetails(Arg.Any<int>()).Returns(accountDetails);

            AccountDetails checkAccount = validation.GetValidBankDetails(123);

            Assert.AreEqual(accountDetails, checkAccount);
        }

        [Test]
        public void GetVendorAmountTest()
        {
            userService.GetVendorAmountDue(Arg.Any<string>(), Arg.Any<int>()).Returns(999);

            int result = validation.GetValidVendorAmountDue("123abc", 123);

            Assert.AreEqual(999, result);
        }

        [Test]
        public void AddTransactionTest()
        {
            userService.AddTransaction(Arg.Any<Transactions>()).Returns(true);

            bool result = validation.AddValidTransaction(transactions[0]);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void UpdateAmountTest()
        {
            userService.UpdateAmountDue(Arg.Any<string>()).Returns(true);

            bool result = validation.UpdateAmountDue("abc123");

            Assert.AreEqual(true, result);
        }

        [Test]
        public void GetVendorByIdTest()
        {
            userService.GetVendorByID(Arg.Any<int>()).Returns(vendorList[0]);

            Vendors chkvendor = validation.GetValidVendorByID(123);

            Assert.AreEqual(vendorList[0], chkvendor);
        }

        [Test]
        public void GetCategoryByIdTest()
        {
            userService.GetCategoryByID(Arg.Any<int>()).Returns(categoryList[0]);

            Categories chkCategory = validation.GetValidCategoryByID(123);

            Assert.AreEqual(categoryList[0], chkCategory);
        }

        [Test]
        public void CustomerPaymentsTest()
        {
            userService.OverallPayments(Arg.Any<int>()).Returns(reportList);

            List<Report> chkReports = validation.OverallPayments(111);

            Assert.AreEqual(reportList, chkReports);
        }

        [Test]
        public void ConsistencyTest()
        {
            userService.UpdateCustomerVendorForConsistency(Arg.Any<string>(), Arg.Any<int>()).Returns(true);

            bool result = validation.UpdateCustomerVendorForConsistency("123abc", 123);

            Assert.AreEqual(true, result);
        }
        #endregion
    }
}
