using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Articles = new List<Article>();
            HowToArticles = new List<HowToArticle>();
            DateOfBirth = new DateTime();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Article> Articles { get; private set; }
        public ICollection<HowToArticle> HowToArticles { get; private set; }
        public Company Company { get; set; }
        public Company EmployeesCompany { get; set; }

        [ForeignKey("EmployeesCompany")]
        public Guid? CompanyId { get; set; }
        public ICollection<Report> Reports { get; set; }
        public Guid? TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Poll> Polls { get; set; }
        public ICollection<AppUserOption> AppUserOptions { get; set; }
        public ICollection<AppUserEvent> AppUserEvents { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
