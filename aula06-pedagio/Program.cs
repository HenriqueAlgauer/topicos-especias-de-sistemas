pedagio Ped_Curitiba = new pedagio();
Ped_Curitiba.preco_eixo = 5;

passeio Santana = new passeio();
Santana.combustivel = "Gasolina";
Santana.eixos = 2;

motocicleta Mobilete = new motocicleta();
Mobilete.cilindrada = "50";

caminhao bongo = new caminhao();
bongo.eixos =  5;

Ped_Curitiba.CobrarPedagio(Santana);
Ped_Curitiba.CobrarPedagio(Mobilete);
Ped_Curitiba.CobrarPedagio(bongo);