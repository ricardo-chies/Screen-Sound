using Microsoft.EntityFrameworkCore;
using ScreenSound.Data;

namespace ScreenSound.Tests.IntegracaoDB
{
    public class ContextFixture : IDisposable
    {
        public Context Context { get; }

        public ContextFixture()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseMySql("server=localhost;port=3306;database=ScreenSound_Test;user=root;password=123456;Persist Security Info=False",
                new MySqlServerVersion(new Version(7, 0, 0)))
                .Options;

            Context = new Context(options);

            // Cria o banco de dados de teste se não existir
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            // Opção para deletar o banco de dados após os testes, para começar com um banco limpo nos próximos testes
            // Somente faça isso em ambientes de teste e não em ambientes de desenvolvimento ou produção
            //Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
