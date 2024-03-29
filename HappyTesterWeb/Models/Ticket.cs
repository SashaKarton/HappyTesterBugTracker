﻿using HappyTesterWeb.Data.Enum;
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
        public ICollection<AppUser>? AppUsers { get; set; }
    }
}
