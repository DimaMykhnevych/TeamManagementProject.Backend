using System;
using System.Collections.Generic;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Poll
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CountOfPeopleVoted { get; set; }
        public List<Option> Options { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public string AppUserId { get; set; }
        public AppUser CreatedBy { get; set; }
    }
}
