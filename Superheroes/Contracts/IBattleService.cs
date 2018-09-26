using Superheroes.Models.Response;
using System.Threading.Tasks;

namespace Superheroes.Contracts
{
    public interface IBattleService
    {
        Task<CharacterResponse> GetWinner(string hero, string villain);
    }
}
