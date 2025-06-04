using revolutionariesrpg.api.Entities.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace revolutionariesrpg.api.Entities
{
    public class Spell : BaseModel
    {
        public Spell()
        {
            SpellEffects = new List<SpellEffect>();
        }
        public required string Name { get; set; }
        public required string Description { get; set; }

        [ForeignKey("SpellType")]
        public required Guid SpellTypeId { get; set; }

        public virtual required SpellType SpellType { get; set; }

        public virtual List<SpellEffect> SpellEffects { get; set; }
    }
}
