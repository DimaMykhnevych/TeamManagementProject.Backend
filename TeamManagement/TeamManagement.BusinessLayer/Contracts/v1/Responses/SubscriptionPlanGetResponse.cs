using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class SubscriptionPlanGetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int ProjectsQuantity { get; set; }
        public int TeamsQuantity { get; set; }
        public string Description { get; set; }
        public SubscriptionGetResponse Subscription { get; set; }
    }
}
