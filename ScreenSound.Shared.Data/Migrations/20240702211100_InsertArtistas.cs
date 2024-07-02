using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    public partial class InsertArtistas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Bonnie Tyler', 'Bonnie Tyler é uma cantora galesa conhecida por sua voz rouca e poderosa.', '/FotosPerfil/bonnie_tyler.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Michael Jackson', 'Michael Jackson foi um cantor, compositor e dançarino americano, conhecido como o Rei do Pop.', '/FotosPerfil/michael_jackson.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Queen', 'Queen é uma banda britânica de rock formada em 1970.', '/FotosPerfil/queen.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Pink Floyd', 'Pink Floyd é uma banda britânica de rock progressivo, conhecida por seus álbuns conceituais.', '/FotosPerfil/pink_floyd.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Europe', 'Europe é uma banda sueca de hard rock famosa.', '/FotosPerfil/europe.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Survivor', 'Survivor é uma banda americana de rock.', '/FotosPerfil/survivor.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Bon Jovi', 'Bon Jovi é uma banda americana de rock, liderada por Jon Bon Jovi.', '/FotosPerfil/bon_jovi.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Scorpions', 'Scorpions é uma banda alemã de hard rock.', '/FotosPerfil/Scorpions.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Guns N'' Roses', 'Guns N'' Roses é uma banda americana de hard rock.', '/FotosPerfil/guns_n_roses.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('The Animals', 'The Animals foi uma banda britânica de rock formada na década de 1960.', '/FotosPerfil/the_animals.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('U2', 'U2 é uma banda irlandesa de rock formada em 1976, famosa por seu som de arena rock.', '/FotosPerfil/u2.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Elton John', 'Elton John é um cantor, compositor e pianista britânico, famoso por seus hits icônicos.', '/FotosPerfil/elton_john.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Aerosmith', 'Aerosmith é uma banda americana de rock formada em 1970, conhecida por seu som de hard rock.', '/FotosPerfil/aerosmith.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('The Beatles', 'The Beatles foi uma banda britânica de rock formada em Liverpool, considerada a mais influente de todos os tempos.', '/FotosPerfil/the_beatles.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Coldplay', 'Coldplay é uma banda britânica de rock alternativo, formada em 1996.', '/FotosPerfil/coldplay.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('The Goo Goo Dolls', 'The Goo Goo Dolls é uma banda americana de rock alternativo.', '/FotosPerfil/the_goo_goo_dolls.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Eminem', 'Eminem é um rapper, compositor e produtor musical americano, considerado um dos melhores rappers de todos os tempos.', '/FotosPerfil/eminem.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Imagine Dragons', 'Imagine Dragons é uma banda americana de rock alternativo.', '/FotosPerfil/imagine_dragons.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Adele', 'Adele é uma cantora e compositora britânica, conhecida por sua voz poderosa e baladas emocionantes.', '/FotosPerfil/adele.jpeg')");
            migrationBuilder.Sql("INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES ('Akon', 'Akon é um cantor, compositor e produtor musical senegalês-americano, conhecido por hits de R&B.', '/FotosPerfil/akon.jpeg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Artistas WHERE Nome IN ('Bonnie Tyler', 'Michael Jackson', 'Queen', 'Pink Floyd', 'Europe', 'Survivor', 'Bon Jovi', 'Scorpions', 'Guns N'' Roses', 'The Animals', 'U2', 'Elton John', 'Aerosmith', 'The Beatles', 'Coldplay', 'The Goo Goo Dolls', 'Eminem', 'Imagine Dragons', 'Adele', 'Akon')");
        }
    }
}
