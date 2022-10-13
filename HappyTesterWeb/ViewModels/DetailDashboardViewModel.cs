using HappyTesterWeb.Models;

namespace HappyTesterWeb.ViewModels
{
    public class DetailDashboardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }
    }
}
