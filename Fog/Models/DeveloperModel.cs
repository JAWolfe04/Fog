using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.Models
{
    public class DeveloperModel
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Username cannot be empty!")]
        [Display(Name = "username")]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [StringLength(45)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password cannot be empty!")]
        public string Password { get; set; }


        [StringLength(45)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Company Email")]
        [Required(ErrorMessage = "Email cannot be empty!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company Name cannot be empty!")]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "The description cannot be empty!")]
        [StringLength(1000, MinimumLength = 10)]
        [Display(Name = "About")]
        public string About { get; set; }

        //[StringLength(45)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number cannot be empty!")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [DataType(DataType.Url)]
        [Required(ErrorMessage = "Web Link cannot be empty!")]
        [Display(Name = "Web Link")]
        public string WebLink { get; set; }

        [Display(Name = "Bank")]
        [Required(ErrorMessage = "Bank cannot be empty!")]
        public int BankRoutingNumber { get; set; }

        [Display(Name = "Account")]
        [Required(ErrorMessage = "Account cannot be empty!")]
        public int BankAccountNumber { get; set; }
    }
}
