using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using loja.models;
using loja.services;
using loja.data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<DepositoService>();
builder.Services.AddScoped<ServicoService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Conexão Com o BD 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

// Configuração do JWT
var secretKey = "12345678123456781234567812345678";
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


builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Gera o token
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

// Rota segura :)
app.MapGet("/rotaSegura", async (HttpContext context) =>
{
    if (!context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("O token não foi fornecido :/");
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
        await context.Response.WriteAsync("Não foi possível validar esse token");
    }
});

// Serviços
app.MapGet("/rotaSegura/servicos", [Authorize] async (ServicoService servicoService) =>
{
    var servicos = await servicoService.GetAllServicosAsync();
    if (servicos != null)
    {
        return Results.Ok(servicos);
    }
    return Results.BadRequest("nenhum servico encontrado");
});

app.MapPost("/rotaSegura/createservico", async (ServicoService servicoService, Servico servico) =>
{
    await servicoService.AddServicoAsync(servico);
    return Results.Created($"/servico/{servico.Id}", servico);
});

app.MapPut("/rotaSegura/updateservico/{id}", [Authorize] async (ServicoService servicoService, int id, Servico servico) =>
{
    var servicoFN = await servicoService.GetServicoByIdAsync(id);

    if (servicoFN != null)
    {
        await servicoService.UpdateServicoAsync(id, servico);
        return Results.Created($"/rotaSegura/servico/{id}", servico);
    }
    return Results.BadRequest("Serviço não encontrado !!");
});

app.MapGet("/rotaSegura/servico/{id}", [Authorize] async (ServicoService servicoService, int id) =>
{
    var servico = await servicoService.GetServicoByIdAsync(id);

    if (servico != null)
    {
        return Results.Ok(servico);
    }
    return Results.BadRequest("Servico não encontrado");
});


// usuário 
app.MapGet("/rotaSegura/usuarios", [Authorize] async (UsuarioService usuarioService) =>
{
    var usuarios = await usuarioService.GetAllUsuariosAsync();
    if (usuarios != null)
    {

        return Results.Ok(usuarios);
    }

    return Results.BadRequest("Nenhum user cadastrado");
});

app.MapGet("/rotaSegura/usuario/{id}", [Authorize] async (UsuarioService usuarioService, int id) =>
{
    var usuario = await usuarioService.GetUsuarioByIdAsync(id);

    if (usuario != null)
    {
        return Results.Ok(usuario);
    }

    return Results.BadRequest($"Nenhum usuário encontrado");
});

app.MapPost("/createusuario", async (UsuarioService usuarioService, Usuario usuario) =>
{
    await usuarioService.AddUsuarioAsync(usuario);
    return Results.Created($"/usuario/{usuario.Id}", usuario);
});

app.MapPut("/rotaSegura/updateusuario/{id}", [Authorize] async (UsuarioService usuarioService, int id, Usuario usuario) =>
{
    var userFound = await usuarioService.GetUsuarioByIdAsync(id);

    if (userFound != null)
    {
        await usuarioService.UpdateUsuarioAsync(id, usuario);
        return Results.Created($"/rotaSegura/usuario/{id}", usuario);
    }
    return Results.BadRequest("Usuario não encontrado !!");
});

app.MapDelete("/rotaSegura/deleteUsuario/{id}", [Authorize] async (UsuarioService usuarioService, int id) =>
{
    await usuarioService.DeleteUsuarioByAsync(id);
    return Results.Ok($"User deletado !!");
});



app.MapGet("/rotaSegura/produtos", [Authorize] async (ProductService productService) =>
{
    var produtos = await productService.GetAllProductsAsync();
    return Results.Ok(produtos);
});

app.MapGet("/rotaSegura/produtos/{id}", [Authorize] async (int id, ProductService productService) =>
{
    var produto = await productService.GetProductByIdAsync(id);
    if (produto == null)
    {
        return Results.NotFound($"Product with ID {id} not found.");
    }
    return Results.Ok(produto);
});

