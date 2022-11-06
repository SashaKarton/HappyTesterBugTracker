using HappyTesterWeb.Models;
namespace HappyTesterWeb.ViewModels
{
    public class DetailUserViewModel
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? ProfileImageUrl { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<Project?> Projects { get; set; }
        public ICollection<Ticket?> Tickets  { get; set; }

    }
}
