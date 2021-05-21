using System;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class AppUserEvent
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Event Event { get; set; }
        public Guid EventId { get; set; }
        public string Status { get; set; }
    }
}
