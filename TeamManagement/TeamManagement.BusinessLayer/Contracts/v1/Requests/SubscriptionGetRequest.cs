using System;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class SubscriptionGetRequest
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }

        public Guid TransactionId { get; set; }
        public TransactionGetRequest Transaction { get; set; }
        public CompanyGetRequest Company { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlanGetRequest SubscriptionPlan { get; set; }
    }
}
