using Microsoft.AspNetCore.Http;
using Moq;
using ScreenSound.API.Requests;
using ScreenSound.API.Services;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Interfaces;
using ScreenSound.Shared.Data.Models;
using System.Security.Claims;

namespace ScreenSound.Tests.ArtistaServices
{
    public class AdicionarAvaliacaoTests
    {
        private readonly Mock<IScreenSoundDAL<Artista>> dalArtista;
        private readonly Mock<IScreenSoundDAL<PessoaAcesso>> dalPessoa;
        private readonly ArtistaService service;

        public AdicionarAvaliacaoTests()
        {
            dalArtista = new Mock<IScreenSoundDAL<Artista>>();
            dalPessoa = new Mock<IScreenSoundDAL<PessoaAcesso>>();
            service = new ArtistaService(dalArtista.Object, dalPessoa.Object);
        }

        [Fact]
        public async Task AdicionarAvaliacaoTest_ReturnTrue()
        {
            // Arrange
            var avaliacaoRequest = new AvaliacaoArtistaRequest(1, 4);
            var email = "usuario@exemplo.com";
            var mockHttpContext = new Mock<HttpContext>();

            var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            mockClaimsPrincipal.Setup(c => c.Claims).Returns(claims);
            mockHttpContext.Setup(c => c.User).Returns(mockClaimsPrincipal.Object);

            var artista = new Artista("Michael Jackson", "Rei do Pop");

            dalArtista.Setup(a => a.RecuperarArtistaComAvaliacoesPorIdAsync(avaliacaoRequest.ArtistaId))
                      .ReturnsAsync(artista);

            var pessoa = new PessoaAcesso { Id = 1, Email = email };
            dalPessoa.Setup(p => p.RecuperarPor(p => p.Email!.Equals(email)))
                     .ReturnsAsync(pessoa);

            // Act
            var result = await service.AdicionarAvaliacao(avaliacaoRequest, mockHttpContext.Object);

            // Assert
            Assert.True(result);
            dalArtista.Verify(a => a.Atualizar(It.IsAny<Artista>()), Times.Once);
        }
    }
}
