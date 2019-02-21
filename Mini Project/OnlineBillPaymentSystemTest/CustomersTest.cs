using System;
using PresentationLayer.Areas.Admin.Controllers;
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
    public class CustomersTest
    {
        IValidation validate;
        CustomersController customerController;
        List<Customers> customerList;

        [OneTimeSetUp]
        public void SetUp()
        {
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
            validate = Substitute.For<IValidation>();
            customerController = new CustomersController(validate);
        }
        [Test]
        public void GetAllValidUsersTest()
        {
            validate.getAllValidUsersByList().Returns(customerList);

            var viewResult = customerController.GetAllUsersByList() as ViewResult;

            Assert.That(viewResult.Model, Is.EqualTo(customerList));
        }
    }
}
