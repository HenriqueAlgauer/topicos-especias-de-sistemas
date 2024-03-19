// ./universidade.cs

class Universidade{
    public string nome {get;set;}
    public string localizacao {get;set;}
    public int anoFundacao {get;set;}

    public List<Curso> cursos = new List<Curso>();
    
    public void AdicionarCurso(Curso c){
        this.cursos.Add(c);
    }
    public void ExibirInfo(){

    }
}