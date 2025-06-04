using revolutionariesrpg.api.Entities.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace revolutionariesrpg.api.Entities
{
    public class Weapon : BaseItemModel
    {
        [ForeignKey("WeaponType")]
        public required Guid WeaponTypeId { get; set; }

        public virtual required WeaponType WeaponType { get; set; }

        public int? Range { get; set; }

        public int? AmmoCapacity { get; set; }

        public int? AltAmmoCapacity { get; set; }

        public bool? Concealable { get; set; }

        [ForeignKey("Die")]
        public Guid? DieId { get; set; }

        public virtual Die? Die { get; set; }

        public int? Damage { get; set; }

        public int? AltDamage { get; set; }

        public int? Radius { get; set; }

        public string? Note { get; set; }

    }
}
