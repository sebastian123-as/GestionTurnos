using Microsoft.AspNetCore.Mvc;

namespace mientras.Controllers {
    public class TurnosController : Controller {

        public IActionResult Index() {
            return View();
        }

        public IActionResult ListaTurnos() {
            return View();
        }
    }
}