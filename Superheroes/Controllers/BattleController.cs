using Microsoft.AspNetCore.Mvc;
using Superheroes.Contracts;
using Superheroes.Models.Request;
using System.Threading.Tasks;

namespace Superheroes.Controllers
{
    [Route("battle")]
    public class BattleController : Controller
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        public async Task<IActionResult> Get(BattleRequest battleRequest)
        {
            return Ok(await _battleService.GetWinner(battleRequest.Hero, battleRequest.Villain));
        }
    }
}