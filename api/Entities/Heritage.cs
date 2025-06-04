using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class Heritage : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
