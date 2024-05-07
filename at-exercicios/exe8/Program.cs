class Curso{
    public string NomeCurso { get; set; }
    public string Idioma { get; set; }
    public float Nota1 { get; set; }
    public float Nota2 { get; set; }

    public Curso(string nomeCurso, string idioma, float nota1, float nota2){
        NomeCurso = nomeCurso;
        Idioma = idioma;
        Nota1 = nota1;
        Nota2 = nota2;
    }

    public void CalcularMedia(){
        float media = (Nota1 + Nota2) / 2;
        return media;
    }
    public void EmitirCertificado(){
        if(this.CalcularMedia() < 6){
            Console.WriteLine("Dançou peão! Estuda mais ai caboco");
        }else{
            Console.WriteLine($"Certificado do curso de {Idioma}, concluído !");
        }
    }
}

public class Ingles : Curso{
    public Ingles(string nomeCurso) : base(nomeCurso, "Inglês"){}

}

public class Espanhol : Curso{
    public Espanhol(string nomeCurso) : base(nomeCurso, "Espanhol"){}

}

public class Frances : Curso{
    public Frances(string nomeCurso) : base(nomeCurso, "Francês"){}

}