using System;
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
    public class CategoryTest
    {
        IValidation valid;
        CategoryController categoryController;
        List<Categories> categories;

        [OneTimeSetUp]
        public void SetUp()
        {
            categories = new List<Categories>
            {
                new Categories()
                {
                    CategoryID = 1,
                    CategoryName = "mobile",
                    Description = "mobile bills"
                },
                new Categories()
                {
                    CategoryID = 2,
                    CategoryName = "gas",
                    Description = "gas bills"
                },
                new Categories()
                {
                    CategoryID = 3,
                    CategoryName = "landline",
                    Description = "landline bills"
                },
            };
            valid = Substitute.For<IValidation>();
            categoryController = new CategoryController(valid);
        }

        [Test]
        public void IndexTest()
        {
            valid.GetAllValidCategories().Returns(categories);

            var viewResult = categoryController.Index() as ViewResult;

            Assert.That(viewResult.Model, Is.EqualTo(categories) );
        }

        [Test]
        public void CreateTest()
        {
            valid.AddValidCategory(categories[0]).Returns(1);

            var result = categoryController.Create(categories[0]) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"));
        }


        [Test]
        public void EditTest()
        {
            valid.UpdateValidCategory(categories[1],1).Returns(1);

            var result = categoryController.Edit(categories[1],1) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"));
        }
    }
}
