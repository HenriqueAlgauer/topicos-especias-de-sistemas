Motor Turbo20 = new Motor();
Turbo20.combustivel = "Gasolina";
Turbo20.potencia = "150CV";
Veiculo Carro = new Veiculo(Turbo20);

Console.WriteLine(Turbo20.potencia);
Console.WriteLine(Carro.motor.potencia);