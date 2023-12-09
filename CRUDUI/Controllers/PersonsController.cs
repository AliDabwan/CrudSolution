using Microsoft.AspNetCore.Mvc;

namespace CRUDUI.Controllers
{
    public class PersonsController : Controller
    {
        [Route("/")]
        [Route("persons")]
        [Route("persons/index")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
