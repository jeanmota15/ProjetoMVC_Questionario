using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio;
using Repositorio;
using System.DirectoryServices;

namespace Aplicacao
{
    public class AutenticacaoADAplicacao
    {
        private Contexto contexto;
        //private OperadorAplicacao operador;
        //private SupervisorAplicacao supervisor;
        //private FuncionarioAplicacao funcionario;

        public AutenticacaoADAplicacao()
        {
            contexto = new Contexto();
            //operador = new OperadorAplicacao();
            //supervisor = new SupervisorAplicacao();
            //funcionario = new FuncionarioAplicacao("bancoFrequencia");
        }

        public bool ValidarLogin(string User, string Senha, string active_directory)
        {
            try
            {
                DirectoryEntry objAD = null;

                objAD = new DirectoryEntry("LDAP://" + active_directory, User, Senha);

                Object obj = objAD.NativeObject;
                DirectorySearcher search = new DirectorySearcher(objAD);

                search.Filter = "(SAMAccountName=" + User + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                //var usuario = int.Parse(User);

                if (result == null)
                    return false;

                  //else if (User.Count() < 1)
                //   {
                //      return true;
                //   }

                else
                    //Usuário Autenticado.
                    return true;
            }
            catch (DirectoryServicesCOMException ex)
            {
                throw;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool isPesquisaValida(string login, string bancoFrequencia)
        {
            bool retorno = false;

            OperadorAplicacao operador = new OperadorAplicacao();
            SupervisorAplicacao supervisor = new SupervisorAplicacao();
            List<Operador> listaOperador;
            List<Supervisor> listaSupervisor;
            Operador objOperador = new Operador();
            Supervisor objSupervisor = new Supervisor();

            objOperador.LoginRede = login;
            objSupervisor.LoginRede = login;

            listaOperador = operador.ObterPesquisa(objOperador);
            listaSupervisor = supervisor.ObterPesquisa(objSupervisor);

            if (listaOperador != null && listaOperador.Count > 0)
            {
                retorno = true;
            }
            if (listaSupervisor != null && listaSupervisor.Count > 0)
            {
                retorno = true;
            }

            return retorno;
        }

        public Funcionario ObterDadosFuncionario(string login, string dominio, string bancoFrequencia)
        {
            FuncionarioAplicacao funcionario = new FuncionarioAplicacao(bancoFrequencia);//pega do método
            Funcionario funcRetorno = null;//cria pra retornar
            List<Funcionario> listaFuncionario;//cria pra receber o método
            Funcionario objFuncionario = new Funcionario();//cria o objeto

            objFuncionario.LoginRede = dominio + "/" + login;//banco já pegou no método aplicação

            listaFuncionario = funcionario.ObterPesquisa(objFuncionario);

            if (listaFuncionario != null && listaFuncionario.Count > 0)
            {
                funcRetorno = listaFuncionario[0];//p/ receber tds
            }

            return funcRetorno;
        }

    }
}
