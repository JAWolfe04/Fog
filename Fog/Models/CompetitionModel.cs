using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fog.Models
{
    public class CompetitionModel
    {
        [Required(ErrorMessage = "Title cannot be empty!")]
        [StringLength(50, MinimumLength = 3)]
        public string Title { set; get; }

        [Required(ErrorMessage = "Date cannot be empty!")]
        [DataType(DataType.Date)]
        public DateTime? Date { set; get; }

        [Display(Name ="Game Title")]
        [Required(ErrorMessage = "Game Title cannot be empty!")]
        [StringLength(50, MinimumLength = 3)]
        public string GameTitle { set; get; }

        [Required(ErrorMessage = "Description cannot be empty!")]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { set; get; }

        public int GameID { set; get; }

        public int CompID { get; set; }

        public bool IsEntered { get; set; }

        public string strDate { get; set; }

        public List<DataLibrary.Models.PlayerModel> EnteredPlayers { get; set; }
    }
}
