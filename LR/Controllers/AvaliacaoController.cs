using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LR.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly LeilaoReversoContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AvaliacaoController(SignInManager<ApplicationUser> signInManager, 
            LeilaoReversoContext context,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Resumo()
        {
            var user = await _context.AspNetUsers.Where(x => x.Id == _userManager.GetUserId(User)).SingleOrDefaultAsync();
            if (user == null)
            {
                TempData["Msg"] = "Sessão esta expirada, favor efetuar login novamente";
                return View("Index", "Home");
            }
            ResumoJogoMV resumoJogoMV = new ResumoJogoMV();
            resumoJogoMV.IdStatusGame = user.IdStatusGame;
            var userRole = await _context.AspNetUserRoles.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            resumoJogoMV.RoleId = userRole.RoleId;
            var lr = await _context.Leilao.Where(x => x.IdComprador == user.Id).ToListAsync();
            if (lr != null)
            {
                resumoJogoMV.QtdLeilaoAberto = lr.Count();
                resumoJogoMV.QtdLeilaoCancelado = lr.Where(x => x.IndicadorAtivo == false).Count();
            }

            var fornecedor = await _context.FornecedorLeilao.Where(x => x.IdFornecedor == user.Id).ToListAsync();
            foreach (var item in fornecedor)
            {
                var fornecedorLances = await _context.FornecedorLeilaoLance.Where(x => x.IdFornecedorLeilao == item.IdFornecedorLeilao).ToListAsync();
                if (item.IndicadorAtivo == true)
                {
                    resumoJogoMV.QtdLancesValidos = resumoJogoMV.QtdLancesValidos + fornecedorLances.Count();
                    resumoJogoMV.QtdJogos = resumoJogoMV.QtdJogos + 1;
                }
                else
                {
                    resumoJogoMV.QtdLancesCancelados = resumoJogoMV.QtdLancesCancelados + fornecedorLances.Count();
                }
                var leilao = await _context.Leilao.Where(x => x.IdLeilao == item.IdLeilao).SingleOrDefaultAsync();
                if (leilao != null)
                {
                    if ((leilao.QuantidadeVencedores == 1 && item.Classificacao == 1) || 
                         leilao.QuantidadeVencedores == 2 && (item.Classificacao == 1 || item.Classificacao == 2))
                        resumoJogoMV.QtdGanhou = resumoJogoMV.QtdGanhou + 1;
                }
            }
            var perfil = User.Identities.First(u => u.IsAuthenticated).FindFirst("Perfil")?.Value.ToString();
            if (perfil == "Fornecedor")
            {
                var fornecedorLeilao = await _context.FornecedorLeilao.Include(f => f.IdLeilaoNavigation).
                                                Where(m => m.IdFornecedor == _userManager.GetUserId(User) && 
                                                      m.IndicadorAtivo == true).FirstOrDefaultAsync();
                if (fornecedorLeilao != null)
                {
                    ViewBag.IdUser = fornecedorLeilao.IdFornecedor;
                    ViewBag.Perfil = "Fornecedor";
                    ViewBag.IdComprador = fornecedorLeilao.IdLeilaoNavigation.IdComprador;
                }
            }
            return View(resumoJogoMV);
        }

        public async Task<IActionResult> Questionario()
        {
            string idUsuario = _userManager.GetUserId(User);
            ViewBag.IdUsuario = idUsuario;
            var avaliacao =  _context.Avaliacao.Where(x => x.UserId == idUsuario).FirstOrDefault();
            if (avaliacao != null)
            {
                TempData["Msg"] = "Você já tem uma avaliação respondida. Obrigado !";
                return RedirectToAction("Resumo", "Avaliacao");
            }
            var usuario = await _context.AspNetUsers.Where(x => x.Id == idUsuario).SingleOrDefaultAsync();
            if (usuario.QtdComoFornecedor + usuario.QtdComoComprador < 1)
            {
                TempData["Msg"] = "Por favor avalie o jogo após participar de pelo menos 4 partidas";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Questionario(string IdUsuario, int? r1i, int? r2i, string r3i, string r4i, int? r5i, int? r6i, int? r7i )
        {
            var avaliacao = _context.Avaliacao.Where(x => x.UserId == IdUsuario).FirstOrDefault();
            if (avaliacao != null)
            {
                TempData["Msg"] = "Você já tem uma avaliação respondida. Obrigado !";
                return RedirectToAction("Resumo", "Avaliacao");
            }
 
            Avaliacao avaliacao1 = new Avaliacao();
            avaliacao1.UserId = IdUsuario;
            avaliacao1.Pergunta = 1;
            _context.Add(avaliacao1);
            await _context.SaveChangesAsync();
            int? opSelecionado = null;
            RespostaAvaliacao respostaAvaliacao1 = new RespostaAvaliacao();
            respostaAvaliacao1.IdAvaliacao = avaliacao1.IdAvalidacao;
            respostaAvaliacao1.Resposta = r1i.Value;
            _context.Add(respostaAvaliacao1);
            await _context.SaveChangesAsync();

            Avaliacao avaliacao2 = new Avaliacao();
            avaliacao2.UserId = IdUsuario;
            avaliacao2.Pergunta = 2;
            _context.Add(avaliacao2);
            await _context.SaveChangesAsync();
            opSelecionado = null;
            RespostaAvaliacao respostaAvaliacao2 = new RespostaAvaliacao();
            respostaAvaliacao2.IdAvaliacao = avaliacao2.IdAvalidacao;
            respostaAvaliacao2.Resposta = r2i.Value;
            _context.Add(respostaAvaliacao2);
            await _context.SaveChangesAsync();

            Avaliacao avaliacao3 = new Avaliacao();
            avaliacao3.UserId = IdUsuario;
            avaliacao3.Pergunta = 3;
            _context.Add(avaliacao3);
            await _context.SaveChangesAsync();
            opSelecionado = null;
            var r3iArray = r3i.Split(',');
            for (int i = 0; i < r3iArray.Length; i++)
            {
                opSelecionado = int.Parse(r3iArray[i]);
                RespostaAvaliacao respostaAvaliacao3 = new RespostaAvaliacao();
                respostaAvaliacao3.IdAvaliacao = avaliacao3.IdAvalidacao;
                respostaAvaliacao3.Resposta = opSelecionado;
                _context.Add(respostaAvaliacao3);
                await _context.SaveChangesAsync();
            }

            Avaliacao avaliacao4 = new Avaliacao();
            avaliacao4.UserId = IdUsuario;
            avaliacao4.Pergunta = 4;
            _context.Add(avaliacao4);
            await _context.SaveChangesAsync();
            opSelecionado = null;
            var r4iArray = r4i.Split(',');
            for (int i = 0; i < r4iArray.Length; i++)
            {
                opSelecionado = int.Parse(r4iArray[i]);
                RespostaAvaliacao respostaAvaliacao4 = new RespostaAvaliacao();
                respostaAvaliacao4.IdAvaliacao = avaliacao4.IdAvalidacao;
                respostaAvaliacao4.Resposta = opSelecionado;
                _context.Add(respostaAvaliacao4);
                await _context.SaveChangesAsync();
            }

            Avaliacao avaliacao5 = new Avaliacao();
            avaliacao5.UserId = IdUsuario;
            avaliacao5.Pergunta = 5;
            _context.Add(avaliacao5);
            await _context.SaveChangesAsync();
            opSelecionado = null;
            RespostaAvaliacao respostaAvaliacao5 = new RespostaAvaliacao();
            respostaAvaliacao5.IdAvaliacao = avaliacao5.IdAvalidacao;
            respostaAvaliacao5.Resposta = r5i;
            _context.Add(respostaAvaliacao5);
            await _context.SaveChangesAsync();

            Avaliacao avaliacao6 = new Avaliacao();
            avaliacao6.UserId = IdUsuario;
            avaliacao6.Pergunta = 6;
            _context.Add(avaliacao6);
            await _context.SaveChangesAsync();
            opSelecionado = null;
            RespostaAvaliacao respostaAvaliacao6 = new RespostaAvaliacao();
            respostaAvaliacao6.IdAvaliacao = avaliacao6.IdAvalidacao;
            respostaAvaliacao6.Resposta = r6i;
            _context.Add(respostaAvaliacao6);
            await _context.SaveChangesAsync();

            Avaliacao avaliacao7 = new Avaliacao();
            avaliacao7.UserId = IdUsuario;
            avaliacao7.Pergunta = 7;
            _context.Add(avaliacao7);
            await _context.SaveChangesAsync();
            opSelecionado = null;
            RespostaAvaliacao respostaAvaliacao7 = new RespostaAvaliacao();
            respostaAvaliacao7.IdAvaliacao = avaliacao7.IdAvalidacao;
            respostaAvaliacao7.Resposta = r7i;
            _context.Add(respostaAvaliacao7);
            await _context.SaveChangesAsync();
            TempData["Msg"] = "Muito obrigado pela sua contribuição !";
            var usuFor = _context.AspNetUsers.Where(x => x.Id == IdUsuario).SingleOrDefault();
            usuFor.IdStatusGame = 4;
            _context.AspNetUsers.Update(usuFor);
            return RedirectToAction("Resumo", "Avaliacao");
        }

    }
}
 