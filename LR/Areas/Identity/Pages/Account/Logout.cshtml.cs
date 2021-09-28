using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LR.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly LeilaoReversoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LogoutModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ILogger<LogoutModel> logger,
            LeilaoReversoContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {

        }
 
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            string idUsuario = _userManager.GetUserId(User);
            var avaliacao = _context.Avaliacao.Where(x => x.UserId == idUsuario).FirstOrDefault();
            //if (avaliacao != null)
            //{
            //    TempData["Msg"] = "Você já tem uma avaliação respondida. Obrigado !";
            //    return RedirectToAction("Resumo", "Avaliacao");
            //}
            var usuario = await _context.AspNetUsers.Where(x => x.Id == idUsuario).SingleOrDefaultAsync();
            if (usuario != null)
            {
                if (usuario.IdStatusGame != null)
                {
                    if (usuario.IdStatusGame.Value > 2)
                    {
                        if (usuario.QtdComoFornecedor + usuario.QtdComoComprador < 4)
                        {
                            TempData["Msg"] = "Por favor avalie o jogo após participar de pelo menos 4 partidas";
//                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
