using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Operador
    {

        public Operador()
        {
            DataPesquisa = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessage="Preenchimento Obrigatório")]
        public string Resposta1 { get; set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string Resposta2 { get; set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string Resposta3 { get; set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string Resposta4 { get; set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string Resposta5 { get; set; }

        public DateTime DataPesquisa { get; set; }

        public string LoginRede { get; set; }
    }
}
