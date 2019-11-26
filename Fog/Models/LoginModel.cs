using System;
using System.ComponentModel.DataAnnotations;

namespace Fog.Models
{
    public class LoginModel
    {
        [StringLength(20)]
        [Required(ErrorMessage = "Username cannot be empty!")]
        public String Username { get; set; }

        [StringLength(45)]
        [Required(ErrorMessage = "Password cannot be empty!")]
        public String Password { get; set; }
    }
}
