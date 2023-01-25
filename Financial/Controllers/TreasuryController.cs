using Financial.Treasury.Entities;
using Financial.ZarinPal;
using Microsoft.AspNetCore.Mvc;

namespace Financial.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TreasuryController : ControllerBase
    {
        private readonly ILogger<TreasuryController> _logger;

        public TreasuryController(ILogger<TreasuryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CreateTerminal(int amount) {
            return Ok(new { amount });
            //var account = new Account { UserId = 100};
            //var reciept = new Payment(account, amount, "Test Payment");

            //var service = new ZarinPalService();

            //return Ok(service.CreateTerminal(reciept).Result);
        }
    }
}