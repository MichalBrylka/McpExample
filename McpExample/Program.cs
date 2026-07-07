using McpExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


// Application services
builder.Services.AddSingleton<TodoService>();


// MCP
builder.Services
    .AddMcpServer()
    .WithHttpTransport(o => o.Stateless = true)
    .WithTools<TodoTools>();


var app = builder.Build();


// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            "/openapi/v1.json",
            "Todo API");
    });
    app.MapOpenApi();
}


app.MapControllers();


// MCP endpoint (move off app root to avoid route conflicts)
app.MapMcp("/mcp");

// Diagnostics: list mapped endpoints and their route patterns
app.MapGet("/__endpoints", (EndpointDataSource ds) =>
    Results.Json(ds.Endpoints.Select(e => new
    {
        displayName = e.DisplayName,
        pattern = (e as RouteEndpoint)?.RoutePattern.RawText
    }))
);

app.Run();


