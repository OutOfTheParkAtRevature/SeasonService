using System;
using Xunit;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Models.DataTransfer;

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
                Logic l = new Logic(r, new NullLogger<Repo>());


                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()

                };


                r.Seasons.Add(season);
                await r.CommitSave();
                var listOfSeasons = await l.GetSeasons();
                Assert.NotNull(listOfSeasons);

            }

        }


        [Fact]
        public async void TestGetSeasonById()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());


                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()

                };


                r.Seasons.Add(season);
                await r.CommitSave();
                var listOfSeasonsId = await l.GetSeasonById(season.SeasonID);
                Assert.NotNull(listOfSeasonsId);

            }

        }

        [Fact]
        public async void TestGetGamesBySeason()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());



                var game = new Game
                {
                    GameID = Guid.NewGuid(),
                    SeasonID = Guid.NewGuid(),
                    HomeTeamID = Guid.NewGuid(),
                    AwayTeamID = Guid.NewGuid(),
                    GameDate = DateTime.Now,
                    WinningTeam = Guid.NewGuid(),
                    HomeScore = 15,
                    AwayScore = 19,
                    HomeStatID = Guid.NewGuid(),
                    AwayStatID = Guid.NewGuid()
                };


                r.Games.Add(game);
                await r.CommitSave();
                var listOfGames = await l.GetGamesBySeason(game.SeasonID);
                Assert.NotNull(listOfGames);

            }

        }


        [Fact]
        public async void TestGetGameById()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());


                var game = new Game
                {
                    GameID = Guid.NewGuid(),
                    SeasonID = Guid.NewGuid(),
                    HomeTeamID = Guid.NewGuid(),
                    AwayTeamID = Guid.NewGuid(),
                    GameDate = DateTime.Now,
                    WinningTeam = Guid.NewGuid(),
                    HomeScore = 15,
                    AwayScore = 19,
                    HomeStatID = Guid.NewGuid(),
                    AwayStatID = Guid.NewGuid()
                };


                r.Games.Add(game);
                await r.CommitSave();
                var getGameById = await l.GetGameById(game.GameID);
                Assert.NotNull(getGameById);

            }

        }


        [Fact]
        public async void TestGetGames()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());
                Logic l = new Logic(r, new NullLogger<Repo>());



                var game = new Game
                {
                    GameID = Guid.NewGuid(),
                    SeasonID = Guid.NewGuid(),
                    HomeTeamID = Guid.NewGuid(),
                    AwayTeamID = Guid.NewGuid(),
                    GameDate = DateTime.Now,
                    WinningTeam = Guid.NewGuid(),
                    HomeScore = 15,
                    AwayScore = 19,
                    HomeStatID = Guid.NewGuid(),
                    AwayStatID = Guid.NewGuid()
                };


                r.Games.Add(game);
                await r.CommitSave();
                var getGames = await l.GetGames();
                Assert.NotNull(getGames);

            }

        }
       

    }
}
