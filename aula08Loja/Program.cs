using aula08.controller;
using System;
using System.IO;

// codigo padrão para lib de autenticação
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer(options =>{
    options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("asdcasdcasdcasdcasdcasdcasdcasdc"))
    };
});

var app = builder.Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

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

    UserController UserController = new UserController(userLogin, userPassword);
    // Se o user existe e tem senha => gerar o token(credencial que da acesso aos demais endpoints)
    var token = "";
    if(UserController.Login(userLogin, userPassword)){
        // Gerar Token
        token = GenerateToken(userLogin);
    }

    return UserController.Login(userLogin, userPassword);
});

// Método de criação do token
// Será traportado para uma classe específica
string GenerateToken(string data){
    var tokenHanler = new JwtSecurityTokenHandler();
    var secretKey = Encoding.ASCII.GetBytes("asdcasdcasdcasdcasdcasdcasdcasdc");
    var tokenDescriptor = new SecurityTokenDescriptor{
        Expires = DateTime.UtcNow.AddHours(1), // o token expira em uma hora
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secretKey),
            SecurityAlgorithms.HmacSha256Signature
        )
    };
    var token = tokenHanler.CreateToken(tokenDescriptor);
    return tokenHanler.WriteToken(token);
}
app.Run();

