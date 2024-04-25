using Microsoft.EntityFrameworkCore;
using eps.Models;
using eps.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace eps.Controllers
{
    public class UserController : Controller
    {
         private readonly ILogger<HomeController> _context;

    public UserController(ILogger<HomeController>context)
    {
        _context = context;
    }
    public IActionResult Index(){
        return View();
    }

    }
}    