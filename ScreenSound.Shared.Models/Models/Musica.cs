﻿namespace ScreenSound.Models;

public class Musica
{
    public Musica(string nome, int idArtista, int anoLancamento)
    {
        Nome = nome;
        Artista.Id = idArtista;
        AnoLancamento = anoLancamento;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public virtual Artista? Artista { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";
    }
}