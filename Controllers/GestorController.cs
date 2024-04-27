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

    public IActionResult Dashboard(){
        ViewBag.GestorActivo = HttpContext.Session.GetInt32("IdGestor");
        CountTurnos oCountTurnos = new(){
            CountPrioritarios = 1,
            CountMedicamentos = 1,
            CountCitas = 1,
            CountInfoGeneral = 1,
            CountPagos = 1
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