using System.Text.Json;
using Application.Interfaces;
using Application.Services;
using BackendApi.Entities;
using Domain.DTO;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=timeSheet.db"));

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

// Injection des dépendances : Repository
builder.Services.AddScoped<IBaseRepository<TeamDao>, TeamRepository>();
builder.Services.AddScoped<IBaseRepository<ProductDao>, ProductRepository>();
builder.Services.AddScoped<IBaseRepository<ModuleDao>, ModuleRepository>();
builder.Services.AddScoped<IBaseRepository<ActivityDao>, ActivityRepository>();

// Injection des dépendances : Service
builder.Services.AddScoped<IBaseService<TeamDto>, TeamService>();
builder.Services.AddScoped<IBaseService<ProductDto>, ProductService>();
builder.Services.AddScoped<IBaseService<ModuleDto>, ModuleService>();
builder.Services.AddScoped<IBaseService<ActivityDto>, ActivityService>();

builder.Logging.AddConsole();

var app = builder.Build();

// Initialisation de la base
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.InitializeAsync(context);
}

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
