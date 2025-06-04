using revolutionariesrpg.api.Entities.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace revolutionariesrpg.api.Entities
{
    public class SpellEffect : BaseModel
    {
        public required int Level { get; set; }

        public required int Damage { get; set; }

        public required string Effect { get; set; }

        public required int Cooldown { get; set; }

        [ForeignKey("Spell")]
        public required Guid SpellId { get; set; }
    }
}
