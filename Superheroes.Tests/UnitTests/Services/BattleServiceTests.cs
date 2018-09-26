using FluentAssertions;
using Superheroes.Exceptions;
using Superheroes.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Superheroes.Tests.UnitTests.Services
{
    public class BattleServiceTests : BaseTests, IDisposable
    {
        BattleService _battleService;

        public BattleServiceTests()
        {
            var charactersProvider = CreateCharactersProvider();
            _battleService = new BattleService(charactersProvider);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("Batman", null)]
        [InlineData(null, "Joker")]
        public async Task GetWinner_ShouldThrow_ArgumentNullExceptionOnInvalidInput(string hero, string villain)
        {
            try
            {
                await _battleService.GetWinner(hero, villain);
            }
            catch (Exception exception)
            {
                exception.Should().BeOfType<ValidationException>();
            }
        }

        [Theory]
        [InlineData("Batman1", "Joker")]
        [InlineData("Batman", "Joker1")]
        [InlineData("Joker", "Joker")]
        [InlineData("Superman", "Superman")]
        public async Task GetWinner_ShouldThrow_CharacterNotFoundExceptionOnInvalidCharacterName(string hero, string villain)
        {
            try
            {
                await _battleService.GetWinner(hero, villain);
            }
            catch (Exception exception)
            {
                exception.Should().BeOfType<ValidationException>();
            }
        }

        [Theory]
        [InlineData("Superman", "Joker", 9.6)]
        [InlineData("Gamora", "Joker", 8.4)]
        [InlineData("Wonder Woman", "Joker", 8.7)]
        [InlineData("Thor", "Joker", 9.2)]
        [InlineData("Spiderman", "Harley Quinn", 7.9)]
        public async Task GetWinner_ShouldReturn_HeroCharacter(string hero, string villain, double score)
        {
            var winner = await _battleService.GetWinner(hero, villain);

            winner.Should().NotBeNull();
            Assert.Equal(hero, winner.Name);
            Assert.Equal(score, winner.Score);
        }

        [Theory]
        [InlineData("Batman", "Thanos", 9.9)]
        [InlineData("Aquaman", "Harley Quinn", 7.3)]
        [InlineData("Spiderman", "Lex Luthor", 8)]
        public async Task GetWinner_ShouldReturn_VillainCharacter(string hero, string villain, double score)
        {
            var winner = await _battleService.GetWinner(hero, villain);

            winner.Should().NotBeNull();
            Assert.Equal(villain, winner.Name);
            Assert.Equal(score, winner.Score);
        }

        [Theory]
        [InlineData("Batman", "Joker", 8.2)]
        public async Task GetWinner_ShouldReturn_VillainCharacterWhenFightingHeroIsWeakAgainstVillain(string hero, string villain, double score)
        {
            var winner = await _battleService.GetWinner(hero, villain);

            winner.Should().NotBeNull();
            Assert.Equal(villain, winner.Name);
            Assert.Equal(score, winner.Score);
        }

        [Theory]
        [InlineData("Superman", "Lex Luthor", 9.6)]
        public async Task GetWinner_ShouldReturn_HeroCharacterWhenFightingHeroIsWeakAgainstVillain(string hero, string villain, double score)
        {
            var winner = await _battleService.GetWinner(hero, villain);

            winner.Should().NotBeNull();
            Assert.Equal(hero, winner.Name);
            Assert.Equal(score, winner.Score);
        }

        [Theory]
        [InlineData("Superman", "Superman")]
        [InlineData("Lex Luthor", "Lex Luthor")]
        public async Task GetWinner_ShouldThrow_ValidationExceptionWhenCharacterTypesAreSame(string hero, string villain)
        {
            try
            {
                await _battleService.GetWinner(hero, villain);
            }
            catch (Exception exception)
            {
                exception.Should().BeOfType<ValidationException>();
            }
        }

        public void Dispose()
        {
            _battleService = null;
        }
    }
}