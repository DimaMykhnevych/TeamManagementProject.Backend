using System;
using System.Collections.Generic;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Tag : IIdentificated
    {
        public Tag()
        {
            Articles = new List<Article>();
        }

        public Guid Id { get; set; }
        public string Label { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