app.MapPost("/rotaSegura/createProdutos", [Authorize] async (Produto produto, ProductService productService) =>
{
    await productService.AddProductAsync(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

app.MapPut("/rotaSegura/updateProdutos/{id}", [Authorize] async (int id, Produto produto, ProductService productService) =>
{
    if (id != produto.Id)
    {
        return Results.BadRequest("Product ID mismatch.");
    }
    await productService.UpdateProductAsync(produto);
    return Results.Ok();
});

app.MapDelete("/rotaSegura/deleteProdutos/{id}", [Authorize] async (int id, ProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.Ok("Produto Deletado");
});

// Cliente

app.MapPost("/rotaSegura/createcliente", [Authorize] async (Cliente newCliente, ClienteService clienteService) =>
{
    await clienteService.AddClienteAsync(newCliente);
    return Results.Created($"createcliente/{newCliente.Id}", newCliente);
});


app.MapGet("/rotaSegura/clientes", [Authorize] async (ClienteService clienteService) =>
{

    var Cliente = await clienteService.GetAllClienteAsync();

    return Results.Ok(Cliente);

});

app.MapGet("/rotaSegura/clientes/{id}", [Authorize] async (int id, ClienteService clienteService) =>
{

    var Cliente = await clienteService.GetClienteByIdAsync(id);

    if (Cliente == null)
    {
        return Results.NotFound($"Cliente com o ID {id} não encontrado");
    }


    return Results.Ok(Cliente);

});

app.MapPut("/rotaSegura/clienteUpdate/{id}", [Authorize] async (int id, ClienteService clienteService, Cliente updateCliente) =>
{
    if (id != updateCliente.Id)
    {
        return Results.BadRequest("O ID nao corresponde a um ID ja cadastrado");
    }
    await clienteService.UpdateClienteAsync(updateCliente);
    return Results.Ok();
});


app.MapDelete("/rotaSegura/clienteDelete/{id}", [Authorize] async (int id, ClienteService clienteService) =>
{
    await clienteService.DeleteClienteAsync(id);
    return Results.Ok("Registro deletado com sucesso!");
});


// Fornecedor
app.MapPost("/rotaSegura/createFornecedor", [Authorize] async (FornecedorService fornecedorService, Fornecedor newFornecedor) =>
{
    await fornecedorService.AddFornecedorAsync(newFornecedor);
    return Results.Created($"/createFornecedor/{newFornecedor.Id}", newFornecedor);
});


app.MapGet("/rotaSegura/fornecedores", [Authorize] async (FornecedorService fornecedorService) =>
{
    var fornecedores = await fornecedorService.GetAllFornecedoresAsync();
    return Results.Ok(fornecedores);
});


app.MapGet("/rotaSegura/fornecedores/{id}", [Authorize] async (int id, FornecedorService fornecedorService) =>
{
    var fornecedor = await fornecedorService.GetFornecedorByIdAsync(id);

    if (fornecedor == null)
    {
        return Results.NotFound($"Fornecedor com {id} não encontrado");
    }

    return Results.Ok(fornecedor);

});


app.MapPut("/rotaSegura/updateFornecedor/{id}", [Authorize] async (int id, FornecedorService fornecedorService, Fornecedor fornecedor) =>
{
    if (id != fornecedor.Id)
    {
        return Results.BadRequest("ID fornecedor não localizado !");
    }
    await fornecedorService.UpdateFornecedorAsync(fornecedor);
    return Results.Ok(fornecedor);
});



app.MapDelete("/rotaSegura/deleteFornecedor/{id}", [Authorize] async (int id, FornecedorService fornecedorService) =>
{
    await fornecedorService.DeleteFornecedorAsync(id);
    return Results.Ok("Fornecedor deletado");
});

// Venda 

app.MapPost("/rotaSegura/createVenda", [Authorize] async (VendaService vendaService, ProductService produtoService, ClienteService clienteService, Venda venda) =>
{
    var produto = await produtoService.GetProductByIdAsync(venda.IdProduto);
    var cliente = await clienteService.GetClienteByIdAsync(venda.IdCliente);

    if (produto != null && cliente != null)
    {
        if (venda.quantidadeVendida <= produto.QuantidadeEstoque)
        {
            await vendaService.AddVendaAsync(venda);
            produto.QuantidadeEstoque -= venda.quantidadeVendida;
            await produtoService.UpdateProductAsync(produto);

            return Results.Ok(venda);
        }
        return Results.BadRequest("Quantidade Vendida maior que disponível em estoque");
    }

    return Results.BadRequest("Cliente ID ou Produto ID inválidos");
});

app.MapGet("/rotaSegura/vendas", [Authorize] async (VendaService vendaService) =>
{
    var vendas = await vendaService.GetAllVendasAsync();

    if (vendas != null)
    {
        return Results.Ok(vendas);
    }

    return Results.BadRequest("Nenhuma venda encontrada :(");
});

app.MapGet("/rotaSegura/venda/{id}", [Authorize] async (VendaService vendaService, int id) =>
{
    var venda = await vendaService.GetVendaByIdAsync(id);
    if (venda != null)
    {
        return Results.Ok(venda);
    }

    return Results.BadRequest($"Venda com o ID {id} não encontrada !");
});

app.MapPut("/rotaSegura/vendaUpdate/{id}", [Authorize] async (VendaService vendaService, int id) =>
{
    var venda = await vendaService.GetVendaByIdAsync(id);
    if (venda != null)
    {
        await vendaService.UpdateVendaAsync(venda);
        return Results.Ok(venda);
    }

    return Results.BadRequest("ID fornecido inválido");
});

app.MapGet("/rotaSegura/vendaDetalhada/{idProduto}", async (int idProduto, VendaService vendaService) =>
{
    var result = await vendaService.ConsultarVendasPorProdutoDetalhada(idProduto);
    if (result.Any())
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound("Nenhuma venda encontrada para o produto especificado.");
    }
});

app.MapGet("/rotaSegura/vendaSM/{idProduto}", async (int idProduto, VendaService vendaService) =>
{
    var result = await vendaService.ConsultarVendasPorProdutoSumarizada(idProduto);
    if (result != null)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound("Venda não encontrada ://");
    }
});

app.MapGet("/rotaSegura/vendaDetalhadaCliente/{idCliente}", async (int idCliente, VendaService vendaService) =>
{
    var result = await vendaService.ConsultarVendasPorClienteDetalhada(idCliente);
    if (result.Any())
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound("Venda não encontrada ://");
    }
});

