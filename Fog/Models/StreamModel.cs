using System.ComponentModel.DataAnnotations;

namespace Fog.Models
{
    public class StreamModel
    {
        public int StreamID { get; set; }

        [StringLength(45, MinimumLength = 3)]
        [Required(ErrorMessage = "Title cannot be empty!")]
        public string Title { get; set; }

        [Display(Name = "Game Played")]
        [StringLength(45, MinimumLength = 3)]
        [Required(ErrorMessage = "Game Title cannot be empty!")]
        public string GameTitle { get; set; }

        [StringLength(45, MinimumLength = 3)]
        [DataType(DataType.Url)]
        [Required(ErrorMessage = "Link cannot be empty!")]
        public string Link { get; set; }
    }
}
