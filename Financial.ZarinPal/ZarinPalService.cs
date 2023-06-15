using Financial.Treasury.Dependencies;
using Financial.Treasury.Entities;
using Financial.Treasury.Models;
using Financial.ZarinPal.Dependencies;
using Financial.ZarinPal.Entities;
using Financial.ZarinPal.Models;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Financial.ZarinPal
{
    public class ZarinPalService : IPaymentService
    {
        private readonly IZarinPalRepository _zarinPalRepository;
        private readonly IZarinPalGateWay _zarinPalGateWay;
        private readonly ZarinPalOptions _options;

        public string SourceName => "ZarinPal";

        public ZarinPalService(IZarinPalRepository zarinPalRepository,
            IZarinPalGateWay zarinPalGateWay,
            IOptions<ZarinPalOptions> options)
        {
            _zarinPalRepository = zarinPalRepository;
            _zarinPalGateWay = zarinPalGateWay;
            _options = options.Value;
        }

        public async Task<VerifyResultModel> Verify(Payment payment)
        {
            var terminal = _zarinPalRepository.GetTerminal(payment.Id);
            var result = new VerifyResultModel();

            if (terminal == null)
            {
                return result;
            }

            var resultObject = await _zarinPalGateWay.Verify(terminal);
            if (resultObject?.Data != null)
            {
                var verfy = new VerifyResult(terminal, resultObject.Data);
                _zarinPalRepository.SaveVerifyResult(verfy);
                result.IsVerified = resultObject.Data.IsSucceded;
            }

            LogError(payment, resultObject?.Errors);
            return result;
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

        public string GetPaymentLink(Payment payment)
        {
            if (payment.Id == default)
                throw new InvalidOperationException("Payment Id Should Have Value");

            return _options.PaymentUrl + payment.Id;
        }
    }
}
