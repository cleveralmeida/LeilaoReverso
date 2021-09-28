using LR.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace LR.Hubs
{
    public class ChatHub : Hub
    {

        private readonly LeilaoReversoContext _context;
        public ChatHub(LeilaoReversoContext context)
        {
            _context = context;
        }

        public async Task SendComprador(string idComprador, string idLeilao)
        {
            try
            {
                TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
                DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
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
                var leilao = await _context.Leilao
                    .Where(x => x.IdLeilao == int.Parse(idLeilao)  
                            ).SingleOrDefaultAsync();
                if (leilao != null)
                {
                    if (leilao.IndicadorAtivo == false)
                    {
                        var fornecedores = await _context.FornecedorLeilao.Where(x => x.IdLeilao == leilao.IdLeilao && x.IndicadorAtivo == true).ToListAsync();
                        foreach (var item in fornecedores)
                        {
                            await Clients.All.SendAsync("broadcastFornecedorLRCancelado", idComprador, leilao.IdLeilao, item.IdFornecedor);
                        }
                        return;
                    }
                    var listFornecedores = await _context.FornecedorLeilao.
                                Where(x => x.IdLeilao == leilao.IdLeilao &&
                                           x.IndicadorAtivo == true).ToListAsync();
                    if (leilao.DataFechamento < clientDate && leilao.SelecaoRealizada == null && listFornecedores.Count() > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " broadcastFornecedorAguardandoApuracao1 leilao= " + leilao.IdLeilao + " rodada = " + leilao.Rodada);
                        await Clients.All.SendAsync("broadcastFornecedorAguardandoApuracao", idComprador, leilao.IdLeilao);
                        return;
                    }
                    if (leilao.DataFechamento > clientDate && leilao.SelecaoRealizada == null)
                    {
                        var compradorfornecedor = await _context.CompradorFornecedor.Where(x => x.IdComprador == leilao.IdComprador).ToListAsync();
                        foreach (var item in compradorfornecedor)
                        {
                            var forn = await _context.FornecedorLeilao.
                                                Where(x => x.IdFornecedor == item.IdFornecedor &&
                                                x.IdLeilao == leilao.IdLeilao).FirstOrDefaultAsync();
                            if (forn == null)
                            {
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " broadcastCompradorFornecedorNovo leilao = " + leilao.IdLeilao + " rodada = " + leilao.Rodada + " dataF1 = " + leilao.DataFechamentoRodada2.ToString());
                                await Clients.All.SendAsync("broadcastCompradorFornecedorNovo", idComprador, item.IdFornecedor, leilao.IdLeilao);
                                await Clients.All.SendAsync("broadcastCompradorFornecedorNovoLayout", idComprador, item.IdFornecedor, leilao.IdLeilao);
                            }
                        }
                    }

                    if (listFornecedores.Where(x=>x.IndicadorAtivo == true).Count() == 0)
                    {
                        await Clients.All.SendAsync("broadcastFornecedorLanceLimpa", idComprador, leilao.IdLeilao);
                    }

                    if (leilao.IdTipoLeilao > 2 && leilao.DataFechamento > clientDate && leilao.IndicadorAtivo == true && leilao.SelecaoRealizada == null)
                    {
                        if (leilao.Rodada == 1)
                        {
                            if (leilao.DataFechamentoRodada1 < clientDate)
                            {
                                leilao.Rodada = 2;
                                if (leilao.IdTipoLeilao == 3)
                                {
                                    leilao.ValorInicial = leilao.ValorInicial.Value + (decimal.Parse("0.40") / 100);
                                }
                                else if (leilao.IdTipoLeilao == 4)
                                {
                                    leilao.ValorInicial = leilao.ValorInicial.Value - (decimal.Parse("0.40") / 100);
                                }
                                await _context.SaveChangesAsync();
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " alterou rodada leilao = " + leilao.IdLeilao + " rodada = " + leilao.Rodada + " ValorInicial=" + leilao.ValorInicial + " dataF2 = " + leilao.DataFechamentoRodada2.ToString());
                                await Clients.All.SendAsync("broadcastMudaRodadaComprador", idComprador, leilao.IdLeilao.ToString(), leilao.Rodada, leilao.ValorInicial.ToString());
                                await Clients.All.SendAsync("broadcastMudaRodadaFornecedor", idComprador, leilao.IdLeilao.ToString(), leilao.Rodada, leilao.ValorInicial.ToString());
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " n alt rodada leilao = " + leilao.IdLeilao + " rodada = " + leilao.Rodada + " ValorInicial=" + leilao.ValorInicial + " dataF2 = " + leilao.DataFechamentoRodada2.ToString());
                            }

                        }
                        else if (leilao.Rodada == 2)
                        {
                            if (leilao.DataFechamentoRodada2 < clientDate)
                            {
                                leilao.Rodada = 3;
                                if (leilao.IdTipoLeilao == 3)
                                {
                                    leilao.ValorInicial = leilao.ValorInicial.Value + (decimal.Parse("0.40") / 100);
                                }
                                else if (leilao.IdTipoLeilao == 4)
                                {
                                    leilao.ValorInicial = leilao.ValorInicial.Value - (decimal.Parse("0.40") / 100);
                                }
                                _context.Leilao.Update(leilao);
                                await _context.SaveChangesAsync();
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " alterou rodada leilao = " + leilao.IdLeilao + " rodada = " + leilao.Rodada + " dataF2 = " + leilao.DataFechamentoRodada2.ToString());
                                await Clients.All.SendAsync("broadcastMudaRodadaComprador", idComprador, leilao.IdLeilao.ToString(), leilao.Rodada, leilao.ValorInicial.ToString());
                                await Clients.All.SendAsync("broadcastMudaRodadaFornecedor", idComprador, leilao.IdLeilao.ToString(), leilao.Rodada, leilao.ValorInicial.ToString());
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " n alt rodada leilao = " + leilao.IdLeilao + " rodada = " + leilao.Rodada + " ValorInicial=" + leilao.ValorInicial + " dataF2 = " + leilao.DataFechamentoRodada2.ToString() );
                            }
                        }
                        else if (leilao.Rodada == 3)
                        {
                            if (leilao.DataFechamentoRodada3 < clientDate)
                            {
                                leilao.Rodada = 4;
                                if (leilao.IdTipoLeilao == 3)
                                {
                                    leilao.ValorInicial = leilao.ValorInicial.Value + (decimal.Parse("0.40") / 100);
                                }
                                else if (leilao.IdTipoLeilao == 4)
                                {
                                    leilao.ValorInicial = leilao.ValorInicial.Value - (decimal.Parse("0.40") / 100);
                                }
                                _context.Leilao.Update(leilao);
                                await _context.SaveChangesAsync();
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " alterou rodada leilao = " + leilao.IdLeilao + " rodada = " + leilao.Rodada + " ValorInicial=" + leilao.ValorInicial + " dataF2 = " + leilao.DataFechamentoRodada2.ToString());
                                await Clients.All.SendAsync("broadcastMudaRodadaComprador", idComprador, leilao.IdLeilao.ToString(), leilao.Rodada, leilao.ValorInicial.ToString());
                                await Clients.All.SendAsync("broadcastMudaRodadaFornecedor", idComprador, leilao.IdLeilao.ToString(), leilao.Rodada, leilao.ValorInicial.ToString());
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " n alt rodada leilao = " + leilao.IdLeilao + " rodada = " + leilao.Rodada + " ValorInicial=" + leilao.ValorInicial + " dataF2 = " + leilao.DataFechamentoRodada2.ToString());
                            }
                        }
                    }
                }
                var lances = await  (from l in _context.Leilao
                              join f in _context.FornecedorLeilao on l.IdLeilao equals f.IdLeilao
                              where l.IdComprador == idComprador && 
                              l.IndicadorAtivo == true && 
                              f.IndicadorAtivo == true && 
                              l.DataFechamento > clientDate && 
                              l.SelecaoRealizada != true
                              select new FornecedorLeilaoMV
                              {
                                  IdFornecedor = f.IdFornecedor,
                                  Rodada = l.Rodada,
                                  IdLeilao = l.IdLeilao,
                                  ValorInicial = l.ValorInicial.ToString(),
                                  IdTipoLeilao = l.IdTipoLeilao.ToString(),
                                  Lance = f.Lance,
                                  DescAtributoPrecoPrazo = atributoPrecoPrazo[f.AtributoPrecoPrazo.Value-1],
                                  Data = f.Data
                              }).ToListAsync();
                if (lances.Count > 0)
                {
                    var lancesJson = JsonConvert.SerializeObject(lances);
                    if (lancesJson != null)
                        await Clients.All.SendAsync("broadcastCompradorFornecedor", idComprador, lancesJson.ToString(), leilao.IdLeilao);
                }
                else if (leilao != null)
                {
                   await Clients.All.SendAsync("broadcastComprador", idComprador, leilao.Rodada, leilao.ValorInicial.ToString(), leilao.IdLeilao);
                }
                if (leilao == null)
                {
                    await Clients.All.SendAsync("broadcastComprador", idComprador, 99, 0);
                }
            }
            catch (Exception e)
            {
                string teste = e.Message.ToString();
            }
        }

        public async Task SendFornecedor(string idComprador, string idFornecedor, string idLeilao)
        {
            try
            {
                TimeZoneInfo timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
                DateTime clientDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone);
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
                var leilao = await _context.Leilao
                    .Where(x => x.IdLeilao == int.Parse(idLeilao)
                            ).SingleOrDefaultAsync();
                if (leilao != null)
                {
                    if (leilao.IndicadorAtivo == false)
                    {
                        await Clients.All.SendAsync("broadcastFornecedorLRCancelado", idComprador, leilao.IdLeilao, idFornecedor);
                        return;
                    }
                    var listFornecedores = await _context.FornecedorLeilao.
                                Where(x => x.IdLeilao == leilao.IdLeilao &&
                                           x.IndicadorAtivo == true).ToListAsync();
                    if (leilao.DataFechamento < clientDate && leilao.SelecaoRealizada == null && listFornecedores.Count() > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " broadcastFornecedorAguardandoApuracao2 leilao= " + leilao.IdLeilao + " rodada = " + leilao.Rodada );
                        await Clients.All.SendAsync("broadcastFornecedorAguardandoApuracao", idComprador, leilao.IdLeilao);
                        return;
                    }
                }
                var lance = await (from l in _context.Leilao
                                    join f in _context.FornecedorLeilao on l.IdLeilao equals f.IdLeilao
                                    where l.IdComprador == idComprador 
                                        && f.IdFornecedor == idFornecedor
                                        && l.IndicadorAtivo == true
                                        && f.IndicadorAtivo == true
                                        && l.SelecaoRealizada != true
                                        && l.DataFechamento > clientDate
                                    select new FornecedorLeilaoMV
                                    {
                                        IdFornecedor = f.IdFornecedor,
                                        Rodada = l.Rodada,
                                        IdLeilao = l.IdLeilao,
                                        ValorInicial = l.ValorInicial.ToString(),
                                        IdTipoLeilao = l.IdTipoLeilao.ToString(),
                                        Classificacao = f.Classificacao,
                                        Lance = f.Lance,
                                        DescAtributoPrecoPrazo = atributoPrecoPrazo[f.AtributoPrecoPrazo.Value - 1],
                                        Data = f.Data
                                    }).ToListAsync();
                if (lance.Count() == 1)
                {
                    var lancesConcorrentes = await (from l in _context.Leilao
                                       join f in _context.FornecedorLeilao on l.IdLeilao equals f.IdLeilao
                                       where l.IdComprador == idComprador
                                           && f.IdFornecedor != idFornecedor
                                           && l.IndicadorAtivo == true
                                           && f.IndicadorAtivo == true
                                           && l.SelecaoRealizada != true
                                           && l.DataFechamento > clientDate
                                       select new FornecedorLeilaoMV
                                       {
                                           IdFornecedor = f.IdFornecedor,
                                           Rodada = l.Rodada,
                                           IdLeilao = l.IdLeilao,
                                           ValorInicial = l.ValorInicial.ToString(),
                                           IdTipoLeilao = l.IdTipoLeilao.ToString(),
                                           Classificacao = f.Classificacao,
                                           Lance = f.Lance,
                                           DescAtributoPrecoPrazo = atributoPrecoPrazo[f.AtributoPrecoPrazo.Value - 1],
                                           Data = f.Data
                                       }).ToListAsync();
                    var lancesConcorrentesJson = JsonConvert.SerializeObject(lancesConcorrentes);
                    System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " broadcastFornecedorLancesConcorrentes idcomprador=" + idComprador + " idFornecedor=" + idFornecedor);
                    await Clients.All.SendAsync("broadcastFornecedorLancesConcorrentes", idComprador, lancesConcorrentesJson.ToString(), idFornecedor);
                    var fLances = await (from l in _context.Leilao
                                         join f in _context.FornecedorLeilao on l.IdLeilao equals f.IdLeilao
                                         join fl in _context.FornecedorLeilaoLance on f.IdFornecedorLeilao equals fl.IdFornecedorLeilao
                                         where l.IdComprador == idComprador
                                                && l.IndicadorAtivo == true
                                                && f.IndicadorAtivo == true
                                                && l.DataFechamento > clientDate
                                         select new FornecedorLeilaoMV
                                         {
                                             IdFornecedor = f.IdFornecedor,
                                             Rodada = fl.Rodada,
                                             IdLeilao = l.IdLeilao,
                                             Sequencia = fl.Sequencia,
                                             IdTipoLeilao = l.IdTipoLeilao.ToString(),
                                             Classificacao = f.Classificacao,
                                             Lance = f.Lance,
                                             DescAtributoPrecoPrazo = atributoPrecoPrazo[fl.AtributoPrecoPrazo.Value - 1],
                                             Data = fl.Data
                                         }).ToListAsync();
                    var fLancesJson = JsonConvert.SerializeObject(fLances);
                    if (fLancesJson != null)
                        await Clients.All.SendAsync("broadcastFornecedorHistorico", idComprador, fLancesJson.ToString(), idFornecedor);
                }
                else if (lance.Count() > 1)
                {
                    await Clients.All.SendAsync("broadcastFornecedorLancesDuplicados", idComprador, idFornecedor);
                } else if (lance.Count() == 0)
                {
                    System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " broadcastFornecedorApurado Entrou ");

                    await Clients.All.SendAsync("broadcastFornecedorLance", idComprador, null, null, idFornecedor);
                    if (idLeilao != null)
                    {
                        var lrFechado = await _context.Leilao.Where(x => x.IdLeilao == int.Parse(idLeilao)).SingleOrDefaultAsync();
                        if (lrFechado != null)
                        {
                            if (lrFechado.SelecaoRealizada == true)
                            {
                                System.Diagnostics.Debug.WriteLine(clientDate.ToString() + " broadcastFornecedorApurado Passou leilao= " + leilao.IdLeilao + " rodada = " + leilao.Rodada);
                                await Clients.All.SendAsync("broadcastFornecedorApurado", idComprador, idFornecedor, lrFechado.IdTipoLeilao.ToString(), lrFechado.IdLeilao);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string teste = e.Message.ToString();
            }
        }
    }
}