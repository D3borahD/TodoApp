# Mise en place du Backend

## Créer un projet webApi

- Dans Program.cs : Mettre en place les paramètres de base de l'API
- Créer un contrôler et mettre en place la première route.
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

# Mise en place du Frontend

- Mettre en place d'un service faisant appel à la route en backend
```JS
   constructor(private readonly http: HttpClient) { }
    public url : string = "http://localhost:5062/tasks"

    public getTasks(name: ITask | undefined): Observable<ITask>
    {
        return this.http.get<ITask>(this.url)
    }
```


