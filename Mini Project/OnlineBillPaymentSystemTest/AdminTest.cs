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
using PresentationLayer.Models.ViewModels;

namespace OnlineBillPaymentSystemTest
{
    public class AdminTest
    {
        AdminHimselfController adminController;
        AdminViewModel adminViewModel;

        [OneTimeSetUp]
        public void SetUp()
        {
            adminViewModel = new AdminViewModel
            {
                Email = "test@test.com",
                Name = "test"
            };
            adminController = new AdminHimselfController(adminViewModel);
        }

        [Test]
        public void DetailsTest()
        {
            var viewResult = adminController.Details() as ViewResult;

            Assert.That(viewResult.Model, Is.EqualTo(adminViewModel));
        }
    }
}
