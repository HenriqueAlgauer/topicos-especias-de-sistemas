public class ContaPoupanca : IContaBancaria{
    int numConta {get;set;}
    double saldo {get;set;}

    public ContaPoupanca(int numConta,double saldo){
        this.numConta = numConta;
        this.saldo = saldo;
    }

    public double getSaldo() {
        return this.saldo;
    }

    public void Depositar(double valor){
        Console.WriteLine("Você depositou: "+ valor);
        this.saldo += valor;
        Transacao transacao = new Transacao("Deposito", valor);
        transacao.ExibirDetalhes();
        Console.WriteLine($"Saldo da conta: {this.getSaldo()}");
    }
    public void Sacar(double valor){
        if(this.saldo < valor){
            Console.WriteLine("Saldo insuficiente");
        }else{
            this.saldo -= valor;
            Console.WriteLine($"Saque de ${valor} efetuado !");
        }
        Transacao transacao = new Transacao("Saque", valor);
        transacao.ExibirDetalhes();
        Console.WriteLine($"Saldo da conta: {this.getSaldo()}");
    }

}   