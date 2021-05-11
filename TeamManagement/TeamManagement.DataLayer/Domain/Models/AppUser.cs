using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Articles = new List<Article>();
            HowToArticles = new List<HowToArticle>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Article> Articles { get; private set; }
        public ICollection<HowToArticle> HowToArticles { get; private set; }
        public Company Company { get; set; }
        public ICollection<Report> Reports { get; set; }
        public Guid? TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Poll> Polls { get; set; }

    }
}
