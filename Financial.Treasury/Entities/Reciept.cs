using System;
using System.Collections.Generic;

namespace Financial.Treasury.Entities
{
    public class Reciept
    {
        public Account Account { get; set; }
        public int Amount { get; set; }
        public Guid IssuerToken { get; set; }
        public List<Payment> Payments { get; set; }
        public string Description { get; set; }

        private Reciept()
        {
        }

        public Reciept(Account account, int amount, Guid issuerToken)
        {
            Account = account;
            Amount = amount;
            IssuerToken = issuerToken;
            Payments = new List<Payment>();
        }
    }
}
