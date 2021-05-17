using AutoMapper;
using System;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IGenericRepository<Subscription> _subscriptionRepository;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public SubscriptionService(IGenericRepository<Subscription> subscriptionRepository, 
            IMapper mapper, ITransactionService transactionService)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        public async Task<SubscriptionCreateResponse> AddSubscription(SubscriptionCreateRequest subscription)
        {
            Subscription subscriptionToAdd = _mapper.Map<Subscription>(subscription);
            await _subscriptionRepository.CreateAsync(subscriptionToAdd);
            return _mapper.Map<SubscriptionCreateResponse>(subscriptionToAdd);
        }

        public async Task<bool> UpdateSubscription(SubscriptionUpdateRequest entityToUpdate)
        {
            Subscription subscription = _mapper.Map<Subscription>(entityToUpdate);
            await _transactionService.UpdateTransaction(entityToUpdate.Transaction);
            subscription.Transaction = null;
            subscription.Company = null;
            bool res = await _subscriptionRepository.UpdateAsync(subscription);
            return res;
        }
    }
}
