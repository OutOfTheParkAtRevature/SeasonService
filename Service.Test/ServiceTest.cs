using System;
using Xunit;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;

namespace Service.Test
{
    public class ServiceTest
    {
        [Fact]
        public async void TestGetSeasons()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());


                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()

                };


                r.Seasons.Add(season);
                await r.CommitSave();
                var listOfSeasons = await r.GetSeasons();
                Assert.NotNull(listOfSeasons);

            }

        }
    }
}
