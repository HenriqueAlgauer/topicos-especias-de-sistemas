Industria industria01 = new Industria();

LinhaProducao linhaPd01 = new LinhaProducao();
LinhaProducao linhaPd02 = new LinhaProducao();

Maquina maquina01 = new Maquina();
Maquina maquina02 = new Maquina();
Maquina maquina03 = new Maquina();
Maquina maquina04 = new Maquina();

Produto produto01 = new Produto();
Produto produto02 = new Produto();
Produto produto03 = new Produto();
Produto produto04 = new Produto();

industria01.nomeIndustria = "Industria 1";
industria01.localizacao = "ACRE, Brasil";
industria01.anoFundacao = 2010;

linhaPd01.numeroLinha = 1;
linhaPd01.tipo = "micro componentes";
linhaPd01.quantidade = 2000;

linhaPd02.numeroLinha = 2;
linhaPd02.tipo = "micro processadores";
linhaPd02.quantidade = 2000;

maquina01.id = 01;
maquina01.marca = "Maquina BOA";
maquina01.modelo = "MQBOA- 01";

maquina02.id = 02;
maquina02.marca = "Maquina GOOD";
maquina02.modelo = "MQGOOD - 01";

maquina03.id = 03;
maquina03.marca = "Maquina BOA";
maquina03.modelo = "MQBOA - 02";

maquina04.id = 04;
maquina04.marca = "Maquina Very Nice";
maquina04.modelo = "MQNICE - 01";

produto01.nomeProduto = "Lente grande angular";
produto01.codigo = 056114;
produto01.preco = 230;

produto02.nomeProduto = "Mola";
produto02.codigo = 565415;
produto02.preco = 8.5;

produto03.nomeProduto = "Capacitor";
produto03.codigo = 65544;
produto03.preco = 59.7;

produto04.nomeProduto = "Placa";
produto04.codigo = 519563;
produto04.preco = 64.24;


industria01.adicionarLinhaProducao(linhaPd01);

industria01.adicionarLinhaProducao(linhaPd02);


linhaPd01.adicionarMaquina(maquina01);

linhaPd01.adicionarMaquina(maquina02);

linhaPd02.adicionarMaquina(maquina03);

linhaPd02.adicionarMaquina(maquina04);


maquina01.adicionarProduto(produto01);

maquina02.adicionarProduto(produto02);

maquina03.adicionarProduto(produto03);

maquina04.adicionarProduto(produto04);


industria01.exibirInfoIndustria();


linhaPd01.exibirInfoLinhaProdução();

linhaPd02.exibirInfoLinhaProdução();


maquina01.exibirInfoMaquina();

maquina02.exibirInfoMaquina();

maquina03.exibirInfoMaquina();

maquina04.exibirInfoMaquina();



produto01.exibirInfoProduto();

produto02.exibirInfoProduto();

produto03.exibirInfoProduto();

produto04.exibirInfoProduto();



maquina01.iniciarProducao();

maquina02.iniciarProducao();

maquina03.iniciarProducao();

maquina04.iniciarProducao();