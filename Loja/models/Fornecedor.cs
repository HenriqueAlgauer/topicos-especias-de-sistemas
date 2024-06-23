namespace loja.models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public int Cnpj { get; set; }
        public String? Nome { get; set; }
        public String? Endereco { get; set; }
        public String? Email { get; set; }
        public int Telefone { get; set; }
    }
}
