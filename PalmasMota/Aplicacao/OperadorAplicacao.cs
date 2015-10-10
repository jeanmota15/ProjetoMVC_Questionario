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
   public class OperadorAplicacao
    {
       private Contexto contexto;

       private void Inserir(Operador operador)
       {
           using (contexto = new Contexto())
           {
               string strQuery = " INSERT INTO OPERADOR2(Resposta1, Resposta2, Resposta3, Resposta4, Resposta5, DataPesquisa, LoginRede) ";
               strQuery += string.Format(" VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}') ", operador.Resposta1, operador.Resposta2,
                   operador.Resposta3, operador.Resposta4, operador.Resposta5, DateTime.Now, operador.LoginRede);
               contexto.ExecutaComando(strQuery);
           }
       }

       private void Alterar(Operador operador)
       {
           using (contexto = new Contexto())
           {
               string strQuery = " UPDATE OPERADOR2 SET ";
               strQuery += string.Format(" Resposta1 = '{0}', ", operador.Resposta1);
               strQuery += string.Format(" Resposta2 = '{0}', ", operador.Resposta2);
               strQuery += string.Format(" Resposta3 = '{0}', ", operador.Resposta3);
               strQuery += string.Format(" Resposta4 = '{0}', ", operador.Resposta4);
               strQuery += string.Format(" Resposta5 = '{0}', ", operador.Resposta5);
               strQuery += string.Format(" LoginRede = '{0}' ", operador.LoginRede);
               strQuery += string.Format(" WHERE Id = {0} ", operador.Id);

               contexto.ExecutaComando(strQuery);
           }
       }

       public void Salvar(Operador operador)
       {
           if (operador.Id > 0)
           {
               Alterar(operador);
           }
           else
           {
               Inserir(operador);
           }
       }

       public void Excluir(int id)
       {
           using (contexto = new Contexto())
           {
               string strQuery = string.Format(" DELETE FROM OPERADOR2 WHERE Id = {0} ", id);
               contexto.ExecutaComando(strQuery);
           }
       }

       public List<Operador> ListarTodos()
       {
           using (contexto = new Contexto())
           {
               string strQuery = " SELECT * FROM OPERADOR2 ";
               var retorno = contexto.ExecutaComandoComRetorno(strQuery);
               return TransformaDataReaderEmLista(retorno);
           }
       }

       public Operador ListarPorId(int id)
       {
           using (contexto = new Contexto())
           {
               string strQuery = string.Format(" SELECT * FROM OPERADOR2 WHERE Id = {0} ", id);
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

       public List<Operador> ObterPesquisa(Operador operador)
       {

          // string strQuery = null;

           using (contexto = new Contexto())
           {
               string strQuery = null;//declara a variável antes

               if (operador.LoginRede != null)
               {
                   strQuery += string.Format(" SELECT * FROM OPERADOR2 WHERE LoginRede = '{0}' ", operador.LoginRede);//vai guardar login no banco
                   var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                   return TransformaDataReaderEmLista(retorno);
               }
               else
               {
                   return null;//ñ retorna nada, ñ faz nada, se quiser coloca
               }
           }
       }

       private List<Operador> TransformaDataReaderEmLista(SqlDataReader reader)
       {
           var operadores = new List<Operador>();

           while (reader.Read())
           {
               Operador operador = new Operador();
               operador.Id = int.Parse(reader["Id"].ToString());
               operador.Resposta1 = reader["Resposta1"].ToString();
               operador.Resposta2 = reader["Resposta2"].ToString();
               operador.Resposta3 = reader["Resposta3"].ToString();
               operador.Resposta4 = reader["Resposta4"].ToString();
               operador.Resposta5 = reader["Resposta5"].ToString();
               operador.DataPesquisa = DateTime.Parse(reader["DataPesquisa"].ToString());
               operador.LoginRede = reader["LoginRede"].ToString();

               operadores.Add(operador);
           }

           reader.Close();
           return operadores;
       }
    }
}
