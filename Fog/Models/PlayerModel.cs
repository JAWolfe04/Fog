using System.ComponentModel.DataAnnotations;

namespace Fog.Models
{
    public class PlayerModel
    {
        [StringLength(20)]
        [Required(ErrorMessage = "Username cannot be empty!")]
        public string Username { get; set; }

        [StringLength(45)]
        [Display(Name = "Display Name")]
        [Required(ErrorMessage = "Display Name cannot be empty!")]
        public string DisplayName { get; set; }
        public int Permission { get; set; }
        public int LoginAttCount { get; set; }

        [StringLength(45)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password cannot be empty!")]
        public string Password { get; set; }

        [StringLength(45)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email cannot be empty!")]
        public string Email { get; set; }

        public int StreamID { get; set; }
    }
}
