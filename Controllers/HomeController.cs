using Microsoft.AspNetCore.Mvc;
using Turnos.Data;
using Turnos.Models;
using Microsoft.EntityFrameworkCore;



namespace Turnos.Controllers
{

    public class HomeController : Controller
    {
        public readonly BaseContext _context;


        public HomeController(BaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? Documento, bool atencion, int tipodoc)
        {
            var user = await _context.Pacientes.FirstOrDefaultAsync(m => m.Documento == Documento && m.IdTipoDocumento == tipodoc);
            if (user != null)
            {
                HttpContext.Session.SetInt32("Id", user.Id); //crear variable de sesion
                TempData["Discapacidad"] = atencion;
                return RedirectToAction("VistaGenerar", "Home");
            }
            else
            {
                return RedirectToAction("ErrorValidacion", "Turnos");
            }
        }

        public IActionResult VistaGenerar()
        {
            return View();
        }

        public IActionResult Generar(int? tipo, string? UsuarioNoexistente)
        {
            int turnoactual = _context.Turnos.ToList().Count();

            if (UsuarioNoexistente != "True")
            {
                var Tipoturno = _context.TipoTurno.FirstOrDefault(m => m.Id == tipo);
                var pacientes = _context.Pacientes.FirstOrDefault(m => m.Id == HttpContext.Session.GetInt32("Id"));

                Turno turnon = new()
                {
                    Discapacidad = bool.Parse(TempData["Discapacidad"].ToString()),
                    IdEstado = 1,
                    Tiket = $"{Tipoturno.Tipo}-{turnoactual + 1}",
                    IdPaciente = pacientes.Id,
                    IdTipoTurno = Tipoturno.Id

                };
                _context.Turnos.Add(turnon);
                _context.SaveChanges();
                return RedirectToAction("Turno", "Home", new {id = turnon.Id});
            }else{
                Turno oTurno = new(){
                    Discapacidad = false,
                    IdEstado = 1,
                    Tiket = $"IG-{turnoactual + 1}",
                    IdPaciente = null,
                    IdTipoTurno = 2
                };
                _context.Turnos.Add(oTurno);
                _context.SaveChanges();
                return RedirectToAction("Turno", "Home", new {id = oTurno.Id});
            }
        }
        public async Task<IActionResult> Turno(int id)
        {
            ViewBag.Esto = await _context.Turnos.FirstOrDefaultAsync(x => x.Id == id);
            
            return View();
        }

    }
}