using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Turnos.Data;
using Turnos.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;



namespace GestionTurnos.Controllers
{

    public class UserController : Controller
    {
        public readonly BaseContext _context;


        public UserController(BaseContext context)
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
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Generar(int? tipo)
        {
            //return Json (discapacidad);
            var turnoactual = _context.Turnos.ToList().Count();
            var Tipoturno = _context.TipoTurno.FirstOrDefault(m => m.Id == tipo);
            var pacientes = _context.Pacientes.FirstOrDefault(m => m.Id == HttpContext.Session.GetInt32("Id"));

            var turnon = new Turno()
            {
                Discapacidad = bool.Parse(TempData["Discapacidad"].ToString()),
                Estado = true,
                Tiket = $"{Tipoturno.Tipo}-{turnoactual + 1}",
                IdPaciente = pacientes.Id,
                IdTipoTurno = Tipoturno.Id

            };
            _context.Turnos.Add(turnon);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}