using System;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Active { get; set; }
        public string Resolved { get; set; }
        public string CodeReview { get; set; }
        [Required]
        public DateTime DateOfPublishsing { get; set; }

        public string PublisherId { get; set; }
        public AppUser Publisher { get; set; }
    }
}
