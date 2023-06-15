using System.Threading.Tasks;

namespace Financial.Treasury.Dependencies
{
    public interface IOrderingService
    {
        Task ProcessOrderPaymentCompletedAsync(int orderId);
    }
}
