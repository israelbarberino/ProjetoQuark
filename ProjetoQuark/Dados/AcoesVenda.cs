using MySql.Data.MySqlClient;
using ProjetoQuark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Dados
{
    public class AcoesVenda
    {

        Conexao con = new Conexao();

        public void InserirVenda(ModelVenda cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbVenda values(default, @codUsu, @datavenda, @valorFinal)", con.MyConectarBD());

            cmd.Parameters.Add("@codUsu", MySqlDbType.VarChar).Value = cm.UsuarioID;
            cmd.Parameters.Add("@datavenda", MySqlDbType.VarChar).Value = cm.DtVenda;
            cmd.Parameters.Add("@valorFinal", MySqlDbType.VarChar).Value = cm.ValorTotal;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        MySqlDataReader dr;
        public void BuscaIdVenda(ModelVenda modVend)
        {
            MySqlCommand cmd = new MySqlCommand("select codVenda from tbVenda ORDER BY codVenda DESC limit 1", con.MyConectarBD());
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                modVend.codVenda = dr[0].ToString();
            }
            con.MyDesconectarBD();
        }

    }
}