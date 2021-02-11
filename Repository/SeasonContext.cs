using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class SeasonContext : DbContext
    {
        public DbSet<Game> Games;
        public DbSet<PlayerGame> PlayerGames;
        public DbSet<Season> Seasons;

        public SeasonContext() { }

        public SeasonContext(DbContextOptions<SeasonContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerGame>()
                .HasKey(c => new { c.UserID, c.GameID });
        }
    }
}
