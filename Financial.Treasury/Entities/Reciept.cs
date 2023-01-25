using Financial.Treasury.Abstractions;
using System;
using System.Collections.Generic;

namespace Financial.Treasury.Entities
{
    public class Reciept : TransactionSource
    {
        public Guid IssuerToken { get; set; }
        public List<Payment> Payments { get; set; }

        public Reciept(Account account, int amount, Guid issuerToken, string description)
            : base(account, amount, description)
        {
            IssuerToken = issuerToken;
            Payments = new List<Payment>();
        }

        private Reciept() { }
    }
}
