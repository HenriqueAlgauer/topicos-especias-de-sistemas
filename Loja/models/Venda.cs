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
        public DateTime DataVenda { get; set; }
        public int quantidadeVendida { get; set; }
        public Double valorVenda { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public Cliente Cliente { get; set; }
        public Produto Produto { get; set; }
    }
}