using System;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class ProjectCreateRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectDescription { get; set; }
        public Guid CompanyId { get; set; }
    }
}
