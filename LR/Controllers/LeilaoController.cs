using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TimeZoneConverter;

namespace LR.Controllers
{
    [Authorize]
    public class LeilaoController : Controller
    {
        private readonly LeilaoReversoContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LeilaoController(LeilaoReversoContext context,
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
            var leilaoAberto = await _context.Leilao
                .Where(x => x.IdComprador == _userManager.GetUserId(User)).ToListAsync();
            int anteriorIdTipoLeilao = 0;
            if (leilaoAberto.Count() > 0)
            {
                var primeiroLeilaoAberto = leilaoAberto.Where(x => x.IndicadorAtivo == true && 
                                            x.DataFechamento > clientDate && 
                                            x.SelecaoRealizada != true).FirstOrDefault();
                if (primeiroLeilaoAberto != null)
                {
                    TempData["Msg"] = "Você já tem um LR ativo no momento";
                    var compradorFornecedor = await _context.CompradorFornecedor.FirstOrDefaultAsync(m => m.IdComprador == _userManager.GetUserId(User));
                    return RedirectToAction("PainelComprador", "CompradorFornecedor", new { @id = compradorFornecedor.IdCompradorFornecedor });
                }
                var ultimoLeilaoFechado = leilaoAberto
                                            .Where(x => x.DataApuracao != null
                                            &&  x.SelecaoRealizada == true
                                            ).
                                            OrderBy(x=>x.DataFechamento).LastOrDefault();
                if (ultimoLeilaoFechado != null)
                {
                    anteriorIdTipoLeilao = ultimoLeilaoFechado.IdTipoLeilao;
                }
            }
            var user = await _context.AspNetUsers.FindAsync(_userManager.GetUserId(User));
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
            var lr = await _context.Leilao.Include(f => f.IdTipoLeilaoNavigation)
                        .Where(x => x.IdCurso == user.IdCurso &&
                                    x.IdNivelEnsino == user.IdNivelEnsino &&
                                    x.IndicadorAtivo == true &&
                                    x.DataFechamento != null &&
                                    x.DataFechamento > clientDate).FirstOrDefaultAsync();
            if (lr != null)
            {
                fornecedores = _context.FornecedorLeilao
                                .Where(x => x.IdLeilao == lr.IdLeilao &&
                                x.IndicadorAtivo == true).Count();
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
                if (lr == null && comprador != null)
                {
                    ehComprador = true;
                }
                if (fornecedores > 3)
                {
                    ehComprador = true;
                }
            }
            if (lr != null)
                if (lr.SelecaoRealizada == true)
                    ehComprador = true;
            int registro2 = 0;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("Registro2")))
            {
                registro2 =  int.Parse(HttpContext.Session.GetString("Registro2"));
                if (ehComprador == false && registro2 == 2)
                {
                    ehComprador = true;
                }
            }
            ehComprador = true;
  
