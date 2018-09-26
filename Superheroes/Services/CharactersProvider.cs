using Newtonsoft.Json;
using Superheroes.Contracts;
using Superheroes.Models.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace Superheroes.Services
{
    internal class CharactersProvider : ICharactersProvider
    {
        private const string CharactersUri = "https://s3.eu-west-2.amazonaws.com/build-circle/characters.json";

        private readonly HttpClient _client = new HttpClient();

        public async Task<CharactersResponse> GetCharacters()
        {
            var response = await _client.GetAsync(CharactersUri);

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CharactersResponse>(responseJson);
        }
    }
}