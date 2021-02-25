using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Repository;
using SeasonService.Controllers;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SeasonService.Tests
{
   public class SeasonControllerTests
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
                Logic logic = new Logic(r, new NullLogger<Repo>());
                SeasonController seasonsController = new SeasonController(logic);

                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()
                };

            
                r.Seasons.Add(season);
                await r.CommitSave();

                var getSeasons = await seasonsController.GetSeasons();
                Assert.NotNull(getSeasons);
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
                Logic logic = new Logic(r, new NullLogger<Repo>());
                SeasonController seasonsController = new SeasonController(logic);

                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()
                };


                r.Seasons.Add(season);
                await r.CommitSave();

                var getSeasonByID = await seasonsController.GetSeasonById(season.SeasonID);
                Assert.NotNull(getSeasonByID);
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
                Logic logic = new Logic(r, new NullLogger<Repo>());
                SeasonController seasonsController = new SeasonController(logic);

                var season = new Season
                {
                    SeasonID = Guid.NewGuid(),
                    LeagueID = Guid.NewGuid()
                };


                r.Seasons.Add(season);
                await r.CommitSave();

                var getGamesBySeason = await seasonsController.GetGamesBySeason(season.SeasonID);
                Assert.NotNull(getGamesBySeason);
            }
        }


        //[Fact]
        //public async void TestCreateSeason()
        //{
        //    var options = new DbContextOptionsBuilder<SeasonContext>()
        //    .UseInMemoryDatabase(databaseName: "p3SeasonService")
        //    .Options;

        //    using (var context = new SeasonContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repo r = new Repo(context, new NullLogger<Repo>());
        //        Logic logic = new Logic(r, new NullLogger<Repo>());
        //        SeasonController seasonsController = new SeasonController(logic);

        //        var season = new Season
        //        {
        //            SeasonID = Guid.NewGuid(),
        //            LeagueID = Guid.NewGuid()
        //        };


                

        //        var getCreateSeason = await seasonsController.CreateSeason();
        //        Assert.NotNull(getCreateSeason);
        //    }
        //}



    }
}
