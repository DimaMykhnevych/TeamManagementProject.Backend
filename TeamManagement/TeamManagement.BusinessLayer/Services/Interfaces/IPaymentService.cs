using Stripe;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IPaymentService
    {
        Charge ProceedPaymentRequest(StripePaymentRequest stripePaymentRequest, string secretKey);
    }
}
