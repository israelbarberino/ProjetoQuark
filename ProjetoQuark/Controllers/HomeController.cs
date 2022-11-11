using MySql.Data.MySqlClient;
using ProjetoQuark.Dados;
using ProjetoQuark.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ProjetoQuark.Controllers
{
    public class HomeController : Controller
    {
        AcoesLogin acLg = new AcoesLogin();
        AcoesProduto acV = new AcoesProduto();


        /* ------- CONTROLE DE ACESSO - LOGIN LOGOUT   ---------- */

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ModelLogin verlogin)
        {
            acLg.TestarUsuario(verlogin);

            if (verlogin.usuario != null && verlogin.senha != null)
            {
                Session["usuarioLogado"] = verlogin.usuario.ToString();
                Session["senhaLogado"] = verlogin.usuario.ToString();

                if (verlogin.tipo == "1")
                {
                    Session["tipoLogado1"] = verlogin.tipo.ToString();
                }
                else
                {
                    Session["tipoLogado2"] = verlogin.tipo.ToString();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome de usuário e a senha";
                return View();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult semAcesso()
        {
            Response.Write("<script>alert('Sem acesso!')</script>");
            ViewBag.Message = "Acesso negado!";
            return View();
        }

        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            return RedirectToAction("Index", "Home");
        }


        /* ------- CONTROLE DA AREA PUBLICA   ---------- */

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /* ------- CONTROLES DO USUARIO LOGADO   ---------- */









        /* ------- CONTROLES DO ADMINISTRADOR DO SISTEMA - ACESSO RESTRITO   ---------- */
        public ActionResult Adm()
        {
            if (Session["usuariologado"] == null || Session["senhalogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipologado2"] == null)
                {
                    return RedirectToAction("semAcesso", "Home");
                }
                else
                {
                    ViewBag.Message = "Your contact page.";
                    return View();
                }
            }
        }

        public ActionResult CadVeiculos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadVeiculos(ModelProduto modProd)
        {
            acV.InserirVeiculo(modProd);
            ViewBag.mssge = "Cadastro efetuado com sucesso!";
            return RedirectToAction("CadVeiculos", "Home"); // redirecionar para a página CarregaPaciente e finge limpar a tela
        }

        public void CarregaProduto()
        {
            List<SelectListItem> produto = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdConcessionaria; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProduto", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    produto.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.pacientes = new SelectList(produto, "value", "Text");
        }

        public ActionResult ConsultaCarros(ModelProduto modProd)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acV.CarregaCarros(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }
        public ActionResult ConsultaMotos(ModelProduto modProd)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acV.CarregaMotos(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }
        public ActionResult ConsultaCarroEletrico(ModelProduto modProd)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acV.CarregaCarroEletrico(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }
        public ActionResult ConsultaCarroHibrido(ModelProduto modProd)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acV.CarregaCarroHibrido(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }
        public ActionResult ConsultaMotoEletrica(ModelProduto modProd)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acV.CarregaMotoEletrica(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }
        public ActionResult ConsultaMotoHibrida(ModelProduto modProd)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acV.CarregaMotoHibrida(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }

    }
}