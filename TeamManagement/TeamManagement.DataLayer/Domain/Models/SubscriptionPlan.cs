using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class SubscriptionPlan
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int ProjectsQuantity { get; set; }
        public int TeamsQuantity { get; set; }
        public string Description { get; set; }

        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
