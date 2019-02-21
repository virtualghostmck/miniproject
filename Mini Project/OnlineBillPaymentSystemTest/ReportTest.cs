using System;
using PresentationLayer.Areas.Admin.Controllers;
using PresentationLayer.Areas.Admin.Models;
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
    public class ReportTest
    {
        IValidation validation;
        ReportController reportController;
        List<Report> reportList;
        ReportFilter filter;
        ReportFilter crossCheckFilter;

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

            filter = new ReportFilter
            {
                FilterType = type.None,
                Reports = reportList
            };
            crossCheckFilter = new ReportFilter
            {
                StartDate = new DateTime(2018, 5, 8),
                EndDate = DateTime.Now,
                FilterType = type.Category,
                Reports = reportList
            };
            validation = Substitute.For<IValidation>();
            reportController = new ReportController(validation);
            filter.StartDate = new DateTime(2018, 5, 8);
            filter.EndDate = DateTime.Now;

        }
        /// <summary>
        /// Check this test case
        /// </summary>
        [Test]
        public void IndexGetTest()
        {
            validation.OverallValidPayments(null, null).Returns(reportList);

            var viewResult = reportController.Index() as ViewResult;
           
            Assert.That((viewResult.Model as ReportFilter).Reports, Is.EqualTo(filter.Reports));
            Assert.That((viewResult.Model as ReportFilter).FilterType, Is.EqualTo(filter.FilterType));
        }

        [Test]
        public void IndexTypeNoneDateNone()
        {
            validation.OverallValidPayments(null, null).Returns(reportList);

            var viewResult = reportController.Index(filter) as ViewResult;

            Assert.That((viewResult.Model as ReportFilter).Reports, Is.EqualTo(filter.Reports));
            Assert.That((viewResult.Model as ReportFilter).FilterType, Is.EqualTo(filter.FilterType));
        }

        [Test]
        public void IndexTypeCategory()
        {
            validation.ValidCategoryWisePayment(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reportList);
           
            filter.FilterType = type.Category;
            crossCheckFilter.FilterType = type.Category;

            var viewResult = reportController.Index(filter) as ViewResult;


            Assert.That((viewResult.Model as ReportFilter).Reports, Is.EqualTo(crossCheckFilter.Reports));
            Assert.That((viewResult.Model as ReportFilter).FilterType, Is.EqualTo(crossCheckFilter.FilterType));
        }
        [Test]
        public void IndexTypeCity()
        {
            validation.ValidCityWisePayment(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reportList);

            filter.FilterType = type.City;
            crossCheckFilter.FilterType = type.City;

            var viewResult = reportController.Index(filter) as ViewResult;
            
            Assert.That((viewResult.Model as ReportFilter).Reports, Is.EqualTo(crossCheckFilter.Reports));
            Assert.That((viewResult.Model as ReportFilter).FilterType, Is.EqualTo(crossCheckFilter.FilterType));
        }
        [Test]
        public void IndexTypeVendor()
        {
            validation.ValidVendorWisePayment(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reportList);

            filter.FilterType = type.Vendor;
            crossCheckFilter.FilterType = type.Vendor;

            var viewResult = reportController.Index(filter) as ViewResult;
            
            Assert.That((viewResult.Model as ReportFilter).Reports, Is.EqualTo(crossCheckFilter.Reports));
            Assert.That((viewResult.Model as ReportFilter).FilterType, Is.EqualTo(crossCheckFilter.FilterType));
        }
    }
}
