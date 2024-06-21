using Microsoft.EntityFrameworkCore;
using ScreenSound.Models;

namespace ScreenSound.Data
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=ScreenSound;user=root;password=123456;Persist Security Info=False",
                new MySqlServerVersion(new Version(7, 0, 0)));
        }

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeando relacionamento de n para n
            modelBuilder.Entity<Musica>()
                .HasMany(c => c.Generos)
                .WithMany(c => c.Musicas);
        }
    }
}