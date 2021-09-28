using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace LR.Models
{
    [ModelMetadataType(typeof(LeilaoMetadata))]
    [Display(Name = "Leilao")]
    public partial class Leilao
    {
    }
    public partial class LeilaoMetadata
    {

        [Display(Name = "Data Cadastro")]
        public DateTime? DataCadastro { get; set; }

        [Display(Name = "Dt Abertura Lance")]
        public DateTime? DataAberturaLance { get; set; }

        [Display(Name = "Dt Fechamento Lance")]
        public DateTime? DataFechamento { get; set; }

        [Display(Name = "Requisição")]
        public string Requisicao { get; set; }

        [Display(Name = "Modelo de LR")]
        public int IdTipoLeilao { get; set; }

        [Display(Name = "Vr Inicial")]
        public decimal? ValorInicial { get; set; }
        [Display(Name = "Ativo?")]
        public bool IndicadorAtivo { get; set; }
        [Display(Name = "Comprador")]
        public string IdComprador { get; set; }
        [Display(Name = "Motivo Cancelamento")]
        public string JustificativaCancelamento { get; set; }
        [Display(Name = "Vl Máximo")]
        public decimal? ValoMaximo { get; set; }
        [Display(Name = "Custo Esperado de compra (por km)")]
        public decimal? CustoCompraEsperado { get; set; }
        [Display(Name = "Qtd Vencedores")]
        public short? QuantidadeVencedores { get; set; }
        [Display(Name = "Indicador Risco")]
        public short? NivelRisco { get; set; }

        public short? IdAtributoPrecoPrazo6 { get; set; }

        [Display(Name = "Comprador")]
        public virtual AspNetUsers IdCompradorNavigation { get; set; }

        [Display(Name = "Tipo Leilão")]
        public virtual TipoLeilao IdTipoLeilaoNavigation { get; set; }

    }
}
