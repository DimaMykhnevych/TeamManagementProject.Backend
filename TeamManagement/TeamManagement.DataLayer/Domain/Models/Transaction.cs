using System;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public DateTime TransactionDate { get; set; }

        public Subscription Subscription { get; set; }
    }
}
