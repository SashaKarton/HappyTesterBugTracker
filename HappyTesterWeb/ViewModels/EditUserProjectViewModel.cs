using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyTesterWeb.ViewModels
{
    public class EditUserProjectViewModel
    {
        public int ProjectsId { get; set; }
        //public string AppUsersId { get; set; }
        public IEnumerable<string> AppUsersIds { get; set; }
        //public IEnumerable<SelectListItem>? UserChoises { get; set; }
        

    }
}
