using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Turnos.Data;
using Turnos.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;



namespace Turnos.Controllers
{

public class HomeController : Controller
{ 
public readonly BaseContext _context;


public HomeController(BaseContext context){
    _context = context;
}
        public IActionResult Index()
        {   
            return View();
        }

        }
}