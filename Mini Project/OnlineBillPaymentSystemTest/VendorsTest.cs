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
    public class VendorsTest
    {
        IValidation validation;
        VendorController vendorController;
        List<Vendors> vendorList;

        [OneTimeSetUp]
        public void SetUp()
        {
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
            validation = Substitute.For<IValidation>();
            vendorController = new VendorController(validation);
        }

        [Test]
        public void IndexTest()
        {
            validation.GetAllValidVendors().Returns(vendorList);

            var viewResult = vendorController.Index() as ViewResult;

            Assert.That(viewResult.Model, Is.EqualTo(vendorList));
        }

        [Test]
        public void CreateTest()
        {
            validation.AddValidVendor(vendorList[0]).Returns(1);

            var result = vendorController.Create(vendorList[0]) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"));
        }

        [Test]
        public void EditTest()
        {
            validation.UpdateValidVendor(vendorList[1],1).Returns(1);

            var result = vendorController.Edit(vendorList[1],1) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"));
        }
    }
}
