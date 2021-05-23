using System;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class TeamProjectCreateResponse
    {
        public Guid Id { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? ProjectId { get; set; }

        public Team Team { get; set; }
        public Project Project { get; set; }
    }
}
