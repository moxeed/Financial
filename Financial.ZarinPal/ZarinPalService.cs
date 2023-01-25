using Financial.Treasury.Dependecies.PaymentService;
using Financial.Treasury.Entities;
using Financial.ZarinPal.Dependencies;
using Financial.ZarinPal.Entities;
using Financial.ZarinPal.Models;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace Financial.ZarinPal
{
    public class ZarinPalService : IPaymentService
    {
        const string BaseUrl = "https://api.zarinpal.com/pg/";
        const string MerchantId = "123456789123456789123456789123456789";
        private readonly IZarinPalRepository _zarinPalRepository;

        public ZarinPalService(IZarinPalRepository zarinPalRepository)
        {
            _zarinPalRepository = zarinPalRepository;
        }

        public async Task<bool> Verify(Payment payment)
        {
            var terminal = _zarinPalRepository.GetTerminal(payment.ReferenceCode);
            try
            {
                var response = await BaseUrl.AppendPathSegment("v4/payment/verify.json")
                    .PostJsonAsync(new VerifyRequest
                    {
                        Amount = payment.Amount,
                        Authority = terminal.Authority,
                        Merchant_id = MerchantId
                    }).ReceiveString();

                var resultObject = Deserialize<VerifyResponse>(response);
                if (resultObject?.Data != null)
                {
                    var verfy = new VerifyResult(terminal, resultObject.Data);
                    _zarinPalRepository.SaveVerifyResult(verfy);
                    return resultObject.Data.IsSucceded;
                }

                LogError(payment, resultObject?.Errors);
                return false;
            }
            catch (FlurlHttpException exception)
            {
                var data = await exception.GetResponseStringAsync();
                Console.WriteLine(data);
                throw;
            }
        }

        public async Task<bool> CreateTerminal(Payment payment)
        {
            try
            {
                var response = await BaseUrl.AppendPathSegment("v4/payment/request.json")
                .PostJsonAsync(new PaymentRequest
                {
                    Amount = payment.Amount,
                    Callback_url = "http://localhost:5148/verify/",
                    Merchant_id = MerchantId,
                    Description = payment.Description
                }).ReceiveString();

                var resultObject = Deserialize<PaymentResponse>(response);
                if (resultObject?.Data != null)
                {
                    var terminal = new Terminal(payment, resultObject.Data);
                    return true;
                }

                LogError(payment, resultObject?.Errors);
                return false;
            }
            catch (FlurlHttpException exception)
            {
                var data = await exception.GetResponseStringAsync();
                Console.WriteLine(data);
                throw;
            }
        }

        private void LogError(Payment payment, Error? error) {
            var errorData = new ErrorLog(payment, error);
            _zarinPalRepository.SaveError(errorData);
        }

        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                Error = HandleDeserializationError
            });
        }

        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
