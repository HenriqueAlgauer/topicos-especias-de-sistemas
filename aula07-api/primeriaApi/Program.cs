var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {
    Console.WriteLine("/ foi exec");
    return "Hello World!";
});

app.MapGet("/seunome", () => {
    return "Henrique";
});

app.MapPost("/criarConta", async (HttpContext context) => {
    Console.WriteLine(context);
    using var reader = new System.IO.StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    var json = JsonDocument.Parse(body);
    
    return "recebido";
});

app.Run();
