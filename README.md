<img src="https://img.shields.io/badge/made%20with- C Sharp-blue.svg" alt="made with c sharp">
<img src="https://img.shields.io/badge/made%20with- Angular-red.svg" alt="made with c sharp">

Angular CLI: 18.2.10

Node: 20.11.0

Package Manager: npm 10.5.0

Swagger : http://localhost:5062/swagger/index.html

# Mise en place du Backend

## Créer un projet webApi

- Dans Program.cs : Mettre en place les paramètres de base de l'API
- Créer un contrôler et mettre en place la première route
- Lancer l'application et vérifier le retour dans le navigateur.

# Mise en place de la gestion des CORS

## Backend :

```c#
// Dans le fichier program.cs

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
```

## Mise en place de Swagger 

```c#
// Dans le fichier program.cs
// Services pour générer Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Configurer Swagger uniquement en environnement de développement
if (app.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    });
}
```

# Mise en place du Frontend

## Mettre en place d'un service faisant appel à la route en backend
```JS
   constructor(private readonly http: HttpClient) { }
    public url : string = "http://localhost:5062/tasks"

    public getTasks(name: ITask | undefined): Observable<ITask>
    {
        return this.http.get<ITask>(this.url)
    }
```

## Problème : l'image qui ne s'affiche pas dans le html, mais s'affiche dans le css
```
// html => ne foncitonne pas
  <img src="../assets/icons/setting.png" alt="Settings Icon">
// CSS => fonctionne
    background-image: url("../assets/gradiant-bg.jpg");
 ```
### Solution : 
Dans le ficher angular.json, modifier les assets (en prod et en dev)
```json
 "assets": [
              "src/favicon.ico",
              "src/assets"
            ],
```

# A faire

## Backend : 
- [ ] Assurer la bonne mise en forme des données en base (Première lettre en maj, le reste en min);
- [ ] Assurer qu'on entre pas plusieurs fois le même nom d'équipe, de lot, de projet;
- [ ] Enregistrer l'image au bon format


##
- enlever : 
-  "@angular/cdk": "^17.0.0",
-  "@angular/material": "^17.0.0",

