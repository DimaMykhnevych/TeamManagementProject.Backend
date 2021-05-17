using System;
using System.ComponentModel.DataAnnotations;

namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class SubscriptionGetResponse
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }

        public Guid TransactionId { get; set; }
        public TransactionGetResponse Transaction { get; set; }
        public CompanyGetResponse Company { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlanGetResponse SubscriptionPlan { get; set; }
    }
}
