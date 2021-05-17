using AutoMapper;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(IGenericRepository<Transaction> genericRepository, IMapper mapper)
        {
            _transactionRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<bool> UpdateTransaction(TransactionUpdateRequest transaction)
        {
            Transaction transactioToUpdate = _mapper.Map<Transaction>(transaction);
            bool resp = await _transactionRepository.UpdateAsync(transactioToUpdate);
            return resp;
        }
    }
}
