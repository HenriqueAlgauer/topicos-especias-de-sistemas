// Prompt de comando GPT-4
// Preciso que me ajude a redigir a resposta para uma questão: resuma o que é sobrecarga 
// Agora resuma o que é sobreposição 
// Me ajude com um exeplo de cada um 

// A sobrecarga de métodos acontece quando uma classe possui múltiplos métodos com o mesmo nome, mas diferenciados pelo número, tipo ou ordem de seus parâmetros. Isso permite que os métodos realizem tarefas similares com diferentes tipos de entrada, facilitando a interação do usuário com o objeto sem a necessidade de memorizar vários nomes de métodos. 

//Sobrecarga -> é passado diferentes construtores para uma mesma classe, em que se encaixam diferentes parâmetros
public class Calculadora
{
    public int Somar(int a, int b){
        return a + b;
    }

    public int Somar(int a, int b, int c){
        return a + b + c;
    }

    public double Somar(double a, double b){
        return a + b;
    }
}

// A sobreposição ocorre quando uma subclasse redefine um método de sua superclasse. O método na subclasse deve ter a mesma assinatura que o método na superclasse. Isso é utilizado para modificar o comportamento de métodos herdados de acordo com a necessidade da subclasse, mantendo o mesmo contrato definido pela superclasse.

// Sobreposição-> um conceito do Polimorfismo: diferentes significados para um mesmo método, em que é possível personalizar para cada classe, tornando um código mais legível 
public class Animal{
    public virtual void EmitirSom(){
        Console.WriteLine("Som genérico de animal");
    }
}

public class Cachorro : Animal{
    public override void EmitirSom(){
        Console.WriteLine("Au AU !");
    }
}