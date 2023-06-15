using Financial.Treasury.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financial.Treasury.Dependencies
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetUnusedPaymentsByUserIdAsync(int userId);
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task SavePaymentAsync(Payment payment);
    }
}