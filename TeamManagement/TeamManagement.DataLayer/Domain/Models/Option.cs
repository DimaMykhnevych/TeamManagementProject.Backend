using System;
using System.Collections.Generic;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Option
    {
        public Guid Id { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }

        public Guid PollId { get; set; }
        public Poll Poll { get; set; }

        public ICollection<AppUserOption> AppUserOptions { get; set; }
    }
}
