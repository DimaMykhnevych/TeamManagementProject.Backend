using System;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Article : IIdentificated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime DateOfPublishing { get; set; }

        public AppUser Publisher { get; set; }
        public string PublisherId { get; set; }
        public Tag Tag { get; set; }
        public Guid TagId { get; set; }
    }
}
