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

            //Se valida que lo que contiene no sea null
            if (TipoCola != null)
            {
                //En caso de que sea prioritario
                if (TipoCola != 6)
                {
                    ViewBag.Turnos = _context.Turnos.Where(x => x.IdTipoTurno == TipoCola && x.Discapacidad == false && x.IdEstado == 1).Take(5);
                    ViewBag.SiguienteTurno = await _context.Turnos.FirstOrDefaultAsync(x => x.Discapacidad == false && x.IdEstado == 3 && x.IdTipoTurno == TipoCola);
                    return View();
                }
                //Si es diferente a prioritario
                else
                {
                    ViewBag.Turnos = _context.Turnos.Where(x => x.Discapacidad == true && x.IdEstado == 1).Take(5);
                    ViewBag.SiguienteTurno = await _context.Turnos.FirstOrDefaultAsync(x => x.Discapacidad == true && x.IdEstado == 3);
                    return View();
                }
            }
            //Si es null mostrara todas las actuales
            else
            {
                ViewBag.Turnos = _context.Turnos.Where(x => x.IdEstado == 1).Take(5);
                return View();
            }
        }

        public IActionResult ErrorValidacion(){
            return View();
        }

    }
}