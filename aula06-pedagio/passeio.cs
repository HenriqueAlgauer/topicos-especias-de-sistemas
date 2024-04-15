public class passeio : iVeiculo{
    public string combustivel{get;set;}
    public int eixos{get;set;}

    public double PagarPedagio(double preco){
        return preco*1;
    }
}