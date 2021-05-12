using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class CompanyCreateResponse
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
    }
}
