using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.DataTransfer;
using Models.DataTransfer;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
                var response = await httpClient.GetAsync("http://10.0.118.116/api/League");
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
        public async Task<GameDto> GetGameById(Guid id, string token)
        {
            Game game = await _repo.GetGameById(id);
            if (game == null) return null;
            Team homeTeam = new Team();
            Team awayTeam = new Team();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{game.HomeTeamID}");
                var apiResponse = await response.Content.ReadAsStringAsync();
                homeTeam = JsonConvert.DeserializeObject<Team>(apiResponse);

                response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{game.AwayTeamID}");
                apiResponse = await response.Content.ReadAsStringAsync();
                awayTeam = JsonConvert.DeserializeObject<Team>(apiResponse);
            }
            GameDto gameDto = new GameDto
            {
                GameID = game.GameID,
                GameDate = game.GameDate,
                HomeScore = game.HomeScore,
                AwayScore = game.AwayScore,
                AwayTeam = awayTeam,
                HomeTeam = homeTeam,
                HomeTeamID = game.HomeTeamID,
                AwayTeamID = game.AwayTeamID,
                SeasonID = game.SeasonID,
                WinningTeamID = game.WinningTeam 
            };
            if (game.WinningTeam == game.AwayTeamID) gameDto.WinningTeam = awayTeam;
            if (game.WinningTeam == game.HomeTeamID) gameDto.WinningTeam = homeTeam;
            return gameDto;
        }

        /// <summary>
        /// Get a list of Games
        /// </summary>
        /// <returns>list of Games</returns>
        public async Task<IEnumerable<GameDto>> GetGames(string token)
        {
            IEnumerable<Game> games = await _repo.GetGames();
            List<GameDto> gameDtos = new List<GameDto>();
            foreach(Game g in games)
            {
                Team homeTeam = new Team();
                Team awayTeam = new Team();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{g.HomeTeamID}");
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    homeTeam = JsonConvert.DeserializeObject<Team>(apiResponse);

                    response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{g.AwayTeamID}");
                    apiResponse = await response.Content.ReadAsStringAsync();
                    awayTeam = JsonConvert.DeserializeObject<Team>(apiResponse);
                }
                GameDto gameDto = new GameDto
                {
                    GameID = g.GameID,
                    GameDate = g.GameDate,
                    HomeScore = g.HomeScore,
                    AwayScore = g.AwayScore,
                    AwayTeam = awayTeam,
                    HomeTeam = homeTeam,
                    HomeTeamID = g.HomeTeamID,
                    AwayTeamID = g.AwayTeamID,
                    SeasonID = g.SeasonID,
                    WinningTeamID = g.WinningTeam
                };
                if (g.WinningTeam == g.AwayTeamID) gameDto.WinningTeam = awayTeam;
                if (g.WinningTeam == g.HomeTeamID) gameDto.WinningTeam = homeTeam;
                gameDtos.Add(gameDto);
            }
            return gameDtos;
        }

        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="createGameDto">Game from input</param>
        /// <returns>Game</returns>
        public async Task<GameDto> CreateGame(CreateGameDto createGameDto, string token)
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
            
            // Create Calendar event for game
            Team homeTeam = new Team();
            Team awayTeam = new Team();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{newGame.HomeTeamID}");
                var apiResponse = await response.Content.ReadAsStringAsync();
                homeTeam = JsonConvert.DeserializeObject<Team>(apiResponse);
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{newGame.AwayTeamID}");
                var apiResponse = await response.Content.ReadAsStringAsync();
                awayTeam = JsonConvert.DeserializeObject<Team>(apiResponse);
            }
            
            EventDto eDto = new EventDto()
            {
                Description = $"Game - {awayTeam.Name} @ {homeTeam.Name}",
                Location = $"{homeTeam}",
                StartTime = newGame.GameDate,
                EndTime = newGame.GameDate.AddMinutes(60)
            };
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostAsJsonAsync($"http://10.0.6.107/api/Calendar", eDto);
            }
            
            await _repo.CommitSave();
            GameDto gameDto = new GameDto
            {
                GameID = newGame.GameID,
                GameDate = newGame.GameDate,
                SeasonID = newGame.SeasonID,
                AwayTeam = awayTeam,
                HomeTeam = homeTeam,
                AwayTeamID = newGame.AwayTeamID,
                HomeTeamID = newGame.HomeTeamID
            };

            return gameDto;
        }

        /// <summary>
        /// Edit a Game
        /// </summary>
        /// <param name="id">GameID</param>
        /// <param name="editGameDto">New information</param>
        /// <returns>modified Game</returns>
        public async Task<GameDto> EditGame(Guid id, EditGameDto editGameDto, string token)
        {
            Game editedGame = await _repo.GetGameById(id);

            if (editedGame.GameDate != editGameDto.GameDate && editGameDto.GameDate != null) { editedGame.GameDate = (DateTime)editGameDto.GameDate; }
            if (editedGame.WinningTeam != editGameDto.WinningTeamID && editGameDto.GameDate != null) { editedGame.WinningTeam = (Guid)editGameDto.WinningTeamID; }
            if (editedGame.HomeScore != editGameDto.HomeScore && editGameDto.GameDate != null) { editedGame.HomeScore = (int)editGameDto.HomeScore; }
            if (editedGame.AwayScore != editGameDto.AwayScore && editGameDto.GameDate != null) { editedGame.AwayScore = (int)editGameDto.AwayScore; }

            await _repo.CommitSave();
            
            Team homeTeam = new Team();
            Team awayTeam = new Team();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{editedGame.HomeTeamID}");
                var apiResponse = await response.Content.ReadAsStringAsync();
                homeTeam = JsonConvert.DeserializeObject<Team>(apiResponse);

                response = await httpClient.GetAsync($"http://10.0.118.116/api/Team/{editedGame.AwayTeamID}");
                apiResponse = await response.Content.ReadAsStringAsync();
                awayTeam = JsonConvert.DeserializeObject<Team>(apiResponse);
            }
            GameDto gameDto = new GameDto
            {
                GameID = editedGame.GameID,
                GameDate = editedGame.GameDate,
                HomeScore = editedGame.HomeScore,
                AwayScore = editedGame.AwayScore,
                AwayTeam = awayTeam,
                HomeTeam = homeTeam,
                HomeTeamID = editedGame.HomeTeamID,
                AwayTeamID = editedGame.AwayTeamID,
                SeasonID = editedGame.SeasonID,
                WinningTeamID = editedGame.WinningTeam
            };
            if (editedGame.WinningTeam == editedGame.AwayTeamID) gameDto.WinningTeam = awayTeam;
            if (editedGame.WinningTeam == editedGame.HomeTeamID) gameDto.WinningTeam = homeTeam;
            return gameDto;
        }    
        
        public async Task<bool> DeleteGame(Guid id)
        {
            Game game = await _repo.GetGameById(id);
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
