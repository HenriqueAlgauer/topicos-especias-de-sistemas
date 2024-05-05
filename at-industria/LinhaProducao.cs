// Estrutura com atributos e metodos da classe Linha de Produção

public class LinhaProducao{

    public int numeroLinha{get;set;}
    public string tipo{get;set;}  = string.Empty;
    public int quantidade{get;set;}

    public List<Maquina> maquinas = new List<Maquina>();

    public void exibirInfoLinhaProdução(){
        Console.WriteLine("\nNumero da linha de produção: " + this.numeroLinha);
        Console.WriteLine("Tipo de produto da linha: " + this.tipo);
        Console.WriteLine("Quantidade de produtos produzidos por hora: " + this.quantidade);

    }

    public void adicionarMaquina(Maquina m){
        this.maquinas.Add(m);
    }


}