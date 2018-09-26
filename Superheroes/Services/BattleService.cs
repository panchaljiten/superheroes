using Superheroes.Contracts;
using Superheroes.Exceptions;
using Superheroes.Models.Response;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Superheroes.Services
{
    public class BattleService : IBattleService
    {
        private const string SuperheroesCanOnlyFightSupervillainsErrorMessage = "Superheroes can only fight supervillains and vice versa.";
        private const string CharacterNotFoundErrorMessage = "Unable to find character: {0}";
        private const string RequiredFieldVillainErrorMessage = "The Villain field is required";
        private const string RequiredFieldHeroErrorMessage = "The Hero field is required";

        private readonly ICharactersProvider _charactersProvider;

        public BattleService(ICharactersProvider charactersProvider)
        {
            _charactersProvider = charactersProvider;
        }

        public async Task<CharacterResponse> GetWinner(string hero, string villain)
        {
            if (string.IsNullOrWhiteSpace(hero))
                throw new ValidationException(nameof(hero), RequiredFieldHeroErrorMessage);

            if (string.IsNullOrWhiteSpace(villain))
                throw new ValidationException(nameof(villain), RequiredFieldVillainErrorMessage);

            var characters = await _charactersProvider.GetCharacters();

            CharacterResponse FindCharacter(string name)
            {
                return characters.Items
                        .Where(it =>
                            string.Equals(name, it.Name, StringComparison.OrdinalIgnoreCase)
                        ).FirstOrDefault();
            }

            var characterHero = FindCharacter(hero);

            if (characterHero == null || string.Equals(characterHero.Type, "villain", StringComparison.OrdinalIgnoreCase))
                throw new ValidationException(string.Format(CharacterNotFoundErrorMessage, hero));

            var characterVillian = FindCharacter(villain);

            if (characterVillian == null || string.Equals(characterVillian.Type, "hero", StringComparison.OrdinalIgnoreCase))
                throw new ValidationException(string.Format(CharacterNotFoundErrorMessage, villain));

            if (string.Equals(characterHero.Type, characterVillian.Type, StringComparison.OrdinalIgnoreCase))
                throw new ValidationException(SuperheroesCanOnlyFightSupervillainsErrorMessage);

            var heroScore = characterHero.Score;

            if (string.Equals(villain, characterHero.Weakness, StringComparison.OrdinalIgnoreCase))
                heroScore = heroScore - 1;

            if (heroScore > characterVillian.Score)
                return characterHero;

            return characterVillian;
        }
    }
}