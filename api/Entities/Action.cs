using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class Action : BaseModel
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        [ForeignKey("ActionType")]
        public required Guid ActionTypeId { get; set; }

        public virtual required ActionType ActionType { get; set; }
    }
}
