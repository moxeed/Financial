using Financial.Treasury.Entities;
using System.Threading.Tasks;

namespace Financial.Treasury.Dependecies.PaymentService
{
    public interface IPaymentService
    {
        Task<bool> Verify(Payment payment);
    }
}
