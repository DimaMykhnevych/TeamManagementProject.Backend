using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> UpdateTransaction(TransactionUpdateRequest transaction);
    }
}
