using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Data;



namespace Turnos.Controllers {
    public class TurnosController : Controller {

        public readonly BaseContext _context;

        public TurnosController(BaseContext context) {
            _context = context;
        } 

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> ListaTurnos() {
            ViewBag.Turnos = _context.Turnos.Where(x => x.Estado == true).Take(6);
            return View();
        }

        public IActionResult ErrorValidacion() {
            return View();
        }

    }
}