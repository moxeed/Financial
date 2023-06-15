using Financial.Common;

namespace Financial.Treasury.Entities
{
    public class Block : Entity
    {
        public int Amount { get; set; }
        public int PaymentId { get; }
        public string Description { get; set; }
        public Payment ForPayment { get; set; }

        public Block(int amount, string description, Payment forPayment)
        {
            Amount = amount;
            Description = description;
            ForPayment = forPayment;
        }

        private Block()
        {
        }
    }
}
