using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LR.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TimeZoneConverter;

namespace LR.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel2 : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel2> _logger;
        private readonly LeilaoReversoContext _context;

        public RegisterModel2(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel2> logger,
            LeilaoReversoContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel2 Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel2
        {
            [Required(ErrorMessage = "Campo deve ser informado")]
            [Display(Name = "Nome de Usuário")]
            public string Nome { get; set; }


            [Display(Name = "NomeComprador")]
            public string NomeComprador { get; set; }

            [Required(ErrorMessage = "Campo deve ser informado")]
            [StringLength(100, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

 

            [Required(ErrorMessage = "Campo deve ser informado")]
            [Display(Name = "Tipo Jogador")]
            public int IdTipoJogador { get; set; }

            [Required(ErrorMessage = "Campo deve ser informado")]
            [Display(Name = "Curso")]
            public int IdCurso { get; set; }

            [Required(ErrorMessage = "Campo deve ser informado")]
            [Display(Name = "Nível ensino")]
            public int IdNivelensino { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ViewData["ListaCurso"] = new SelectList(_context.Curso.OrderBy(x => x.Descricao), "IdCurso", "Descricao");
            ViewData["ListaNivelEnsino"] = new SelectList(_context.NivelEnsino.OrderBy(x => x.Descricao), "IdNivelEnsino", "Descricao");
            List<TextValue2> tipoJogador = new List<TextValue2>();
            tipoJogador.Add(new TextValue2("1", "Comprador"));
            tipoJogador.Add(new TextValue2("2", "Fornecedor"));
            ViewData["IdTipoJogador"] = new SelectList(tipoJogador.OrderBy(x => x.Value), "Text", "Value");

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            ViewData["ListaCurso"] = new SelectList(_context.Curso.OrderBy(x => x.Descricao), "IdCurso", "Descricao", Input.IdCurso);
            ViewData["ListaNivelEnsino"] = new SelectList(_context.NivelEnsino.OrderBy(x => x.Descricao), "IdNivelEnsino", "Descricao", Input.IdNivelensino);
            List<TextValue2> tipoJogador = new List<TextValue2>();
            tipoJogador.Add(new TextValue2("1", "Comprador"));
            tipoJogador.Add(new TextValue2("2", "Fornecedor"));
            ViewData["IdTipoJogador"] = new SelectList(tipoJogador.OrderBy(x => x.Value), "Text", "Value",Input.IdTipoJogador);
            if (Input.IdTipoJogador == 0)
            {
                TempData["Msg"] = "Por favor selecione o tipo de jogador";
                return Page();
            }
            string tipoUsuario = "";
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var cadastrados = _context.AspNetUserRoles.ToList();
            Boolean CadastroDuplo = false;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Nome = Input.Nome, UserName = Input.Nome, Email = Input.Nome, IdCurso = Input.IdCurso, IdNivelEnsino = Input.IdNivelensino, DataCadastro = clientDate, QtdComoComprador = 0, QtdComoFornecedor = 0 };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //var usuarios = (from u in _context.AspNetUsers
                    //                join p in _context.AspNetUserRoles on u.Id equals p.UserId
                    //                where (u.IdStatusGame == 1 || u.IdStatusGame == 2) && u.IdCurso == user.IdCurso && u.IdNivelEnsino == user.IdNivelEnsino
                    //                select new UsuarioPerfilVM
                    //                {
                    //                    UserId = u.Id,
                    //                    ProfileId = p.RoleId,
                    //                    TipoUsuario = p.RoleId,
                    //                    UserName = u.UserName,
                    //                    IdStatusGame = u.IdStatusGame
                    //                }).ToList();
                    //UsuarioPerfilVM comprador = new UsuarioPerfilVM();
                    if (Input.IdTipoJogador == 1)
                    {
                        tipoUsuario = "2";
                        HttpContext.Session.SetSession("Registro2", 2);
                        CompradorFornecedor compradorFornecedor = new CompradorFornecedor();
                        compradorFornecedor.IdComprador = user.Id;
                        _context.Add(compradorFornecedor);
                        var usuario = _context.AspNetUsers.Where(x => x.Id == user.Id).SingleOrDefault();
                        usuario.IdStatusGame = 1;
                        _context.Update(usuario);
                    }
                    else
                    {
                        if (Input.NomeComprador == null)
                        {
                            TempData["Msg"] = "Informe o nome do comprador de referencia";
                            return Page();
                        }
                        var comprador = _context.AspNetUsers.Where(x=> x.UserName == Input.NomeComprador).SingleOrDefault();
                        //   comprador = usuarios.Where(x => x.IdStatusGame == 1 && x.TipoUsuario == "2").Last(); // comprador
                        var compradorFornecedor = _context.CompradorFornecedor.Where(x => x.IdComprador == comprador.Id && x.IdFornecedor == null).FirstOrDefault();
                        if (compradorFornecedor != null)
                        {
                            tipoUsuario = "3"; // fornecedor
                            HttpContext.Session.SetSession("Registro2", 3);
                            compradorFornecedor.IdFornecedor = user.Id;
                            _context.Update(compradorFornecedor);
                            var usuario = _context.AspNetUsers.Where(x => x.Id == user.Id).SingleOrDefault();
                            usuario.IdStatusGame = 2;
                            _context.Update(usuario);
                        }
                        else
                        {
                            tipoUsuario = "3"; // fornecedor
                            HttpContext.Session.SetSession("Registro2", 3);
                            CompradorFornecedor compradorFornecedor1 = new CompradorFornecedor();
                            compradorFornecedor1.IdComprador = comprador.Id;
                            compradorFornecedor1.IdFornecedor = user.Id;
                            _context.Add(compradorFornecedor1);
                            var usuario = _context.AspNetUsers.Where(x => x.Id == comprador.Id).SingleOrDefault();
                            var qtdFornecedores = _context.CompradorFornecedor.Where(x => x.IdComprador == comprador.Id).Count();
                            if (qtdFornecedores > 3)
                                usuario.IdStatusGame = 2;
                            else
                                usuario.IdStatusGame = 1;
                            _context.Update(usuario);
                            var usuario2 = _context.AspNetUsers.Where(x => x.Id == user.Id).SingleOrDefault();
                            usuario2.IdStatusGame = 2;
                            _context.Update(usuario2);
                        }
                    }
                    AspNetUserRoles aspNetUserRoles = new AspNetUserRoles();
                    aspNetUserRoles.UserId = user.Id;
                    aspNetUserRoles.RoleId = tipoUsuario;
                    _context.Add(aspNetUserRoles);
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/Identity/Account/Register2");
                }

                foreach (var error in result.Errors)
                {
                    if (error.Description.Contains(""))
                    {
                        CadastroDuplo = true;
                    }
                }
             }
            if (CadastroDuplo)
            {
                TempData["Msg"] = "Já existe um usuário com este nome cadastrado, por favor escolha outro nome de usuário";
            }
            return Page();
        }
    }
}
