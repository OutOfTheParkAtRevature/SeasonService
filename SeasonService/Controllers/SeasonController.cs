using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeasonService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly Logic _logic;

        public SeasonController(Logic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeasons()
        {
            return Ok(await _logic.GetSeasons());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeasonById(Guid id)
        {
            Season season = await _logic.GetSeasonById(id);
            if (season == null) return NotFound("No season with that ID was found.");
            return Ok(season);
        }

        [HttpGet("{id}/games")]
        public async Task<IActionResult> GetGamesBySeason(Guid seasonId)
        {
            if (await _logic.GetSeasonById(seasonId) == null) return NotFound("No season with that ID was found.");
            return Ok(await _logic.GetGamesBySeason(seasonId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSeason()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return Ok(await _logic.CreateSeason(token));
        }


    }
}
