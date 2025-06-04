using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class AttributeEffect : BaseModel
    {
        [ForeignKey("Attribute")]
        public required Guid AttributeId { get; set; }

        public required string Effect { get; set; }
    }
}