app.MapGet("/rotaSegura/vendaSMCliente/{idCliente}", async (int idCliente, VendaService vendaService) =>
{
    var result = await vendaService.ConsultarVendasPorClienteSumarizada(idCliente);
    if (result != null)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound("Venda não encontrada ://");
    }
});

app.MapDelete("/rotaSegura/vendaDelete/{id}", [Authorize] async (VendaService vendaService, int id) =>
{
    await vendaService.DeleteVendaAsync(id);
    return Results.Ok($"Venda com o ID {id} deletada");
});

// Deposito

app.MapPost("/rotaSegura/depositocreate", [Authorize] async (DepositoService depositoService, ProductService produtoService, Deposito deposito) =>
{
    var produtoDP = await produtoService.GetProductByIdAsync(deposito.IdProduto);

    if (produtoDP != null)
    {
        await depositoService.AddDepositoAsync(deposito);
        produtoDP.QuantidadeEstoque += deposito.quantidade;
        await produtoService.UpdateProductAsync(produtoDP);
        return Results.Ok(deposito);
    }
    return Results.BadRequest("Id de produto não encontrado");
});

app.MapGet("/rotaSegura/depositos", [Authorize] async (DepositoService depositoService) =>
{
    var depositos = await depositoService.GetAllDepositoAsync();

    if (depositos != null)
    {
        return Results.Ok(depositos);
    }

    return Results.BadRequest("Cadastre produtos !!");
});

app.MapGet("/rotaSegura/deposito/{id}", [Authorize] async (DepositoService depositoService, int id) =>
{
    var deposito = await depositoService.GetDepositoById(id);

    if (deposito != null)
    {
        return Results.Ok(deposito);
    }

    return Results.BadRequest("Id não encontrado");
});

app.MapPut("/rotaSegura/depositoUpdate/{id}", [Authorize] async (DepositoService depositoService, int id, Deposito deposito) =>
{
    var dp = await depositoService.GetDepositoById(id);

    if (dp.Id == deposito.Id)
    {
        await depositoService.UpdateDeposito(deposito);
        return Results.Ok(deposito);
    }

    return Results.BadRequest("ID não encontrado");
});

app.MapDelete("/rotaSegura/depositoDelete/{id}", [Authorize] async (DepositoService depositoService, int id) =>
{
    await depositoService.DeleteDeposito(id);
    return Results.Ok("Produto deletado com sucesso");
});

app.MapGet("/rotaSegura/produtosDeposito/{idDeposito}", async (int idDeposito, VendaService vendaService) =>
{
    var result = await vendaService.ConsultarProdutosNoDepositoSumarizada(idDeposito);
    if (result.Any())
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound("produto não encontrado !!");
    }
});

app.MapGet("/rotaSegura/produtoDP/{idProduto}", async (int idProduto, VendaService vendaService) =>
{
    var result = await vendaService.ConsultarQuantidadeProdutoDeposito(idProduto);
    if (result != null)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound("Produto não encontrado !!");
    }
});

app.Run();