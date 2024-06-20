using ScreenSound.Data;
using ScreenSound.Models;

namespace ScreenSound.Menus;
internal class MenuMostrarMusicasPorAno : Menu
{
    public override void Executar(ScreenSoundDAL<Artista> artistaDAL)
    {
        base.Executar(artistaDAL);
        ExibirTituloDaOpcao("Mostrar musicas por ano de lançamento");
        Console.Write("Digite o ano para consultar músicas:");
        string anoLancamento = Console.ReadLine()!;
        var musicaDal = new ScreenSoundDAL<Musica>(new Context());
        var listaAnoLancamento = musicaDal.ListarPor(a => a.AnoLancamento == Convert.ToInt32(anoLancamento));
        if (listaAnoLancamento.Any())
        {
            Console.WriteLine($"\nMusicas do Ano {anoLancamento}:");
            foreach (var musica in listaAnoLancamento)
            {
                musica.ExibirFichaTecnica();
            }
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO ano {anoLancamento} não foi encontrada!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
