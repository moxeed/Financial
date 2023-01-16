using Financial.Treasury.Entities;
using Financial.ZarinPal;
using Microsoft.AspNetCore.Mvc;

namespace Financial.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CreateTerminal(int amount) { 
            var account = new Account { UserId = 100};
            var reciept = new Reciept(account, amount, Guid.NewGuid());

            var service = new ZarinPalService();

            return Ok(service.CreateTerminal(reciept).Result);
        }
    }
}