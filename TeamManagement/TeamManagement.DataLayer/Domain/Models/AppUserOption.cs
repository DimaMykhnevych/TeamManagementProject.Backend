using System;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class AppUserOption
    {
        public Guid OptionId { get; set; }
        public string AppUserId { get; set; }
        public Option Option { get; set; }
        public AppUser AppUser { get; set; }
    }
}
