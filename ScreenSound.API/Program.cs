using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Artistas", () =>
{
    var dal = new ScreenSoundDAL<Artista>(new Context());
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", (string nome) =>
{
    var dal = new ScreenSoundDAL<Artista>(new Context());
    var artista = dal.RecuperarPor(artista => artista.Nome.ToUpper().Equals(nome.ToUpper()));

    if (artista is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromBody]Artista artista) =>
{
    var dal = new ScreenSoundDAL<Artista>(new Context());
    dal.Adicionar(artista);
    return Results.Ok();
});

app.Run();