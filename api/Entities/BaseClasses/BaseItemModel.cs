namespace revolutionariesrpg.api.Entities.BaseClasses
{
    public class BaseItemModel : BaseModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? Cost { get; set; }
        public int? Level { get; set; }

    }
}
