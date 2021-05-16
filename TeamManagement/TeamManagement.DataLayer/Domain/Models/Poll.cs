using System;
using System.Collections.Generic;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Poll : IIdentificated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CountOfPeopleVoted { get; set; }
        public bool DoesAllowMultiple { get; set; }
        public List<Option> Options { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public string AppUserId { get; set; }
        public AppUser CreatedBy { get; set; }
    }
}
