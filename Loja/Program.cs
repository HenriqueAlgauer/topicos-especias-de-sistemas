using loja.models;
using loja.services;
using loja.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o serviço do ProductService
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<UsuarioService>();
// builder.Services.AddScoped<VendaService>();
// builder.Services.AddScoped<DepositoService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Starta Conexão Com o BD 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 37))));

// Configuração do JWT
var secretKey = "asdcasdcasdcasdcasdcasdcasdcasdc";
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


// Adiciona serviços de autorização
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Método para gerar o token
string GenerateToken(string data)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

// Endpoint para login e geração de token
app.MapPost("/login", async (UsuarioService usuarioService, Usuario usuario) =>
{
    var user = await usuarioService.GetUsuarioByLoginAsync(usuario.Email);

    if (user != null && user.Senha == usuario.Senha)
    {
        var token = GenerateToken(usuario.Email);
        return Results.Ok(new { message = $"Bem Vindo {usuario.Email}!", token = token });
    }

    return Results.BadRequest(user == null ? "Usuário Inválido" : "Senha inválida");
});

// Endpoint de rota segura
app.MapGet("/rotaSegura", async (HttpContext context) =>
{
    if (!context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Token não fornecido");
        return;
    }

    var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    var tokenHandler = new JwtSecurityTokenHandler();
    SecurityToken validatedToken;

    try
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        await context.Response.WriteAsync($"Autorizado, token: {token}");
    }
    catch (Exception)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Token inválido");
    }
});

// Métodos do usuário 
app.MapGet("/rotaSegura/usuarios", [Authorize] async (UsuarioService usuarioService) =>
{
    var usuarios = await usuarioService.GetAllUsuariosAsync();
    if (usuarios != null)
    {

        return Results.Ok(usuarios);
    }

    return Results.BadRequest("Nenhum Usuário Cadastrado");
});

app.MapGet("/rotaSegura/usuario/{id}", [Authorize] async (UsuarioService usuarioService, int id) =>
{
    var usuario = await usuarioService.GetUsuarioByIdAsync(id);

    if (usuario != null)
    {
        return Results.Ok(usuario);
    }

    return Results.BadRequest($"Nenhum usuário com o ID {id} Localizado");
});

app.MapPost("/addUsuario", async (UsuarioService usuarioService, Usuario usuario) =>
{
    await usuarioService.AddUsuarioAsync(usuario);
    return Results.Created($"/Usuario/{usuario.Id}", usuario);
});

app.MapPut("/rotaSegura/updateUsuario/{id}", [Authorize] async (UsuarioService usuarioService, int id, Usuario usuario) =>
{
    var userFound = await usuarioService.GetUsuarioByIdAsync(id);

    if (userFound != null)
    {
        await usuarioService.UpdateUsuarioAsync(id, usuario);
        return Results.Created($"/rotaSegura/usuario/{id}", usuario);
    }
    return Results.BadRequest("Usuario não localizado no sistema");
});

app.MapDelete("/rotaSegura/deleteUsuario/{id}", [Authorize] async (UsuarioService usuarioService, int id) =>
{
    await usuarioService.DeleteUsuarioAsync(id);
    return Results.Ok($"Usuario com o ID {id} deletado");
});



// Novo MapGet pega todos os produtos usando o serviço ProductService
app.MapGet("/rotaSegura/produtos", [Authorize] async (ProductService productService) =>
{
    var produtos = await productService.GetAllProductsAsync();
    return Results.Ok(produtos);
});

// Novo MapGet pega produtos pelo ID usando o serviço ProductService
app.MapGet("/rotaSegura/produtos/{id}", [Authorize] async (int id, ProductService productService) =>
{
    var produto = await productService.GetProductByIdAsync(id);
    if (produto == null)
    {
        return Results.NotFound($"Product with ID {id} not found.");
    }
    return Results.Ok(produto);
});

