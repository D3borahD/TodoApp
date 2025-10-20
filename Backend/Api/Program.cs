using System.Text.Json;
using Application.Interfaces;
using Application.Services;
using Infrastructure.IRepository;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Ajouter les services nécessaires JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddEndpointsApiExplorer();

// Services pour générer Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Injection des dépendances
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Logging.AddConsole();

var app = builder.Build();

// Configurer Swagger uniquement en environnement de développement
if (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        option.RoutePrefix = string.Empty; 
    });
}

// TODO : 
// Protéger Swagger avec une route spécifique
/*app.UseWhen(context => context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
{
    appBuilder.UseAuthentication();
    appBuilder.UseAuthorization();
});*/

// Middleware de l'application
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
