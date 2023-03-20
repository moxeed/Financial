using System;
using Financial.Common;
using Financial.Treasury.Entities;

namespace Financial.Treasury.Abstractions
{
    public abstract class TransactionSource : Entity
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }

        public TransactionSource(Account account, int amount, string description)
        {
            Account = account;
            Amount = amount;
            Description = description;
        }

        protected TransactionSource() { }
    }
}
