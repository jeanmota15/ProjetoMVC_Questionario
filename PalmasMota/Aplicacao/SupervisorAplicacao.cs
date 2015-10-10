using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Repositorio;
using System.Data;
using System.Data.SqlClient;

namespace Aplicacao
{
    public class SupervisorAplicacao
    {
        private Contexto contexto;

        private void Inserir(Supervisor supervisor)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " INSERT INTO SUPERVISOR2(Resposta1, Resposta2, Resposta3, Resposta4, LoginRede) ";
                strQuery += string.Format(" VALUES('{0}', '{1}', '{2}', '{3}', '{4}') ", supervisor.Resposta1, supervisor.Resposta2,
                    supervisor.Resposta3, supervisor.Resposta4, supervisor.LoginRede);
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Supervisor supervisor)
        {
            using (contexto = new Contexto())
            {
                string strQuery = " UPDATE SUPERVISOR2 SET ";
                strQuery += string.Format(" Resposta1 = '{0}', ", supervisor.Resposta1);
                strQuery += string.Format(" Resposta2 = '{0}', ", supervisor.Resposta2);
                strQuery += string.Format(" Resposta3 = '{0}', ", supervisor.Resposta3);
                strQuery += string.Format(" Resposta4 = '{0}', ", supervisor.Resposta4);
                strQuery += string.Format(" LoginRede = '{0}' ", supervisor.LoginRede);
                strQuery += string.Format(" WHERE Id = {0} ", supervisor.Id);

                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Supervisor supervisor)
        {
            if (supervisor.Id > 0)
            {
                Alterar(supervisor);
            }
            else
            {
                Inserir(supervisor);
            }
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" DELETE FROM SUPERVISOR2 WHERE Id = {0} ", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Supervisor> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                string strQuery = " SELECT * FROM SUPERVISOR2 ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno);
            }
        }

        public Supervisor ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = string.Format(" SELECT * FROM SUPERVISOR2 WHERE Id = {0} ", id);
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaDataReaderEmLista(retorno).FirstOrDefault();
            }
        }

        //public Operador ListarPorLogin(string login)
        //{
        //    using (contexto = new Contexto())
        //    {
        //        string strQuery = string.Format(" SELECT * FROM OPERADOR2 WHERE Id = '{0}' ", login);
        //        var retorno = contexto.ExecutaComandoComRetorno(strQuery);
        //        return TransformaDataReaderEmLista(retorno).FirstOrDefault();
        //    }
        //}

        public List<Supervisor> ObterPesquisa(Supervisor supervisor)
        {

            // string strQuery = null;

            using (contexto = new Contexto())
            {
                string strQuery = null;

                if (supervisor.LoginRede != null)
                {
                    strQuery += string.Format(" SELECT * FROM SUPERVISOR2 WHERE LoginRede = '{0}' ", supervisor.LoginRede);//vai guardar login no banco
                    var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                    return TransformaDataReaderEmLista(retorno);
                }
                else
                {
                    return null;
                }
            }
        }

        private List<Supervisor> TransformaDataReaderEmLista(SqlDataReader reader)
        {
            var supervisores = new List<Supervisor>();

            while (reader.Read())
            {
                Supervisor supervisor = new Supervisor();
                supervisor.Id = int.Parse(reader["Id"].ToString());
                supervisor.Resposta1 = reader["Resposta1"].ToString();
                supervisor.Resposta2 = reader["Resposta2"].ToString();
                supervisor.Resposta3 = reader["Resposta3"].ToString();
                supervisor.Resposta4 = reader["Resposta4"].ToString();
                supervisor.DataPesquisa = DateTime.Parse(reader["DataPesquisa"].ToString());
                supervisor.LoginRede = reader["LoginRede"].ToString();

                supervisores.Add(supervisor);
            }

            reader.Close();
            return supervisores;
        }
    }
}

