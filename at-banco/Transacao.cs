class Transacao{
    string tipo {get; set;}
    double valor {get; set;}

    public Transacao(string tipo, double valor) {
        this.tipo = tipo;
        this.valor = valor;
    }

    public void ExibirDetalhes(){
        Console.WriteLine($"Transação solicitada: {this.tipo} || Valor: ${this.valor}");
    }
}