public class pedagio{
    public string nome{get;set;}
    public double preco_eixo{get;set;}

    //Método de cobrança
    public bool CobrarPedagio(iVeiculo veiculo){
        double preco_cobrado = veiculo.PagarPedagio(this.preco_eixo);
        Console.WriteLine(preco_cobrado);
        return true;
    }
}