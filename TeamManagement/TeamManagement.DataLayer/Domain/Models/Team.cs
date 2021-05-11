using System;
using System.Collections.Generic;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; } 

        public List<AppUser> Members { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public List<TeamProject> TeamProjects { get; set; }
        public List<Poll> Polls { get; set; }
    }
}
