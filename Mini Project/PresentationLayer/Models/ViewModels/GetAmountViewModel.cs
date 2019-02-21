using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace PresentationLayer.Models.ViewModels
{
    public class GetAmountViewModel:IValidatableObject
    {
        public string Categories  { get; set; }
        public string Vendors { get; set; }

        public string UniqueID { get; set; }
        public int Amount { get; set; }



        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            UserService userService = new UserService();
            int vendorID = Convert.ToInt32(Vendors);
            
            var unoqVendor = userService.GetVendorAmountDue(UniqueID, vendorID);
            if(unoqVendor == -1)
            {
                yield return new ValidationResult("Enter Valid Details");
            }
            //else if (unoqVendor.CategoryID != Convert.ToInt32(Categories) )
            //{
            //    yield return new ValidationResult("Enter Valid Details");
            //}
        }
    }
}