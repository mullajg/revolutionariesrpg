using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class PlayerCharacterSkill : BaseModel
    {
        [ForeignKey("PlayerCharacter")]
        public required Guid PlayerCharacterId { get; set; }

        public required virtual PlayerCharacter PlayerCharacter { get; set; }

        [ForeignKey("Skill")]
        public required Guid SkillId { get; set; }

        public required virtual Skill Skill { get; set; }

        public required int Bonus { get; set; }
    }
}
