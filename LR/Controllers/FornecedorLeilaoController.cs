using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LR.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TimeZoneConverter;

namespace LR.Controllers
{
    public class FornecedorLeilaoController : Controller
    {

        private readonly LeilaoReversoContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public FornecedorLeilaoController(
                        LeilaoReversoContext context,
                        SignInManager<ApplicationUser> signInManager,
                        UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Create()
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            var user = await _context.AspNetUsers.FindAsync(_userManager.GetUserId(User));
            var compradorFornecedor = await _context.CompradorFornecedor.Where(x => x.IdFornecedor == user.Id).FirstOrDefaultAsync();
            var usuarios = (from u in _context.AspNetUsers
                            join p in _context.AspNetUserRoles on u.Id equals p.UserId
                            where (u.IdStatusGame == 1 || u.IdStatusGame == 2) && u.IdCurso == user.IdCurso
                            select new UsuarioPerfilVM
                            {
                                UserId = u.Id,
                                ProfileId = p.RoleId,
                                TipoUsuario = p.RoleId,
                                UserName = u.UserName,
                                IdStatusGame = u.IdStatusGame
                            }).ToList();
            int fornecedores = 0;
            var leilao = await _context.Leilao.Include(f => f.IdTipoLeilaoNavigation)
                        .Where(x => x.IdCurso == user.IdCurso &&
                                    x.IdNivelEnsino == user.IdNivelEnsino &&
                                    x.IndicadorAtivo == true &&
                                    x.DataFechamento != null &&
                                    x.SelecaoRealizada != true &&
                                    x.IdComprador == compradorFornecedor.IdComprador && 
                                    x.DataFechamento > clientDate).FirstOrDefaultAsync();
            if (leilao != null)
            {
                fornecedores = _context.FornecedorLeilao
                                .Where(x => x.IdLeilao == leilao.IdLeilao &&
                                x.IndicadorAtivo == true).Count();
            } else
            {
                TempData["Msg"] = "LR foi encerrado, cancelado ou ainda não existe. Por favor aguarde o comprador criar novo LR e enviar um convite";
                return RedirectToAction("Index", "Home");
            }
            bool? ehComprador = null;
            if (usuarios.Count == 0 || usuarios.Where(x => x.IdStatusGame == 1 && x.TipoUsuario == "2").Count() == 0)
            {
                ehComprador = true;
            }
            else
            {
                ehComprador = false;
                var comprador = usuarios.Where(x => x.IdStatusGame == 1 && x.TipoUsuario == "2").Last(); // comprador
                if (leilao == null && comprador != null)
                {
                    ehComprador = true;
                }
                if (fornecedores > 3)
                {
                    ehComprador = true;
                }
            }
            if (leilao != null)
            {
                if (leilao.SelecaoRealizada == true)
                    ehComprador = true;
            } 
 
            

            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("Registro2")))
            {
                int registro2 = int.Parse(HttpContext.Session.GetString("Registro2"));
                if (ehComprador == true && registro2 == 3)
                {
                    ehComprador = false;
                }
            }
            ehComprador = false;
 

