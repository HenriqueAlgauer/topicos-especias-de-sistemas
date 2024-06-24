using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loja.models
{
    public class Servico
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Status { get; set; }


    }
}