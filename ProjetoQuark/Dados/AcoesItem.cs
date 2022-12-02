using MySql.Data.MySqlClient;
using ProjetoQuark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Dados
{
    public class AcoesItem
    {
        Conexao con = new Conexao();

        public void InserirItem(ModelItemCarrinho cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into itemVenda values(default, @codVenda, @codProd, @qtdeVendas, @valorParcial)", con.MyConectarBD());

            cmd.Parameters.Add("@codVenda", MySqlDbType.VarChar).Value = cm.PedidoID;
            cmd.Parameters.Add("@codProd", MySqlDbType.VarChar).Value = cm.ProdutoID;
            cmd.Parameters.Add("@qtdeVendas", MySqlDbType.VarChar).Value = cm.Qtd;
            cmd.Parameters.Add("@valorParcial", MySqlDbType.VarChar).Value = cm.valorParcial;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}