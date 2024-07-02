using Microsoft.EntityFrameworkCore.Migrations;

namespace ScreenSound.Migrations
{
    public partial class InsertGenerosMusicas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Rock
                INSERT INTO GeneroMusica (GenerosId, MusicasId)
                VALUES
                (1, 1),    -- Total Eclipse of the Heart
                (1, 2),    -- Holding Out for a Hero
                (1, 5),    -- Bohemian Rhapsody
                (1, 6),    -- We Will Rock You
                (1, 7),    -- Another Brick in the Wall
                (1, 8),    -- Comfortably Numb
                (1, 9),    -- The Final Countdown
                (1, 10),   -- Carrie
                (1, 11),   -- Eye of the Tiger
                (1, 12),   -- Burning Heart
                (1, 13),   -- Livin' on a Prayer
                (1, 14),   -- You Give Love a Bad Name
                (1, 15),   -- Wind of Change
                (1, 16),   -- Rock You Like a Hurricane
                (1, 17),   -- Sweet Child o' Mine
                (1, 18),   -- November Rain
                (1, 19),   -- The House of the Rising Sun
                (1, 20);   -- Don't Let Me Be Misunderstood

                -- Pop
                INSERT INTO GeneroMusica (GenerosId, MusicasId)
                VALUES
                (2, 3),    -- Billie Jean
                (2, 4);    -- Thriller

                -- Rock
                INSERT INTO GeneroMusica (GenerosId, MusicasId)
                VALUES
                (1, 21),   -- With or Without You
                (1, 22),   -- One
                (1, 23),   -- Rocket Man
                (1, 24),   -- Tiny Dancer
                (1, 25),   -- Dream On
                (1, 26),   -- I Don't Want to Miss a Thing
                (1, 27),   -- Hey Jude
                (1, 28),   -- Let It Be
                (1, 29),   -- Yellow
                (1, 30),   -- Fix You
                (1, 31),   -- Iris
                (1, 32);   -- Slide

                -- Hip Hop
                INSERT INTO GeneroMusica (GenerosId, MusicasId)
                VALUES
                (3, 33),   -- Lose Yourself
                (3, 34),   -- Stan
                (3, 39),   -- Lonely
                (3, 40);   -- Smack That

                -- Electronic
                INSERT INTO GeneroMusica (GenerosId, MusicasId)
                VALUES
                (4, 35),   -- Radioactive
                (4, 36);   -- Demons

                -- Blues
                INSERT INTO GeneroMusica (GenerosId, MusicasId)
                VALUES
                (5, 37),   -- Someone Like You
                (5, 38);   -- Hello
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM GeneroMusica WHERE MusicasId BETWEEN 1 AND 40;
            ");
        }
    }
}
