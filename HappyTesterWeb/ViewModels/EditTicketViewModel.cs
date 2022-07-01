using HappyTesterWeb.Data.Enum;
using HappyTesterWeb.Models;

namespace HappyTesterWeb.ViewModels
{
    public class EditTicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public IssuePriorityEnum IssuePriority { get; set; }
        public IssueStatusEnum TicketStatus { get; set; }
        public IssueTypeEnum IssueType { get; set; }
    }
}
