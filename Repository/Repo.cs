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
        /// <summary>
        /// saves changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task CommitSave()
        {
            await _seasonContext.SaveChangesAsync();
        }
        /// <summary>
        /// returns a game based on the gameID passed in.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Game> GetGameById(Guid ID)
        {
            return await Games.FindAsync(ID);
        }
        /// <summary>
        /// returns all games
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await Games.ToListAsync();
        }
        /// <summary>
        /// returns a PlayerGame based on the PlayerId and the gameId
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<PlayerGame> GetPlayerGameById(string playerId, Guid gameId)
        {
            return await PlayerGames.FirstOrDefaultAsync(x => x.UserID == playerId && x.GameID == gameId);
        }
        /// <summary>
        /// reutrns all playergames
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerGame>> GetPlayerGames()
        {
            return await PlayerGames.ToListAsync();
        }
        /// <summary>
        /// returns a list of playerGames where the playerId == UserId
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerGame>> GetPlayerGamesByPlayerId(string playerId)
        {
            return await PlayerGames.Where(x => x.UserID == playerId).ToListAsync();
        }
        /// <summary>
        /// reutnrs a list of PlayerGames ased on the gameId
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerGame>> GetPlayerGamesByGameId(Guid gameId)
        {
            return await PlayerGames.Where(x => x.GameID == gameId).ToListAsync();
        }
        /// <summary>
        /// returns the season of the seasonID passed into the method
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Season> GetSeasonById(Guid ID)
        {
            return await Seasons.FindAsync(ID);
        }
        /// <summary>
        /// returns all seasons
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await Seasons.ToListAsync();
        }
    }
}
