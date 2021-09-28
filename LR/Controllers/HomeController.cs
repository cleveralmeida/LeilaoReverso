using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LR.Models;
using TimeZoneConverter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LR.Controllers
{
    public class HomeController : Controller
    {
        private readonly LeilaoReversoContext _context;
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(
            ILogger<HomeController> logger,
            LeilaoReversoContext context,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var perfil = User.Identities.First(u => u.IsAuthenticated).FindFirst("Perfil")?.Value.ToString();
                ViewBag.IdUser = _userManager.GetUserId(User);
                ViewBag.Perfil = perfil;
                if (perfil == "Fornecedor")
                {
                    var compradorFornecedor = await _context.CompradorFornecedor.Where(x => x.IdFornecedor == _userManager.GetUserId(User)).FirstOrDefaultAsync();
                    if (compradorFornecedor != null)
                    {
                        var leilao = await _context.Leilao.Where(x => x.IdComprador == compradorFornecedor.IdComprador && x.IndicadorAtivo == true && x.SelecaoRealizada == null && x.DataFechamento.Value > DateTime.Now).FirstOrDefaultAsync();
                        if (leilao != null)
                        {
                            ViewBag.IdLeilao = leilao.IdLeilao;
                            ViewBag.IdComprador = leilao.IdComprador;
                        }
                    }
                }
            } else 
            {
                ViewBag.IdUser = "x";
                ViewBag.Perfil = "xx";
            }
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
