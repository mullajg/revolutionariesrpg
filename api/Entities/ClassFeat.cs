using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class ClassFeat : BaseModel
    {
        [ForeignKey("Class")]
        public required Guid ClassId { get; set; }

        [ForeignKey("Feat")]
        public required Guid FeatId { get; set; }

        public virtual required Feat Feat { get; set; }
    }
}
