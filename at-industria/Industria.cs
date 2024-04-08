// Estrutura com método e atributos Da classe da "Indústria"

public class Industria{

    public string nomeIndustria{get;set;} = string.Empty;
    public string localizacao{get;set;} = string.Empty;
    public int anoFundacao{get;set;}

    public List<LinhaProducao> linhasProducao = new List<LinhaProducao>();

    public void exibirInfoIndustria(){
        Console.WriteLine("\nNome da industria: " + this.nomeIndustria);
        Console.WriteLine("Localizacão: " + this.localizacao);
        Console.WriteLine("Ano de Fundação: " + this.anoFundacao);
    }

    public void adicionarLinhaProducao(LinhaProducao lP){
        this.linhasProducao.Add(lP);
    }
}