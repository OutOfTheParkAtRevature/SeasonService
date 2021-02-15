using Microsoft.Extensions.Logging;
using Model;
using Models.DataTransfer;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Game newGame = new Game()
            {
                HomeTeamID = createGameDto.HomeTeamID,
                AwayTeamID = createGameDto.AwayTeamID,
                GameDate = createGameDto.GameDate
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
            if (editedGame != null)
            {
                if (editedGame.WinningTeam != editGameDto.WinningTeamID) { editedGame.WinningTeam = editGameDto.WinningTeamID; }
                if (editedGame.HomeScore != editGameDto.HomeScore) { editedGame.HomeScore = editGameDto.HomeScore; }
                if (editedGame.AwayScore != editGameDto.AwayScore) { editedGame.AwayScore = editGameDto.AwayScore; }

                await _repo.CommitSave();
            }
            return editedGame;
        }
        /// <summary>
        /// creates a PlayerGame with the passed in parameters, adds the PlayerGameto the 
        /// playerGames repo dbset
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<PlayerGame> BuildPlayerGame(string playerId, Guid gameId)
        {
            PlayerGame pGame = new PlayerGame()
            {
                UserID = playerId,
                GameID = gameId,
                StatLineID = Guid.NewGuid()
            };
            await _repo.PlayerGames.AddAsync(pGame);
            return pGame;
        }
    }
}
