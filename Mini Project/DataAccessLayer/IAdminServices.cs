using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IAdminServices
    {
        int AddVendor(Vendors vendor);

        int UpdateVendor(Vendors vendor, int vendorId);

        List<Vendors> GetAllVendors();

        Vendors GetVendorDet(int vendorId);

        int AddCategory(Categories category);

        int UpdateCategory(Categories category, int categoryId);

        List<Categories> GetAllCategories();

        int AddCity(City city);

        int UpdateCity(City city, int cityId);

        List<City> GetAllCities();

        List<Report> VendorWisePayment(DateTime? Start = null, DateTime? End = null);

        List<Report> CityWisePayment(DateTime? Start = null, DateTime? End = null);

        List<Report> CategoryWisePayment(DateTime? Start = null, DateTime? End = null);

        List<Report> OverallPayments(DateTime? Start = null, DateTime? End = null);

        List<Customers> getAllUsersByList();

        List<Transactions> getPaymentDetailsByList();
    }
}
