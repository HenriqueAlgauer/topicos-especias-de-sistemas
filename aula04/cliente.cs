public class Cliente{
    public string cpf;
    public string nome;
    public Cliente(string cpf){
        this.cpf = cpf;
        if(cpf == "888"){
            this.nome = "Pedro Lara";
        }else if(cpf == "999"){
            this.nome = "Araci";
        }
    }
    public Cliente(){};
}