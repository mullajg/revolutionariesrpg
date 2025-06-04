using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class PlayerCharacterLanguage : BaseModel
    {
        [ForeignKey("PlayerCharacter")]
        public required Guid PlayerCharacterId { get; set; }

        public required virtual PlayerCharacter PlayerCharacter { get; set; }

        [ForeignKey("Language")]
        public required Guid LanguageId { get; set; }

        public required virtual Language Language { get; set; }
    }
}
