using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Turnos.Data;
using Turnos.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;



namespace GestionTurnos.Controllers
{

public class UserController : Controller
{ 
public readonly BaseContext _context;


public UserController(BaseContext context){
_context = context;
}
        public IActionResult Index(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? Documento)
        {  
            var user = await _context.Pacientes.FirstOrDefaultAsync(m => m.Documento == Documento);
            if(user != null){
                HttpContext.Session.SetInt32("Id", user.Id); //crear variable de sesion
            return View();
            }else{ 
                return RedirectToAction("Index", "Home");
            }
        }
    public IActionResult Generar(string documento){
        var paciente = _context.Pacientes.FirstOrDefault(m => m.Documento==documento);
        paciente = new Paciente(){ 
            Documento=paciente.Documento,
            Nombre=paciente.Nombre,
            IdTipoDocumento=paciente.IdTipoDocumento
        };
        return RedirectToAction("Index", "Home");
    }

    }
}