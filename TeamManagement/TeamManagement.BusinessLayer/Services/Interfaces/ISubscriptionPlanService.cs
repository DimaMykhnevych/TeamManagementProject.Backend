using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface ISubscriptionPlanService
    {
        Task<IEnumerable<SubscriptionPlanGetResponse>> GetSubscriptionPlans();
    }
}
