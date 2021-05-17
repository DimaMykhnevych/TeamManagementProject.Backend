using System;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Transaction:IIdentificated
    {
        public Guid Id { get; set; }
        public string PublicKey { get; set; }
        public DateTime TransactionDate { get; set; }

        public Subscription Subscription { get; set; }
    }
}
