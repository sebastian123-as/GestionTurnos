using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Data;

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
        return View();
    }
}