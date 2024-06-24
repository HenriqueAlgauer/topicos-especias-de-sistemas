using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loja.models
{
    public class Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime dataVenda { get; set; }
        public int NumNotaFiscal { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; } // Singular
        public int quantidadeVendida { get; set; }
        public double valorVenda { get; set; }
    }
}