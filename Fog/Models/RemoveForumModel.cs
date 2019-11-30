using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fog.Models
{
    public class RemoveForumModel
    {
        public int GameID { get; set; }
        public int position { get; set; }
        public List<DataLibrary.Models.ForumModel> forums { get; set; }
        public List<SelectListItem> forumList { get; set; }
    }
}
