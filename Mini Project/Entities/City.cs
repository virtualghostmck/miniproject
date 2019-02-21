using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class City
    {
        [Key]
        public int CityID { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only aplhabets in City Name field")]
        public string CityName { get; set; }
        public virtual  ICollection<Vendors> Vendors { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public City()
        {
            this.Vendors = new HashSet<Vendors>();
            this.Customers = new HashSet<Customers>();
        }
    }
}
