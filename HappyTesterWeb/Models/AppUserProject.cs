using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Models
{    
    public partial class AppUserProject
    {
        
        public string AppUsersId { get; set; }
        
        public int ProjectsId { get; set; }
    }
}
