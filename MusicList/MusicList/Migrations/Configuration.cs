namespace MusicList.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MusicList.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicList.Models.MusicListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicList.Models.MusicListContext context)
        {
            context.Artists.AddOrUpdate(x=> x.Id,
                new Artist() { Id = 1, Name = "BnS" },
                new Artist() { Id = 2, Name = "Backstreet Boys" },
                new Artist() { Id = 3, Name = "Mical Jackson" }
                ); 

            context.Songs.AddOrUpdate(x=> x.Id,
                new Songs()
                {
                    Id = 1,
                    Title = "Master Sir",
                    Year = 2011,
                    ArtistId = 1,
                    Price = 3.00M,
                    Type="POP"
                },
                new Songs()
                {
                    Id = 2,
                    Title = "Unmadani",
                    Year = 2011,
                    ArtistId = 1,
                    Price = 3.00M,
                    Type = "POP"
                },
                new Songs()
                {
                    Id = 3,
                    Title = "Who you are",
                    Year= 1999, 
                    ArtistId = 2,
                    Price = 3.00M,
                    Type = "POP"
                },
                new Songs()
                {
                    Id = 4,
                    Title = "As long as love",
                    Year = 2000,
                    ArtistId = 2,
                    Price = 3.00M, 
                    Type = "POP"
                },
                new Songs()
                {
                    Id = 5,
                    Title = "Heal the World",
                    Year = 2000,
                    ArtistId = 3,
                    Price = 3.00M,
                    Type = "POP"
                },
                new Songs()
                {
                    Id = 6,
                    Title = "Black or White",
                    Year = 2000,
                    ArtistId = 3,
                    Price = 3.00M,
                    Type = "POP"
                }
                );
        }
    }
}
