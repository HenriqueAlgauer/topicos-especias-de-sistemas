// Prompt de comando GPT-4
// Preciso que me ajude a redigir a resposta para uma questão: 'Explique o é interface em orientação a objeto. E depois disso faça um exemplo para de como pode ser usada para garantir a nteroperabilidade entre diferentes classes em um sistema de pagamento online. 

// Interfaces em programação orientada a objetos atuam como contratos que estipulam métodos que as classes devem implementar, sem fornecer uma implementação real. Elas são fundamentais para criar sistemas modulares e flexíveis, como em C#, pois facilitam a abstração e reduzem o acoplamento entre classes. Isso simplifica a manutenção, a expansão do código e o teste de componentes. 

public interface IPagamento{
    bool Pagamento(decimal quantia);
}

public class CartaoCredito : IPagamento{
    public bool Pagamento(decimal quantia){
        return true;
    }
}

public class AppBanco : IPagamento{
    public bool Pagamento(decimal quantia){
        return true;
    }
}

public class Maquininha{
    public void FazPagamento(IPagamento metodoPagamento, decimal quantia){
        bool sucesso = metodoPagamento.Pagamento(quantia);
        if (sucesso){
            Console.WriteLine("Pagamento reailzado !");
        }else{
            Console.WriteLine("Falha no pagamento ! ");
        }
    }
}