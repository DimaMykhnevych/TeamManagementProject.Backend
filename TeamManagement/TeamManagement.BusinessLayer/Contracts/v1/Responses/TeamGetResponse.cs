using System;
using System.Collections.Generic;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class TeamGetResponse
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
