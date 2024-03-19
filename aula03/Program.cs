Universidade objUniversidade = new Universidade();

objUniversidade.nome = "UP";
objUniversidade.localizacao = "Ecoville";
objUniversidade.anoFundacao = 1990;

Curso objCurso = new Curso();
objCurso.nome = "ADS";
objUniversidade.AdicionarCurso(objCurso);


foreach(var curso in objUniversidade.cursos){
    Console.WriteLine(curso.nome);
}


Console.Write(objUniversidade.nome);