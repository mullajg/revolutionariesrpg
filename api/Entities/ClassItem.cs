using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class ClassItem : BaseModel
    {
        [ForeignKey("Class")]
        public required Guid ClassId { get; set; }

        //Slot = each class has a number of equipment that you start with, this is the order
        public required int Slot { get; set; }

        //Option = some slots have a few different options to choose from
        public required int Option { get; set; }

        [ForeignKey("Weapon")]
        public Guid? WeaponId { get; set; }

        public virtual Weapon? Weapon { get; set; }

        [ForeignKey("Equipment")]
        public Guid? EquipmentId { get; set; }

        public virtual Equipment? Equipment { get; set; }
    }
}
