using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View("Index");
        }
        
        [HttpGet]
        [Route("Info")]
        public async Task<IActionResult> EnvironmentInfo()
        {
            return Ok(_environment.EnvironmentName);
        }
    }
}