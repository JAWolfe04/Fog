using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataLibrary.Models
{
    public class SaleModel
    {
        public int SaleID { set; get; }

        [Display(Name = "Sale Percent")]
        [Range(1, 100)]
        [Required(ErrorMessage = "The Sale Percent field is required.")]
        public int? SalePercent { set; get; }

        [Display(Name = "Sale Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The Sale Date field is required.")]
        public DateTime? SaleDate { set; get; }

        [Required]
        [Display(Name = "Sale Game")]
        [StringLength(50, MinimumLength = 3)]
        public string SaleGame { set; get; }
    }
}
