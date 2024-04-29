public class Moto : Veiculo
{
    public int Cilindrada { get; set; }

    public Moto(string marca, string modelo, int cilindrada) : base(marca, modelo)
    {
        Cilindrada = cilindrada;
    }

    public new void ExibirInfo()
    {
        base.ExibirInfo();
        Console.WriteLine($"Cilindrada: {Cilindrada}");
    }

    public override void Ligar(){
        Console.WriteLine("Moto ligada");
    }
}