using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Models
{
    public class ModelCategoria
    {
        public string codCat { get; set; }

        [DisplayName("Categoria")]
        public string categoria { get; set; }
    }
}