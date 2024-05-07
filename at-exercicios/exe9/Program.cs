class CartaoCredito : IMetodoPagamento{
    public string NumeroCartao { get; set; }
    public decimal Limite { get; set; }
    public bool Status {get; set; }

    public CartaoCredito(string numeroCartao, decimal limite){
        NumeroCartao = numeroCartao;
        Limite = limite;
    }

    public bool Pagamento(decimal quantia){
        if(Limite < quantia ){
            Console.WriteLine("Estourou o cartão! pqp");
            Status = false;
        }else{
            Limite -= quantia;
            Console.WriteLine("Pagemento feito no credito");
            Status = true;
        }

    }

    public string VerificarStatus(){
        if(Status){
            return "Pagamento aprovado.";
        }else{
            return "Pagamento não foi aprovado";
        }
    }
}

class BoletoBancario : IMetodoPagamento{
    public string CodigoBoleto { get; set; }

    public BoletoBancario(string codigoBoleto){
        CodigoBoleto = codigoBoleto;
    }

    public bool Pagamento(decimal quantia){
        Console.WriteLine($"Boleto emitido no valor de {quantia:C}. Código do boleto: {CodigoBoleto}");
        return true;
    }

    public string VerificarStatus(){
        return "Boleto emitido, aguardando pagamento.";
    }
}

class TransferenciaBancaria : IMetodoPagamento{
    public string ContaBancaria { get; set; }
    public decimal Saldo { get; set; }
    public bool Status { get; set; }

    public TransferenciaBancaria(string contaBancaria, decimal saldo){
        ContaBancaria = contaBancaria;
        Saldo = saldo;
    }

    public bool Pagamento(decimal quantia){
        if(Saldo < quantia ){
            Console.WriteLine("Estourou o cartão! pqp");
            Status = false;
        }else{
            Saldo -= quantia;
            Console.WriteLine("Pagemento feito no credito");
            Status = true;
        }

        Console.WriteLine($"Realizando transferência de {quantia} para a conta {ContaBancaria}.");
        return true;
    }

    public string VerificarStatus(){
        if(Status){
            return "Transferência realizada com sucesso.";
        }else{
            return "Transferência não realizada.";
        }
    }
}

public class Program{
    public static void Main(string[] args){
        IMetodoPagamento pagamentoCartao = new CartaoCredito("9101", 5000);
        IMetodoPagamento pagamentoBoleto = new BoletoBancario("74198");
        IMetodoPagamento pagamentoTransferencia = new TransferenciaBancaria ("000123456789", 300);

        pagamentoCartao.Pagamento(1000);
        Console.WriteLine(pagamentoCartao.VerificarStatus());

        pagamentoBoleto.Pagamento(500);
        Console.WriteLine(pagamentoBoleto.VerificarStatus());

        pagamentoTransferencia.Pagamento(750);
        Console.WriteLine(pagamentoTransferencia.VerificarStatus());
    }
}