using System;
using System.ComponentModel.DataAnnotations;

namespace Fog.Models
{
    public class PurchaseModel
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 numbers")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card number must be 16 numbers")]
        [Display(Name ="Card Number")]
        public string CardNumber { get; set; }
        [Required]
        [Display(Name = "Name on Card")]
        public string CardName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Card expiration must be in the format of MM/YY")]
        [RegularExpression(@"^(1[0-2]|0[1-9]|[1-9])\/(0(?!0)\d|[0-9]\d)$", 
            ErrorMessage = "Card expiration must be in the format of MM/YY")]
        [Display(Name = "Card Expiration Date")]
        public string CardExp { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage ="Security number must be 3 numbers")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Security number must be 3 numbers")]
        [Display(Name = "Card Security Number")]
        public string CardSecurity { get; set; }
        public string GameTitle { get; set; }
        public string Username { get; set; }
        public int GameID { get; set; }
    }
}
