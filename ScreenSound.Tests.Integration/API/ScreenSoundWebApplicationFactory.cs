using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ScreenSound.Data;

namespace ScreenSound.Tests.Integration.API
{
    public class ScreenSoundWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<Context>));
                services.AddDbContext<Context>(options => options
                .UseLazyLoadingProxies()
                .UseMySql("server = localhost; port = 3307; database = ScreenSound; user = root; password = 123456; Persist Security Info = False", // MySql Container
                new MySqlServerVersion(new Version(7, 0, 0))));

                base.ConfigureWebHost(builder);
            });
        }
    }
}