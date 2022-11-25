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
using System.Security.Cryptography;

namespace ProjetoQuark.Controllers
{
    // Criando instancias --------------------------
    public class HomeController : Controller
    {
        AcoesLogin acLg = new AcoesLogin();
        AcoesVeiculos acV = new AcoesVeiculos();
        AcoesCliente acC = new AcoesCliente();


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
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        /* ------- CONTROLES DO USUARIO LOGADO   ---------- */

        /*FAZER UM CONTROLE DE LOGIN E USUSRIO AQUI */

        public ActionResult ListarClientes()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(acC.GetUsuario());
            }
        }


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

        // ------------------ CADASTRO DE VEICULOS - SISTEMA DE CARREGAR OS DADOS PARA CADASTRO ---------------- //

        public void CarregaCategoria()
        {
            List<SelectListItem> categoria = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdConcessionaria; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCategoria", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categoria.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.categoria = new SelectList(categoria, "value", "Text");

        }


        public ActionResult CadVeiculos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadVeiculos(ModelVeiculo modV, HttpPostedFile file)
        {
            CarregaCategoria();

            modV.codCat = Request["categoria"];

            // esta dando erro ao anexar imagem /

            string arquivo = Path.GetFileName(file.FileName);
            string file2 = "/Imagens/" + Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
            file.SaveAs(_path);
            modV.imagemProd = file2;
            acV.InserirVeiculo(modV);
            ViewBag.msg = "Cadastro realizado";
            return RedirectToAction("CadVeiculos", "Home"); // redirecionar para a página CarregaPaciente e finge limpar a tela
        }


        ///////// tentando inserir seletor por radio button ////////


        /*[HttpPost]
        public ActionResult CadVeiculos(string recebeNome, int recebeOpcao)
        {
            try
            {
                AcoesVeiculos acVec = new AcoesVeiculos();
                IQueryable<HomeController> sql;
                sql = null;

                if (recebeOpcao == 1)
                {
                    sql = from c in acVec.InserirVeiculo
                          where c.nome.StartsWith(recebeNome.Trim())
                          select c;
                    TempData["opcao1"] = "nome";
                }
                return View(sql.ToList());

            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro na gravação dos dados " + ex.Message;
            }

            return View();

        }*/



        // ------------------ Pagina que carrega os dados ----------------

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
        

        // ---------------------- CONSULTAS  ---------------------- //
        public ActionResult ConsultaCarros()
        {
            return View(acV.GetCarros());
        }

        public ActionResult ConsultaCarroHibrido()
        {
            return View(acV.GetCarroHibrido());
        }

        public ActionResult ConsultaCarroEletrico()
        {
            return View(acV.GetCarroEletrico());
        }

        public ActionResult ConsultaMotos()
        {
            return View(acV.GetMotos());
        }

        public ActionResult ConsultaMotoEletrica()
        {
            return View(acV.GetMotoEletrica());
        }

        public ActionResult ConsultaMotoHibrida()
        {
            return View(acV.GetMotoHibrida());
        }


        // carrega as listas
       

        public ActionResult ListarMotocicletas()
        {
            return View(acV.GetCategoria());
        }


    }
}