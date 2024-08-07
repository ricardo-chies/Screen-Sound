using Microsoft.EntityFrameworkCore;
using ScreenSound.Data;
using Testcontainers.MySql;

namespace ScreenSound.Tests.Integration.MySql.Context
{
    public class ContextFixture : IAsyncLifetime
    {
        // Utilizado para testes de Integração no banco de dados MySql, criando novo banco para os testes.
        private readonly MySqlContainer _mySqlContainer;
        public Data.Context Context { get; private set; }

        public ContextFixture()
        {
            _mySqlContainer = new MySqlBuilder()
                .WithImage("mysql:8.0")
                .WithUsername("tester")
                .WithPassword("123456")
                .WithDatabase("ScreenSound_Test")
                .Build();
        }

        public async Task InitializeAsync()
        {
            // Inicia o container MySQL
            await _mySqlContainer.StartAsync();

            // Configura o contexto do Entity Framework para usar o banco de dados no container
            var options = new DbContextOptionsBuilder<Data.Context>()
                .UseMySql(_mySqlContainer.GetConnectionString(),
                new MySqlServerVersion(new Version(8, 0, 0)))
                .Options;

            Context = new Data.Context(options);

            // Aplica as migrations para criar o banco de dados
            await Context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            // Limpeza: Deleta o banco de dados de teste, se descomentar, irá gerar um novo banco a cada teste
            // await Context.Database.EnsureDeletedAsync();

            // Para o container e libera os recursos
            await _mySqlContainer.StopAsync();
            await _mySqlContainer.DisposeAsync();

            await Context.DisposeAsync();
        }
    }
}
