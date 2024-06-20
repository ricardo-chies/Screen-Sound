using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();
builder.Services.AddTransient<ScreenSoundDAL<Artista>>();

var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] ScreenSoundDAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices] ScreenSoundDAL<Artista> dal, string nome) =>
{
    var artista = dal.RecuperarPor(artista => artista.Nome.ToUpper().Equals(nome.ToUpper()));

    if (artista is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromServices] ScreenSoundDAL<Artista> dal, [FromBody]Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Ok();
});

app.Run();