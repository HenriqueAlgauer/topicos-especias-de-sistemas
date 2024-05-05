// ./turma.cs

public class Turma{
    public int ano {get;set;}
    public string turno {get;set;}
    public string sala {get;set;}

    public List<Estudante> estudantes = new List<Estudante>();
    // Metodo adicionar estudante
    // Recebe um estudante e adiciona esse estudante

    public void AdicionarEstudante(Estudante e){
        this.estudantes.Add(e);
    }
}