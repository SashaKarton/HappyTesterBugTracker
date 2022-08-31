using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyTesterWeb.ViewModels
{
    public class EditUserProjectViewModel
    {
        public int ProjectsId { get; set; } 
        public IEnumerable<string> AppUsersIds { get; set; }

        

    }
}
