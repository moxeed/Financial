using Financial.Treasury.Dependencies;
using Financial.Treasury.Entities;
using Financial.Treasury.Models;
using System.Threading.Tasks;
using Bartarha.Exception;

namespace Financial.Treasury
{
    public class TreasuryService
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderingService _orderingService;

        public TreasuryService(IPaymentService paymentService,
            IPaymentRepository paymentRepository,
            IOrderingService orderingService)
        {
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
            _orderingService = orderingService;
        }

        public async Task<PaymentSummaryModel> RegisterOrder(int userId, int amount, int orderId, string description) 
        {
            var source = _paymentService.SourceName;
            var payment = await _paymentRepository.GetPaymentByOrderIdAsync(orderId);

            if (payment == null) {
                var unusedPayments = await _paymentRepository.GetUnusedPaymentsByUserIdAsync(userId);
                payment = new Payment(userId, orderId, amount, description, source, unusedPayments);
                await _paymentRepository.SavePaymentAsync(payment);
            }

            return new PaymentSummaryModel
            {
                BlockedAmount = payment.BlockedAmount,
                PaymentAmount = payment.PayableAmount,
                PaymentUrl = _paymentService.GetPaymentLink(payment)
            };
        }

        public async Task CompletePayment(int paymentId) {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);

            if (payment == null) {
                throw new BusinessException(1, "پرداخت مورد نظر پیدا نشد");
            }

            var verifyResult = await _paymentService.Verify(payment);
            if (!verifyResult.IsSucceded) 
            {
                return;
            }

            if (verifyResult.IsVerified)
            {
                payment.Done();
                await _orderingService.ProcessOrderPaymentCompletedAsync(payment.OrderId);
            }
            else 
            { 
                payment.Cancel();
            }
        }
    }
}