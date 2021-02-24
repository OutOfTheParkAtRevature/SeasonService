using System;
using System.Collections.Generic;
using System.Text;

namespace SeasonService.Tests
{
   public class SeasonControllerTests
    {
        [Fact]
        public async void TestGetSeasons()
        {
            var options = new DbContextOptions<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                SeasonController seasonsController = new SeasonController(logic);

                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()
                };

            
                r.Seasons.Add(seasons);
                await r.CommitSave();

                var getSeasons = await seasonController.GetSeasons();
                Assert.NotNull(getSeasons);
            }
        }

        [Fact]
        public async void TestGetSeasonById()
        {
            var options = new DbContextOptions<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                SeasonController seasonsController = new SeasonController(logic);

                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()
                };


                r.Seasons.Add(seasons);
                await r.CommitSave();

                var getSeasonByID = await seasonController.GetSeasonById(season.SeasonID);
                Assert.NotNull(getSeasonByID);
            }
        }


        [Fact]
        public async void TestGetGamesBySeason()
        {
            var options = new DbContextOptions<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                SeasonController seasonsController = new SeasonController(logic);

                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()
                };


                r.Seasons.Add(seasons);
                await r.CommitSave();

                var getGamesBySeason = await seasonController.GetGamesBySeason(season.SeasonID);
                Assert.NotNull(getGamesBySeason);
            }
        }


        [Fact]
        public async void TestCreateSeason()
        {
            var options = new DbContextOptions<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic logic = new Logic(r, new NullLogger<Repo>());
                SeasonController seasonsController = new SeasonController(logic);

                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()
                };


                r.Seasons.Add(seasons);
                await r.CommitSave();

                var getCreateSeason = await seasonController.CreateSeason();
                Assert.NotNull(getCreateSeason);
            }
        }



    }
}
