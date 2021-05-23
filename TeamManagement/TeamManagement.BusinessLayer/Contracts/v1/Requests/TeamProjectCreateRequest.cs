using System;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class TeamProjectCreateRequest
    {
        public Guid? TeamId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
