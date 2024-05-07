Produto produto= new Produto("P1", 10, 10);

produto.AddEstoque(10);
produto.RmEstoque(15);
Console.WriteLine(produto.calc());