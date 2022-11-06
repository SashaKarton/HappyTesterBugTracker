using HappyTesterWeb.Models;

namespace HappyTesterWeb.ViewModels
{
    public class IndexProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }
        public int? TicketCount { get; set; }

    }
}
