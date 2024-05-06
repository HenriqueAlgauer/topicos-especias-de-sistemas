using System;

namespace App
{
    class Veiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }

        public Veiculo(string marca, string modelo)
        {
            Marca = marca;
            Modelo = modelo;
        }
    }


    class Carro : Veiculo
    {
        public int NumeroDePortas { get; set; }

        public Carro(string marca, string modelo, int numeroDePortas) : base(marca, modelo)
        {
            NumeroDePortas = numeroDePortas;
        }
    }

    class Moto : Veiculo
    {
        public int Cilindrada { get; set; }

        public Moto(string marca, string modelo, int cilindrada) : base(marca, modelo)
        {
            Cilindrada = cilindrada;
        }
    }

    class Bike : Veiculo
    {
        public int numMarchas { get; set; }

        public Bike(string marca, string modelo, int marchas) : base(marca, modelo)
        {
            numMarchas = marchas;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Carro meuCarro = new Carro("Toyota", "Corolla", 4);
            Moto minhaMoto = new Moto("Honda", "CB500", 500);
            Bike minhaBike = new Bike("Oggi", "7.1", 12);

            Console.WriteLine(minhaBike.numMarchas);
        }
    }
}


