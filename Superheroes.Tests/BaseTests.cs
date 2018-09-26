using Moq;
using Superheroes.Contracts;
using Superheroes.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superheroes.Tests
{
    public abstract class BaseTests
    {
        protected static CharactersResponse GetCharacterResponse()
        {
            CharactersResponse charactersResponse = new CharactersResponse();

            var items = new List<CharacterResponse>();

            items.Add(new CharacterResponse()
            {
                Name = "Batman",
                Score = 8.3,
                Type = "hero",
                Weakness = "Joker",
            });
            items.Add(new CharacterResponse()
            {
                Name = "Joker",
                Score = 8.2,
                Type = "villain"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Superman",
                Score = 9.6,
                Type = "hero",
                Weakness = "Lex Luthor",
            });
            items.Add(new CharacterResponse()
            {
                Name = "Gamora",
                Score = 8.4,
                Type = "hero"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Thanos",
                Score = 9.9,
                Type = "villain"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Wonder Woman",
                Score = 8.7,
                Type = "hero"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Lex Luthor",
                Score = 8,
                Type = "villain"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Aquaman",
                Score = 3.5,
                Type = "hero"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Thor",
                Score = 9.2,
                Type = "hero"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Spiderman",
                Score = 7.9,
                Type = "hero"
            });
            items.Add(new CharacterResponse()
            {
                Name = "Harley Quinn",
                Score = 7.3,
                Type = "villain"
            });

            charactersResponse.Items = items.ToArray();

            return charactersResponse;
        }

        public static ICharactersProvider CreateCharactersProvider()
        {
            var charactersProvider = new Mock<ICharactersProvider>();

            charactersProvider.Setup(m => m.GetCharacters()).Returns(Task.FromResult(GetCharacterResponse()));

            return charactersProvider.Object;
        }
    }
}
