using System;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class SubscriptionUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionUpdateRequest Transaction { get; set; }
        public CompanyGetRequest Company { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlanGetRequest SubscriptionPlan { get; set; }
    }
}
