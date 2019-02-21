using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Areas.Admin.Models
{
    public enum type { None,Category, Vendor,City }
    public class ReportFilter:IValidatableObject
    {
        public type? FilterType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Report> Reports { get; set; }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("EndDate must be greater than StartDate");
            }
        }
    }
}