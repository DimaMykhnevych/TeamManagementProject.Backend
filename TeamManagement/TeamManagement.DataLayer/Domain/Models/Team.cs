using System;
using System.Collections.Generic;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Team : IIdentificated
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; } 

        public List<AppUser> Members { get; set; }

        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }
        public List<TeamProject> TeamProjects { get; set; }
        public List<Poll> Polls { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
