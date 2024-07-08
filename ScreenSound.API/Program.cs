using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ScreenSound.API.Endpoints;
using ScreenSound.API.Interfaces;
using ScreenSound.API.Services;
using ScreenSound.Data;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;
using ScreenSound.Shared.Data.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configurando serviço CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
    builder =>
    {
        builder.WithOrigins("https://localhost:7073")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(pol => true)
               .AllowCredentials();
    });
});

builder.Services.AddDbContext<Context>((options) =>
{
    options.UseMySql(builder.Configuration["ConnectionStrings:ScreenSoundDB"],
                new MySqlServerVersion(new Version(7, 0, 0)));
});

builder.Services.AddIdentityApiEndpoints<PessoaAcesso>().AddEntityFrameworkStores<Context>();
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Registrar IScreenSoundDAL<T> com a implementação ScreenSoundDAL<T>
builder.Services.AddTransient(typeof(IScreenSoundDAL<>), typeof(ScreenSoundDAL<>));

builder.Services.AddTransient<ScreenSoundDAL<Artista>>();
builder.Services.AddTransient<ScreenSoundDAL<Musica>>();
builder.Services.AddTransient<ScreenSoundDAL<Genero>>();
builder.Services.AddTransient<ScreenSoundDAL<PessoaAcesso>>();

builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<IMusicaService, MusicaService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();

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

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.UseHttpsRedirection();

// Configure o middleware CORS
app.UseCors("AllowSpecificOrigin");

app.UseStaticFiles();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.AddEndpointsAuth();

app.MapControllers();

app.Run();