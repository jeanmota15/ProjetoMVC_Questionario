using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;


namespace Web.Controllers
{
    public class SupervisorController : Controller
    {
        private SupervisorAplicacao aplicacao;

        public SupervisorController()
        {
            aplicacao = new SupervisorAplicacao();
        }
        public ActionResult Sucesso()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }     
        }

        [HttpPost]
        public ActionResult Cadastrar(Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                supervisor.LoginRede = Session["Usuario"].ToString();
                aplicacao.Salvar(supervisor);
                return RedirectToAction("Sucesso");
            }
            else
            {
                return View(supervisor);
            }
            
        }
    }
}