using API.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterModules();

var app = builder.Build();

app.RegisterHttpPipeline();
app.MapEndpoints();

app.Run();