// Novo MapPost cria um produto usando o serviço ProductService
app.MapPost("/rotaSegura/createProdutos", [Authorize] async (Produto produto, ProductService productService) =>
{
    await productService.AddProductAsync(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

// Novo MapPut atualiza um produto usando o serviço ProductService
app.MapPut("/rotaSegura/updateProdutos/{id}", [Authorize] async (int id, Produto produto, ProductService productService) =>
{
    if (id != produto.Id)
    {
        return Results.BadRequest("Product ID mismatch.");
    }
    await productService.UpdateProductAsync(produto);
    return Results.Ok();
});

// Novo MapDelete deleta um produto usando o serviço ProductService
app.MapDelete("/rotaSegura/deleteProdutos/{id}", [Authorize] async (int id, ProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.Ok("Produto Deletado");
});

// Endpoint's Cliente

// End point de criação do Cliente
app.MapPost("/rotaSegura/createCliente", [Authorize] async (Cliente newCliente, ClienteService clienteService) =>
{
    await clienteService.AddClienteAsync(newCliente);
    return Results.Created($"createCliente/{newCliente.Id}", newCliente);
});

// EndPoint de mostrar todos Clientes cadastrados

app.MapGet("/rotaSegura/Clientes", [Authorize] async (ClienteService clienteService) =>
{

    var Cliente = await clienteService.GetAllClienteAsync();

    return Results.Ok(Cliente);

});

// EndPoint para pesquisar Cliente por ID

// Lembrar que para procurar voce so precisa do LojaDbContext dbContext para criação e update precisa Model newModel ou updateModel

app.MapGet("/rotaSegura/Clientes/{id}", [Authorize] async (int id, ClienteService clienteService) =>
{

    var Cliente = await clienteService.GetClienteByIdAsync(id);

    if (Cliente == null)
    {
        return Results.NotFound($"Nenhum registro de cliente com o ID {id}");
    }


    return Results.Ok(Cliente);

});

// EndPoint para editar um Cliente

app.MapPut("/rotaSegura/clienteUpdate/{id}", [Authorize] async (int id, ClienteService clienteService, Cliente updateCliente) =>
{
    if (id != updateCliente.Id)
    {
        return Results.BadRequest("O ID nao corresponde a um ID ja cadastrado");
    }
    await clienteService.UpdateClienteAsync(updateCliente);
    return Results.Ok();
});

// EndPoint para deletar um cliente

app.MapDelete("/rotaSegura/clienteDelete/{id}", [Authorize] async (int id, ClienteService clienteService) =>
{
    await clienteService.DeleteClienteAsync(id);
    return Results.Ok("Cliente deletado");
});

// EndPoint's para o fornecedor

// EndPoint de criação de fornecedor

app.MapPost("/rotaSegura/createFornecedor", [Authorize] async (FornecedorService fornecedorService, Fornecedor newFornecedor) =>
{
    await fornecedorService.AddFornecedorAsync(newFornecedor);
    return Results.Created($"/createFornecedor/{newFornecedor.Id}", newFornecedor);
});

// EndPoint de Busca de todos os fornecedores

app.MapGet("/rotaSegura/Fornecedores", [Authorize] async (FornecedorService fornecedorService) =>
{
    var fornecedores = await fornecedorService.GetAllFornecedoresAsync();
    return Results.Ok(fornecedores);
});

// EndPoint de busca de Fornecedor por ID

app.MapGet("/rotaSegura/Fornecedores/{id}", [Authorize] async (int id, FornecedorService fornecedorService) =>
{
    var fornecedor = await fornecedorService.GetFornecedorByIdAsync(id);

    if (fornecedor == null)
    {
        return Results.NotFound($"Nenhum registro de cliente com o ID {id}");
    }

    return Results.Ok(fornecedor);

});

// EndPoint de alteração de dados de fornecedor por ID

app.MapPut("/rotaSegura/updateFornecedor/{id}", [Authorize] async (int id, FornecedorService fornecedorService, Fornecedor fornecedor) =>
{
    if (id != fornecedor.Id)
    {
        return Results.BadRequest("ID fornecedor não localizado nos registro");
    }
    await fornecedorService.UpdateFornecedorAsync(fornecedor);
    return Results.Ok(fornecedor);
});


// EndPoint para deletar fornecedor

app.MapDelete("/rotaSegura/deleteFornecedor/{id}", [Authorize] async (int id, FornecedorService fornecedorService) =>
{
    await fornecedorService.DeleteFornecedorAsync(id);
    return Results.Ok("Fornecedor Deletado com sucesso");
});

// EndPoint's da Venda 

// EndPoint para adicionar uma venda 
// app.MapPost("/rotaSegura/createVenda", [Authorize] async (VendaService vendaService, ProductService produtoService, ClienteService clienteService, Venda venda) =>
// {
//     var produtoVenda = await produtoService.GetProductByIdAsync(venda.IdProduto);
//     var clienteVenda = await clienteService.GetClienteAsync(venda.IdCliente);

//     if (produtoVenda != null && clienteVenda != null)
//     {
//         if (venda.quantidadeVendida <= produtoVenda.QuantidadeEstoque)
//         {
//             await vendaService.AddVendaAsync(venda);
//             produtoVenda.QuantidadeEstoque -= venda.quantidadeVendida;
//             await produtoService.UpdateProductAsync(produtoVenda); // Certifique-se de atualizar o produto no banco de dados

//             return Results.Ok(venda);
//         }
//         return Results.BadRequest("Quantidade Vendida maior que disponível em estoque");
//     }

//     return Results.BadRequest("Cliente ID ou Produto ID inválidos");
// });

// // EndPoint para buscar todas as vendas
// app.MapGet("/rotaSegura/vendas", [Authorize] async (VendaService vendaService) =>
// {
//     var vendas = await vendaService.GetAllVendasAsync();

//     if (vendas != null)
//     {
//         return Results.Ok(vendas);
//     }

//     return Results.BadRequest("Nenhum usuário cadastrado");
// });

// EndPoint para pegar venda por Id
// app.MapGet("/rotaSegura/venda/{id}", [Authorize] async (VendaService vendaService, int id) =>
// {
//     var venda = await vendaService.GetVendaByIdAsync(id);

//     if (venda != null)
//     {
//         return Results.Ok(venda);
//     }

//     return Results.BadRequest($"Nenhuma Venda encontrada com o ID {id}");
// });

// app.MapPut("/rotaSegura/vendaUpdate/{id}", [Authorize] async (VendaService vendaService, int id) =>
// {
//     var Venda = await vendaService.GetVendaByIdAsync(id);

//     if (Venda != null)
//     {
//         await vendaService.UpdateVendaAsync(Venda);
//         return Results.Ok(Venda);
//     }

//     return Results.BadRequest("ID fornecido inválido");
// });

// app.MapDelete("/rotaSegura/vendaDelete/{id}", [Authorize] async (VendaService vendaService, int id) =>
// {
//     await vendaService.DeleteVendaAsync(id);
//     return Results.Ok($"Venda com o ID {id} deletada");
// });

// // EndPoint's Deposito

// app.MapPost("/rotaSegura/depositoCreate", [Authorize] async (DepositoService depositoService, ProductService produtoService, Deposito deposito) =>
// {
//     var produtoDeposito = await produtoService.GetProductByIdAsync(deposito.IdProduto);

//     if (produtoDeposito != null)
//     {
//         await depositoService.AddDepositoAsync(deposito);
//         produtoDeposito.QuantidadeEstoque += deposito.quantidade;
//         await produtoService.UpdateProductAsync(produtoDeposito);
//         return Results.Ok(deposito);
//     }
//     return Results.BadRequest("Id de produto inválido");
// });

// app.MapGet("/rotaSegura/depositos", [Authorize] async (DepositoService depositoService) =>
// {
//     var depositos = await depositoService.GetAllDepositoAsync();

//     if (depositos != null)
//     {
//         return Results.Ok(depositos);
//     }

//     return Results.BadRequest("Nenhum produto cadastrado");
// });

// app.MapGet("/rotaSegura/deposito/{id}", [Authorize] async (DepositoService depositoService, int id) =>
// {
//     var deposito = await depositoService.GetDepositoById(id);

//     if (deposito != null)
//     {
//         return Results.Ok(deposito);
//     }

//     return Results.BadRequest("Id fornecido inválido");
// });

// app.MapPut("/rotaSegura/depositoUpdate/{id}", [Authorize] async (DepositoService depositoService, int id, Deposito deposito) =>
// {
//     var depositoFound = await depositoService.GetDepositoById(id);

//     if (depositoFound.Id == deposito.Id)
//     {
//         await depositoService.UpdateDeposito(deposito);
//         return Results.Ok(deposito);
//     }

//     return Results.BadRequest("ID produto inválido");
// });

// app.MapDelete("/rotaSegura/depositoDelete/{id}", [Authorize] async (DepositoService depositoService, int id) =>
// {
//     await depositoService.DeleteDeposito(id);
//     return Results.Ok("Produto deletado com sucesso");
// });

// // Consultar vendas detalhadas por produto
// app.MapGet("/rotaSegura/vendaDetalhada/{idProduto}", async (int idProduto, VendaService vendaService) =>
// {
//     var result = await vendaService.ConsultarVendasPorProdutoDetalhada(idProduto);
//     if (result.Any())
//     {
//         return Results.Ok(result);
//     }
//     else
//     {
//         return Results.NotFound("Nenhuma venda encontrada para o produto especificado.");
//     }
// });

// // Consultar vendas sumarizadas por produto
// app.MapGet("/rotaSegura/vendaSumarizada/{idProduto}", async (int idProduto, VendaService vendaService) =>
// {
//     var result = await vendaService.ConsultarVendasPorProdutoSumarizada(idProduto);
//     if (result != null)
//     {
//         return Results.Ok(result);
//     }
//     else
//     {
//         return Results.NotFound("Nenhuma venda encontrada para o produto especificado.");
//     }
// });

// // Consultar vendas detalhadas por cliente
// app.MapGet("/rotaSegura/vendaDetalhadaCliente/{idCliente}", async (int idCliente, VendaService vendaService) =>
// {
//     var result = await vendaService.ConsultarVendasPorClienteDetalhada(idCliente);
//     if (result.Any())
//     {
//         return Results.Ok(result);
//     }
//     else
//     {
//         return Results.NotFound("Nenhuma venda encontrada para o cliente especificado.");
//     }
// });

// // Consultar vendas sumarizadas por cliente
// app.MapGet("/rotaSegura/vendaSumarizadaCliente/{idCliente}", async (int idCliente, VendaService vendaService) =>
// {
//     var result = await vendaService.ConsultarVendasPorClienteSumarizada(idCliente);
//     if (result != null)
//     {
//         return Results.Ok(result);
//     }
//     else
//     {
//         return Results.NotFound("Nenhuma venda encontrada para o cliente especificado.");
//     }
// });

// // Consultar produtos no depósito de forma sumarizada
// app.MapGet("/rotaSegura/produtosDeposito/{idDeposito}", async (int idDeposito, VendaService vendaService) =>
// {
//     var result = await vendaService.ConsultarProdutosNoDepositoSumarizada(idDeposito);
//     if (result.Any())
//     {
//         return Results.Ok(result);
//     }
//     else
//     {
//         return Results.NotFound("Nenhum produto encontrado no depósito especificado.");
//     }
// });

// // Consultar a quantidade de um produto no depósito
// app.MapGet("/rotaSegura/produtoDeposito/{idProduto}", async (int idProduto, VendaService vendaService) =>
// {
//     var result = await vendaService.ConsultarQuantidadeProdutoDeposito(idProduto);
//     if (result != null)
//     {
//         return Results.Ok(result);
//     }
//     else
//     {
//         return Results.NotFound("Produto não encontrado no depósito.");
//     }
// });


app.Run();