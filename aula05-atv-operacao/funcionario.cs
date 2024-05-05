//./funcionario.cs
public class funcionario{
    public string cpf {get;set;} = string.Empty;
    public string nome {get;set;} = string.Empty;
    public string email {get;set;} = string.Empty;
    public double salario {get;set;}

    public double calcularBonus(){
        return this.salario * 0.5;
    }
}