using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Models
{
    public class ModelVeiculo
    {
        public string codProd { get; set; }

        [MaxLength(100)]
        [DisplayName("Nome do Produto")]
        public string nomeProd { get; set; }

        [DisplayName("Imagem do Produto")]
        public string imagemProd { get; set; }

        [DisplayName("Quantidade do Produto")]
        public string quantidadeProd { get; set; }

        [MaxLength(20)]
        [DisplayName("Valor do Produto")]
        public string valorProd { get; set; }

        [MaxLength(300)]
        [DisplayName("Descrição do Produto")]
        public string descricaoProd { get; set; }

        [DisplayName("Código da Categoria")]
        public string codCat { get; set; }

        [DisplayName("Categoria")]
        public string categoria { get; set; }

    }
}