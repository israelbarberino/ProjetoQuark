using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjetoQuark.Models
{
    public class ModelLogin
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [StringLength(15, ErrorMessage = "Nome de usuário deve conter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string usuario { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4)]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [DisplayName("Tipo de usuário: 1 - para cliente | 2 - para administrador")]
        public string tipo { get; set; }
        
        [Required]
        [Display(Name = "Nome")]
        public string nomeCli { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string emailCli { get; set; }


    }
}