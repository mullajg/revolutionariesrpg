using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class Feat : BaseModel
    {
        public Feat()
        {
            FeatPrerequisites = new List<FeatPrerequisite>();
        }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public virtual List<FeatPrerequisite> FeatPrerequisites { get; set; }
    }
}
