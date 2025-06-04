using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class Die : BaseModel
    {
        public required int Sides { get; set; }
    }
}
