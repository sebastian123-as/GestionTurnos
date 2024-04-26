using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Turnos.Data;

namespace Turnos.Controllers;

public class GestorController : Controller{
    public readonly BaseContext _context;

    public GestorController(BaseContext context){
        _context = context;
    }

    public IActionResult Home(){
        return View();
    }

}