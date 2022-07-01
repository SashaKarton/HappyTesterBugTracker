using HappyTesterWeb.Models;
using HappyTesterWeb.Data.Enum;

namespace HappyTesterWeb.ViewModels
{
    public class CreateTicketViewModel
    {       
        public string Title { get; set; }
        public string? Description { get; set; }        
        public int ProjectId { get; set; }
        public IssuePriorityEnum IssuePriority { get; set; } = default;
        public IssueStatusEnum TicketStatus { get; set; } = IssueStatusEnum.New;
        public IssueTypeEnum IssueType { get; set; } = default;
    }
}
