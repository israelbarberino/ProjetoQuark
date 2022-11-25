using MySql.Data.MySqlClient;
using ProjetoQuark.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Dados
{
    public class AcoesVeiculos
    {
        Conexao con = new Conexao();
        public void InserirVeiculo(ModelVeiculo cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbProduto (nomeProd, imagemProd, quantidadeProd, valorProd, descricaoProd) values (@nomeProd, @imagemProd, @quantidadeProd, @valorProd, @descricaoProd)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomeProd", MySqlDbType.VarChar).Value = cm.nomeProd;
            cmd.Parameters.Add("@imagemProd", MySqlDbType.VarChar).Value = cm.imagemProd;
            cmd.Parameters.Add("@quantidadeProd", MySqlDbType.VarChar).Value = cm.quantidadeProd;
            cmd.Parameters.Add("@valorProd", MySqlDbType.VarChar).Value = cm.valorProd;
            cmd.Parameters.Add("@descricaoProd", MySqlDbType.VarChar).Value = cm.descricaoProd;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<ModelCategoria> CarregaCategoria()
        {
            List<ModelCategoria> ListaCategoria = new List<ModelCategoria>();

            MySqlCommand cmd = new MySqlCommand("select * from tbCategoria;", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Categoria = new DataTable();
            da.Fill(Categoria);
            con.MyDesconectarBD();

            foreach (DataRow dt in Categoria.Rows)
            {
                ListaCategoria.Add(
                    new ModelCategoria
                    {
                        codCat = Convert.ToString(dt["codCat"]),
                        categoria = Convert.ToString(dt["Categoria"]),
                    });
            }
            return ListaCategoria;
        }


        public List<ModelVeiculo> GetCarros()
        {
            List<ModelVeiculo> Produtoslist = new List<ModelVeiculo>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat <= 2;", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelVeiculo
                    {
                        codProd = Convert.ToString(dr["codProd"]),
                        nomeProd = Convert.ToString(dr["nomeProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        valorProd = Convert.ToString(dr["valorProd"]),
                        quantidadeProd = Convert.ToString(dr["quantidadeProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"])
                    });
            }
            return Produtoslist;
        }

        public List<ModelVeiculo> GetCarroHibrido()
        {
            List<ModelVeiculo> Produtoslist = new List<ModelVeiculo>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 1", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelVeiculo
                    {
                        codProd = Convert.ToString(dr["codProd"]),
                        nomeProd = Convert.ToString(dr["nomeProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        valorProd = Convert.ToString(dr["valorProd"]),
                        quantidadeProd = Convert.ToString(dr["quantidadeProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"])
                    });
            }
            return Produtoslist;
        }

        public List<ModelVeiculo> GetCarroEletrico()
        {
            List<ModelVeiculo> Produtoslist = new List<ModelVeiculo>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 2", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelVeiculo
                    {
                        codProd = Convert.ToString(dr["codProd"]),
                        nomeProd = Convert.ToString(dr["nomeProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        valorProd = Convert.ToString(dr["valorProd"]),
                        quantidadeProd = Convert.ToString(dr["quantidadeProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"])
                    });
            }
            return Produtoslist;
        }

        public List<ModelVeiculo> GetMotos()
        {
            List<ModelVeiculo> Produtoslist = new List<ModelVeiculo>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat >= 3", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelVeiculo
                    {
                        codProd = Convert.ToString(dr["codProd"]),
                        nomeProd = Convert.ToString(dr["nomeProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        valorProd = Convert.ToString(dr["valorProd"]),
                        quantidadeProd = Convert.ToString(dr["quantidadeProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"])
                    });
            }
            return Produtoslist;
        }

        public List<ModelVeiculo> GetMotoHibrida()
        {
            List<ModelVeiculo> Produtoslist = new List<ModelVeiculo>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 3", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelVeiculo
                    {
                        codProd = Convert.ToString(dr["codProd"]),
                        nomeProd = Convert.ToString(dr["nomeProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        valorProd = Convert.ToString(dr["valorProd"]),
                        quantidadeProd = Convert.ToString(dr["quantidadeProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"])
                    });
            }
            return Produtoslist;
        }

        public List<ModelVeiculo> GetMotoEletrica()
        {
            List<ModelVeiculo> Produtoslist = new List<ModelVeiculo>();

            MySqlCommand cmd = new MySqlCommand("select * from vw_veiculos where codCat = 4", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelVeiculo
                    {
                        codProd = Convert.ToString(dr["codProd"]),
                        nomeProd = Convert.ToString(dr["nomeProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        valorProd = Convert.ToString(dr["valorProd"]),
                        quantidadeProd = Convert.ToString(dr["quantidadeProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"])
                    });
            }
            return Produtoslist;
        }

        public List<ModelCategoria> GetCategoria()
        {
            List<ModelCategoria> CategoriaList = new List<ModelCategoria>();

            MySqlCommand cmd = new MySqlCommand("select * from tbCategoria order by codCat", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                CategoriaList.Add(
                    new ModelCategoria
                    {
                        codCat = Convert.ToString(dr["codCat"]),
                        categoria = Convert.ToString(dr["Categoria"])
                    });
            }
            return CategoriaList;
        }
        public List<ModelVeiculo> GetConsProd(int id)
        {
            List<ModelVeiculo> Produtoslist = new List<ModelVeiculo>();

            MySqlCommand cmd = new MySqlCommand("select * from tbProduto where codProd=@cod", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@cod", id);
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelVeiculo
                    {
                        codProd = Convert.ToString(dr["codProd"]),
                        nomeProd = Convert.ToString(dr["nomeProd"]),
                        descricaoProd = Convert.ToString(dr["descricaoProd"]),
                        valorProd = Convert.ToString(dr["valorProd"]),
                        quantidadeProd = Convert.ToString(dr["quantidadeProd"]),
                        imagemProd = Convert.ToString(dr["imagemProd"])
                    });
            }
            return Produtoslist;
        }

    }
}