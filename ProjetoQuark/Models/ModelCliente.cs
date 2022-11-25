using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjetoQuark.Models
{
    public class ModelCliente
    {
        public string usuario { get; set; }


        [Required]
        [Display(Name = "Nome")]
        public string nomeCli { get; set; }


        [Required]
        [Display(Name = "E-mail")]
        public string emailCli { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string senha { get; set; }


        public string tipo { get; set; }

    }
}