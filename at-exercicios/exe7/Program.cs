class ContaBancaria{
    public string NumConta { get; protected set; }
    public double Saldo { get; protected set; }

    public ContaBancaria(string numConta, double saldo){
        NumConta = numConta;
        Saldo = saldoInicial;
    }

    public void Depositar(double valor){
        if (valor > 0){
            Saldo += valor;
            Console.WriteLine($"Depósito de {valor} pila realizado com sucesso !!");
        }else{
            Console.WriteLine("Digite um numero mair que 0.");
        }
    }

    public virtual void Sacar(double valor){
        if (valor > 0 && Saldo >= valor){
            Saldo -= valor;
            Console.WriteLine($"Saque de {valor:C} realizado com sucesso.");
        }else{
            Console.WriteLine("Saldo indisponível panguá !!.");
        }
    }

    public void Saldo(){
        Console.WriteLine($"Saldo atual: {Saldo} pila");
    }
}

class ContaPoupanca : ContaBancaria{
    public double TaxaJuro { get; set; }

    public ContaPoupanca(string numeroConta, double saldoInicial, double taxaJuro) : base(numeroConta, saldoInicial){
        TaxaJuro = taxaJuro;
    }

    public void RendimentoJuros(){
        double juros = Saldo * TaxaJuro / 100;
        Saldo += juros;
        Console.WriteLine($"Taxa de juros: {TaxaJuro}");
        Console.WriteLine($"Saldo atual: {Saldo}");
    }
}

class ContaCorrente : ContaBancaria{
    public ContaCorrente(string numeroConta, double saldoInicial) : base(numeroConta, saldoInicial){}

    public void EmitirCheque(double valor){
        if (valor <= 0){
            Console.WriteLine("Valor do cheque deve ser positivo.");
        }
        if (Saldo >= valor){
            Console.WriteLine($"Cheque no valor de {valor:C} emitido com sucesso.");
            Saldo -= valor;
        }else{
            Console.WriteLine("Saldo insuficiente para emitir o cheque.");
        }
    }
}

public class ContaEmpresarial : ContaBancaria{
    private double SaldoMinimo { get; set; }
    public ContaEmpresarial(string numeroConta, double saldoInicial, double saldoMinimo) : base(numeroConta, saldoInicial){
        SaldoMinimo = saldoMinimo;
    }

    public bool VerificaSaldo(){
        if(Saldo < SaldoMinimo){
            Console.WriteLine("Sua conta está em débito com o banco! Deposite o valor do saldo mínimo");
            return false;
        }else{
            Console.WriteLine("Sua conta esta com o saldo mínimo em dia !");
            return true;
        }
    }
}


public class Program{
    public static void Main(string[] args){
        ContaPoupanca poupanca = new ContaPoupanca("131223", 500, 2);
        poupanca.Depositar(500);
        poupanca.RendimentoJurosJuros();
        poupanca.Saldo();

        ContaEmpresarial empresarial = new ContaEmpresarial("2949", 100200, 5000);
        empresarial.Depositar(2000);
        empresarial.Sacar(2500);
        empresarial.Saldo();
    }
}
