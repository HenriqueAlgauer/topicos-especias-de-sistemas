//./gerente.cs  
public class gerente : funcionario{
    public double calcularBonus(double vlrExtra){
        double bonusPadrao = base.calcularBonus();
        
        return this.salario + vlrExtra;
    }
}