using System;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class TeamProject
    {
        public Guid Id { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? ProjectId { get; set; }

        public Team Team { get; set; }
        public Project Project { get; set; }
    }
}
