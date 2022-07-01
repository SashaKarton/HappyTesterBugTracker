using System.ComponentModel.DataAnnotations;

namespace HappyTesterWeb.Models
{
    public class User //: IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Role UserRole { get; set; }
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

    }
}
