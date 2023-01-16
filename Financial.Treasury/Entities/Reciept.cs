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

        public Reciept(Account account, int amount, Guid issuerToken, string description)
        {
            Account = account;
            Amount = amount;
            IssuerToken = issuerToken;
            Description = description;
            Payments = new List<Payment>();
        }
    }
}
