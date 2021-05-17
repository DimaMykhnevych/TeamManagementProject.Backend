using Stripe;
using System;
using System.Collections.Generic;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class PaymentService : IPaymentService
    {
        public Charge ProceedPaymentRequest(StripePaymentRequest paymentRequest, string secretKey)
        {
            StripeConfiguration.ApiKey = secretKey;

            var myCharge = new ChargeCreateOptions();
            myCharge.Source = paymentRequest.TokenId;
            myCharge.Amount = (long)paymentRequest.Subscription.SubscriptionPlan.Price;
            myCharge.Currency = "usd";
            myCharge.Description = paymentRequest.Subscription.SubscriptionPlan.Name;
            myCharge.Metadata = new Dictionary<string, string>();
            myCharge.Metadata["OurRef"] = "OurRef-" + Guid.NewGuid().ToString();

            var chargeService = new ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);
            return stripeCharge;
        }
    }
}
