using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace LR.Models
{
    [ModelMetadataType(typeof(FornecedorLeilaoMetadata))]
    [Display(Name = "Fornecedor Leilao")]
    public partial class FornecedorLeilao
    {
    }
    public partial class FornecedorLeilaoMetadata
    {

        [Display(Name = "Data do Lance")]
        public DateTime? Data { get; set; }

        [Display(Name = "Lance")]
        public decimal? Lance { get; set; }

        [Display(Name = "Classificação")]
        public int? Classificação { get; set; }


        [Display(Name = "Leilão")]
        public int IdLeilao { get; set; }

        [Display(Name = "Custo do Frete (combustível por km)")]
        public decimal? CustoServicoFrete { get; set; }
        [Display(Name = "Ativo?")]
        public bool IndicadorAtivo { get; set; }
        [Display(Name = "Comprador")]
        public string IdComprador { get; set; }
        [Display(Name = "Motivo Cancelamento")]
        public string JustificativaCancelamento { get; set; }
        [Display(Name = "Vl Máximo")]
        public decimal? ValoMaximo { get; set; }
        [Display(Name = "Custo Compra Esperado")]
        public decimal? CustoCompraEsperado { get; set; }
        [Display(Name = "Qtd Vencedores")]
        public short? QuantidadeVencedores { get; set; }
        [Display(Name = "Indicador de Risco")]
        public short? NivelRisco { get; set; }

        [Display(Name = "Lance (Preço e Prazo)")]
        public int? AtributoPrecoPrazo { get; set; }

        [Display(Name = "Comprador")]
        public virtual AspNetUsers IdCompradorNavigation { get; set; }

        [Display(Name = "Tipo Leilão")]
        public virtual TipoLeilao IdTipoLeilaoNavigation { get; set; }

    }
}
