using System;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class TransactionUpdateRequest
    {
        public Guid Id { get; set; }
        public string PublicKey { get; set; }
        public DateTime TransactionDate { get; set; }

        public SubscriptionGetRequest Subscription { get; set; }
    }
}
