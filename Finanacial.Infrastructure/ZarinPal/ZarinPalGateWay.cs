using Financial.Treasury.Entities;
using Financial.ZarinPal.Dependencies;
using Financial.ZarinPal.Entities;
using Financial.ZarinPal.Models;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace Finanacial.Infrastructure.ZarinPal
{
    internal class ZarinPalGateWay : IZarinPalGateWay
    {
        const string BaseUrl = "https://api.zarinpal.com/pg/";
        const string MerchantId = "e4350398-c2be-11ea-9b53-000c295eb8fc";

        public async Task<VerifyResponse> Verify(Terminal terminal)
        {
            try
            {
                var response = await BaseUrl.AppendPathSegment("v4/payment/verify.json")
                    .PostJsonAsync(new VerifyRequest
                    {
                        Amount = terminal.Amount,
                        Authority = terminal.Authority,
                        Merchant_id = MerchantId
                    }).ReceiveString();

                return Deserialize<VerifyResponse>(response);
            }
            catch (FlurlHttpException exception)
            {
                var data = await exception.GetResponseStringAsync();
                Console.WriteLine(data);
                throw;
            }
        }

        public async Task<PaymentResponse> CreateTerminal(Payment payment)
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

                return Deserialize<PaymentResponse>(response);
            }
            catch (FlurlHttpException exception)
            {
                var data = await exception.GetResponseStringAsync();
                Console.WriteLine(data);
                throw;
            }
        }

        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                Error = HandleDeserializationError
            });
        }

        public void HandleDeserializationError(object? sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs)
        {
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
