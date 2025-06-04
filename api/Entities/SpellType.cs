using revolutionariesrpg.api.Entities.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace revolutionariesrpg.api.Entities
{
    public class SpellType : BaseModel
    {
        public required string Type { get; set; }

        [ForeignKey("Heritage")]
        public Guid? HeritageId { get; set; }

        public virtual Heritage? Heritage { get; set; }
    }
}
