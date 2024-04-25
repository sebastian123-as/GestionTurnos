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
        public IActionResult Index()
        {   
            return View();
        }

        }
}