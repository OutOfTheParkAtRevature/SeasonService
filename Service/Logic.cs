using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Models.DataTransfer;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Logic
    {
        public Logic() { }
        public Logic(Repo repo, ILogger<Repo> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        private readonly Repo _repo;
        private readonly ILogger<Repo> _logger;


        /*
         * 
         * 
         * Season logic
         * 
         * 
         */

        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await _repo.GetSeasons();
        }

        public async Task<Season> GetSeasonById(Guid seasonId)
        {
            return await _repo.GetSeasonById(seasonId);
        }

        public async Task<IEnumerable<Game>> GetGamesBySeason(Guid id)
        {
            return await _repo.Games.Where(x => x.SeasonID == id).ToListAsync();
        }

        public async Task<Season> CreateSeason(string token)
        {
            Season season = new Season();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync("https://localhost:44313/api/League");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var league = JsonConvert.DeserializeObject<List<League>>(apiResponse);
                season.LeagueID = league[0].LeagueID;
            }
            await _repo.Seasons.AddAsync(season);
            await _repo.CommitSave();
            return season;
        }

        /*
         * 
         * 
         * Game logic
         * 
         *
         */


        public async Task<bool> GameExists(CreateGameDto createGameDto)
        {
            var games = await _repo.GetGames();
            foreach(Game game in games)
            {
                if(game.GameDate == createGameDto.GameDate && 
                    game.HomeTeamID == createGameDto.HomeTeamID && 
                     game.AwayTeamID == createGameDto.AwayTeamID)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get a Game by GameID
        /// </summary>
        /// <param name="id">GameID</param>
        /// <returns>Game</returns>
        public async Task<Game> GetGameById(Guid id)
        {
            return await _repo.GetGameById(id);
        }

        /// <summary>
        /// Get a list of Games
        /// </summary>
        /// <returns>list of Games</returns>
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _repo.GetGames();
        }

        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="createGameDto">Game from input</param>
        /// <returns>Game</returns>
        public async Task<Game> CreateGame(CreateGameDto createGameDto)
        {
            IEnumerable<Season> seasons = await _repo.GetSeasons();
            Game newGame = new Game()
            {
                SeasonID = seasons.ToList().LastOrDefault().SeasonID,
                HomeTeamID = createGameDto.HomeTeamID,
                AwayTeamID = createGameDto.AwayTeamID,
                GameDate = createGameDto.GameDate,                
            };
            await _repo.Games.AddAsync(newGame);
            await _repo.CommitSave();
            return newGame;
        }

        /// <summary>
        /// Edit a Game
        /// </summary>
        /// <param name="id">GameID</param>
        /// <param name="editGameDto">New information</param>
        /// <returns>modified Game</returns>
        public async Task<Game> EditGame(Guid id, EditGameDto editGameDto)
        {
            Game editedGame = await GetGameById(id);

            if (editedGame.GameDate != editGameDto.GameDate && editGameDto.GameDate != null) { editedGame.GameDate = (DateTime)editGameDto.GameDate; }
            if (editedGame.WinningTeam != editGameDto.WinningTeamID && editGameDto.GameDate != null) { editedGame.WinningTeam = (Guid)editGameDto.WinningTeamID; }
            if (editedGame.HomeScore != editGameDto.HomeScore && editGameDto.GameDate != null) { editedGame.HomeScore = (int)editGameDto.HomeScore; }
            if (editedGame.AwayScore != editGameDto.AwayScore && editGameDto.GameDate != null) { editedGame.AwayScore = (int)editGameDto.AwayScore; }

            await _repo.CommitSave();
            return editedGame;
        }    
        
        public async Task<bool> DeleteGame(Guid id)
        {
            Game game = await GetGameById(id);
            try
            {
                _repo.Games.Remove(game);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            await _repo.CommitSave();
            return true;
        }
    }
}
