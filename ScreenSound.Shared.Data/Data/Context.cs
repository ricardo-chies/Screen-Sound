using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Models;
using ScreenSound.Shared.Data.Models;

namespace ScreenSound.Data
{
    public class Context : IdentityDbContext<PessoaAcesso, PerfilAcesso, int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (optionsBuilder.IsConfigured)
        //    {
        //        return;
        //    }

        //    optionsBuilder.UseMySql("server = localhost; port = 3306; database = ScreenSound; user = root; password = 123456; Persist Security Info = False",
        //        new MySqlServerVersion(new Version(7, 0, 0)));
        //}

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeando relacionamento de n para n
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Musica>()
                .HasMany(c => c.Generos)
                .WithMany(c => c.Musicas);

            // Mapeando relacionamento de 1 para n com cascade delete
            modelBuilder.Entity<Musica>()
                .HasOne(m => m.Artista)
                .WithMany(a => a.Musicas)
                .HasForeignKey(m => m.ArtistaId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete
        }
    }
}