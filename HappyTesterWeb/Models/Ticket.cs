using HappyTesterWeb.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyTesterWeb.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public IssuePriorityEnum IssuePriority { get; set; }
        public IssueStatusEnum TicketStatus { get; set; } = IssueStatusEnum.New;
        public IssueTypeEnum IssueType { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        //public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        //public User? DevAssigned { get; set; }
        //public User Submitter { get; set; }

        //I need to make comments for tickets
        //public ICollection<Comment>? TicketComments { get; set; }

        //I want to change create time for update time
        //public DateTime LastUpdatedDateTime { get; set; } 

    }
}