public interface IMetodoPagamento
{
    bool Pagamento(decimal quantia);
    string VerificarStatus();
}