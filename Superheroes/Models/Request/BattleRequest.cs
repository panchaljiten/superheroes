using System.ComponentModel.DataAnnotations;

namespace Superheroes.Models.Request
{
    public class BattleRequest
    {
        [Required]
        public string Hero { get; set; }
        [Required]
        public string Villain { get; set; }
    }
}
