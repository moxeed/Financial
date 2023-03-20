using Financial.Treasury.Entities;
using Financial.ZarinPal.Entities;
using Financial.ZarinPal.Models;
using System.Threading.Tasks;

namespace Financial.ZarinPal.Dependencies
{
    public interface IZarinPalGateWay
    {
        Task<PaymentResponse> CreateTerminal(Payment payment);
        Task<VerifyResponse> Verify(Terminal terminal);
    }
}