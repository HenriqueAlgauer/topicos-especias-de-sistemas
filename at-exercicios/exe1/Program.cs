// Prompt de comando GPT-4
// Preciso que me ajude a redigir a resposta para uma questão: 'Explique o que é herança em orientação a objetos e como ela é implementada em C#'. Tente fazer de maneira resumida


// Herança é um princípio fundamental da programação orientada a objetos que permite a uma classe derivar propriedades e comportamentos de outra classe. A classe que fornece as propriedades é conhecida como classe base ou superclasse, enquanto a classe que as herda é chamada de classe derivada ou subclasse. Herança facilita a reutilização de código, promove a organização lógica do código e facilita a manutenção. 
// Ela é implementada usando o símbolo " : " [Funcionario : Operador ]. C# suporta herança simples, significando que uma classe pode herdar diretamente de apenas uma classe base.


public class Funcionario
{
    public string Nome { get; set; }
    public string Id { get; set; }
    public decimal Salario { get; set; }

    public Funcionario(string nome, string id, decimal salario){
        Nome = nome;
        ID = id;
        Salario = salario;
    }

    public void CalcularBonus(){
        Console.WriteLine($"Bônus: {Salario * 0.2}");
    }
}

public class Gerente : Funcionario{
    public Gerente(string nome, string id, decimal salario) : base(nome, id, salario){}
}

public class Operador : Funcionario{
    public Operador(string nome, string id, decimal salario) : base(nome, id, salario){}
}