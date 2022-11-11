using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Models
{
    public class ModelProduto
    {
        public int codProd { get; set; }

        [MaxLength(100)]
        public string nomeProd { get; set; }

        [MaxLength(500)]
        public string imagemProd { get; set; }

        public int quantidadeProd { get; set; }

        [MaxLength(20)]
        public string valorProd { get; set; }

        [MaxLength(300)]
        public string descricaoProd { get; set; }

        public int codCat { get; set; }

    }
}