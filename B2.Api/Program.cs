using B2.Api.Middleware;
using B2.App;
using B2.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices();
builder.Services.AddDataServices();
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<B2ExceptionHandler>();

builder.Services.AddProblemDetails();
builder.Services.AddControllers();

builder.WebHost.UseUrls(builder.Configuration["applicationUrl"]!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(cfg =>
{
    cfg.AllowAnyHeader();
    cfg.AllowAnyMethod();
    cfg.AllowAnyOrigin();
});

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapControllers();

app.Services.EnsureDbCreated();

await app.RunAsync();