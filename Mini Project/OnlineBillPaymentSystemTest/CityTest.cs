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
    public class CityTest
    {
        IValidation valid;
        CityController city;
        List<City> cityList;

       [OneTimeSetUp]
        public void SetUpCity()
        {
            cityList = new List<City>() { new City() {

                CityID = 1,
                CityName = "City1",
            },
            new City() {

                CityID = 2,
                CityName = "City2",
            },
            new City() {

                CityID = 3,
                CityName = "City3",
            }};
            valid = Substitute.For<IValidation>();
            city = new CityController(valid);
        }

        [Test]
        public void IndexTest()
        {
            valid.GetAllValidCities().Returns(cityList);

            var viewResult = city.Index() as ViewResult;

            Assert.That(viewResult.Model, Is.EqualTo(cityList));

        }

        [Test]
        public void CreateTest()
        {
            valid.AddValidCity(cityList[0]).Returns(1);

            RedirectToRouteResult result = city.Create(cityList[0]) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"));
            
        }

        [Test]
        public void EditTest()
        {
            valid.UpdateValidCity(cityList[1],1).Returns(1);

            RedirectToRouteResult result = city.Edit(cityList[1],1) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"));

        }
    }
}
