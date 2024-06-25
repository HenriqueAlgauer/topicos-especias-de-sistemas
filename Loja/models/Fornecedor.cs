using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loja.models
{

    public class Fornecedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Cnpj { get; set; }
        public String Endereco { get; set; }
        public String Email { get; set; }
        public String Telefone { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}