using ScreenSound.Data;
using ScreenSound.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    var dal = new ScreenSoundDAL<Artista>(new Context());
    return dal.Listar();
});

app.Run();