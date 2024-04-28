using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Data;

namespace Turnos.Controllers
{
    public class TurnosController : Controller
    {

        public readonly BaseContext _context;

        public TurnosController(BaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaTurnos()
        {
            //Obtencion de la session necesaria
            var TipoCola = HttpContext.Session.GetInt32("ColaTurnoActual");

            if (TipoCola != null)
            {
                if (TipoCola != 6)
                {
                    ViewBag.Turnos = _context.Turnos.Where(x => x.IdTipoTurno == TipoCola && x.Discapacidad == false && x.IdEstado == 1).Take(5);
                    ViewBag.SiguienteTurno = await _context.Turnos.FirstOrDefaultAsync(x => x.Discapacidad == false && x.IdEstado == 3 && x.IdTipoTurno == TipoCola);
                    return View();
                }
                else
                {
                    ViewBag.Turnos = _context.Turnos.Where(x => x.Discapacidad == true && x.IdEstado == 1).Take(5);
                    ViewBag.SiguienteTurno = await _context.Turnos.FirstOrDefaultAsync(x => x.Discapacidad == true && x.IdEstado == 3);
                    return View();
                }
            }
            else
            {
                ViewBag.Turnos = _context.Turnos.Where(x => x.IdEstado == 1).Take(5);
                return View();
            }





        }

    }
}