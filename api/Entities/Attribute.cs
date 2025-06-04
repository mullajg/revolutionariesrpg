using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class Attribute : BaseModel
    {
        public Attribute()
        {
            AttributeEffects = new List<AttributeEffect>();
        }
        public required string Name { get; set; }
        public required string ShortName { get; set; }
        public string? Notes { get; set; }
        public virtual List<AttributeEffect> AttributeEffects { get; set; }
    }
}
