using System;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class TeamProject : IIdentificated
    {
        public Guid Id { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? ProjectId { get; set; }

        public Team Team { get; set; }
        public Project Project { get; set; }
    }
}
