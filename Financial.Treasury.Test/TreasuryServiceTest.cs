using Financial.Treasury.Dependencies;
using Financial.Treasury.Entities;
using Moq;

namespace Financial.Treasury.Test
{
    public class TreasuryServiceTest
    {
        [Fact]
        public async Task Test() {
            var paymentService = new Mock<IPaymentService>();
            var paymentRepository = new Mock<IPaymentRepository>();
            var orderingService = new Mock<IOrderingService>();

            var service = new TreasuryService(paymentService.Object,
                paymentRepository.Object,
                orderingService.Object);

            var result = await service.RegisterOrder(100, 100, 100, "test");

            Assert.Equal(100, result.PaymentAmount);
            Assert.Equal(0, result.BlockedAmount);
            Assert.Null(result.PaymentUrl);
        }

        [Fact]
        public async Task Complete()
        {
            var paymentService = new Mock<IPaymentService>();
            var paymentRepository = new Mock<IPaymentRepository>();
            var orderingService = new Mock<IOrderingService>();
            var payment1 = new Payment(100, 100, 100, "test", "test", new List<Payment>());

            paymentRepository.Setup(p => p.GetPaymentByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(payment1));

            paymentService.Setup(p => p.Verify(It.IsAny<Payment>()))
                .Returns(Task.FromResult(new Models.VerifyResultModel { IsSucceded = true, IsVerified = true }));

            var service = new TreasuryService(paymentService.Object,
                paymentRepository.Object,
                orderingService.Object);

            await service.CompletePayment(1);

            Assert.NotNull(payment1.DoneDateTime);
        }


        [Fact]
        public async Task CompleteNull()
        {
            var paymentService = new Mock<IPaymentService>();
            var paymentRepository = new Mock<IPaymentRepository>();
            var orderingService = new Mock<IOrderingService>();
            var payment1 = new Payment(100, 100, 100, "test", "test", new List<Payment>());

            paymentRepository.Setup(p => p.GetPaymentByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(payment1));

            paymentService.Setup(p => p.Verify(It.IsAny<Payment>()))
                .Returns(Task.FromResult(new Models.VerifyResultModel { IsSucceded = false, IsVerified = false }));

            var service = new TreasuryService(paymentService.Object,
                paymentRepository.Object,
                orderingService.Object);

            await service.CompletePayment(1);

            Assert.Null(payment1.DoneDateTime);
        }

        [Fact]
        public async Task CompleteFalse()
        {
            var paymentService = new Mock<IPaymentService>();
            var paymentRepository = new Mock<IPaymentRepository>();
            var orderingService = new Mock<IOrderingService>();
            var payment1 = new Payment(100, 100, 100, "test", "test", new List<Payment>());

            paymentRepository.Setup(p => p.GetPaymentByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(payment1));

            paymentService.Setup(p => p.Verify(It.IsAny<Payment>()))
                .Returns(Task.FromResult(new Models.VerifyResultModel { IsSucceded = true, IsVerified = false }));

            var service = new TreasuryService(paymentService.Object,
                paymentRepository.Object,
                orderingService.Object);

            await service.CompletePayment(1);

            Assert.Null(payment1.DoneDateTime);
        }
    }
}
