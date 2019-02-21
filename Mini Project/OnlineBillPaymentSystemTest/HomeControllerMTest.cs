using System;
using PresentationLayer.Controllers;
using PresentationLayer.Models.ViewModels;
using BusinessLogicLayer;
using NSubstitute;
using Entities;
using System.Web.Mvc;
using NSubstituteAutoMocker;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web;
using MvcContrib.TestHelper;

namespace OnlineBillPaymentSystemTest
{
    [TestFixture]
    public class HomeControllerMTest
    {
        IValidation valid;
        List<Categories> categoryList;
        List<Customers> customerList;
        List<City> cityList;
        List<Report> reportList;
        List<Vendors> vendorList;
        AccountDetails accountDetails;
        List<Transactions> transactions;
        HomeController homeController;
        GetAmountViewModel getAmountViewModel;

        [OneTimeSetUp]
        public void SetUp()
        {

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

            accountDetails = new AccountDetails
            {
                BankAcNo = 652173567123,
                BankName = "test",
                CustomerID = 1,
                BranchName = "test",
                IFSC = 652173567123,
                MICRCode = 652173567123
            };

            getAmountViewModel = new GetAmountViewModel
            {
                Amount = 500,
                UniqueID = "12345678",
                Categories = "1",
                Vendors = "1"
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

            transactions = new List<Transactions>()
            {
                new Transactions { TransactionID="t1", UniqueID="u1", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t2", UniqueID="u2", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t3", UniqueID="u3", Amount=123, TransactionDate=DateTime.Now},
                new Transactions { TransactionID="t4", UniqueID="u4", Amount=123, TransactionDate=DateTime.Now},
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
            
            TestControllerBuilder builder = new TestControllerBuilder();

            valid = Substitute.For<IValidation>();
            homeController = new HomeController(valid);
            builder.InitializeController(homeController);
            homeController.setSessionForUnitTesting(customerList[0]);
           
        }

        [Test]
        public void GetPaymentDetailsTest()
        {
            valid.GetTransactions(1).Returns(transactions);

            ViewResult viewResult = homeController.GetPaymentDetails() as ViewResult;

            Assert.That(viewResult.Model, Is.EqualTo(transactions));
        }

        [Test]
        public void GetAmountTest()
        {
            valid.GetCategories().Returns(categoryList);
            valid.GetAllVendors().Returns(vendorList);

            var result = homeController.GetAmount() as ViewResult;

            Assert.IsNotNull(result);

        }

        [Test]
        public void ShowAmountTest()
        {
            valid.GetValidVendorAmountDue("12345678", 1).Returns(500);
            valid.GetValidVendorByID(1).Returns(vendorList[0]);
            valid.GetValidCategoryByID(1).Returns(categoryList[0]);

            var result = homeController.ShowAmount(getAmountViewModel) as ViewResult;

            Assert.That(result.Model, Is.EqualTo(getAmountViewModel));

        }

        [Test]
        public void MakePaymentTest()
        {
            valid.GetBankDetails(1).Returns(accountDetails);

            var result = homeController.MakePayment() as ViewResult;

            Assert.That(result.Model, Is.EqualTo(accountDetails));
            
        }

        [Test]
        public void PaymentDetailsTest()
        {
            valid.AddTransaction(transactions[0]).Returns(true);
            valid.UpdateAmountDue("12345678");
            valid.UpdateCustomerVendorForConsistency("12345678", 1);

            var result = homeController.PaymentDetails() as ViewResult;

            //check the assert conditions
            Assert.That(result.Model.GetType().Name, Is.EqualTo("Transactions"));
        }

        [Test]
        public void PaymentHistoryTest()
        {
            valid.OverallPayments(1).Returns(reportList);

            var result = homeController.PaymentHistory() as ViewResult;

            Assert.That(result.Model, Is.EqualTo(reportList));
        }

        //not working yet, find solution for usermanager
        [Test]
        public void IndexTest()
        {
            valid.ValidateGetUserByEmail(Arg.Any<string>()).Returns(customerList[0]);

            RedirectToRouteResult result = homeController.Index() as RedirectToRouteResult;

            Assert.AreEqual(customerList[0], homeController.Session["existingCustomer"] as Customers);
            Assert.AreEqual(true, result.RouteValues.ContainsValue("Details"));
        }

        [Test]
        public void DetailsTest()
        {
            ViewResult result = homeController.Details() as ViewResult;

            Assert.AreEqual(customerList[0], result.Model);
        }

        [Test]
        public void EditGetTest()
        {
            ViewResult result = homeController.Edit() as ViewResult;

            Assert.AreEqual(customerList[0], result.Model);
        }

        [Test]
        public void EditPostTest()
        {
            valid.UpdateValidUser(Arg.Any<Customers>(), Arg.Any<int>()).Returns(1);

            RedirectToRouteResult result = homeController.Edit(customerList[0]) as RedirectToRouteResult;

            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Test]
        public void GetVendorsTest()
        {
            valid.GetVendors(Arg.Any<int>()).Returns(vendorList);

            ViewResult result = homeController.GetAllVendorDetails() as ViewResult;

            Assert.AreEqual(vendorList, result.Model);
        }

        [Test]
        public void AddBankGetTest()
        {
            ActionResult result = homeController.AddBankDetails() as ActionResult;

            Assert.NotNull(result);

        }

        [Test]
        public void AddBankPostTest()
        {
            valid.AddBankDetails(Arg.Any<AccountDetails>(), Arg.Any<int>()).Returns(true);

            RedirectToRouteResult result = homeController.AddBankDetails(accountDetails) as RedirectToRouteResult;

            Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
        }

        [Test]
        public void GetBankTest()
        {
            valid.GetBankDetails(Arg.Any<int>()).Returns(accountDetails);

            ViewResult result = homeController.GetBankDetails() as ViewResult;

            Assert.AreEqual(accountDetails, result.Model);
        }

        
    }
}
