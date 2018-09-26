using Superheroes.Models.Response;
using System.Threading.Tasks;

namespace Superheroes.Contracts
{
    public interface ICharactersProvider
    {
        Task<CharactersResponse> GetCharacters();
    }
}