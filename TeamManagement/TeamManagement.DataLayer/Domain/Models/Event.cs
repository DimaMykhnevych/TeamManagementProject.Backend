using System;
using System.Collections.Generic;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Event : IIdentificated
    {
        public Event()
        {
            AppUserEvents = new List<AppUserEvent>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Passcode { get; set; }
        public AppUser CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<AppUserEvent> AppUserEvents { get; set; }
    }
}
