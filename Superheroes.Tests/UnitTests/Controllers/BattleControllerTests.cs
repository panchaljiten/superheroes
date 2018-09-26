using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Superheroes.Tests.Fixtures;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Superheroes.Tests.UnitTests.Controllers
{
    public class BattleControllerTests : BaseTests, IClassFixture<TestServerFixture>
    {
        HttpClient _client;

        public BattleControllerTests(TestServerFixture testServerFixture)
        {
            _client = testServerFixture.HttpClient;
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("Batman", null)]
        [InlineData(null, "Joker")]
        public async Task GetWinner_ShouldReturn_BadRequestResponse(string hero, string villain)
        {
            var response = await _client.GetAsync($"battle?hero={hero}&villain={villain}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("Batman1", "Joker")]
        [InlineData("Batman", "Joker1")]
        public async Task GetWinner_ShouldReturn_BadRequestStatusOnInvalidCharacterName(string hero, string villain)
        {
            var response = await _client.GetAsync($"battle?hero={hero}&villain={villain}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("Superman", "Joker", 9.6)]
        [InlineData("Gamora", "Joker", 8.4)]
        [InlineData("Wonder Woman", "Joker", 8.7)]
        [InlineData("Thor", "Joker", 9.2)]
        [InlineData("Spiderman", "Harley Quinn", 7.9)]
        public async Task Get_ShouldReturn_HeroCharacter(string hero, string villain, double score)
        {
            var response = await _client.GetAsync($"battle?hero={hero}&villain={villain}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

            responseObject.Value<string>("name").Should().Be(hero);
            responseObject.Value<double>("score").Should().Be(score);
        }

        [Theory]
        [InlineData("Batman", "Thanos", 9.9)]
        [InlineData("Aquaman", "Harley Quinn", 7.3)]
        [InlineData("Spiderman", "Lex Luthor", 8)]
        public async Task GetWinner_ShouldReturn_VillainCharacter(string hero, string villain, double score)
        {
            var response = await _client.GetAsync($"battle?hero={hero}&villain={villain}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

            responseObject.Value<string>("name").Should().Be(villain);
            responseObject.Value<double>("score").Should().Be(score);
        }

        [Theory]
        [InlineData("Batman", "Joker", 8.2)]
        public async Task GetWinner_ShouldReturn_VillainCharacterWhenFightingHeroIsWeakAgainstVillain(string hero, string villain, double score)
        {
            var response = await _client.GetAsync($"battle?hero={hero}&villain={villain}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

            responseObject.Value<string>("name").Should().Be(villain);
            responseObject.Value<double>("score").Should().Be(score);
        }

        [Theory]
        [InlineData("Superman", "Lex Luthor", 9.6)]
        public async Task GetWinner_ShouldReturn_HeroCharacterWhenFightingHeroIsWeakAgainstVillain(string hero, string villain, double score)
        {
            var response = await _client.GetAsync($"battle?hero={hero}&villain={villain}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

            responseObject.Value<string>("name").Should().Be(hero);
            responseObject.Value<double>("score").Should().Be(score);
        }

        [Theory]
        [InlineData("Superman", "Superman")]
        [InlineData("Lex Luthor", "Lex Luthor")]
        public async Task GetWinner_ShouldThrow_ValidationExceptionWhenCharacterTypesAreSame(string hero, string villain)
        {
            var response = await _client.GetAsync($"battle?hero={hero}&villain={villain}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}