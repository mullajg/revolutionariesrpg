using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class PlayerCharacterEquipment : BaseModel
    {
        [ForeignKey("PlayerCharacter")]
        public required Guid PlayerCharacterId { get; set; }

        public required virtual PlayerCharacter PlayerCharacter { get; set; }

        [ForeignKey("Equipment")]
        public required Guid EquipmentId { get; set; }

        public required virtual Equipment Equipment { get; set; }
    }
}
