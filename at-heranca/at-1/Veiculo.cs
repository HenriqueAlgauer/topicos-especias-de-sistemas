using System;
public class Veiculo
{
    public string Marca { get; set; }
    public string Modelo { get; set; }

    // Construtor da classe Veiculo
    public Veiculo(string marca, string modelo)
    {
        Marca = marca;
        Modelo = modelo;
    }
    public virtual void ExibirInfo()
    {
        Console.WriteLine($"Marca: {Marca}, Modelo: {Modelo}");
    }

    public virtual void Ligar(){
        Console.WriteLine("Veiculo ligado");
    }
}