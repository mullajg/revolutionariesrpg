using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class PlayerCharacterFeat : BaseModel
    {
        [ForeignKey("PlayerCharacter")]
        public required Guid PlayerCharacterId { get; set; }

        public required virtual PlayerCharacter PlayerCharacter { get; set; }

        [ForeignKey("Feat")]
        public required Guid FeatId { get; set; }

        public required virtual Feat Feat { get; set; }
    }
}
