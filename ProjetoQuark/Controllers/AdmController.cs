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
using System.Security.Claims;
using System.Security.Policy;
using System.Security.Cryptography;

namespace ProjetoQuark.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        AcoesLogin acLog = new AcoesLogin();
        AcoesCliente AcCli = new AcoesCliente();
        AcoesVeiculos acV = new AcoesVeiculos();


        public ActionResult CadLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadLogin(ModelLogin modLog)
        {
            acLog.InserirLogin(modLog);

            return RedirectToAction("Login", "Home");
        }

        public ActionResult CadCliente()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CadCliente(ModelCliente modCli)
        {
            AcCli.InserirCliente(modCli);

            return RedirectToAction("Login", "Home");
        }

        public void CarregaLogin()
        {
            List<SelectListItem> logins = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdConcessionaria; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLogin", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    logins.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.logins = new SelectList(logins, "value", "Text");

        }
        public ActionResult ConsultaLogin(ModelLogin modLog)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acLog.CarregaLogin(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }

        


    }
}