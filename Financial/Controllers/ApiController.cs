using Microsoft.AspNetCore.Mvc;

namespace Financial.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class ApiController : Controller
    {
    }
}
