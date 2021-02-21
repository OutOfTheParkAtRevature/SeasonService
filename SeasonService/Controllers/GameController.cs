using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.DataTransfer;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeasonService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly Logic _logic;

        public GameController(Logic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            return Ok(await _logic.GetGames());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            Game game = await _logic.GetGameById(id);
            if (game == null) return NotFound("No Game with that ID was found.");
            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameDto createGameDto)
        {
            if (await _logic.GameExists(createGameDto) == true) return Conflict("A game with those teams is already scheduled for that day.");
            return Ok(await _logic.CreateGame(createGameDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditGame(Guid id, [FromBody] EditGameDto editGameDto)
        {
            if (await _logic.GetGameById(id) == null) return NotFound("Game with that ID was not found.");
            return Ok(await _logic.EditGame(id, editGameDto));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            if (await _logic.GetGameById(id) == null) return NotFound("Game with that ID was not found.");
            bool result = await _logic.DeleteGame(id);
            if (result) return Ok("Game deleted.");
            return NotFound("Game with that ID was already deleted.");
        }

        
    }
}
