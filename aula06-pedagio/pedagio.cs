public class pedagio{
    public string nome{get;set;}
    public double preco_eixo{get;set;}

    //Método de cobrança
    public bool CobrarPedagio(passeio veiculo){
        double preco_cobrado = veiculo.PagarPedagio(this.preco_eixo);
        Console.WriteLine(preco_cobrado);
        return true;
    }
    public bool CobrarPedagio(passeio moto){
        double preco_cobrado = moto.PagarPedagio(this.preco_eixo);
        Console.WriteLine(preco_cobrado);
        return true;
    }
}