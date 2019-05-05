using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View("Index");
        }
    }
}