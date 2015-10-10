using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio;
using Repositorio;

namespace Aplicacao
{
    public class FuncionarioAplicacao
    {
        private Contexto contexto;
        private string bancoFrequencia;

        public FuncionarioAplicacao(string bancoFrequencia)
        {
            this.bancoFrequencia = bancoFrequencia;
        }

        public void Inserir(Funcionario funcionario)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " INSERT INTO " + this.bancoFrequencia + "FUNCIONARIO(Matricula, Funcao, LoginRede) ";
                strQuery += string.Format(" VALUES('{0}', '{1}', '{2}') ", funcionario.Matricula, funcionario.Funcao, funcionario.LoginRede);
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Alterar(Funcionario funcionario)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " UPDATE " + this.bancoFrequencia + "FUNCIONARIO SET ";
                strQuery += string.Format(" Matricula = '{0}', ", funcionario.Matricula);
                strQuery += string.Format(" Funcao = '{0}', ", funcionario.Funcao);
                strQuery += string.Format(" LoginRede = '{0}' ", funcionario.LoginRede);
                strQuery += string.Format(" WHERE Matricula = '{0}' ", funcionario.Matricula);
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Excluir(string matricula)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" DELETE FROM " + this.bancoFrequencia + "FUNCIONARIO WHERE Matricula = '{0}' ", matricula);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Funcionario> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM " + this.bancoFrequencia + "FUNCIONARIO ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);
            }
        }

        public List<Funcionario> ObterPesquisa(Funcionario funcionario)//lista pode ter parâmetro
        {
            using (contexto = new Contexto())
            {
                string strQuery = null;

                if (funcionario.LoginRede != null)
                {
                    strQuery = " SELECT F.Matricula, F.Funcao, F.LoginRede FROM " + this.bancoFrequencia + "FUNCIONARIO as F with (nolock) ";
                    strQuery += " LEFT JOIN " + this.bancoFrequencia + "FUNCIONARIODADO as FD with (nolock) ON (F.Matricula = FD.Chapa) ";
                    strQuery += string.Format(" WHERE upper(FD.Status) <> 'DEMITIDO' AND F.LoginRede = '{0}' ", funcionario.LoginRede);
                    var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                    return TransformaDataReaderEmLista(retorno);
                }
                else
                {
                    return null;
                }              
            }
        }
        public Funcionario ListarPorMatricula(string matricula)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" SELECT * FROM " + this.bancoFrequencia + "FUNCIONARIO WHERE Matricula = '{0}' ", matricula);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        private List<Funcionario> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var funcionarios = new List<Funcionario>();

            while (reader.Read())
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Matricula = reader["Matricula"].ToString();
                funcionario.Funcao = reader["Funcao"].ToString();
                funcionario.LoginRede = reader["LoginRede"].ToString();

                funcionarios.Add(funcionario);
            }

            reader.Close();
            return funcionarios;
        }
    }
}