            List<TextValue2> nivelRisco = new List<TextValue2>();
            nivelRisco.Add(new TextValue2("1", "Disposto ao risco"));
            nivelRisco.Add(new TextValue2("2", "Risco neutro"));
            nivelRisco.Add(new TextValue2("3", "Contrário ao risco"));
            Random random = new Random();
            int nivel = random.Next(1, 4);
            ViewData["NivelRisco"] = new SelectList(nivelRisco, "Text", "Value", nivel);
            ViewData["IdNivelRisco"] = nivel;
            List<TextValue2> custoServicoFrete = new List<TextValue2>();
            custoServicoFrete.Add(new TextValue2("1", "R$ 1,00"));
            custoServicoFrete.Add(new TextValue2("2", "R$ 1,20"));
            custoServicoFrete.Add(new TextValue2("3", "R$ 1,50"));
            Random random2 = new Random();
            double custo = random2.Next(1, 4);
            ViewData["CustoServicoFrete"] = new SelectList(custoServicoFrete, "Text", "Value", custo);
            ViewData["IdCusto"] = custo;
            List<TextValue2> atributoPrecoPrazo = new List<TextValue2>();
            atributoPrecoPrazo.Add(new TextValue2("1", "R$ 1,50 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("2", "R$ 1,50 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("3", "R$ 1,50 - 30 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("4", "R$ 2,00 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("5", "R$ 2,00 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("6", "R$ 2,00 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("7", "R$ 2,50 -  5 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("8", "R$ 2,50 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("9", "R$ 2,50 - 15 dias p/ entrega"));
            ViewData["AtributoPrecoPrazo"] = new SelectList(atributoPrecoPrazo, "Text", "Value");
            ViewData["IdPrecoPrazo"] = nivel;
            List<TextValue2> justificativa = new List<TextValue2>();
            justificativa.Add(new TextValue2("1", "Não há lucro"));
            justificativa.Add(new TextValue2("2", "Lucro baixo"));
            justificativa.Add(new TextValue2("3", "Não tenho interesse"));
            ViewData["IdJustificativa"] = new SelectList(justificativa, "Text", "Value");
            List<string> atributoPrecoPrazo2 = new List<string>();
            atributoPrecoPrazo2.Add("R$ 1,50 - 15 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 1,50 - 20 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 1,50 - 30 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,00 - 10 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,00 - 15 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,00 - 20 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,50 -  5 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,50 - 10 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,50 - 15 dias p/ entrega");
            ViewData["IdTipoLeilao"] = leilao.IdTipoLeilao;
            ViewBag.DataAberturaLance = string.Format("{0:yyyy-MM-dd HH:mm:ss}", leilao.DataAberturaLance);
            ViewBag.DataFechamento = string.Format("{0:yyyy-MM-dd HH:mm:ss}", leilao.DataFechamento);
            ViewBag.Requisicao = leilao.Requisicao;
            ViewBag.ValorInicial = leilao.ValorInicial;
            ViewBag.IdLeilao = leilao.IdLeilao;
            ViewBag.IdOpcaoComprador = leilao.IdAtributoPrecoPrazo1;
            ViewBag.TipoLeilaoDescricao = leilao.IdTipoLeilaoNavigation.Descricao;
            var restanteData = leilao.DataFechamento.Value - leilao.DataAberturaLance.Value;
            if (restanteData.TotalMilliseconds < 0)
            {
                ViewBag.MinutoEncerramento = 1;
                ViewBag.SegundoEncerramento = 0;
            }
            else
            {
                ViewBag.MinutoEncerramento = restanteData.Minutes;
                ViewBag.SegundoEncerramento = restanteData.Seconds;
            }
            var fornecedorLeilao = await _context.FornecedorLeilao
                .Where(x => x.IndicadorAtivo == true && x.Classificacao == 1)
                .Take(3).OrderByDescending(x => x.Data).ToListAsync();
            List<HistoricoLeilaoMV> listHistoricoLeilaoMV = new List<HistoricoLeilaoMV>();
            foreach (var item in fornecedorLeilao)
            {
                HistoricoLeilaoMV historicoLeilaoMV = new HistoricoLeilaoMV();
                var lHistorico = await _context.Leilao.Where(x => x.IdLeilao == item.IdLeilao).SingleOrDefaultAsync();
                historicoLeilaoMV.DataFechamento = lHistorico.DataFechamento;
                if (lHistorico.IdTipoLeilao == 1)
                    historicoLeilaoMV.TipoLeilao = "Selado de primeiro preço";
                else if (lHistorico.IdTipoLeilao == 2)
                    historicoLeilaoMV.TipoLeilao = "Selado de segundo preço";
                else if (lHistorico.IdTipoLeilao == 3)
                    historicoLeilaoMV.TipoLeilao = "Aberto de lance crescente";
                else if (lHistorico.IdTipoLeilao == 4)
                    historicoLeilaoMV.TipoLeilao = "Aberto de lance decrescente";
                historicoLeilaoMV.DescAtributoPrecoPrazo = atributoPrecoPrazo2[item.AtributoPrecoPrazo.Value-1];
                listHistoricoLeilaoMV.Add(historicoLeilaoMV);
                historicoLeilaoMV.IdOpcaoComprador = lHistorico.IdAtributoPrecoPrazo1;
            }
            ViewBag.HistoricoLeilaoMV = listHistoricoLeilaoMV;
            ViewBag.IdUser = user.Id;
            ViewBag.Perfil = "Fornecedor";
            ViewBag.IdComprador = leilao.IdComprador;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFornecedorLeilao,IdFornecedor,IdLeilao,Lance,Data,Classificacao,CustoServicoFrete,NivelRisco,AtributoPrecoPrazo,IndicadorAtivo")] FornecedorLeilao fornecedorLeilao, short? IdNivelRisco, int? IdCusto, int? IdPrecoPrazo)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            List<TextValue2> nivelRisco = new List<TextValue2>();
            nivelRisco.Add(new TextValue2("1", "Disposto ao risco"));
            nivelRisco.Add(new TextValue2("2", "Risco neutro"));
            nivelRisco.Add(new TextValue2("3", "Contrário ao risco"));
            ViewData["NivelRisco"] = new SelectList(nivelRisco, "Text", "Value", fornecedorLeilao.NivelRisco);
            List<TextValue2> custoServicoFrete = new List<TextValue2>();
            custoServicoFrete.Add(new TextValue2("1", "R$ 1,00"));
            custoServicoFrete.Add(new TextValue2("2", "R$ 1,20"));
            custoServicoFrete.Add(new TextValue2("3", "R$ 1,50"));
            ViewData["CustoServicoFrete"] = new SelectList(custoServicoFrete, "Text", "Value", fornecedorLeilao.CustoServicoFrete);
            List<TextValue2> atributoPrecoPrazo = new List<TextValue2>();
            atributoPrecoPrazo.Add(new TextValue2("1", "R$ 1,50 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("2", "R$ 1,50 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("3", "R$ 1,50 - 30 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("4", "R$ 2,00 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("5", "R$ 2,00 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("6", "R$ 2,00 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("7", "R$ 2,50 -  5 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("8", "R$ 2,50 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("9", "R$ 2,50 - 15 dias p/ entrega"));
            ViewData["AtributoPrecoPrazo"] = new SelectList(atributoPrecoPrazo, "Text", "Value", fornecedorLeilao.AtributoPrecoPrazo);
            List<TextValue2> justificativa = new List<TextValue2>();
            justificativa.Add(new TextValue2("1", "Não há lucro"));
            justificativa.Add(new TextValue2("2", "Não quero me arriscar"));
            justificativa.Add(new TextValue2("3", "Não tenho interesse"));
            ViewData["IdJustificativa"] = new SelectList(justificativa, "Text", "Value");
            fornecedorLeilao.NivelRisco = IdNivelRisco;
            fornecedorLeilao.CustoServicoFrete = IdCusto;
            List<string> atributoPrecoPrazo2 = new List<string>();
            atributoPrecoPrazo2.Add("R$ 1,50 - 15 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 1,50 - 20 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 1,50 - 30 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,00 - 10 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,00 - 15 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,00 - 20 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,50 -  5 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,50 - 10 dias p/ entrega");
            atributoPrecoPrazo2.Add("R$ 2,50 - 15 dias p/ entrega");
            var hFornecedorLeilao = await _context.FornecedorLeilao.Where(x => x.IndicadorAtivo == true && x.Classificacao == 1)
                                    .Take(3).OrderByDescending(x => x.Data).ToListAsync();
            var leilao = await _context.Leilao.Include(f => f.IdTipoLeilaoNavigation)
                                .Where(x => x.IdLeilao == fornecedorLeilao.IdLeilao && x.IndicadorAtivo == true && x.DataFechamento > clientDate).FirstOrDefaultAsync();
            if (leilao == null)
            {
                TempData["Msg"] = "LR foi encerrado ou cancelado. Por favor aguarde criar novo LR e receber um convite";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IdLeilao = leilao.IdLeilao;
            ViewBag.DataAberturaLance = leilao.DataAberturaLance;
            ViewBag.DataFechamento = leilao.DataFechamento;
            ViewBag.Requisicao = leilao.Requisicao;
            ViewBag.ValorInicial = leilao.ValorInicial;
            ViewBag.IdOpcaoComprador = leilao.IdAtributoPrecoPrazo1;
            ViewBag.TipoLeilaoDescricao = leilao.IdTipoLeilaoNavigation.Descricao;
            List<HistoricoLeilaoMV> listHistoricoLeilaoMV = new List<HistoricoLeilaoMV>();
            foreach (var item in hFornecedorLeilao)
            {
                HistoricoLeilaoMV historicoLeilaoMV = new HistoricoLeilaoMV();
                var lHistorico = await _context.Leilao.Where(x => x.IdLeilao == item.IdLeilao).SingleOrDefaultAsync();
                historicoLeilaoMV.DataFechamento = lHistorico.DataFechamento;
                if (lHistorico.IdTipoLeilao == 1)
                    historicoLeilaoMV.TipoLeilao = "Selado de primeiro preço";
                else if (lHistorico.IdTipoLeilao == 2)
                    historicoLeilaoMV.TipoLeilao = "Selado de segundo preço";
                else if (lHistorico.IdTipoLeilao == 3)
                    historicoLeilaoMV.TipoLeilao = "Aberto de lance crescente";
                else if (lHistorico.IdTipoLeilao == 4)
                    historicoLeilaoMV.TipoLeilao = "Aberto de lance decrescente";
                historicoLeilaoMV.DescAtributoPrecoPrazo = atributoPrecoPrazo2[item.AtributoPrecoPrazo.Value-1];
                historicoLeilaoMV.IdOpcaoComprador = lHistorico.IdAtributoPrecoPrazo1;
                listHistoricoLeilaoMV.Add(historicoLeilaoMV);
            }
            ViewBag.HistoricoLeilaoMV = listHistoricoLeilaoMV;
            if (leilao.DataFechamento != null && leilao.DataFechamento.Value < clientDate)
            {
                TempData["Msg"] = "Não há como incluir novo lance pois este leilão já está fechado";
                return View(fornecedorLeilao);
            }
            if (fornecedorLeilao.AtributoPrecoPrazo == null)
            {
                TempData["Msg"] = "Por favor selecione quais os atributos: valor do frete com o prazo de entrega que entender mais aderente ao risco e ao custo de entrega do frete";
                return View(fornecedorLeilao);
            }
            if (ModelState.IsValid)
            {
                fornecedorLeilao.Data = clientDate;
                fornecedorLeilao.IndicadorAtivo = true;
                fornecedorLeilao.IdFornecedor = _userManager.GetUserId(User);
                _context.Add(fornecedorLeilao);
                await _context.SaveChangesAsync();
                var fLeilaoLance = await _context.FornecedorLeilaoLance.Where(x => x.IdFornecedorLeilao == fornecedorLeilao.IdFornecedorLeilao).CountAsync();
                FornecedorLeilaoLance fornecedorLeilaoLance = new FornecedorLeilaoLance();
                fornecedorLeilaoLance.IdFornecedorLeilao = fornecedorLeilao.IdFornecedorLeilao;
                fornecedorLeilaoLance.Rodada = leilao.Rodada;
                fornecedorLeilaoLance.Data = clientDate;
                fornecedorLeilaoLance.Sequencia = fLeilaoLance + 1;
                _context.FornecedorLeilaoLance.Add(fornecedorLeilaoLance);
                fornecedorLeilaoLance.AtributoPrecoPrazo = fornecedorLeilao.AtributoPrecoPrazo;
                await _context.SaveChangesAsync();
                return RedirectToAction("PainelFornecedor","FornecedorLeilao", new { @id = fornecedorLeilao.IdFornecedorLeilao });
            }
            return View(fornecedorLeilao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NaoSubmissao([Bind("IdFornecedorLeilao,IdFornecedor,IdLeilao,Lance,Data,Classificacao,CustoServicoFrete,NivelRisco,AtributoPrecoPrazo,IndicadorAtivo,IdJustificativa")] FornecedorLeilao fornecedorLeilao)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            List<TextValue2> justificativa = new List<TextValue2>();
            justificativa.Add(new TextValue2("1", "Não há lucro"));
            justificativa.Add(new TextValue2("2", "Não quero me arriscar"));
            justificativa.Add(new TextValue2("3", "Não tenho interesse"));
            ViewData["IdJustificativa"] = new SelectList(justificativa, "Text", "Value");
            if (fornecedorLeilao.IdJustificativa == null)
            {
                TempData["Msg"] = "Por favor selecione o motivo por não submeter o lance";
                return RedirectToAction(nameof(Create));
            }
            var cFornecedor = await _context.CompradorFornecedor.Where(x => x.IdFornecedor == _userManager.GetUserId(User)).FirstOrDefaultAsync();
            var hFornecedorLeilao = await _context.FornecedorLeilao.Where(x => x.IndicadorAtivo == true && x.Classificacao == 1)
                                    .Take(3).OrderByDescending(x => x.Data).ToListAsync();
            var leilao = await _context.Leilao.Include(f => f.IdTipoLeilaoNavigation)
                                .Where(x => x.IdComprador == cFornecedor.IdComprador && x.IndicadorAtivo == true && x.DataFechamento > clientDate).FirstOrDefaultAsync();
            if (leilao == null)
            {
                TempData["Msg"] = "LR foi encerrado ou cancelado. Por favor aguarde o comprador criar novo LR e receber um convite";
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                if (cFornecedor != null)
                {
                    if (leilao == null)
                    {
                        var fLeilao = _context.FornecedorLeilao.Where(x => x.IdFornecedor == cFornecedor.IdFornecedor && x.IdLeilao == leilao.IdLeilao && x.IndicadorAtivo == true);
                        TempData["Msg"] = "Não há Leilão Reverso cadastrado ou ativo";
                        return RedirectToAction(nameof(Index));
                    }
                    var compradorFornecedor = await _context.CompradorFornecedor.Where(x => x.IdFornecedor == fornecedorLeilao.IdFornecedor).FirstOrDefaultAsync();
                    fornecedorLeilao.IdLeilao = leilao.IdLeilao;
                }
                fornecedorLeilao.Data = clientDate;
                fornecedorLeilao.IndicadorAtivo = false;
                fornecedorLeilao.IdFornecedor = _userManager.GetUserId(User);
                _context.Add(fornecedorLeilao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(fornecedorLeilao);
        }

 
        public async Task<IActionResult> CancelarLeilao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorLeilao = await _context.FornecedorLeilao
                .Include(f => f.IdFornecedorNavigation)
                .Include(f => f.IdLeilaoNavigation)
                .FirstOrDefaultAsync(m => m.IdFornecedorLeilao == id);
            if (fornecedorLeilao == null)
            {
                return NotFound();
            }
            List<TextValue2> atributoPrecoPrazo = new List<TextValue2>();
            atributoPrecoPrazo.Add(new TextValue2("1", "R$ 1,50 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("2", "R$ 1,50 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("3", "R$ 1,50 - 30 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("4", "R$ 2,00 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("5", "R$ 2,00 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("6", "R$ 2,00 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("7", "R$ 2,50 -  5 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("8", "R$ 2,50 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("9", "R$ 2,50 - 15 dias p/ entrega"));
            ViewData["AtributoPrecoPrazo"] = new SelectList(atributoPrecoPrazo, "Text", "Value", fornecedorLeilao.AtributoPrecoPrazo);
            List<TextValue2> justificativa = new List<TextValue2>();
            justificativa.Add(new TextValue2("1", "Não há lucro"));
            justificativa.Add(new TextValue2("2", "Não quero me arriscar"));
            justificativa.Add(new TextValue2("3", "Não me interessou"));
            ViewData["IdJustificativa"] = new SelectList(justificativa, "Text", "Value");
            ViewBag.IdUser = fornecedorLeilao.IdFornecedor;
            ViewBag.Perfil = "Fornecedor";
            ViewBag.IdComprador = fornecedorLeilao.IdLeilaoNavigation.IdComprador;
            return View(fornecedorLeilao);
        }

 
        [HttpPost, ActionName("CancelarLeilao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelarLeilaoConfirmed(int id, short? idJustificativa)
        {
            var fornecedorLeilao = await _context.FornecedorLeilao.FindAsync(id);
            List<TextValue2> justificativa = new List<TextValue2>();
            justificativa.Add(new TextValue2("1", "Não há lucro"));
            justificativa.Add(new TextValue2("2", "Não quero me arriscar"));
            justificativa.Add(new TextValue2("3", "Não me interessou"));
            ViewData["IdJustificativa"] = new SelectList(justificativa, "Text", "Value", idJustificativa);
            if (idJustificativa == null)
            {
                TempData["Msg"] = "Por favor selecione a motivo por não submeter o lance neste LR";
                return View(fornecedorLeilao);
            }
            fornecedorLeilao.IndicadorAtivo = false;
            fornecedorLeilao.IdJustificativa = idJustificativa;
            _context.FornecedorLeilao.Update(fornecedorLeilao);
            var usuario = await _context.AspNetUsers.Where(x => x.Id == fornecedorLeilao.IdFornecedor).SingleOrDefaultAsync();
            usuario.QtdComoFornecedor = usuario.QtdComoFornecedor + 1;
            _context.AspNetUsers.Update(usuario);
            await _context.SaveChangesAsync();
            TempData["Msg"] = "Seu lance foi excluido. Por favor inclua novo lance.";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PainelFornecedor(int? id)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            FornecedorLeilao fornecedorLeilao = new FornecedorLeilao();
            if (id == null)
            {
                fornecedorLeilao = await _context.FornecedorLeilao.FirstOrDefaultAsync(m => m.IdFornecedor == _userManager.GetUserId(User) && m.IndicadorAtivo == true && m.Classificacao == null);
            }
            else
            {
                fornecedorLeilao = await _context.FornecedorLeilao.FirstOrDefaultAsync(m => m.IdFornecedorLeilao == id && m.IndicadorAtivo == true && m.Classificacao == null);
            }
            if (fornecedorLeilao == null)
            {
                TempData["Msg"] = "Seu lance foi cancelado. Por favor inclua novo lance.";
                return RedirectToAction(nameof(Create));
            }
            if (fornecedorLeilao.Classificacao != null)
            {
                TempData["Msg"] = "Este LR já foi fechado";
                return RedirectToAction("Resultado", "FornecedorLeilao");
            }
            ViewBag.IdFornecedor = fornecedorLeilao.IdFornecedor;
            var leilao = await _context.Leilao.Include(l => l.IdCompradorNavigation).Include(l => l.IdTipoLeilaoNavigation)
                                .Where(m => m.IdLeilao == fornecedorLeilao.IdLeilao && m.IndicadorAtivo == true && m.DataFechamento > clientDate).FirstOrDefaultAsync();
            if (leilao == null)
            {
                TempData["Msg"] = "LR foi cancelado ou fechado pelo comprador. Por favor aguarde ser chamado para novo LR";
                return RedirectToAction("Index","Home");
            }
            ViewBag.IdComprador = leilao.IdComprador;
            int rodada = 0;
            if (leilao.Rodada > 0)
            {
                rodada = leilao.Rodada.Value;
            }
            ViewBag.Rodada = rodada;
            ViewBag.DataAberturaLance = string.Format("{0:yyyy-MM-dd HH:mm:ss}", leilao.DataAberturaLance);
            ViewBag.DataFechamento = string.Format("{0:yyyy-MM-dd HH:mm:ss}", leilao.DataFechamento);
            ViewBag.DataFechamentoRodada1 = string.Format("{0:yyyy-MM-dd HH:mm:ss}", leilao.DataFechamentoRodada1);
            ViewBag.DataFechamentoRodada2 = string.Format("{0:yyyy-MM-dd HH:mm:ss}", leilao.DataFechamentoRodada2);
            ViewBag.DataFechamentoRodada3 = string.Format("{0:yyyy-MM-dd HH:mm:ss}", leilao.DataFechamentoRodada3);
            var restanteData = leilao.DataFechamento.Value - leilao.DataAberturaLance.Value;
            int tempoTotal = rodada * 2;
            DateTime dataFechamentoRodada = new DateTime();
            if (rodada == 1)
            {
                dataFechamentoRodada = leilao.DataFechamentoRodada1.Value;
            } else  
            if (rodada == 2)
            {
                dataFechamentoRodada = leilao.DataFechamentoRodada2.Value;
            } else if (rodada == 3)
            {
                dataFechamentoRodada = leilao.DataFechamentoRodada3.Value;
            }
            ViewBag.DataFechamentoRodada = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dataFechamentoRodada);
            TimeSpan restanteDataRodada2 = leilao.DataFechamento.Value.AddMinutes(-tempoTotal) - leilao.DataAberturaLance.Value;
            TimeSpan restanteDataRodada = dataFechamentoRodada - leilao.DataAberturaLance.Value;
            if (restanteData.TotalMilliseconds < 0)
            {
                ViewBag.MinutoEncerramento = 1;
                ViewBag.SegundoEncerramento = 0;
            }
            else
            {
                ViewBag.MinutoEncerramento = restanteData.Minutes;
                ViewBag.SegundoEncerramento = restanteData.Seconds;
                ViewBag.MinutoEncerramentoRodada = restanteDataRodada.Minutes;
                ViewBag.SegundoEncerramentoRodada = restanteDataRodada.Seconds;
            }
            ViewBag.IdLeilao = leilao.IdLeilao;
            ViewBag.IdTipoLeilao = leilao.IdTipoLeilao;
            ViewBag.IdFornecedor = fornecedorLeilao.IdFornecedor;
            ViewBag.IdFornecedorLeilao = fornecedorLeilao.IdFornecedorLeilao;
            List<string> atributoPrecoPrazo = new List<string>();
            atributoPrecoPrazo.Add("R$ 1,50 - 15 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 20 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 30 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 10 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 15 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 20 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,50 -  5 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,50 - 10 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,50 - 15 dias p/ entrega");
            ViewBag.DescAtributoPrecoPrazo = atributoPrecoPrazo[fornecedorLeilao.AtributoPrecoPrazo.Value-1];
            List<TextValue2> atributoPrecoPrazo2 = new List<TextValue2>();
            atributoPrecoPrazo2.Add(new TextValue2("1", "R$ 1,50 - 15 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("2", "R$ 1,50 - 20 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("3", "R$ 1,50 - 30 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("4", "R$ 2,00 - 10 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("5", "R$ 2,00 - 15 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("6", "R$ 2,00 - 20 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("7", "R$ 2,50 -  5 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("8", "R$ 2,50 - 10 dias p/ entrega"));
            atributoPrecoPrazo2.Add(new TextValue2("9", "R$ 2,50 - 15 dias p/ entrega"));
            ViewData["AtributoPrecoPrazo"] = new SelectList(atributoPrecoPrazo2, "Text", "Value");
            var fornecedorLeilaoTop = await _context.FornecedorLeilao
                 .Where(x => x.IndicadorAtivo == true && x.Classificacao == 1)
                 .Take(3).OrderByDescending(x => x.Data).ToListAsync();
            List<HistoricoLeilaoMV> listHistoricoLeilaoMV = new List<HistoricoLeilaoMV>();
            foreach (var item in fornecedorLeilaoTop)
            {
                HistoricoLeilaoMV historicoLeilaoMV = new HistoricoLeilaoMV();
                var lHistorico = await _context.Leilao.Where(x => x.IdLeilao == item.IdLeilao).SingleOrDefaultAsync();
                historicoLeilaoMV.DataFechamento = lHistorico.DataFechamento;
                if (lHistorico.IdTipoLeilao == 1)
                    historicoLeilaoMV.TipoLeilao = "Selado de primeiro preço";
                else if (lHistorico.IdTipoLeilao == 2)
                    historicoLeilaoMV.TipoLeilao = "Selado de segundo preço";
                else if (lHistorico.IdTipoLeilao == 3)
                    historicoLeilaoMV.TipoLeilao = "Aberto de lance crescente";
                else if (lHistorico.IdTipoLeilao == 4)
                    historicoLeilaoMV.TipoLeilao = "Aberto de lance decrescente";
                historicoLeilaoMV.DescAtributoPrecoPrazo = atributoPrecoPrazo[item.AtributoPrecoPrazo.Value - 1];
                historicoLeilaoMV.IdOpcaoComprador = lHistorico.IdAtributoPrecoPrazo1;
                listHistoricoLeilaoMV.Add(historicoLeilaoMV);
            }
            ViewBag.HistoricoLeilaoMV = listHistoricoLeilaoMV;
            ViewBag.IdUser = fornecedorLeilao.IdFornecedor;
            ViewBag.Perfil = "Fornecedor";
            ViewBag.IdComprador = leilao.IdComprador;
            return View(leilao);
        }

       [HttpGet]
        public async Task<IActionResult> SubmeterNovoLance(int id, int? AtributoPrecoPrazo)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            var fornecedorLeilao = await _context.FornecedorLeilao.FindAsync(id);
            if (fornecedorLeilao.IndicadorAtivo == false)
            {
                TempData["Msg"] = "Seu lance foi cancelado. Por favor inclua novo lance.";
                return RedirectToAction(nameof(Create));
            }
            var fLeilaoLance = await _context.FornecedorLeilaoLance.Where(x => x.IdFornecedorLeilao == fornecedorLeilao.IdFornecedorLeilao).CountAsync();
            var cFornecedor = await _context.CompradorFornecedor.Where(x => x.IdFornecedor == fornecedorLeilao.IdFornecedor).FirstOrDefaultAsync();
            var leilao = await _context.Leilao
                        .Where(x => x.IdComprador == cFornecedor.IdComprador &&
                        x.IndicadorAtivo == true && x.DataFechamento > clientDate).FirstOrDefaultAsync();
            if (leilao == null)
            {
                TempData["Msg"] = "LR foi encerrado ou cancelado. Por favor aguarde o comprador criar novo LR e receber um convite";
                return RedirectToAction("Index", "Home");
            }
            fornecedorLeilao.AtributoPrecoPrazo = AtributoPrecoPrazo;
            fornecedorLeilao.Data = clientDate;
            _context.FornecedorLeilao.Update(fornecedorLeilao);
            FornecedorLeilaoLance fornecedorLeilaoLance = new FornecedorLeilaoLance();
            fornecedorLeilaoLance.IdFornecedorLeilao = fornecedorLeilao.IdFornecedorLeilao;
            fornecedorLeilaoLance.Rodada = leilao.Rodada;
            fornecedorLeilaoLance.Data = clientDate;
            fornecedorLeilaoLance.AtributoPrecoPrazo = AtributoPrecoPrazo;
            fornecedorLeilaoLance.Sequencia = fLeilaoLance+1;
            _context.FornecedorLeilaoLance.Add(fornecedorLeilaoLance);
            await _context.SaveChangesAsync();
            TempData["Msg"] = "Seu novo lance foi gravado com sucesso.";
            return RedirectToAction("PainelFornecedor", "FornecedorLeilao", new { @id = fornecedorLeilao.IdFornecedorLeilao });
        }

        public async Task<IActionResult> Vencedor(int? id)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            Leilao leilao = null;
            if (id !=  null)
                leilao = await _context.Leilao.Where(x => x.IdLeilao == id && x.IndicadorAtivo == true).SingleOrDefaultAsync();
            else
            {
                string idUser = _userManager.GetUserId(User);
                leilao = await _context.Leilao.Where(x => x.IdComprador == idUser && x.IndicadorAtivo == true && x.SelecaoRealizada == null).OrderByDescending(x=>x.DataCadastro).FirstOrDefaultAsync();
            }
            if (leilao == null)
            {
                TempData["Msg"] = "Não há nenhuma apuração LR concluído e apurado para este comprador";
                return RedirectToAction("Index", "Home");
            }
            if (leilao.SelecaoRealizada == true)
            {
                TempData["Msg"] = "A seleção de vencedor(es) do LR atual já foi apurada. Por favor clique em Resultados";
                return RedirectToAction("Index", "Home");
            }
            var fornecedorLeilaoLista = _context.FornecedorLeilao.Where(x => x.IdLeilao == leilao.IdLeilao && x.IndicadorAtivo == true);
 
            int i = 1;
            Boolean gravou = false;
            List<TextValue2> atributoPrecoPrazo = new List<TextValue2>();
            atributoPrecoPrazo.Add(new TextValue2("1", "R$ 1,50 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("2", "R$ 1,50 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("3", "R$ 1,50 - 30 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("4", "R$ 2,00 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("5", "R$ 2,00 - 15 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("6", "R$ 2,00 - 20 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("7", "R$ 2,50 -  5 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("8", "R$ 2,50 - 10 dias p/ entrega"));
            atributoPrecoPrazo.Add(new TextValue2("9", "R$ 2,50 - 15 dias p/ entrega"));
            var pesoPrioridadeLista = await _context.PesoPrioridade.ToListAsync();
            foreach (var item in fornecedorLeilaoLista.Where(x => x.Classificacao == null))
            {
                var pesoPrioridade = pesoPrioridadeLista.Where(x => x.IdOpcaoComprador == leilao.IdAtributoPrecoPrazo1.Value && x.IdOpcaoFornecedor.Value == item.AtributoPrecoPrazo).SingleOrDefault();
                item.PontuacaoPeso = pesoPrioridade.Classificacao;
                var usuFor = await _context.AspNetUsers.Where(x => x.Id == item.IdFornecedor).SingleOrDefaultAsync();
                usuFor.IdStatusGame = 3;
                usuFor.QtdComoFornecedor = usuFor.QtdComoFornecedor + 1;
                _context.AspNetUsers.Update(usuFor);
                _context.FornecedorLeilao.Update(item);
                gravou = true;
            }
            if (gravou)
            {
                await _context.SaveChangesAsync();
                foreach (var item in fornecedorLeilaoLista.Where(x => x.Classificacao == null).OrderBy(x => x.PontuacaoPeso))
                {
                    item.Classificacao = i;
                    if (leilao.QuantidadeVencedores >= i)
                    {
                        item.Vendedor = true;
                    }
                    else
                    {
                        item.Vendedor = false;
                    }
                    _context.Update(item);
                    i = i + 1;
                }
            } else
            {
                TempData["Msg"] = "Ainda não há lances ativos dos fornecedores para poder selecionar o(s) vencedor(es).";
                return RedirectToAction("Index", "Home");
            }
            var usuario = await _context.AspNetUsers.Where(x => x.Id == leilao.IdComprador).SingleOrDefaultAsync();
            usuario.IdStatusGame = 3;
            usuario.QtdComoComprador = usuario.QtdComoComprador + 1;
            _context.AspNetUsers.Update(usuario);
            leilao.DataApuracao = clientDate;
            leilao.SelecaoRealizada = true;
            _context.Leilao.Update(leilao);
            await _context.SaveChangesAsync();
            List<string> atributoPrecoPrazo1 = new List<string>();
            atributoPrecoPrazo1.Add("Menor preço");
            atributoPrecoPrazo1.Add("Menor prazo");
            atributoPrecoPrazo1.Add("Menor preço e prazo");
            ViewBag.OpcaoComprador = atributoPrecoPrazo1[leilao.IdAtributoPrecoPrazo1.Value - 1];
            ViewBag.QtdVencedores = leilao.QuantidadeVencedores;
            return View(await fornecedorLeilaoLista.Include(f => f.IdFornecedorNavigation).OrderBy(x => x.Classificacao).ToListAsync());
        }

        public async Task<IActionResult> Resultado(int? id)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            if (_signInManager.IsSignedIn(User))
            {
                var perfil = User.Identities.First(u => u.IsAuthenticated).FindFirst("Perfil")?.Value.ToString();
                ViewBag.IdUser = _userManager.GetUserId(User);
                ViewBag.Perfil = perfil;
            }
            else
            {
                ViewBag.IdUser = "";
                ViewBag.Perfil = "";
            }
            if (id == null)
            {
                string idUser = _userManager.GetUserId(User);
                var fornecedorLeilao = await _context.FornecedorLeilao.Where(m => m.IdFornecedor == idUser && m.IndicadorAtivo == true).OrderByDescending(x => x.Data).FirstOrDefaultAsync();
                if (fornecedorLeilao != null)
                {
                    var leilao = await _context.Leilao.Where(x => x.IdLeilao == fornecedorLeilao.IdLeilao && x.IndicadorAtivo == true).OrderByDescending(x => x.DataCadastro).FirstOrDefaultAsync();
                    if (leilao != null)
                    {
                        ViewBag.IdUser = fornecedorLeilao.IdFornecedor;
                        ViewBag.Perfil = "Fornecedor";
                        ViewBag.IdComprador = leilao.IdComprador;
                    }
                    List<string> atributoPrecoPrazo1 = new List<string>();
                    atributoPrecoPrazo1.Add("Menor preço");
                    atributoPrecoPrazo1.Add("Menor prazo");
                    atributoPrecoPrazo1.Add("Menor preço e prazo");
                    ViewBag.OpcaoComprador = atributoPrecoPrazo1[leilao.IdAtributoPrecoPrazo1.Value-1];
                    ViewBag.QtdVencedores = leilao.QuantidadeVencedores;
                    var fLeilaoList = await _context.FornecedorLeilao.Include(f => f.IdFornecedorNavigation).Where(x => x.IdLeilao == leilao.IdLeilao &&
                                            x.IndicadorAtivo == true).OrderBy(x => x.Classificacao).ToListAsync();
                    return View(fLeilaoList);
                }
              }
            else
            {
                var leilao = await _context.Leilao.Where(x => x.IdLeilao == id && x.IndicadorAtivo == true).SingleOrDefaultAsync();
                if (leilao != null)
                {
                    var fornecedorLeilao = await _context.FornecedorLeilao.Where(x => x.IdLeilao == id && x.IndicadorAtivo == true).FirstOrDefaultAsync();
                    if (fornecedorLeilao != null)
                    {
                        ViewBag.IdUser = fornecedorLeilao.IdFornecedor;
                        ViewBag.Perfil = "Fornecedor";
                        ViewBag.IdComprador = leilao.IdComprador;
                    }
                    List<string> atributoPrecoPrazo1 = new List<string>();
                    atributoPrecoPrazo1.Add("Menor preço");
                    atributoPrecoPrazo1.Add("Menor prazo");
                    atributoPrecoPrazo1.Add("Menor preço e prazo");
                    ViewBag.OpcaoComprador = atributoPrecoPrazo1[leilao.IdAtributoPrecoPrazo1.Value - 1];
                    ViewBag.QtdVencedores = leilao.QuantidadeVencedores;
                    var fLeilaoList = await _context.FornecedorLeilao.Include(f => f.IdFornecedorNavigation)
                                        .Where(x => x.IdLeilao == leilao.IdLeilao &&
                                            x.Classificacao != null &&
                                            x.IndicadorAtivo == true)
                                        .OrderBy(x => x.Classificacao).ToListAsync();
                    if (fLeilaoList.Count == 0)
                    {
                        TempData["Msg"] = "Ainda não há apuração do LR. Por favor aguarde criação de novo LR";
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        return View(fLeilaoList);
                    }
                }
            }
            TempData["Msg"] = "Ainda não há apuração do LR. Por favor aguarde criação de novo LR";
            return RedirectToAction("Index", "Home");
        }


        private bool FornecedorLeilaoExists(int id)
        {
            return _context.FornecedorLeilao.Any(e => e.IdFornecedorLeilao == id);
        }
    }
}

 