public class caminhao:iVeiculo{
    public int eixos {get;set;}
    public string tipoCargo {get;set;}
    public bool cargaRisco {get;set;}
    public double PagarPedagio(double preco){
        return preco * this.eixos;
    }
}