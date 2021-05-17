using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.Options;

namespace TeamManagement.Controllers
{
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IPaymentService _paymentService;
        private readonly StripeKeys _stripeKeys;

        public SubscriptionController(ISubscriptionService service, 
            IPaymentService paymentService, IOptions<StripeKeys> keys)
        {
            _subscriptionService = service;
            _paymentService = paymentService;
            _stripeKeys = keys.Value ?? throw new ArgumentException(nameof(keys));
        }

        [HttpPut(ApiRoutes.Subscription.Update)]
        public async Task<IActionResult> UpdateSubscription([FromBody] StripePaymentRequest paymentRequest)
        {
            bool subscriptionCreateResponse = 
                await _subscriptionService.UpdateSubscription(paymentRequest.Subscription);

            Charge charge = _paymentService.ProceedPaymentRequest(paymentRequest, _stripeKeys.SecretKey);

            if (subscriptionCreateResponse)
            {
                return Ok(charge);
            }
            return BadRequest();
        }
    }
}
