using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class FeatPrerequisite : BaseModel
    {
        [ForeignKey("Feat")]
        public required Guid FeatId { get; set; }

        [ForeignKey("FeatPrerequisiteType")]
        public required Guid FeatPrerequisiteTypeId { get; set; }

        public virtual required FeatPrerequisiteType FeatPrerequisiteType { get; set; }

        public required string FeatPrequisiteValue { get; set; }
    }
}
