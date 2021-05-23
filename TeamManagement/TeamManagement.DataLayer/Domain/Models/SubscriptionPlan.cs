using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class SubscriptionPlan : IIdentificated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int ProjectsQuantity { get; set; }
        public int TeamsQuantity { get; set; }
        public string Description { get; set; }
        public List<Subscription> Subscription { get; set; }
    }
}
