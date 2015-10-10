using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;

namespace Web.Controllers
{
    public class OperadorController : Controller
    {
        private OperadorAplicacao aplicacao;

        public OperadorController()
        {
            aplicacao = new OperadorAplicacao();
        }
        public ActionResult Sucesso()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            //if (Session["Usuario"] == null)
            //{
            //    return RedirectToAction("Index","Home");
            //}
            //else
            //{
                return View();
            //}          
        }

        [HttpPost]
        public ActionResult Cadastrar(Operador operador)
        {
            
            if (ModelState.IsValid)
            {
                operador.LoginRede = "rodrigo.mota";
                    //Session["Usuario"].ToString();
                aplicacao.Salvar(operador);
                return RedirectToAction("Sucesso");
            }
            else
            {
                return View(operador);
            }  
        }
    }
}