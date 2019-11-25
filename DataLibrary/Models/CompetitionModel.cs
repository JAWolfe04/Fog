using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.Models
{
    public class CompetitionModel
    {
        [Required(ErrorMessage = "Title cannot be empty!")]
        [StringLength(50, MinimumLength = 3)]
        public string Title { set; get; }

        [Required(ErrorMessage = "Date cannot be empty!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string Date { set; get; }

        [Required(ErrorMessage = "Game title cannot be empty!")]
        [StringLength(50, MinimumLength = 3)]
        public string Game { set; get; }

        [Required(ErrorMessage = "Description cannot be empty!")]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { set; get; }

        public int GameID { set; get; }
    }
}
