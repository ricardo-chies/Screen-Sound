﻿namespace ScreenSound.Web.Requests
{
    public record MusicaRequestEdit(int Id, string Nome, int ArtistaId, int AnoLancamento, ICollection<GeneroRequest> Generos = null)
        : MusicaRequest(Nome, ArtistaId, AnoLancamento, Generos);
}
