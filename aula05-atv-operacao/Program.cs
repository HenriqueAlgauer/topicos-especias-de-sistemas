operario OperadorDeCaixa = new operario();
OperadorDeCaixa.nome = "Escobar";
OperadorDeCaixa.salario = 1200;

Console.WriteLine(OperadorDeCaixa.calcularBonus());

gerente CoordenadorLinha = new gerente();

CoordenadorLinha.nome = "Xereente !";
CoordenadorLinha.salario = 5000;
Console.WriteLine(CoordenadorLinha.calcularBonus(500));