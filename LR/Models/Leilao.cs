using System;
using System.Collections.Generic;

namespace LR.Models
{
    public partial class Leilao
    {
        public Leilao()
        {
            FornecedorLeilao = new HashSet<FornecedorLeilao>();
            PesoValorLeilao = new HashSet<PesoValorLeilao>();
        }

        public int IdLeilao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAberturaLance { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string Requisicao { get; set; }
        public int IdTipoLeilao { get; set; }
        public decimal? ValorInicial { get; set; }
        public bool IndicadorAtivo { get; set; }
        public string IdComprador { get; set; }
        public string JustificativaCancelamento { get; set; }
        public decimal? ValorMaximo { get; set; }
        public decimal? CustoCompraEsperado { get; set; }
        public short? QuantidadeVencedores { get; set; }
        public short? NivelRisco { get; set; }
        public short? IdAtributoPrecoPrazo1 { get; set; }
        public short? IdAtributoPrecoPrazo2 { get; set; }
        public short? IdAtributoPrecoPrazo3 { get; set; }
        public short? IdAtributoPrecoPrazo4 { get; set; }
        public short? IdAtributoPrecoPrazo5 { get; set; }
        public short? IdAtributoPrecoPrazo6 { get; set; }
        public DateTime? DataApuracao { get; set; }
        public decimal? ValorIncremento { get; set; }
        public short? Rodada { get; set; }
        public int? IdCurso { get; set; }
        public int? IdNivelEnsino { get; set; }
        public bool? SelecaoRealizada { get; set; }
        public DateTime? DataFechamentoRodada1 { get; set; }
        public DateTime? DataFechamentoRodada2 { get; set; }
        public DateTime? DataFechamentoRodada3 { get; set; }
        public short? IdJustificativa { get; set; }

        public virtual AspNetUsers IdCompradorNavigation { get; set; }
        public virtual TipoLeilao IdTipoLeilaoNavigation { get; set; }
        public virtual ICollection<FornecedorLeilao> FornecedorLeilao { get; set; }
        public virtual ICollection<PesoValorLeilao> PesoValorLeilao { get; set; }
    }
}
