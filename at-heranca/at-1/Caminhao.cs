using System;

public class Caminhao : Veiculo
{
    public int CapacidadeDeCarga { get; set; }

    public Caminhao(string marca, string modelo, int capacidadeDeCarga) : base(marca, modelo)
    {
        CapacidadeDeCarga = capacidadeDeCarga;
    }

    public override void ExibirInfo()
    {
        base.ExibirInfo();
        Console.WriteLine($"Capacidade de Carga: {CapacidadeDeCarga}");
    }
}