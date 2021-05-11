using System;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class HowToArticle : IIdentificated
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
        public DateTime DateOfPublishing { get; set; }

        public string PublisherId { get; set; }
        public AppUser Publisher { get; set; }
    }
}
