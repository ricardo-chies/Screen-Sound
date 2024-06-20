using ScreenSound.API.Endpoints;
using ScreenSound.Data;
using ScreenSound.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();
builder.Services.AddTransient<ScreenSoundDAL<Artista>>();
builder.Services.AddTransient<ScreenSoundDAL<Musica>>();

var app = builder.Build();

app.AddEnpointsArtistas();
app.AddEndpointsMusicas();

app.Run();