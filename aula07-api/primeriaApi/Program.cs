var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {
    Console.WriteLine("/ foi exec");
    return "Hello World!";
});

app.MapGet("/seunome", () => {
    return "Henrique";
});

app.Run();
