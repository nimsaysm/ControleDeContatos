using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControleDeContatos.Models;
using ControleDeContatos.Filters;

namespace ControleDeContatos.Controllers
{
    //importando o filter de acesso dos usuários
    [PaginaParaUsuarioLogado]

    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;

        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}