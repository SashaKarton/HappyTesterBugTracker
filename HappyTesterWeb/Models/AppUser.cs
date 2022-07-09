﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HappyTesterWeb.Models
{
    public class AppUser : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }        
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

    }
}
