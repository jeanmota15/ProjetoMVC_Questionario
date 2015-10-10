using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;
using System.Configuration;
using System.Web.Security;

namespace Web.Controllers
{
    public class InicioController : Controller
    {
        private OperadorAplicacao operador;
        private SupervisorAplicacao supervisor;
        private FuncionarioAplicacao funcionario;
        private AutenticacaoADAplicacao autenticacao;

        public InicioController()
        {
            operador = new OperadorAplicacao();
            supervisor = new SupervisorAplicacao();
            funcionario = new FuncionarioAplicacao("bancoFrequencia");
            autenticacao = new AutenticacaoADAplicacao();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login, string senha)
        {
            try
            {
                string AD = "";
                AppSettingsReader reader = new AppSettingsReader();
                AD = (string)reader.GetValue("ACTIVE_DIRECTORY", typeof(string));
                var isValido = autenticacao.ValidarLogin(login, senha, AD);

                if (isValido)
                {
                    FormsAuthentication.SetAuthCookie(login, false);
                    Session["Usuario"] = login;
                    string dominio = (string)reader.GetValue("DOMINIO_ALIAS", typeof(string));
                    string bancoFrequencia = (string)reader.GetValue("BANCO_FREQUENCIA", typeof(string));

                    var pesquisa = autenticacao.isPesquisaValida(login, bancoFrequencia);

                    if (pesquisa)
                    {
                        ViewBag.Mensagem = "Usuário já preencheu o questionário";
                    }

                    var funcionarioDado = autenticacao.ObterDadosFuncionario(login, dominio, AD);//tipo funcionario qd dá ponto pega qq coisa dele

                    //Funcionario func = new Funcionario();
                    //var f = funcionario.ObterPesquisa(func);//qd é do tipo pega qq coisa dele

                    //foreach (var item in f)
                    //{
                    //    if (item.Funcao)
                    //    {
                            
                    //    }
                          
                    //}

                    if (funcionarioDado != null && funcionarioDado.Funcao.ToUpper().Contains("OPERADOR"))
                    {
                        return RedirectToAction("Cadastrar","Operador");
                    }
                    else
                    {
                        return RedirectToAction("Cadastrar","Supervisor");
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;//pode ser uma msg no viewbag
            }
            return View();
        }
    }
}