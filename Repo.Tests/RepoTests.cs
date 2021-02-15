using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Repository;
using Xunit;

namespace Repository.Tests
{
    public class RepoTests
    {

        /// <summary>
        /// Tests the CommitSave() method of Repo
        /// </summary>
        /// 
        [Fact]
        public async void TestCommitSave()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
       .UseInMemoryDatabase(databaseName: "p3SeasonService")
       .Options;


            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());

                Season season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid(),

                };

                r.Seasons.Add(season);
                await r.CommitSave();
                Assert.NotEmpty(context.Seasons);

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

                var game = new Game
                {
                    GameID = Guid.NewGuid(),
                    SeasonID = Guid.NewGuid(),
                    HomeTeamID = Guid.NewGuid(),
                    AwayTeamID = Guid.NewGuid(),
                    GameDate = DateTime.UtcNow,
                    WinningTeam = Guid.NewGuid(),
                    HomeScore = 5,
                    AwayScore = 7,
                    HomeStatID = Guid.NewGuid(),
                    AwayStatID = Guid.NewGuid()


                };

                r.Games.Add(game);
                await r.CommitSave();
                var listOfGames = await r.GetGameById(game.GameID);
                Assert.True(listOfGames.Equals(game));



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

                var game = new Game
                {
                    GameID = Guid.NewGuid(),
                    SeasonID = Guid.NewGuid(),
                    HomeTeamID = Guid.NewGuid(),
                    AwayTeamID = Guid.NewGuid(),
                    GameDate = DateTime.UtcNow,
                    WinningTeam = Guid.NewGuid(),
                    HomeScore = 5,
                    AwayScore = 7,
                    HomeStatID = Guid.NewGuid(),
                    AwayStatID = Guid.NewGuid()


                };

                r.Games.Add(game);
                await r.CommitSave();
                var listOfGames = await r.GetGames();
                Assert.NotNull(listOfGames);


            }

        }


        [Fact]

        public async void TestGetPlayerGameById()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());

                var game = new Game
                {
                    GameID = Guid.NewGuid(),
                    SeasonID = Guid.NewGuid(),
                    HomeTeamID = Guid.NewGuid(),
                    AwayTeamID = Guid.NewGuid(),
                    GameDate = DateTime.UtcNow,
                    WinningTeam = Guid.NewGuid(),
                    HomeScore = 5,
                    AwayScore = 7,
                    HomeStatID = Guid.NewGuid(),
                    AwayStatID = Guid.NewGuid()

                };

                var playerGame = new PlayerGame
                {
                    UserID = "12345",
                    GameID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid()
                };

                //r.games.Add(game);
                r.PlayerGames.Add(playerGame);
                await r.CommitSave();
                var listOfPlayerGames = await r.GetPlayerGameById(playerGame.UserID, game.GameID);
                Assert.True(listOfPlayerGames.Equals(playerGame));

            }
        }


        [Fact]

        public async void TestGetPlayerGames()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());


                var playerGame = new PlayerGame
                {
                    UserID = "12345",
                    GameID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid()
                };

                r.PlayerGames.Add(playerGame);
                await r.CommitSave();
                var listOfPlayerGames = await r.GetPlayerGames();
                Assert.NotNull(listOfPlayerGames);
            }
        }

        [Fact]

        public async void TestGetPlayerGamesByPlayerId()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());


                var playerGame = new PlayerGame
                {
                    UserID = "123456789",
                    GameID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid()
                };

                r.PlayerGames.Add(playerGame);
                await r.CommitSave();
                var listOfPlayerGames = await r.GetPlayerGamesByPlayerId(playerGame.UserID);
                Assert.True(listOfPlayerGames.Equals(playerGame));

            }
        }

        [Fact]

        public async void TestGetPlayerGamesByGameId()
        {
            var options = new DbContextOptionsBuilder<SeasonContext>()
            .UseInMemoryDatabase(databaseName: "p3SeasonService")
            .Options;

            using (var context = new SeasonContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repo r = new Repo(context, new NullLogger<Repo>());


                var playerGame = new PlayerGame
                {
                    UserID = "123456789",
                    GameID = Guid.NewGuid(),
                    StatLineID = Guid.NewGuid()
                };

                r.PlayerGames.Add(playerGame);
                await r.CommitSave();
                var listOfPlayerGames = await r.GetPlayerGamesByGameId(playerGame.GameID);
                Assert.True(listOfPlayerGames.Equals(playerGame));

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


                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()

                };


                r.Seasons.Add(season);
                await r.CommitSave();
                var listOfSeasons = await r.GetSeasonById(season.SeasonID);
                Assert.True(listOfSeasons.Equals(season));

            }

        }

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