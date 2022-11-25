using MySql.Data.MySqlClient;
using ProjetoQuark.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoQuark.Dados
{
    public class AcoesCliente
    {
        Conexao con = new Conexao();

        public void TestarUsuario(ModelLogin user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbLogin where emailCli = @emailCli and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;
            cmd.Parameters.Add("@emailCli", MySqlDbType.VarChar).Value = user.emailCli;
            cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = user.nomeCli;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.usuario = Convert.ToString(leitor["usuario"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.tipo = Convert.ToString(leitor["tipo"]);
                    user.emailCli = Convert.ToString(leitor["emailCli"]);
                    user.nomeCli = Convert.ToString(leitor["nomeCli"]);

                }
            }
            else
            {
                user.usuario = null;
                user.senha = null;
                user.tipo = null;
                user.nomeCli = null;
                user.emailCli = null;
            }
            con.MyDesconectarBD();
        }

        public void InserirCliente(ModelCliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbLogin (usuario, senha, tipo, nomeCli, emailCli) values ('usuario', @senha, '1', @nomeCli, @emailCli)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cm.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cm.tipo;
            cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = cm.nomeCli;
            cmd.Parameters.Add("@emailCli", MySqlDbType.VarChar).Value = cm.emailCli;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
        public List<ModelLogin> GetUsuario()
        {
            List<ModelLogin> LoginList = new List<ModelLogin>();

            MySqlCommand cmd = new MySqlCommand("select * from tbLogin where tipo = 1", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                LoginList.Add(
                    new ModelLogin
                    {
                        usuario = Convert.ToString(dr["usuario"]),
                        senha = Convert.ToString(dr["senha"]),
                        tipo = Convert.ToString(dr["tipo"]),
                        nomeCli = Convert.ToString(dr["nomeCli"]),
                        emailCli = Convert.ToString(dr["emailCli"])
                    });
            }
            return LoginList;
        }
    }
}