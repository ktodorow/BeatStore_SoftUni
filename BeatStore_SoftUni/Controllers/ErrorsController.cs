using Microsoft.AspNetCore.Mvc;

namespace BeatStore_SoftUni.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Errors/404")]
        public IActionResult NotFoundPage()
        {
            return View("404");
        }

        [Route("Errors/500")]
        public IActionResult InternalServerErrorPage()
        {
            return View("500");
        }
    }
}
