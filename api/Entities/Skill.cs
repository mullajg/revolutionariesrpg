using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities 
{ 
    public class Skill : BaseModel
    {
        public required string Name { get; set; }

        [ForeignKey("Attribute")]
        public required Guid AttributeId { get; set; }

        public virtual required Attribute Attribute { get; set; }

        public required string Description { get; set; }
    }
}
