//./funcionario.cs
public class Funcionario{
    public string cpf {get;set;}
    public string nome {get;set;}
    public string email {get;set;}
    public float salario {get;set;}

    public float calcularBonus(){
        return this.salario;
    }
}