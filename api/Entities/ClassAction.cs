using revolutionariesrpg.api.Entities.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace revolutionariesrpg.api.Entities
{
    public class ClassAction : BaseModel
    {
        [ForeignKey("Class")]
        public required Guid ClassId { get; set; }

        [ForeignKey("Action")]
        public required Guid ActionId { get; set; }

        public virtual required Action Action { get; set; }
    }
}
