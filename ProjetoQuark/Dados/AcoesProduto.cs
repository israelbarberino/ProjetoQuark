using MySql.Data.MySqlClient;
using ProjetoQuark.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Dados
{
    public class AcoesProduto
    {
        Conexao con = new Conexao();
        public void InserirVeiculo(ModelProduto cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbProduto (nomeProd, imagemProd, quantidadeProd, valorProd, descricacaoProd, codCat) values (@nomeProd, @imagemProd, @quantidadeProd, @valorProd, @descricacaoProd, @codCat)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomeProd", MySqlDbType.VarChar).Value = cm.nomeProd;
            cmd.Parameters.Add("@imagemProd", MySqlDbType.VarChar).Value = cm.imagemProd;
            cmd.Parameters.Add("@quantidadeProd", MySqlDbType.VarChar).Value = cm.quantidadeProd;
            cmd.Parameters.Add("@valorProd", MySqlDbType.VarChar).Value = cm.valorProd;
            cmd.Parameters.Add("@descricaoProd", MySqlDbType.VarChar).Value = cm.descricaoProd;
            cmd.Parameters.Add("@codCat", MySqlDbType.VarChar).Value = cm.codCat;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable CarregaCarros()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat <= 2", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Veiculo = new DataTable();
            da.Fill(Veiculo);
            con.MyDesconectarBD();
            return Veiculo;
        }
        public DataTable CarregaMotos()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat >= 3", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Veiculo = new DataTable();
            da.Fill(Veiculo);
            con.MyDesconectarBD();
            return Veiculo;
        }

        public DataTable CarregaCarroEletrico()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 1", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Veiculo = new DataTable();
            da.Fill(Veiculo);
            con.MyDesconectarBD();
            return Veiculo;
        }

        public DataTable CarregaCarroHibrido()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 2", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Veiculo = new DataTable();
            da.Fill(Veiculo);
            con.MyDesconectarBD();
            return Veiculo;
        }

        public DataTable CarregaMotoEletrica()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 3", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Veiculo = new DataTable();
            da.Fill(Veiculo);
            con.MyDesconectarBD();
            return Veiculo;
        }

        public DataTable CarregaMotoHibrida()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 4", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Veiculo = new DataTable();
            da.Fill(Veiculo);
            con.MyDesconectarBD();
            return Veiculo;
        }
    }
}