using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Data;
using Turnos.Models;

namespace Turnos.Controllers;

public class GestorController : Controller {
    public readonly BaseContext _context;

    public GestorController(BaseContext context){
        _context = context;
    }

    public IActionResult InicioSesion(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> InicioSesion(string Correo, string Contraseña){
        var ValidateCorreo = await _context.Gestores.FirstOrDefaultAsync(x =>
            x.Correo == Correo && x.Contraseña == Contraseña
        );
        if(ValidateCorreo != null){
            HttpContext.Session.SetInt32("IdGestor", ValidateCorreo.Id);
            return RedirectToAction("Dashboard");
        }else{
            TempData["MensajeInicioSesion"] = "Datos incorrectos, por favor intente de nuevo";
            return RedirectToAction("InicioSesion");
        }

        
    }

    public async Task<IActionResult> Dashboard(){
        ViewBag.GestorActivo = HttpContext.Session.GetInt32("IdGestor");
        

        CountTurnos oCountTurnos = new(){
            CountPrioritarios = _context.Turnos.Where(x => x.Discapacidad == true && x.Estado == true).Count(),
            CountMedicamentos = _context.Turnos.Where(x => x.Discapacidad == false && x.Estado == true && x.IdTipoTurno == 1).Count(),
            CountCitas = _context.Turnos.Where(x => x.Discapacidad == false && x.Estado == true && x.IdTipoTurno == 4).Count(),
            CountInfoGeneral = _context.Turnos.Where(x => x.Discapacidad == false && x.Estado == true && x.IdTipoTurno == 2).Count(),
            CountPagos = _context.Turnos.Where(x => x.Discapacidad == false && x.Estado == true && x.IdTipoTurno == 3).Count()
        };

        ViewBag.Counts = oCountTurnos;
        return View();
    }

    public async Task<IActionResult> ColaDeTurnosGestor(int? TipoTurno){
        var BusquedaTurnos = await _context.Turnos.Where(x => x.IdTipoTurno == TipoTurno).ToListAsync();
        ViewBag.BusquedaTurnos = BusquedaTurnos;
        return View();
    }

    

    public IActionResult CerrarSesion(){
        HttpContext.Session.Remove("IdGestor");
        return RedirectToAction("InicioSesion");
    }
}