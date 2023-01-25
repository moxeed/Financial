using Financial.Treasury.Abstractions;

namespace Financial.Treasury.Entities
{
    public class Payment : TransactionSource
    {
        public Payment(Account account, int amount, string description)
            :base(account, amount, description)
        {
        }

        private Payment() { }
    }
}
