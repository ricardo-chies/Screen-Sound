using Bogus;
using FluentAssertions;
using Moq;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;

namespace ScreenSound.Tests.GeneroServices
{
    public class ListarGenerosTests
    {
        private readonly Mock<IScreenSoundDAL<Genero>> dalGenero;
        private readonly GeneroService service;

        public ListarGenerosTests()
        {
            dalGenero = new Mock<IScreenSoundDAL<Genero>>();
            service = new GeneroService(dalGenero.Object);
        }

        [Fact]
        public async Task ListarGeneros_ReturnGeneros()
        {
            // Arrange
            var fakerGenero = new Faker<Genero>()
                .RuleFor(m => m.Nome, f => f.Lorem.Word())
                .RuleFor(m => m.Id, f => f.Random.Int(1, 10));

            var expectedGeneros = fakerGenero.Generate(10);

            dalGenero.Setup(a => a.Listar()).ReturnsAsync(expectedGeneros);

            // Act
            var result = await service.ListarGeneros();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(expectedGeneros.Count);
        }
    }
}
