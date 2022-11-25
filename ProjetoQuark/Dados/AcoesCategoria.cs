using MySql.Data.MySqlClient;
using ProjetoQuark.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Dados
{
    public class AcoesCategoria
    {
        Conexao con = new Conexao();

        public DataTable CarregaCategoria()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbCategoria", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Categoria = new DataTable();
            da.Fill(Categoria);
            con.MyDesconectarBD();
            return Categoria;
        }

    }
}