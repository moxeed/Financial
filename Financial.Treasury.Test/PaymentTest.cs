using Financial.Treasury.Entities;

namespace Financial.Treasury.Test
{
    public class PaymentTest
    {
        [Fact]
        public void ValidPayment_Done_DoneDataIsUpdated()
        {
            var unusedPayments = new List<Payment>();
            var payment = new Payment(100, 100, 100, "test", "test", unusedPayments);

            payment.Done();

            Assert.NotNull(payment.DoneDateTime);
        }

        [Fact]
        public void DonePayment_Done_DoneDataIsIntact()
        {
            var unusedPayments = new List<Payment>();
            var payment = new Payment(100, 100, 100, "test", "test", unusedPayments);
            payment.Done();

            var doneDateTime = payment.DoneDateTime;

            payment.Done();

            Assert.Equal(doneDateTime, payment.DoneDateTime);
        }


        [Fact]
        public void PendingPayment_Block_ReturnsNull()
        {
            var unusedPayments = new List<Payment>();
            var payment1 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            var payment2 = new Payment(100, 100, 100, "test", "test", unusedPayments);

            var block = payment1.Block(payment2, 100, "test");

            Assert.Null(block);
        }

        [Fact]
        public void RefunedPayment_Block_ReturnsBlock()
        {
            var unusedPayments = new List<Payment>();
            var payment1 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            var payment2 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            payment1.Refund(50, "test");

            var block = payment1.Block(payment2, 100, "test");

            Assert.NotNull(block);
            Assert.Equal(50, block?.Amount);
            Assert.Same(payment2, block?.ForPayment);
        }

        [Fact]
        public void SuffisentRemainingAmount_Block_ReturnsTotalAmount()
        {
            var unusedPayments = new List<Payment>();
            var payment1 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            var payment2 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            payment1.Refund(100, "test");

            var block = payment1.Block(payment2, 100, "test");

            Assert.NotNull(block);
            Assert.Equal(payment2.Amount, block?.Amount);
            Assert.Same(payment2, block?.ForPayment);
        }

        [Fact]
        public void InSuffisentRemainingAmount_Block_ReturnsRemaingAmount()
        {
            var unusedPayments = new List<Payment>();
            var payment1 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            var payment2 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            payment1.Refund(70, "test");

            var block = payment1.Block(payment2, 100, "test");

            Assert.NotNull(block);
            Assert.Equal(70, block?.Amount);
            Assert.Same(payment2, block?.ForPayment);
        }


        [Fact]
        public void InSuffisentRemainingAmount_MultipleBlock_ReturnsNullAtSecondTime()
        {
            var unusedPayments = new List<Payment>();
            var payment1 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            var payment2 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            payment1.Refund(70, "test");

            payment1.Block(payment2, 100, "test");
            var block2 = payment1.Block(payment2, 100, "test");

            Assert.Null(block2);
        }

        [Fact]
        public void InSuffisentRemainingAmount_NewPayment_SubtractsBlockFromPayableAmount()
        {
            var unusedPayments = new List<Payment>();
            var payment1 = new Payment(100, 100, 100, "test", "test", unusedPayments);
            payment1.Refund(70, "test");
            unusedPayments.Add(payment1);
            var payment2 = new Payment(100, 100, 100, "test", "test", unusedPayments);

            Assert.Equal(payment2.Amount - payment1.RefundedAmount, payment2.PayableAmount);
            Assert.NotEmpty(payment2.MyBlocks);
            Assert.NotEmpty(payment1.OtherBlocks);
        }
    }
}