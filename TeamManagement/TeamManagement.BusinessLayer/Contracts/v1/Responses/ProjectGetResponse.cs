using System;
using System.Collections.Generic;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class ProjectGetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectDescription { get; set; }
        public List<TeamProject> TeamProjects { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
