using Newtonsoft.Json;

namespace Superheroes.Models.Response
{
    public class CharacterResponse
    {
        public string Name { get; set; }
        public double Score { get; set; }
        public string Type { get; set; }
        //[JsonIgnore]
        public string Weakness { get; set; }
    }
}
