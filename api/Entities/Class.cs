using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class Class : BaseModel
    {
        public Class()
        {
            ClassActions = new List<ClassAction>();
            ClassItems = new List<ClassItem>();
            ClassFeats = new List<ClassFeat>();
        }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int StartingHealthPoints { get; set; }
        public required int StartingSkillsCount { get; set; }
        public required int StartingSpeed { get; set; }
        public required int StartingDashSpeed { get; set; }
        public required int StartingSpellCount { get; set; } //TODO: Starting spell count can be x + some other thing 
        public virtual List<ClassAction> ClassActions { get; set; }
        public virtual List<ClassItem> ClassItems { get; set; }
        public virtual List<ClassFeat> ClassFeats { get; set; }
    }
}
