using HappyTesterWeb.Data.Enum;


namespace HappyTesterWeb.ViewModels
{
    public class CreateProjectViewModel
    { 
        public string Title { get; set; }
        public string? Description { get; set; }             
        public string TicketTitle { get; set; }
        public string? TicketDescription { get; set; }
        public IssuePriorityEnum IssuePriority { get; set; } = default;
        public IssueStatusEnum TicketStatus { get; set; } = IssueStatusEnum.New;
        public IssueTypeEnum IssueType { get; set; } = default;
    }
}


