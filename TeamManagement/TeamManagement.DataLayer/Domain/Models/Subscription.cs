using System;
using System.ComponentModel.DataAnnotations;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Subscription : IIdentificated
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public Company Company { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
