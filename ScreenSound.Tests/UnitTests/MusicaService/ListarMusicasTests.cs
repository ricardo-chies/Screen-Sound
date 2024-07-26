using Bogus;
using FluentAssertions;
using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.UnitTests.MusicaServices
{
    public class ListarMusicasTests
    {
        private readonly Mock<IScreenSoundDAL<Musica>> dalMusica;
        private readonly MusicaService service;

        public ListarMusicasTests()
        {
            dalMusica = new Mock<IScreenSoundDAL<Musica>>();
            service = new MusicaService(dalMusica.Object, null);
        }

        [Fact]
        public async Task ListarMusicas_ReturnMusicas()
        {
            // Arrange
            var fakerMusica = new Faker<Musica>()
                .RuleFor(m => m.Nome, f => f.Lorem.Word())
                .RuleFor(m => m.Id, f => f.Random.Int(1, 40))
                .RuleFor(m => m.AnoLancamento, f => f.Random.Int(1960, 2024))
                .RuleFor(m => m.ArtistaId, f => f.Random.Int(1, 20))
                .RuleFor(m => m.Artista, f => new Artista(f.Person.FullName, f.Lorem.Sentence()));

            var expectedMusicas = fakerMusica.Generate(40);

            dalMusica.Setup(a => a.ListarMusicasComArtistas()).ReturnsAsync(expectedMusicas);

            // Act
            var result = await service.ListarMusicas();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(expectedMusicas.Count);
        }
    }
}
