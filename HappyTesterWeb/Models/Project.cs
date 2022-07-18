using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyTesterWeb.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }

    }
}