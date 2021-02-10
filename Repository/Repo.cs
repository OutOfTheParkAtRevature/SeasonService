using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;

namespace Repository
{
    public class Repo
    {
        private readonly SeasonContext _seasonContext;
        private readonly ILogger _logger;
        public DbSet<Game> games;
        public DbSet<PlayerGame> playerGames;
        public DbSet<Season> seasons;

        public Repo(SeasonContext seasonContext, ILogger<Repo> logger)
        {
            _seasonContext = seasonContext;
            _logger = logger;
            this.games = _seasonContext.games;
            this.playerGames = _seasonContext.playerGames;
            this.seasons = _seasonContext.seasons;
        }

        public async Task CommitSave()
        {
            await _seasonContext.SaveChangesAsync();
        }

        public async Task<Game> GetGameById(Guid ID)
        {
            return await games.FindAsync(ID);
        }
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await games.ToListAsync();
        }
        public async Task<PlayerGame> GetPlayerGameById(Guid ID)
        {
            return await playerGames.FindAsync(ID);
        }
        public async Task<IEnumerable<PlayerGame>> GetPlayerGames()
        {
            return await playerGames.ToListAsync();
        }
        public async Task<Season> GetSeasonById(Guid ID)
        {
            return await seasons.FindAsync(ID);
        }
        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await seasons.ToListAsync();
        }
    }
}
