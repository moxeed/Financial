using Financial.Treasury.Entities;
using System.Threading.Tasks;

namespace Financial.Treasury.Dependecies.PaymentService
{
    public interface IPaymentService
    {
        Task<bool> CreateTerminal(Payment payment);
        Task<bool> Verify(Payment payment);
    }
}
