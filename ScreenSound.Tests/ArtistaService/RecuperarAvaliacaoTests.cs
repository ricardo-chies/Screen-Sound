using Microsoft.AspNetCore.Http;
using Moq;
using ScreenSound.API.Responses;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;
using ScreenSound.Shared.Data.Models;
using ScreenSound.Shared.Models.Models;
using System.Security.Claims;

namespace ScreenSound.Tests.ArtistaServices
{
    public class RecuperarAvaliacaoTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly Mock<IScreenSoundDAL<PessoaAcesso>> dalPessoa;
        private readonly ArtistaService service;

        public RecuperarAvaliacaoTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            dalPessoa = new Mock<IScreenSoundDAL<PessoaAcesso>>();
            service = new ArtistaService(dalArtista.Object, dalPessoa.Object);
        }

        [Fact]
        public async Task RecuperarAvaliacaoTest_ReturnAvaliacao()
        {
            // Arrange
            var artistaId = 1;
            var email = "usuario@exemplo.com";
            var mockHttpContext = new Mock<HttpContext>();

            var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            mockClaimsPrincipal.Setup(c => c.Claims).Returns(claims);
            mockHttpContext.Setup(c => c.User).Returns(mockClaimsPrincipal.Object);

            var artista = new Artista("Michael Jackson", "Rei do Pop")
            {
                Avaliacoes = new List<AvaliacaoArtista>()
            };

            var expectedAvaliacao = new AvaliacaoArtistaResponse(artistaId, 0);

            dalArtista.Setup(a => a.RecuperarArtistaComAvaliacoesPorIdAsync(artistaId))
                      .ReturnsAsync(artista);

            var pessoa = new PessoaAcesso { Id = 1, Email = email };
            dalPessoa.Setup(p => p.RecuperarPor(p => p.Email!.Equals(email)))
                     .ReturnsAsync(pessoa);

            // Act
            var result = await service.RecuperarAvaliacao(artistaId, mockHttpContext.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAvaliacao.Nota, result.Nota);
            Assert.Equal(expectedAvaliacao.ArtistaId, result.ArtistaId);
        }
    }
}
