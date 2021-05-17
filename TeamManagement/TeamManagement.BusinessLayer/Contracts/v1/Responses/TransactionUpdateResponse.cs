using System;

namespace TeamManagement.BusinessLayer.Contracts.v1.Responses
{
    public class TransactionUpdateResponse
    {
        public Guid Id { get; set; }
        public string PublicKey { get; set; }
        public DateTime TransactionDate { get; set; }

        public SubscriptionGetResponse Subscription { get; set; }
    }
}
