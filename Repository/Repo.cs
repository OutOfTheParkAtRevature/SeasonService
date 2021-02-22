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
        public DbSet<Season> Seasons;

        public Repo(SeasonContext seasonContext, ILogger<Repo> logger)
        {
            _seasonContext = seasonContext;
            _logger = logger;
            this.Games = _seasonContext.Games;
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
