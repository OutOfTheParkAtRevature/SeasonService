using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Models.DataTransfer;
using Repository;
using SeasonService.Controllers;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SeasonService.Tests
{
    public class GameControllerTests
    {
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
                Logic logic = new Logic(r, new NullLogger<Repo>());
                GameController gameController = new GameController(logic);

                var game = new Game
                {
                    GameID = Guid.NewGuid(),
                    SeasonID = Guid.NewGuid(),
                    HomeTeamID = Guid.NewGuid(),
                    AwayTeamID = Guid.NewGuid(),
                    GameDate = DateTime.Now,
                    WinningTeam = Guid.NewGuid(),
                    HomeScore = 16,
                    AwayScore = 2,
                    HomeStatID = Guid.NewGuid(),
                    AwayStatID = Guid.NewGuid()
                };


                r.Games.Add(game);
                await r.CommitSave();

                var getGames = await gameController.GetGames();
                Assert.NotNull(getGames);
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
                    Logic logic = new Logic(r, new NullLogger<Repo>());
                    GameController gameController = new GameController(logic);


                    var game = new Game
                    {
                        GameID = Guid.NewGuid(),
                        SeasonID = Guid.NewGuid(),
                        HomeTeamID = Guid.NewGuid(),
                        AwayTeamID = Guid.NewGuid(),
                        GameDate = DateTime.Now,
                        WinningTeam = Guid.NewGuid(),
                        HomeScore = 16,
                        AwayScore = 2,
                        HomeStatID = Guid.NewGuid(),
                        AwayStatID = Guid.NewGuid()
                    };


                    r.Games.Add(game);
                    await r.CommitSave();

                    var getGames = await gameController.GetGameById(game.GameID);
                    Assert.NotNull(getGames);
                }
            }


            //[Fact]
            //public async void TestCreateGame()
            //{
            //    var options = new DbContextOptionsBuilder<SeasonContext>()
            //    .UseInMemoryDatabase(databaseName: "p3SeasonService2")
            //    .Options;

            //    using (var context = new SeasonContext(options))
            //    {
            //        context.Database.EnsureDeleted();
            //        context.Database.EnsureCreated();

            //        Repo r = new Repo(context, new NullLogger<Repo>());
            //        Logic logic = new Logic(r, new NullLogger<Repo>());
            //        GameController gameController = new GameController(logic);


            //        var game = new Game
            //        {
            //            GameID = Guid.NewGuid(),
            //            SeasonID = Guid.NewGuid(),
            //            HomeTeamID = Guid.NewGuid(),
            //            AwayTeamID = Guid.NewGuid(),
            //            GameDate = DateTime.Now,
            //            WinningTeam = Guid.NewGuid(),
            //            HomeScore = 16,
            //            AwayScore = 2,
            //            HomeStatID = Guid.NewGuid(),
            //            AwayStatID = Guid.NewGuid()
            //        };


                  
            //        var gameDto = new CreateGameDto
            //            {
            //                GameDate = DateTime.Now,
            //                HomeTeamID = Guid.NewGuid(),
            //                AwayTeamID = Guid.NewGuid()
            //            };

            //        r.Games.Add(game);
            //        await r.CommitSave();
            //        var createGame = await gameController.CreateGame(gameDto);
            //        Assert.NotNull(createGame);
                   
            //    }
            //}


            [Fact]
            public async void TestEditGame()
            {
                var options = new DbContextOptionsBuilder<SeasonContext>()
                .UseInMemoryDatabase(databaseName: "p3SeasonService2")
                .Options;

                using (var context = new SeasonContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repo r = new Repo(context, new NullLogger<Repo>());
                    Logic logic = new Logic(r, new NullLogger<Repo>());
                    GameController gameController = new GameController(logic);


                    var game = new Game
                    {
                        GameID = Guid.NewGuid(),
                        SeasonID = Guid.NewGuid(),
                        HomeTeamID = Guid.NewGuid(),
                        AwayTeamID = Guid.NewGuid(),
                        GameDate = DateTime.Now,
                        WinningTeam = Guid.NewGuid(),
                        HomeScore = 16,
                        AwayScore = 2,
                        HomeStatID = Guid.NewGuid(),
                        AwayStatID = Guid.NewGuid()
                    };

                    var editGameDto = new EditGameDto
                    {
                        GameDate = DateTime.Now,
                        WinningTeamID = Guid.NewGuid(),
                        HomeScore = 3,
                        AwayScore = 2,
                        HomeStatID = Guid.NewGuid(),
                        AwayStatID = Guid.NewGuid()
                    };

                    var getGame = await gameController.EditGame(game.GameID, editGameDto);
                    Assert.IsAssignableFrom<string>((getGame as NotFoundObjectResult).Value);
                   
                    
                    r.Games.Add(game);
                    await r.CommitSave();

                    var getGame2 = await gameController.EditGame(game.GameID, editGameDto);
                    Assert.IsAssignableFrom<Game>((getGame2 as OkObjectResult).Value);


                }
            }



            [Fact]
            public async void TestDeleteGame()
            {
                var options = new DbContextOptionsBuilder<SeasonContext>()
                .UseInMemoryDatabase(databaseName: "p3SeasonService")
                .Options;

                using (var context = new SeasonContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repo r = new Repo(context, new NullLogger<Repo>());
                    Logic logic = new Logic(r, new NullLogger<Repo>());
                    GameController gameController = new GameController(logic);


                    var game = new Game
                    {
                        GameID = Guid.NewGuid(),
                        SeasonID = Guid.NewGuid(),
                        HomeTeamID = Guid.NewGuid(),
                        AwayTeamID = Guid.NewGuid(),
                        GameDate = DateTime.Now,
                        WinningTeam = Guid.NewGuid(),
                        HomeScore = 16,
                        AwayScore = 2,
                        HomeStatID = Guid.NewGuid(),
                        AwayStatID = Guid.NewGuid()
                    };

                    


                    r.Games.Add(game);
                    await r.CommitSave();

                    var getGame2 = await gameController.DeleteGame(game.GameID);
                Assert.NotNull(getGame2);


                }
            }




        }
    }
