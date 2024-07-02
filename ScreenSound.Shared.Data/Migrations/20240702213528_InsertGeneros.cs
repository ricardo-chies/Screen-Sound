using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class InsertGeneros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Rock', 'Um gênero musical popular que se originou como rock and roll nos Estados Unidos no final dos anos 1940 e início dos anos 1950.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Pop', 'Um gênero de música popular que se originou no seu formato moderno durante a metade da década de 1950 nos Estados Unidos e no Reino Unido.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Hip Hop', 'Um gênero musical e um movimento cultural que se desenvolveram nas comunidades afro-americanas e latinas do Bronx, em Nova Iorque, no final dos anos 1970.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Jazz', 'Um gênero musical que se originou nas comunidades afro-americanas de Nova Orleans, Estados Unidos, no final do século XIX e início do século XX.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Blues', 'Um gênero musical que se originou nas comunidades afro-americanas no Sul dos Estados Unidos em torno do fim do século XIX.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Classical', 'Um gênero de música erudita ocidental que abrange um período desde aproximadamente o século IX até os dias atuais.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Reggae', 'Um gênero musical que se originou na Jamaica no final da década de 1960.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Country', 'Um gênero de música popular que se originou no sul dos Estados Unidos nos anos 1920.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Electronic', 'Um gênero de música que emprega principalmente instrumentos eletrônicos e tecnologia musical eletrônica.')");
            migrationBuilder.Sql("INSERT INTO Generos (Nome, Descricao) VALUES ('Folk', 'Um gênero musical que se desenvolveu a partir da música tradicional de diferentes culturas.')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Generos WHERE Nome IN ('Rock', 'Pop', 'Hip Hop', 'Jazz', 'Blues', 'Classical', 'Reggae', 'Country', 'Electronic', 'Folk')");
        }
    }
}
