using HappyTesterWeb.Models;

namespace HappyTesterWeb.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }

    }
}
