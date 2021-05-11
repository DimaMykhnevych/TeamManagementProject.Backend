using System;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public Company Company { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
