using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LR.Models;
using Microsoft.AspNetCore.Identity;
using TimeZoneConverter;

namespace LR.Controllers
{
    public class CompradorFornecedorController : Controller
    {
        private readonly LeilaoReversoContext _context;
        private UserManager<ApplicationUser> _userManager;
        public CompradorFornecedorController(LeilaoReversoContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
         

        public async Task<IActionResult> PainelComprador(int? id)
        {
            TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
            DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
            CompradorFornecedor compradorFornecedor = new CompradorFornecedor();
            if (id == null)
            {
                string idComprador = _userManager.GetUserId(User);
                compradorFornecedor = await _context.CompradorFornecedor.FirstOrDefaultAsync(m => m.IdComprador == idComprador);
            }
            else
            {
                compradorFornecedor = await _context.CompradorFornecedor.FirstOrDefaultAsync(m => m.IdCompradorFornecedor == id);
            }
            if (compradorFornecedor == null)
            {
                return NotFound();
            }
            ViewBag.IdComprador = compradorFornecedor.IdComprador; 
            var leilao = await _context.Leilao.Include(l => l.IdCompradorNavigation).Include(l => l.IdTipoLeilaoNavigation)
                                .Where(m => m.IdComprador == compradorFornecedor.IdComprador && 
                                            m.IndicadorAtivo == true && 
                                            m.DataApuracao == null &&
                                            m.DataFechamento > clientDate).FirstOrDefaultAsync();
            if (leilao == null)
            {
                TempData["Msg"] = "LR foi encerrado, cancelado ou ainda não existe. Por favor crie novo LR";
                return RedirectToAction("Index", "Home");
            }
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
            }
            else
            if (rodada == 2)
            {
                dataFechamentoRodada = leilao.DataFechamentoRodada2.Value;
            }
            else if (rodada == 3)
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
            ViewBag.Rodada = leilao.Rodada;
            var hFornecedorLeilao = await _context.FornecedorLeilao.Where(x => x.IndicadorAtivo == true && x.Classificacao == 1)
            .Take(3).OrderByDescending(x => x.Data).ToListAsync();
            List<HistoricoLeilaoMV> listHistoricoLeilaoMV = new List<HistoricoLeilaoMV>();
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

        private bool CompradorFornecedorExists(int id)
        {
            return _context.CompradorFornecedor.Any(e => e.IdCompradorFornecedor == id);
        }
    }
}
