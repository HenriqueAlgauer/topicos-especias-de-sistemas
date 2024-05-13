using System.Text.Json;
using aula08.controller;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// Configure the HTTP request pipeline.

app.MapPost("/login", async (HttpContext context) =>
{
    // Receber a request do Body
    using var reader = new System.IO.StreamReader(context.Request.Body);

    // Transformar todo o Código JSON na variável 'body'
    var body = await reader.ReadToEndAsync();

    // Desserializar o objeto JSON
    var user = System.Text.Json.JsonDocument.Parse(body);
    var userLogin = user.RootElement.GetProperty("userLogin").GetString();
    var userPassword = user.RootElement.GetProperty("userPassword").GetString();

    UserController user01 = new UserController(userLogin, userPassword);

    if (!user01.Login())
    {
        
        await context.Response.WriteAsync("Erro! Usuário ou senha inválidos");
    }
    else
    {
        
        await context.Response.WriteAsync("Bem-vindo " + userLogin);
    }
});
app.Run();