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

    //Inicio sesion
    public IActionResult InicioSesion(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> InicioSesion(string Correo, string Contraseña){
        //Se valida que la contraseña y el correo sean los mismos
        var ValidateCorreo = await _context.Gestores.FirstOrDefaultAsync(x =>
            x.Correo == Correo && x.Contraseña == Contraseña
        );

        
        if(ValidateCorreo != null){
            //si encuentra datos guardara una session y lo redireciona al dashboard
            HttpContext.Session.SetInt32("IdGestor", ValidateCorreo.Id);
            return RedirectToAction("Dashboard");
        }else{
            //si no lo regresa al login denuevo
            TempData["MensajeInicioSesion"] = "Datos incorrectos, por favor intente de nuevo";
            return RedirectToAction("InicioSesion");
        }

        
    }

    //Dashboard
    public async Task<IActionResult> Dashboard(){
        //Se recibe el id del gestor
        ViewBag.GestorActivo = HttpContext.Session.GetInt32("IdGestor");
        
        //Se crea un objeto en donde se guardara el conteo de cada modulo (turnos)
        CountTurnos oCountTurnos = new(){
            CountPrioritarios = _context.Turnos.Where(x => x.Discapacidad == true && x.IdEstado == 1).Count(),
            CountMedicamentos = _context.Turnos.Where(x => x.Discapacidad == false && x.IdEstado == 1 && x.IdTipoTurno == 1).Count(),
            CountCitas = _context.Turnos.Where(x => x.Discapacidad == false && x.IdEstado == 1 && x.IdTipoTurno == 4).Count(),
            CountInfoGeneral = _context.Turnos.Where(x => x.Discapacidad == false && x.IdEstado == 1 && x.IdTipoTurno == 2).Count(),
            CountPagos = _context.Turnos.Where(x => x.Discapacidad == false && x.IdEstado == 1 && x.IdTipoTurno == 3).Count()
        };

        //Se retorna un viewbag con estos datos
        ViewBag.Counts = oCountTurnos;
        return View();
    }

    
    //Cola de turnos
    public async Task<IActionResult> ColaDeTurnosGestor(int? TipoTurno){
        //Se valida que el tipo de turno no sea prioridad
        if(TipoTurno != 6){
            HttpContext.Session.SetInt32("ColaTurnoActual", TipoTurno.Value);
            //se valida que el tipo sea igual al que se ingresa por parametro y el estado del turno sea activo de esta busqueda solo tomamos 5
            var BusquedaTurnos = _context.Turnos.Where(x => x.IdTipoTurno == TipoTurno && x.Discapacidad == false && x.IdEstado == 1 ).Take(5);
            
            //Se busca el ultimo que esta siendo atendido para poder despues finalizar
            var BusquedaEstadoAtendiendo = await _context.Turnos.FirstOrDefaultAsync(x => x.IdEstado == 3 && x.IdTipoTurno == TipoTurno);
            
            //Se crean los respectivos ViewBag
            ViewBag.BusquedaEstadoAtendiendo = BusquedaEstadoAtendiendo;
            ViewBag.BusquedaTurnos = BusquedaTurnos;
            return View();
        }else{
            HttpContext.Session.SetInt32("ColaTurnoActual", 6);
            var BusquedaTurnos = _context.Turnos.Where(x => x.Discapacidad == true && x.IdEstado == 1).Take(5);
            ViewBag.BusquedaTurnos = BusquedaTurnos;
            return View();
        }
        
    }

    //Siguiente turno
    public async Task<IActionResult> SiguienteTurno(int? id){
        //Se valida que el id exista
        var SiguienteTurno = await _context.Turnos.FirstOrDefaultAsync(x => x.Id == id);
        
        //Se actualiza el estado a atendiendo
        SiguienteTurno.IdEstado = 3;

        //Se guardan los cambios a la base de datos
        await _context.SaveChangesAsync();

        //Se redireciona a la vista de turnos con el tipo de turno actual
        return RedirectToAction("ColaDeTurnosGestor", "Gestor", new {TipoTurno = SiguienteTurno.IdTipoTurno});
    }


    //Finalizar turno
    public async Task<IActionResult> FinalizarTurno(string? tiket){
        //Buscamos el id del tiket a finalizar
        var TurnoAFinalizar = await _context.Turnos.FirstOrDefaultAsync(x => x.Tiket == tiket && x.IdEstado == 3);
        
        //Validamos que lo que esto regresa no sea null
        if(TurnoAFinalizar != null){
            //Se actualiza el estado a atendido y se guarda
            TurnoAFinalizar.IdEstado = 2;
            await _context.SaveChangesAsync();
            //Se le redireciona a la vista en que estaba
            return RedirectToAction("ColaDeTurnosGestor", "Gestor", new {TipoTurno = TurnoAFinalizar.IdTipoTurno});
        }else{
            return RedirectToAction("Dashboard", "Gestor");
        }
    
    }
    

    public IActionResult CerrarSesion(){
        HttpContext.Session.Remove("IdGestor");
        ViewData.Clear();
        return RedirectToAction("InicioSesion");
    }
}