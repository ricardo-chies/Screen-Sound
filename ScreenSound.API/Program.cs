using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ScreenSound.API.Endpoints;
using ScreenSound.Data;
using ScreenSound.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurando serviço CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7073")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddDbContext<Context>((options) =>
{
    options.UseMySql(builder.Configuration["ConnectionStrings:ScreenSoundDB"],
                new MySqlServerVersion(new Version(7, 0, 0)));
});

builder.Services.AddTransient<ScreenSoundDAL<Artista>>();
builder.Services.AddTransient<ScreenSoundDAL<Musica>>();
builder.Services.AddTransient<ScreenSoundDAL<Genero>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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

app.UseHttpsRedirection();

// Configure o middleware CORS
app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndpointsGeneros();

app.Run();