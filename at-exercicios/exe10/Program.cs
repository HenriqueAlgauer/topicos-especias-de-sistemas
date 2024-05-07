class ReservaRegular : IReserva
{
    public string numVoo { get; set; }
    public string passageiro { get; set; }

    public ReservaRegular(string numVoo, string passageiro){
        numVoo = numVoo;
        passageiro = passageiro;
    }

    public bool ReservarVoo(){
        Console.WriteLine($"Reserva realizada no voo {numVoo} para {passageiro}.");
        return true;
    }

    public bool Cancelar(){
        Console.WriteLine($"Reserva no voo {numVoo} para {passageiro} cancelada.");
        return true;
    }

    public string VerificarStatus(){
        return $"Reserva confirmada no voo {numVoo}.";
    }
}

class ReservaUp : IReserva
{
    public string numVoo { get; set; }
    public string passageiro { get; set; }

    public ReservaUp(string numVoo, string passageiro)
    {
        numVoo = numVoo;
        passageiro = passageiro;
    }

    public bool ReservarVoo(){
        Console.WriteLine($"Reserva com upgrade realizada no voo {numVoo} para {passageiro}.");
        return true;
    }

    public bool Cancelar()
    {
        Console.WriteLine($"Reserva com upgrade no voo {numVoo} para {passageiro} cancelada.");
        return true;
    }

    public string VerificarStatus()
    {
        return $"Reserva com upgrade confirmada no voo {numVoo}.";
    }
}
public class ReservaVooGrupo : IReserva
{
    public string numVoo { get; set; }
    public int numPessoas { get; set; }

    public ReservaVooGrupo(string numVoo, int numPessoas){
        numVoo = numVoo;
        numPessoas = numPessoas;
    }

    public bool ReservarVoo(){
        Console.WriteLine($"Reserva para grupo de {numPessoas} pessoas realizada no voo {numVoo}.");
        return true;
    }

    public bool Cancelar(){
        Console.WriteLine($"Reserva para grupo de {numPessoas} pessoas no voo {numVoo} cancelada.");
        return true;
    }

    public string VerificarStatus(){
        return $"Reserva para grupo confirmada no voo {numVoo}.";
    }
}