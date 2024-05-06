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
<<<<<<< HEAD
    Console.WriteLine(context);
    using var reader = new System.IO.StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    var json = JsonDocument.Parse(body);
    
    return "recebido";
=======
    using var reader = new System.IO.StreamReader(context.Request.Body);
    
    var body = await reader.ReadToEndAsync();

    var json = System.Text.Json.JsonDocument.Parse(body);
    var userName = json.RootElement.GetProperty("nome");

    return "recebido: " + userName;
>>>>>>> 5484b56968e1b65d6cc7ab02310a5fc1f48518dc
});

app.Run();
