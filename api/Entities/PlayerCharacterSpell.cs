using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class PlayerCharacterSpell : BaseModel
    {
        [ForeignKey("PlayerCharacter")]
        public required Guid PlayerCharacterId { get; set; }

        public required virtual PlayerCharacter PlayerCharacter { get; set; }

        [ForeignKey("Spell")]
        public required Guid SpellId { get; set; }

        public required virtual Spell Spell { get; set; }

        public required int Level { get; set; }
    }
}
