using System;
using System.Collections.Generic;
using System.Text;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class SubscriptionCreateRequest
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Guid TransactionId { get; set; }
        public TransactionGetRequest Transaction { get; set; }
        public CompanyGetRequest Company { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlanGetRequest SubscriptionPlan { get; set; }
    }
}
