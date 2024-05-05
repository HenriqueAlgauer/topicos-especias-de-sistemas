public class motocicleta : iVeiculo{
    public string cilindrada{get;set;}
    public double PagarPedagio(double preco){
        return (preco * 0.5);
    }
}