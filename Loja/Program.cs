using Microsoft.EntityFrameworkCore;
using Loja.Data;
using Loja.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Starta Conexão Com o BD 

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDbContext>(options=>options.UseMySql(connectionString, new MySqlServerVersion(new Version(8,0,37))));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPost("/criarproduto", async(LojaDbContext dbContext, Produto newProduto)=>{
    dbContext.Produtos.Add(newProduto);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/criarproduto/{newProduto.Id}", newProduto);
});

app.Run();


