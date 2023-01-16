using Financial.Treasury.Dependecies.PaymentService;
using Financial.Treasury.Entities;
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
        public async Task<bool> Verify(Payment payment)
        {
            try
            {
                var response = await BaseUrl.AppendPathSegment("v4/payment/verify.json")
                    .PostJsonAsync(new VerifyRequest
                    {
                        amount = payment.Amount,
                        authority = payment.Identifier,
                        merchant_id = MerchantId
                    }).ReceiveString();

                var resultObject = Deserialize<VerifyResponse>(response);
                return resultObject.data.code == 100;
            }
            catch (FlurlHttpException exception)
            {
                var data = await exception.GetResponseStringAsync();
                Console.WriteLine(data);
                throw;
            }
        }

        public async Task<string> CreateTerminal(Reciept reciept)
        {
            try
            {
                var response = await BaseUrl.AppendPathSegment("v4/payment/request.json")
                .PostJsonAsync(new PaymentRequest
                {
                    amount = reciept.Amount,
                    callback_url = "http://localhost:5148/verify/",
                    merchant_id = MerchantId,
                    description = reciept.Description
                }).ReceiveString();

                var resultObject = Deserialize<PaymentResponse>(response);
                return resultObject.data.authority;
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

        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
