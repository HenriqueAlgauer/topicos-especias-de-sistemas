class Filme{
    public string titulo {get; set;}
    public string genero {get; set;}
    public int duracao {get; set;}
    public bool disponivel {get; set;}

    public Filme(string titulo, string genero, int duracao){
        this.titulo = titulo;
        this.genero = genero;
        this.duracao = duracao;
        this.disponivel = true;
    }

    public void locar(){
        if(this.disponivel == false){
            Console.WriteLine("O filme ja esta locado");
        }else{
            Console.WriteLine("O filme foi locado com sucesso !");
            this.disponivel = false;
        }
    }

    public void devolucao(){
        if(this.disponivel == false){
            Console.WriteLine ("O filme foi devolvido com sucesso !");
            this.disponivel = true;
        }else{
            Console.WriteLine ("O filme não foi locado !");
        }
    }

    public void disponibilidade(){
        if(this.disponivel == false){
            Console.WriteLine("Não disponível");
        }else{
            Console.WriteLine("Disponível");
        }
    }

}