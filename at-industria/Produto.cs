public class Produto{

    public string nomeProduto{get;set;} = string.Empty;
    public int codigo{get;set;}
    public double preco{get;set;}

    public void exibirInfoProduto(){
        Console.WriteLine("\nNome Produto: " + this.nomeProduto);
        Console.WriteLine("Codigo: " + this.codigo);
        Console.WriteLine("Preço: " + this.preco);
    }

    public void atualizarPreco(double novoPreco){
        
        this.preco = novoPreco;

        Console.WriteLine("\nPreço atualizado\nNovo preço: " + this.preco);

    }
}