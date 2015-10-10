using System;

namespace Dominio
{
    public class Supervisor
    {
        public Supervisor()
        {
            DataPesquisa = DateTime.Now;
        }

        public int Id { get; set; }

        public string Resposta1 { get; set; }

        public string Resposta2 { get; set; }

        public string Resposta3 { get; set; }

        public string Resposta4 { get; set; }

        public DateTime DataPesquisa { get; set; }

        public string LoginRede { get; set; }
    }
}
