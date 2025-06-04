using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class PlayerCharacterAttribute : BaseModel
    {
        [ForeignKey("PlayerCharacter")]
        public required Guid PlayerCharacterId { get; set; }

        public required virtual PlayerCharacter PlayerCharacter { get; set; } 

        [ForeignKey("Attribute")]
        public required Guid AttributeId { get; set; }

        public required virtual Attribute Attribute { get; set; }

        public required int Score { get; set; }
    }
}
