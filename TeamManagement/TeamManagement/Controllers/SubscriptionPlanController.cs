using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;

        public SubscriptionPlanController(ISubscriptionPlanService subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }
        [HttpGet(ApiRoutes.SubscriptionPlan.BaseWithVersion)]
        public async Task<IActionResult> getSubscriptionPlans()
        {
            IEnumerable<SubscriptionPlanGetResponse> subscriptionPlans = 
                await _subscriptionPlanService.GetSubscriptionPlans();
            return Ok(subscriptionPlans);
        }
    }
}
