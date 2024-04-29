using System;

public class Carro : Veiculo
{
    public int NumeroDePortas { get; set; }

    public Carro(string marca, string modelo, int numeroDePortas) : base(marca, modelo)
    {
        NumeroDePortas = numeroDePortas;
    }

    public override void ExibirInfo()
    {
        base.ExibirInfo();
        Console.WriteLine($"Número de Portas: {NumeroDePortas}");
    }
}