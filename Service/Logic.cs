<<<<<<< Updated upstream
﻿using Microsoft.EntityFrameworkCore;
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
=======
﻿using System;

namespace Logic
{
    public class Logic
    {
>>>>>>> Stashed changes
        /// <summary>
        /// Get a Game by GameID
        /// </summary>
        /// <param name="id">GameID</param>
        /// <returns>Game</returns>
<<<<<<< Updated upstream
        public async Task<Game> GetGameById(Guid id)
        {
            return await _repo.GetGameById(id);
        }

=======
        public async Task<Game> GetGameById(int id)
        {
            return await _repo.GetGameById(id);
        }
>>>>>>> Stashed changes
        /// <summary>
        /// Get a list of Games
        /// </summary>
        /// <returns>list of Games</returns>
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _repo.GetGames();
        }
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="createGameDto">Game from input</param>
        /// <returns>Game</returns>
        public async Task<Game> CreateGame(CreateGameDto createGameDto)
        {
<<<<<<< Updated upstream
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
            string homeTeam = "";
            string awayTeam = "";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"api/Team/{newGame.HomeTeamID}");
                homeTeam = response.ToString();
            }
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"api/Team/{newGame.AwayTeamID}");
                awayTeam = response.ToString();
            }
            
            EventDto eDto = new EventDto()
            {
                Description = $"Game - {awayTeam} @ {homeTeam}",
                Location = $"{homeTeam}",
                StartTime = newGame.GameDate,
                EndTime = newGame.GameDate.AddMinutes(60)
            };
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync($"api/Calendar", eDto);
            }
            
            await _repo.CommitSave();
            return newGame;
        }

=======
            Game newGame = new Game()
            {
                HomeTeamID = createGameDto.HomeTeamID,
                AwayTeamID = createGameDto.AwayTeamID,
                GameDate = createGameDto.GameDate
            };
            await _repo.games.AddAsync(newGame);
            await _repo.CommitSave();
            return newGame;
        }
>>>>>>> Stashed changes
        /// <summary>
        /// Edit a Game
        /// </summary>
        /// <param name="id">GameID</param>
        /// <param name="editGameDto">New information</param>
        /// <returns>modified Game</returns>
<<<<<<< Updated upstream
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
=======
        public async Task<Game> EditGame(int id, EditGameDto editGameDto)
        {
            Game editedGame = await GetGameById(id);
            if (editedGame != null)
            {
                if (editedGame.WinningTeam != editGameDto.WinningTeamID) { editedGame.WinningTeam = editGameDto.WinningTeamID; }
                if (editedGame.HomeScore != editGameDto.HomeScore) { editedGame.HomeScore = editGameDto.HomeScore; }
                if (editedGame.AwayScore != editGameDto.AwayScore) { editedGame.AwayScore = editGameDto.AwayScore; }
                if (editedGame.Statistic1 != editGameDto.Statistic1) { editedGame.Statistic1 = editGameDto.Statistic1; }
                if (editedGame.Statistic2 != editGameDto.Statistic2) { editedGame.Statistic2 = editGameDto.Statistic2; }
                if (editedGame.Statistic3 != editGameDto.Statistic3) { editedGame.Statistic3 = editGameDto.Statistic3; }
                await _repo.CommitSave();
            }
            return editedGame;
>>>>>>> Stashed changes
        }
    }
}
