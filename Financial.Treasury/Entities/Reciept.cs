using Financial.Treasury.Abstractions;
using System;

namespace Financial.Treasury.Entities
{
    public class Reciept : TransactionSource
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        private Reciept() { }
        public Reciept(Account account, int amount, int orderId, int productId, string description)
            : base(account, amount, description)
        {
            OrderId = orderId;
        }
    }
}
