class Produto{
    public string Nome{get;set;}
    public double Preco{get;set;}
    public int QuantidadeEmEstoque{get;set;}

    public Produto(string nome, double valor, int quantidade){
        this.Nome = nome;
        this.Preco = valor;
        this.QuantidadeEmEstoque = quantidade;
    }

    public void AddEstoque(int quantidade){
        this.QuantidadeEmEstoque += quantidade;
    }

    public void RmEstoque(int quantidade){
        if(quantidade <= 0){
            Console.WriteLine("O numero deve ser maior que 0;");
        }else if(this.QuantidadeEmEstoque < quantidade){
            Console.WriteLine("Itens em estoque menor que a quantidade de produtos para remover");
        }else{
            this.QuantidadeEmEstoque -= quantidade;   
        }
    }

    public double calc(){
        return this.QuantidadeEmEstoque * Preco;
    }
}