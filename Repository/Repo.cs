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
        public DbSet<Game> Games;
        public DbSet<PlayerGame> PlayerGames;
        public DbSet<Season> Seasons;

        public Repo(SeasonContext seasonContext, ILogger<Repo> logger)
        {
            _seasonContext = seasonContext;
            _logger = logger;
            this.Games = _seasonContext.Games;
            this.PlayerGames = _seasonContext.PlayerGames;
            this.Seasons = _seasonContext.Seasons;
        }

        public async Task CommitSave()
        {
            await _seasonContext.SaveChangesAsync();
        }

        public async Task<Game> GetGameById(Guid ID)
        {
            return await Games.FindAsync(ID);
        }
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await Games.ToListAsync();
        }
        public async Task<PlayerGame> GetPlayerGameById(string playerId, Guid gameId)
        {
            return await PlayerGames.FirstOrDefaultAsync(x => x.UserID == playerId && x.GameID == gameId);
        }
        public async Task<IEnumerable<PlayerGame>> GetPlayerGames()
        {
            return await PlayerGames.ToListAsync();
        }
        public async Task<IEnumerable<PlayerGame>> GetPlayerGamesByPlayerId(string playerId)
        {
            return await PlayerGames.Where(x => x.UserID == playerId).ToListAsync();
        }
        public async Task<IEnumerable<PlayerGame>> GetPlayerGamesByGameId(Guid gameId)
        {
            return await PlayerGames.Where(x => x.GameID == gameId).ToListAsync();
        }
        public async Task<Season> GetSeasonById(Guid ID)
        {
            return await Seasons.FindAsync(ID);
        }
        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await Seasons.ToListAsync();
        }
    }
}
