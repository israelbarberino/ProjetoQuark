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
using System.Web.Services.Description;
using System.Web.Security;
using System.Web.ModelBinding;

namespace ProjetoQuark.Controllers
{
    // Criando instancias --------------------------
    public class HomeController : Controller
    {
        AcoesLogin acLg = new AcoesLogin();
        AcoesVeiculos acV = new AcoesVeiculos();
        AcoesCliente acC = new AcoesCliente();
        AcoesCategoria acCat = new AcoesCategoria();
        AcoesVenda acVend = new AcoesVenda();
        AcoesItem acI = new AcoesItem();

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

        public ActionResult ListarVeiculos()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(acV.GetTodosVeiculos());
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
            List<SelectListItem> categorias = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdConcessionaria; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCategoria", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categorias.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.categorias = new SelectList(categorias, "value", "Text");
        }

        public ActionResult ConsultaCategoria(ModelCategoria modCat)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acCat.CarregaCategoria(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }

        public ActionResult CadVeiculos()
        {
            CarregaCategoria();
            return View();
        }

        [HttpPost]
        public ActionResult CadVeiculos(ModelVeiculo modVec, HttpPostedFileBase file, ModelCategoria modCat)
        {
            CarregaCategoria();

            string arquivo = Path.GetFileName(file.FileName);
            string file2 = "/Imagens/" + Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
            file.SaveAs(_path);
            modVec.imagemProd = file2;
            modVec.codCat = Request["categorias"];
            modCat.codCat = Request["categorias"];
            acV.InserirVeiculo(modVec);
            ViewBag.msg = "Cadastro realizado";
            return RedirectToAction("CadVeiculos", "Home"); // redirecionar para a página CarregaPaciente e finge limpar a tela
        }

        public ActionResult DetalharVeiculo(ModelVeiculo modVec)
        {
            CarregaVeiculos();
            return View();
        }


        // ------------------ Pagina que carrega os dados ----------------

        public void CarregaVeiculos()
        {
            List<SelectListItem> veiculo = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdConcessionaria; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProduto", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    veiculo.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.veiculos = new SelectList(veiculo, "value", "Text");
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


        // ---------------------- CARRINHO, EDITAR, EXCLUIR PRODUTOS  ---------------------- //

        public static string codigo;

        public ActionResult AdicionarCarrinho(int id, double valor)
        {

            ModelVenda carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();
            var produto = acV.GetConsProd(id);
            codigo = id.ToString();

            ModelVeiculo prod = new ModelVeiculo();

            if (produto != null)
            {
                var itemPedido = new ModelItemCarrinho();
                itemPedido.ItemPedidoID = Guid.NewGuid();
                itemPedido.ProdutoID = id.ToString();
                itemPedido.Produto = produto[0].nomeProd;
                itemPedido.Qtd = 1;
                itemPedido.valorUnit = valor;

                List<ModelItemCarrinho> x = carrinho.ItensPedido.FindAll(l => l.Produto == itemPedido.Produto);

                if (x.Count != 0)
                {
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nomeProd).Qtd += 1;
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nomeProd).valorParcial = carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nomeProd).Qtd * itemPedido.valorUnit;

                }

                else
                {
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.Add(itemPedido);
                }

                /*carrinho.ValorTotal = carrinho.ItensPedido.Select(i => i.Produto).Sum(d => d.Valor);*/

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho()
        {
            ModelVenda carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();

            return View(carrinho);
        }

        public ActionResult ExcluirItem(Guid id)
        {
            var carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();
            var itemExclusao = carrinho.ItensPedido.FirstOrDefault(i => i.ItemPedidoID == id);

            carrinho.ValorTotal -= itemExclusao.valorParcial;

            carrinho.ItensPedido.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;
            return RedirectToAction("Carrinho");
        }

        public ActionResult SalvarCarrinho(ModelVenda x)
        {

            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))

            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                /*var carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();

                ModelVenda md = new ModelVenda();
                ModelItemCarrinho mdV = new ModelItemCarrinho();

                md.DtVenda = DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy");
                md.UsuarioID = Session["codUsu"].ToString();
                md.ValorTotal = carrinho.ValorTotal;

                acVend.InserirVenda(md);


                acVend.BuscaIdVenda(x);

                for (int i = 0; i < carrinho.ItensPedido.Count; i++)
                {

                    mdV.PedidoID = x.codVenda;
                    mdV.ProdutoID = carrinho.ItensPedido[i].ProdutoID;
                    mdV.Qtd = carrinho.ItensPedido[i].Qtd;
                    mdV.valorParcial = carrinho.ItensPedido[i].valorParcial;
                    acI.InserirItem(mdV);
                }

                carrinho.ValorTotal = 0;
                carrinho.ItensPedido.Clear();*/

                return RedirectToAction("confVenda");
            }
        }

        public ActionResult confVenda()
        {
            return View();
        }


        //------------------- DETALHA - EDITA - EXCLUI PRODUTOS ---------------------//

        public ActionResult DetalhaProduto(string id)
        {

            return View(acV.GetTodosVeiculos().Find((smodel => smodel.codProd == id)));
        }


        public ActionResult AtualizaProduto(string id)
        {

            return View(acV.GetTodosVeiculos().Find((smodel => smodel.codProd == id)));
        }

        [HttpPost]
        public ActionResult AtualizaProduto(ModelVeiculo modV)
        {
            acV.AtualizaVeiculo(modV);
            return RedirectToAction("ListarVeiculos");
        }


        public ActionResult DeletaProduto(string id)
        {
            return View(acV.GetTodosVeiculos().Find((smodel => smodel.codProd == id)));
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeletaProduto(int id)
        {
            acV.DeleteProduto(id);
            return RedirectToAction("ListarVeiculos");
        }



        // ---------------------- COMPRA EFETUADA  ---------------------- //



        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index2(ModelLogin verLogin)
        {
            acLg.TestarUsuario(verLogin);

            if (verLogin.emailCli != null && verLogin.senha != null)
            {
                FormsAuthentication.SetAuthCookie(verLogin.emailCli, false);
                Session["usuarioLogado"] = verLogin.emailCli.ToString();
                Session["senhaLogado"] = verLogin.senha.ToString();

                return RedirectToAction("Carrinho", "Home");
            }

            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha";
                return View();

            }

        }
    }
}