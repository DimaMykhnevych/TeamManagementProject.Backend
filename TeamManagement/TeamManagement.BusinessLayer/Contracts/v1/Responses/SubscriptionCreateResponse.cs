using System;


namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class SubscriptionCreateResponse
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Guid TransactionId { get; set; }
        public TransactionGetResponse Transaction { get; set; }
        public CompanyGetResponse Company { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlanGetResponse SubscriptionPlan { get; set; }
    }
}
