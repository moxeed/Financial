using Financial.Treasury.Entities;
using Financial.ZarinPal.Dependencies;
using Financial.ZarinPal.Entities;
using Microsoft.Extensions.Options;
using Moq;

namespace Financial.ZarinPal.Test
{
    public class ZarinPalServiceTest
    {
        [Fact]
        public async Task PaymentValid_CreateTerminal_InsertsIntoDataBase()
        {
            var repository = new Mock<IZarinPalRepository>();
            var gateway = new Mock<IZarinPalGateWay>();
            var options = new Mock<IOptions<ZarinPalOptions>>();

            var service = new ZarinPalService(repository.Object, gateway.Object, options.Object);

            var payment = new Payment(100, 100, 100, "test", "online", new List<Payment>());
            var code = 100;
            var message = "Success";
            Terminal? terminal = null;

            repository.Setup(r => r.GetTerminal(It.IsAny<int>())).Returns(terminal);
            repository.Setup(r => r.SaveTerminal(It.IsAny<Terminal>())).Callback((Terminal data) => terminal = data);
            gateway.Setup(r => r.CreateTerminal(It.IsAny<Payment>())).Returns(Task.FromResult(new Models.PaymentResponse
            {
                Data = new Models.PaymentData
                {
                    Authority = "Test",
                    Code = code,
                    Fee = 1000,
                    Fee_type = "M",
                    Message = message
                },
                Errors = null
            }));

            await service.CreateTerminal(payment);

            Assert.NotNull(terminal);
            Assert.Equal(code, terminal?.Code);
            Assert.Equal(message, terminal?.Message);
        }

        [Fact]
        public async Task PaymentIsInvalid_Verfiy_InsertError()
        {
            var repository = new Mock<IZarinPalRepository>();
            var gateway = new Mock<IZarinPalGateWay>();
            var options = new Mock<IOptions<ZarinPalOptions>>();

            var service = new ZarinPalService(repository.Object, gateway.Object, options.Object);

            var payment = new Payment(100, 100, 100, "test", "online", new List<Payment>());

            var terminal = new Terminal(payment, new Models.PaymentData
            {
                Authority = "",
                Code = 1,
                Fee = 10,
                Fee_type = "M",
                Message = "Ok"
            });

            var code = -10;
            var message = "error";
            ErrorLog? errorLog = null;

            repository.Setup(r => r.GetTerminal(It.IsAny<int>())).Returns(terminal);
            repository.Setup(r => r.SaveError(It.IsAny<ErrorLog>())).Callback((ErrorLog log) => errorLog = log);
            gateway.Setup(r => r.Verify(It.IsAny<Terminal>())).Returns(Task.FromResult(new Models.VerifyResponse
            {
                Data = null,
                Errors = new Models.Error
                {
                    Code = code,
                    Message = message,
                    Validations = new IDictionary<string, string>[] {
                        new Dictionary<string, string>{
                            ["f1"] = "e1"
                        }
                    }
                }
            }));

            await service.Verify(payment);

            Assert.NotNull(errorLog);
            Assert.Equal(code, errorLog?.Code);
            Assert.Equal(message, errorLog?.Message);
            Assert.NotEmpty(errorLog?.Data);
        }
    }
}