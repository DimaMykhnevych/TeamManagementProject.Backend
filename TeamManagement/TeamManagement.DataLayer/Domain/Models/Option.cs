using System;
using System.Collections.Generic;
using System.Text;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Option
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

        public Guid PollId { get; set; }
        public Poll Poll { get; set; }
    }
}
