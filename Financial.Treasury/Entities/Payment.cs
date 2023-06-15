using Financial.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Financial.Treasury.Entities
{
    public class Payment : Entity
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int RefundedAmount { get; set; }
        public string? RefundedDescription { get; set; }
        public DateTime? RefundedDateTime { get; set; }
        public DateTime? DoneDateTime { get; set; }
        public DateTime? CancelDateTime { get; set; }
        public ICollection<Block> OtherBlocks { get; set; }
        public ICollection<Block> MyBlocks { get; set; }

        public int UsedRemianingAmount => OtherBlocks.Sum(b => b.Amount);
        public int BlockedAmount => MyBlocks.Sum(b => b.Amount);
        public int RemainingAmount => RefundedAmount - UsedRemianingAmount;
        public int PayableAmount => Amount - BlockedAmount;

        private Payment()
        {
            OtherBlocks = new HashSet<Block>();
            MyBlocks = new HashSet<Block>();
        }

        public Payment(int userId, int orderId, int amount, string description, string source, List<Payment> unusedPayments) : this()
        {
            UserId = userId;
            OrderId = orderId;
            Description = description;
            Source = source;
            Amount = amount;

            if (unusedPayments != null)
            {
                foreach (var payment in unusedPayments)
                {
                    if (PayableAmount == 0)
                        break;

                    var block = payment.Block(this, PayableAmount, description);
                    if (block != null)
                    {
                        MyBlocks.Add(block);
                    }
                }
            }
        }

        public void Done()
        {
            if (DoneDateTime == null)
            {
                DoneDateTime = DateTime.Now;
            }
        }
        
        public void Cancel()
        {
            if (CancelDateTime == null)
            {
                CancelDateTime = DateTime.Now;
            }
        }

        public void Refund(int amount, string description)
        {
            if (amount <= PayableAmount)
            {
                RefundedAmount = amount;
                RefundedDescription = description;
                RefundedDateTime = DateTime.Now;
            }
        }

        public Block? Block(Payment forPayment, int amount, string description)
        {
            if (RemainingAmount == 0)
            {
                return null;
            }

            if (RemainingAmount >= amount)
            {

                var fullBlock = new Block(amount, description, forPayment);
                OtherBlocks.Add(fullBlock);

                return fullBlock;
            }

            var partialBlock = new Block(RemainingAmount, description, forPayment);
            OtherBlocks.Add(partialBlock);

            return partialBlock;
        }
    }
}
