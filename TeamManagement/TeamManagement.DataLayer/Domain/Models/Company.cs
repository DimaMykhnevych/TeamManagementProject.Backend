using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }

        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public string CeoId { get; set; }
        public AppUser CEO { get; set; }
        public List<Team> Teams { get; set; }
        public List<Project> Projects { get; set; }
        public List<AppUser> Employees { get; set; }
    }

}

