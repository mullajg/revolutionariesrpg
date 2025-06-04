using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class Language : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
