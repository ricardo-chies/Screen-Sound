using Microsoft.OpenApi.Models;
using ScreenSound.API.Endpoints;
using ScreenSound.Data;
using ScreenSound.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();
builder.Services.AddTransient<ScreenSoundDAL<Artista>>();
builder.Services.AddTransient<ScreenSoundDAL<Musica>>();
builder.Services.AddTransient<ScreenSoundDAL<Genero>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Esta Aplicação tem como objetivo disponibilizar endpoints referentes a Artistas e Músicas.",
        Contact = new OpenApiContact
        {
            Name = "Ricardo Chies",
            Email = "chies.dev@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/ricardo-chies-087557216")
        },
        Title = "Screen Sound API",
        Version = "v1"
    });
});

var app = builder.Build();

app.AddEnpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndpointsGeneros();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();