            Random random = new Random();
            int tipoLeilao = 1;
            Leilao leilao = new Leilao();
            if (anteriorIdTipoLeilao == 0)
            {
                tipoLeilao = 1;
                leilao.IdTipoLeilao = tipoLeilao;
            }
            else if (anteriorIdTipoLeilao == 4)
            {
                tipoLeilao = 1;
                leilao.IdTipoLeilao = tipoLeilao;
            }
            else 
            {
                leilao.IdTipoLeilao = anteriorIdTipoLeilao + 1;
                tipoLeilao = leilao.IdTipoLeilao;
            }
            ViewData["IdTipoLeilao"] = new SelectList(_context.TipoLeilao.OrderBy(x => x.Descricao), "IdTipoLeilao", "Descricao", tipoLeilao);
            List<TextValue2> nivelRisco = new List<TextValue2>();
            nivelRisco.Add(new TextValue2("1", "Disposto ao risco"));
            nivelRisco.Add(new TextValue2("2", "Risco neutro"));
            nivelRisco.Add(new TextValue2("3", "Contrário ao risco"));
            Random random2 = new Random();
            int nivel = random2.Next(1, 4);
            ViewData["NivelRisco"] = new SelectList(nivelRisco, "Text", "Value", nivel);
            ViewData["IdNivelRisco"] = nivel; 
            List<TextValue2> custoEsperado = new List<TextValue2>();
            custoEsperado.Add(new TextValue2("1", "R$ 1,40 - Abaixo do mercado"));
            custoEsperado.Add(new TextValue2("2", "R$ 1,50 - Média do mercado"));
            custoEsperado.Add(new TextValue2("3", "R$ 1,60 - Acima do mercado"));
            Random random3 = new Random();
            double custo = random3.Next(1, 4);
            ViewData["CustoCompraEsperado"] = new SelectList(custoEsperado.OrderBy(x => x.Value), "Text", "Value", custo);
            ViewData["IdCustoCompraEsperado"] = custo; 
            List<TextValue> quantidadeVencedores = new List<TextValue>();
            quantidadeVencedores.Add(new TextValue("1", 1));
            quantidadeVencedores.Add(new TextValue("2", 2));
            Random random4 = new Random();
            ViewData["QuantidadeVencedores"] = new SelectList(quantidadeVencedores, "Text", "Value");
            leilao.DataAberturaLance = clientDate;
            leilao.Requisicao = "Empresa de logística para efetuar entregas sob demanda entre o endereço X e Y por período de 90 dias. O veículo deverá ser do tipo van, com até 3 anos de fabricação, registro válido na ANTT e movido a diesel. A duração do LR será de acordo com o modelo de LR, e será indicado no momento da submissão do lance do fornecedor. O lance deverá ser o preço do km rodado e o prazo entrega, conforme a prioridade de compra do LR. Pode ser efetuado mais de um lance, mas o válido será o último. Em caso de empate, o que submeteu primeiro terá prioridade. Pode haver 1 ou 2 vencedores (1 entregará 100% das cargas e 2 será 50 % cada)";

            List<TextValue2> atributoPrecoPrazo1 = new List<TextValue2>();
            atributoPrecoPrazo1.Add(new TextValue2("1", "Menor preço"));
            atributoPrecoPrazo1.Add(new TextValue2("2", "Menor prazo"));
            atributoPrecoPrazo1.Add(new TextValue2("3", "Menor preço e prazo"));
            ViewData["IdAtributoPrecoPrazo1"] = new SelectList(atributoPrecoPrazo1, "Text", "Value");

