using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class InsertMusicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserir dados na tabela Musicas
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Total Eclipse of the Heart', 1983, (SELECT Id FROM Artistas WHERE Nome = 'Bonnie Tyler'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Holding Out for a Hero', 1984, (SELECT Id FROM Artistas WHERE Nome = 'Bonnie Tyler'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Billie Jean', 1982, (SELECT Id FROM Artistas WHERE Nome = 'Michael Jackson'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Thriller', 1982, (SELECT Id FROM Artistas WHERE Nome = 'Michael Jackson'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Bohemian Rhapsody', 1975, (SELECT Id FROM Artistas WHERE Nome = 'Queen'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('We Will Rock You', 1977, (SELECT Id FROM Artistas WHERE Nome = 'Queen'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Another Brick in the Wall', 1979, (SELECT Id FROM Artistas WHERE Nome = 'Pink Floyd'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Comfortably Numb', 1979, (SELECT Id FROM Artistas WHERE Nome = 'Pink Floyd'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('The Final Countdown', 1986, (SELECT Id FROM Artistas WHERE Nome = 'Europe'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Carrie', 1986, (SELECT Id FROM Artistas WHERE Nome = 'Europe'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Eye of the Tiger', 1982, (SELECT Id FROM Artistas WHERE Nome = 'Survivor'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Burning Heart', 1985, (SELECT Id FROM Artistas WHERE Nome = 'Survivor'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Livin'' on a Prayer', 1986, (SELECT Id FROM Artistas WHERE Nome = 'Bon Jovi'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('You Give Love a Bad Name', 1986, (SELECT Id FROM Artistas WHERE Nome = 'Bon Jovi'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Wind of Change', 1990, (SELECT Id FROM Artistas WHERE Nome = 'Scorpions'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Rock You Like a Hurricane', 1984, (SELECT Id FROM Artistas WHERE Nome = 'Scorpions'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Sweet Child o'' Mine', 1987, (SELECT Id FROM Artistas WHERE Nome = 'Guns N'' Roses'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('November Rain', 1991, (SELECT Id FROM Artistas WHERE Nome = 'Guns N'' Roses'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('The House of the Rising Sun', 1964, (SELECT Id FROM Artistas WHERE Nome = 'The Animals'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Don''t Let Me Be Misunderstood', 1965, (SELECT Id FROM Artistas WHERE Nome = 'The Animals'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('With or Without You', 1987, (SELECT Id FROM Artistas WHERE Nome = 'U2'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('One', 1991, (SELECT Id FROM Artistas WHERE Nome = 'U2'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Rocket Man', 1972, (SELECT Id FROM Artistas WHERE Nome = 'Elton John'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Tiny Dancer', 1971, (SELECT Id FROM Artistas WHERE Nome = 'Elton John'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Dream On', 1973, (SELECT Id FROM Artistas WHERE Nome = 'Aerosmith'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('I Don''t Want to Miss a Thing', 1998, (SELECT Id FROM Artistas WHERE Nome = 'Aerosmith'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Hey Jude', 1968, (SELECT Id FROM Artistas WHERE Nome = 'The Beatles'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Let It Be', 1970, (SELECT Id FROM Artistas WHERE Nome = 'The Beatles'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Yellow', 2000, (SELECT Id FROM Artistas WHERE Nome = 'Coldplay'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Fix You', 2005, (SELECT Id FROM Artistas WHERE Nome = 'Coldplay'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Iris', 1998, (SELECT Id FROM Artistas WHERE Nome = 'The Goo Goo Dolls'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Slide', 1998, (SELECT Id FROM Artistas WHERE Nome = 'The Goo Goo Dolls'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Lose Yourself', 2002, (SELECT Id FROM Artistas WHERE Nome = 'Eminem'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Stan', 2000, (SELECT Id FROM Artistas WHERE Nome = 'Eminem'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Radioactive', 2012, (SELECT Id FROM Artistas WHERE Nome = 'Imagine Dragons'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Demons', 2012, (SELECT Id FROM Artistas WHERE Nome = 'Imagine Dragons'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Someone Like You', 2011, (SELECT Id FROM Artistas WHERE Nome = 'Adele'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Hello', 2015, (SELECT Id FROM Artistas WHERE Nome = 'Adele'))");

            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Lonely', 2004, (SELECT Id FROM Artistas WHERE Nome = 'Akon'))");
            migrationBuilder.Sql("INSERT INTO Musicas (Nome, AnoLancamento, ArtistaId) VALUES ('Smack That', 2006, (SELECT Id FROM Artistas WHERE Nome = 'Akon'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Comandos para remover os dados inseridos na tabela Musicas
            migrationBuilder.Sql("DELETE FROM Musicas WHERE Nome IN ('Total Eclipse of the Heart', 'Holding Out for a Hero', 'Billie Jean', 'Thriller', 'Bohemian Rhapsody', 'We Will Rock You', 'Another Brick in the Wall', 'Comfortably Numb', 'The Final Countdown', 'Carrie', 'Eye of the Tiger', 'Burning Heart', 'Livin'' on a Prayer', 'You Give Love a Bad Name', 'Wind of Change', 'Rock You Like a Hurricane', 'Sweet Child o'' Mine', 'November Rain', 'The House of the Rising Sun', 'Don''t Let Me Be Misunderstood', 'With or Without You', 'One', 'Rocket Man', 'Tiny Dancer', 'Dream On', 'I Don''t Want to Miss a Thing', 'Hey Jude', 'Let It Be', 'Yellow', 'Fix You', 'Iris', 'Slide', 'Lose Yourself', 'Stan', 'Radioactive', 'Demons', 'Someone Like You', 'Hello', 'Lonely', 'Smack That')");
        }
    }
}
