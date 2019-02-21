using System;
using PresentationLayer.Controllers;
using BusinessLogicLayer;
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
    public class HomeControllerTests
    {
        IValidation valid;
        HomeController homeController;
        List<Categories> categoryList;
        Customers customer;
        List<City> cityList;
        List<Report> reportList;
        List<Vendors> vendorList;
        AccountDetails accountDetails;
        List<Transactions> transactions;
        Customers customer2;
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
            customer = new Customers
            {
                CityID = 1,
                ContactInfo = { ContactNo = "8877778899", Email = "abc@fd.com" },
                CustomerAddress = "TestAddress",
                CustomerID = 1,
                CustomerName = "Test",
                Gender = "female",
                Password = "Test@123"
            };
            customer2 = new Customers
            {
                CityID = 2,
                ContactInfo = { ContactNo = "999999999", Email = "test@xyz.com" },
                CustomerAddress = "AddressTest",
                CustomerID = 2,
                CustomerName = "TestName",
                Gender = "male",
                Password = "Test@123"
            };
            valid = Substitute.For<IValidation>();
            homeController = new HomeController(valid,customer);
        }


        [Test]
        public void IndexTest()
        {
            valid.ValidateGetUserByEmail(Arg.Any<string>()).Returns(customer2);

            var viewResult = homeController.Index() as RedirectToRouteResult;

            Assert.That(homeController.Session["existingCustomer"] as Customers, Is.EqualTo(customer2));
        }

    }
}
