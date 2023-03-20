using Financial.Treasury.Dependecies.PaymentService;
using Financial.Treasury.Entities;
using Financial.ZarinPal.Dependencies;
using Financial.ZarinPal.Entities;
using Financial.ZarinPal.Models;
using System.Threading.Tasks;

namespace Financial.ZarinPal
{
    public class ZarinPalService : IPaymentService
    {
        private readonly IZarinPalRepository _zarinPalRepository;
        private readonly IZarinPalGateWay _zarinPalGateWay;

        public ZarinPalService(IZarinPalRepository zarinPalRepository, IZarinPalGateWay zarinPalGateWay)
        {
            _zarinPalRepository = zarinPalRepository;
            this._zarinPalGateWay = zarinPalGateWay;
        }

        public async Task<bool> Verify(Payment payment)
        {
            var terminal = _zarinPalRepository.GetTerminal(payment.Id);

            if (terminal == null)
            {
                return false;
            }

            var resultObject = await _zarinPalGateWay.Verify(terminal);
            if (resultObject?.Data != null)
            {
                var verfy = new VerifyResult(terminal, resultObject.Data);
                _zarinPalRepository.SaveVerifyResult(verfy);
                return resultObject.Data.IsSucceded;
            }

            LogError(payment, resultObject?.Errors);
            return false;
        }

        public async Task<bool> CreateTerminal(Payment payment)
        {

            var resultObject = await _zarinPalGateWay.CreateTerminal(payment);
            if (resultObject?.Data != null)
            {
                var terminal = new Terminal(payment, resultObject.Data);
                _zarinPalRepository.SaveTerminal(terminal);
                return true;
            }

            LogError(payment, resultObject?.Errors);
            return false;
        }

        private void LogError(Payment payment, Error? error)
        {
            var errorData = new ErrorLog(payment, error);
            _zarinPalRepository.SaveError(errorData);
        }
    }
}
