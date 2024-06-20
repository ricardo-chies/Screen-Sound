using ScreenSound.Data;
using ScreenSound.Models;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(ScreenSoundDAL<Artista> artistaDAL)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