            var hFornecedorLeilao = await _context.FornecedorLeilao.Where(x => x.IndicadorAtivo == true && x.Classificacao == 1)
                        .Take(3).OrderByDescending(x => x.Data).ToListAsync();
            List<HistoricoLeilaoMV> listHistoricoLeilaoMV = new List<HistoricoLeilaoMV>();
            List<string> atributoPrecoPrazo = new List<string>();
            atributoPrecoPrazo.Add("R$ 2,50 - 15 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,50 - 10 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,50 -  5 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 20 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 15 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 10 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 30 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 20 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 15 dias p/ entrega");
            foreach (var item in hFornecedorLeilao)
            {
                HistoricoLeilaoMV historicoLeilaoMV = new HistoricoLeilaoMV();
                var lHistorico = await _context.Leilao.Where(x => x.IdLeilao == item.IdLeilao).SingleOrDefaultAsync();
                historicoLeilaoMV.DataFechamento = lHistorico.DataFechamento;
                historicoLeilaoMV.CustoEsperado = lHistorico.CustoCompraEsperado;
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
            return View(leilao);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLeilao,DataCadastro,DataAberturaLance,DataFechamento,Requisicao,IdTipoLeilao,ValorInicial,IndicadorAtivo,IdComprador,JustificativaCancelamento,ValorMaximo,ValorIncremento,CustoCompraEsperado,QuantidadeVencedores,NivelRisco,IdAtributoPrecoPrazo1,IdAtributoPrecoPrazo2,IdAtributoPrecoPrazo3,IdAtributoPrecoPrazo4,IdAtributoPrecoPrazo5,IdAtributoPrecoPrazo6")] Leilao leilao, short? IdNivelRisco, short? IdCustoCompraEsperado, int IdTipoLeilao2, string Requisicao2)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            if (IdCustoCompraEsperado == 1)
                leilao.CustoCompraEsperado = decimal.Parse("1.40") / 100;
            else if (IdCustoCompraEsperado == 2)
                leilao.CustoCompraEsperado = decimal.Parse("1.50") / 100;
            else if (IdCustoCompraEsperado == 3)
                leilao.CustoCompraEsperado = decimal.Parse("1.60") / 100;
            leilao.IdComprador = _userManager.GetUserId(User);
            leilao.IdTipoLeilao = IdTipoLeilao2;
            leilao.Requisicao = Requisicao2;
            if (leilao.IdTipoLeilao == 3)
            {
                leilao.ValorInicial = decimal.Parse("0.30") / 100;
            }
            if (leilao.IdTipoLeilao == 4)
            {
                leilao.ValorInicial = decimal.Parse("2.70") / 100;
            }
            ViewData["IdTipoLeilao"] = new SelectList(_context.TipoLeilao.OrderBy(x => x.Descricao), "IdTipoLeilao", "Descricao", leilao.IdTipoLeilao);
            List<TextValue2> nivelRisco = new List<TextValue2>();
            nivelRisco.Add(new TextValue2("1", "Disposto ao risco"));
            nivelRisco.Add(new TextValue2("2", "Risco neutro"));
            nivelRisco.Add(new TextValue2("3", "Contrário ao risco"));
            ViewData["NivelRisco"] = new SelectList(nivelRisco, "Text", "Value", leilao.NivelRisco);

            List<TextValue2> custoEsperado = new List<TextValue2>();
            custoEsperado.Add(new TextValue2("1", "R$ 1,40 - Custo abaixo do praticado no mercado"));
            custoEsperado.Add(new TextValue2("2", "R$ 1,50 - Custo médio do praticado no mercado"));
            custoEsperado.Add(new TextValue2("3", "R$ 1,60 - Custo acima do praticado no mercado"));
            ViewData["CustoCompraEsperado"] = new SelectList(custoEsperado.OrderBy(x => x.Value), "Text", "Value", leilao.CustoCompraEsperado);

            List<TextValue> quantidadeVencedores = new List<TextValue>();
            quantidadeVencedores.Add(new TextValue("1", 1));
            quantidadeVencedores.Add(new TextValue("2", 2));
            ViewData["QuantidadeVencedores"] = new SelectList(quantidadeVencedores, "Text", "Value", leilao.QuantidadeVencedores);

            List<TextValue2> atributoPrecoPrazo1 = new List<TextValue2>();
            atributoPrecoPrazo1.Add(new TextValue2("1", "Menor preço"));
            atributoPrecoPrazo1.Add(new TextValue2("2", "Menor prazo"));
            atributoPrecoPrazo1.Add(new TextValue2("3", "Menor preço e prazo"));
            ViewData["IdAtributoPrecoPrazo1"] = new SelectList(atributoPrecoPrazo1, "Text", "Value", leilao.IdAtributoPrecoPrazo1);

            if (leilao.QuantidadeVencedores == null)
            {
                TempData["Msg"] = "Por favor selecione a quantidade de fornecedor(es) vencedor(es) do LR";
                return View(leilao);
            }
            if (leilao.IdAtributoPrecoPrazo1 == null)
            {
                TempData["Msg"] = "Por favor selecione a maior importância dos atributos de Preço e Prazo para o comprador";
                return View(leilao);
            }


            var hFornecedorLeilao = await _context.FornecedorLeilao.Where(x => x.IndicadorAtivo == true && x.Classificacao == 1)
            .Take(3).OrderByDescending(x => x.Data).ToListAsync();
            List<HistoricoLeilaoMV> listHistoricoLeilaoMV = new List<HistoricoLeilaoMV>();
            List<string> atributoPrecoPrazo = new List<string>();
            atributoPrecoPrazo.Add("R$ 2,50 - 15 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,50 - 10 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,50 -  5 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 20 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 15 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 2,00 - 10 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 30 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 20 dias p/ entrega");
            atributoPrecoPrazo.Add("R$ 1,50 - 15 dias p/ entrega");
            foreach (var item in hFornecedorLeilao)
            {
                HistoricoLeilaoMV historicoLeilaoMV = new HistoricoLeilaoMV();
                var lHistorico = await _context.Leilao.Where(x => x.IdLeilao == item.IdLeilao).SingleOrDefaultAsync();
                historicoLeilaoMV.DataFechamento = lHistorico.DataFechamento;
                historicoLeilaoMV.CustoEsperado = lHistorico.CustoCompraEsperado;
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
            if (ModelState.IsValid)
            {
                leilao.DataCadastro = clientDate;
                leilao.IndicadorAtivo = true;
                leilao.NivelRisco = IdNivelRisco;
                var usuario = await _context.AspNetUsers.FindAsync(_userManager.GetUserId(User));
                leilao.IdCurso = usuario.IdCurso;
                leilao.IdNivelEnsino = usuario.IdNivelEnsino;
                int registro2 = 0;
                if (!String.IsNullOrEmpty(HttpContext.Session.GetString("Registro2")))
                {
                    registro2 = int.Parse(HttpContext.Session.GetString("Registro2"));
                }
                var tipoLeilao = await _context.TipoLeilao.Where(x => x.IdTipoLeilao == leilao.IdTipoLeilao).SingleOrDefaultAsync();
                if (registro2 == 0)
                {
                    leilao.DataAberturaLance = clientDate;
                    if (leilao.IdTipoLeilao == 3 || leilao.IdTipoLeilao == 4)
                    {
                        if (leilao.IdTipoLeilao == 3)
                        {
                            leilao.ValorInicial = leilao.ValorInicial.Value + (decimal.Parse("0.40") / 100);
                        }
                        else if (leilao.IdTipoLeilao == 4)
                        {
                            leilao.ValorInicial = leilao.ValorInicial.Value - (decimal.Parse("0.40") / 100);
                        }
                        leilao.DataFechamento = clientDate.AddSeconds(tipoLeilao.Tempo.Value);
                        leilao.DataFechamentoRodada1 = clientDate.AddSeconds(tipoLeilao.Tempo.Value / 3);
                        leilao.DataFechamentoRodada2 = clientDate.AddSeconds((tipoLeilao.Tempo.Value / 3) + (tipoLeilao.Tempo.Value / 3));
                        leilao.DataFechamentoRodada3 = clientDate.AddSeconds(tipoLeilao.Tempo.Value);                       
                        leilao.Rodada = 1;
                    }
                    else
                    {
                        leilao.DataFechamento = clientDate.AddSeconds(tipoLeilao.Tempo.Value);
                    }
                }
                _context.Add(leilao);
                await _context.SaveChangesAsync();
                var compradorFornecedor = await _context.CompradorFornecedor.FirstOrDefaultAsync(m => m.IdComprador == leilao.IdComprador);
                return RedirectToAction("PainelComprador", "CompradorFornecedor", new { @id = compradorFornecedor.IdCompradorFornecedor });
            }
            return View(leilao);
        }

        public async Task<IActionResult> CancelarLeilao(int? id)
        {
            List<TextValue2> justificativa = new List<TextValue2>();
            justificativa.Add(new TextValue2("1", "Não há lucro"));
            justificativa.Add(new TextValue2("2", "Não quero me arriscar"));
            justificativa.Add(new TextValue2("3", "Não me interessou"));
            justificativa.Add(new TextValue2("4", "Não obtive lances"));
            ViewData["IdJustificativa"] = new SelectList(justificativa, "Text", "Value");
            var leilao = await _context.Leilao.Include(b => b.IdTipoLeilaoNavigation).Where(m => m.IdLeilao == id).SingleOrDefaultAsync();
            if (leilao == null)
            {
                TempData["Msg"] = "Este LR não está mais cadastrado";
                return View(leilao);
            }
            if (leilao.IndicadorAtivo == false)
            {
                TempData["Msg"] = "Este LR já está cancelado";
                return View(leilao);
            }
            return View(leilao);
        }


        [HttpPost, ActionName("CancelarLeilao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelarLeilaoConfirmed(int id, short? idJustificativa)
        {
            var leilao = await _context.Leilao.Where(m => m.IdLeilao == id).SingleOrDefaultAsync();
            List<TextValue2> justificativa = new List<TextValue2>();
            justificativa.Add(new TextValue2("1", "Não há lucro"));
            justificativa.Add(new TextValue2("2", "Não quero me arriscar"));
            justificativa.Add(new TextValue2("3", "Não me interessou"));
            justificativa.Add(new TextValue2("4", "Não obtive lances"));
            ViewData["IdJustificativa"] = new SelectList(justificativa, "Text", "Value",idJustificativa);
            if (idJustificativa == null)
            {
                TempData["Msg"] = "Por favor selecione a justificativa por não submeter o lance neste LR";
                return View(leilao);
            }
            leilao.IndicadorAtivo = false;
            leilao.IdJustificativa = idJustificativa;
            _context.Leilao.Update(leilao);
            var usuario = await _context.AspNetUsers.Where(x => x.Id == leilao.IdComprador).SingleOrDefaultAsync();
            usuario.QtdComoComprador = usuario.QtdComoComprador + 1;
            _context.AspNetUsers.Update(usuario);
            await _context.SaveChangesAsync();
            TempData["Msg"] = "Seu LR atual foi excluido. Por favor inclua novo LR.";
            return RedirectToAction(nameof(Create));
        }


        public async Task<IActionResult> Resultado()
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
            List<string> atributoPrecoPrazo1 = new List<string>();
            atributoPrecoPrazo1.Add("Menor preço");
            atributoPrecoPrazo1.Add("Menor prazo");
            atributoPrecoPrazo1.Add("Menor preço e prazo");
            string idUser = _userManager.GetUserId(User);
            var leilao = await _context.Leilao.Where(x => x.IdComprador == idUser && x.IndicadorAtivo == true).OrderByDescending(x => x.DataCadastro).FirstOrDefaultAsync();
            if (leilao != null)
            {
                ViewBag.OpcaoComprador = atributoPrecoPrazo1[leilao.IdAtributoPrecoPrazo1.Value - 1];
                ViewBag.QtdVencedores = leilao.QuantidadeVencedores;
                var fLeilaoList = await _context.FornecedorLeilao.Include(f => f.IdFornecedorNavigation)
                                        .Where(x => x.IdLeilao == leilao.IdLeilao &&
                                                    x.Classificacao != null &&
                                                    x.IndicadorAtivo == true)
                                        .OrderBy(x => x.Classificacao).ToListAsync();
                if (fLeilaoList.Count == 0)
                {
                    TempData["Msg"] = "O LR atual ainda não foi finalizado ou não teve apuração.";
                    return RedirectToAction("Index", "Home");
                }
                return View(fLeilaoList);
            }
            TempData["Msg"] = "Ainda não há resultado para de LR. Talvez ainda não teve nenhuma apuração de LR ou o último LR foi cancelado, por favor crie novo LR";
            return RedirectToAction("Index", "Home");
        }
        private bool LeilaoExists(int id)
        {
            return _context.Leilao.Any(e => e.IdLeilao == id);
        }
    }
}

