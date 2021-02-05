using System;

namespace Logic
{
    public class Logic
    {
        /// <summary>
        /// Get a Game by GameID
        /// </summary>
        /// <param name="id">GameID</param>
        /// <returns>Game</returns>
        public async Task<Game> GetGameById(int id)
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
            await _repo.games.AddAsync(newGame);
            await _repo.CommitSave();
            return newGame;
        }
        /// <summary>
        /// Edit a Game
        /// </summary>
        /// <param name="id">GameID</param>
        /// <param name="editGameDto">New information</param>
        /// <returns>modified Game</returns>
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
        }
    }
}
