using System.ComponentModel.DataAnnotations;

namespace HappyTesterWeb.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

        //public ICollection<User> AssignedUsers { get; set; }

    }
}