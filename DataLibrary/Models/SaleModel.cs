using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.Models
{
    public class SaleModel
    {


        [System.ComponentModel.DisplayName("Sale Percent"), Required(ErrorMessage = "Sale Percent cannot be empty!")]
        public int SalePercent { set; get; }

        [System.ComponentModel.DisplayName("Sale Date"), Required(ErrorMessage = "Sale Date cannot be empty!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string SaleDate { set; get; }

        [System.ComponentModel.DisplayName("Sale Game"), Required(ErrorMessage = "Sale Game cannot be empty!")]
        [StringLength(50, MinimumLength = 3)]
        public string SaleGame { set; get; }

    }
}
