using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.Services;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    // permet de convertir les enums en string
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

// Services pour générer Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Injection des dépendances : Repository
builder.Services.AddScoped<IBaseRepository<ProjectDao>, ProjectRepository>();
builder.Services.AddScoped<IBaseRepository<TypeDao>, ProjectTypeRepository>();
builder.Services.AddScoped<IBaseRepository<StepDao>, StepRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStepRepository, StepRepository>(); 

// Injection des dépendances : Service
builder.Services.AddScoped<IBaseService<ProjectDto>, ProjectService>();
builder.Services.AddScoped<IBaseService<ProjectTypesDto>, ProjectTypeService>();
builder.Services.AddScoped<IBaseService<StepDto>, StepService>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IReferentialService, ReferentialService>();
builder.Services.AddScoped<IProjectTypeService, ProjectTypeService>();
builder.Services.AddScoped<IStepService, StepService>();


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
