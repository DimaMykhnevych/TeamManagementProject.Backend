using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly IGenericRepository<SubscriptionPlan> _genericRepository;
        private readonly IMapper _mapper;
        public SubscriptionPlanService(IGenericRepository<SubscriptionPlan> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubscriptionPlanGetResponse>> GetSubscriptionPlans()
        {
            IEnumerable<SubscriptionPlan> subscriptionPlans = await _genericRepository.GetAsync();
            return _mapper.Map<IEnumerable<SubscriptionPlanGetResponse>>(subscriptionPlans);
        }
    }
}
