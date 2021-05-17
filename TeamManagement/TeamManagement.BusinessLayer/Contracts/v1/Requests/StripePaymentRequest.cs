namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class StripePaymentRequest
    {
        public string TokenId { get; set; }
        public SubscriptionUpdateRequest Subscription {get; set;}
    }
}
