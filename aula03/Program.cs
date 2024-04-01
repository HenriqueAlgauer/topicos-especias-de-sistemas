Curso Curso1 = new Curso();
Turma Turma1 = new Turma();
Turma Turma2 = new Turma();

Curso1.nome = "ADS";
Curso1.duracao = 2;

Turma1.ano = 2025;
Turma2.ano = 2026;
Turma1.turno = "Noturno";
Turma2.turno = "Manha";

Curso1.AdicionarTurma(Turma1);
Curso1.AdicionarTurma(Turma2);

foreach(var T in Curso1.turmas){
    Console.WriteLine(T.turno);
}