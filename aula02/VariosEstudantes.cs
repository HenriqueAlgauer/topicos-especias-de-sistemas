class VariosEstudantes{
    static void Main(){
        Estudante aluno;
        List<Estudante> estudantes = new List<Estudante>();

        for(int i = 0; i<3; i++){
            aluno = new Estudante("r"+i, "Nome"+i);
            estudantes.Add(aluno);
        }

        foreach(var estudante in estudantes){
            Console.WriteLine($"RGM: {estudante.Rgm} | Panguá: {estudante.Nome}");
        }
    }
}

