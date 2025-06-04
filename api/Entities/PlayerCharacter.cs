using System.ComponentModel.DataAnnotations.Schema;
using revolutionariesrpg.api.Entities.BaseClasses;

namespace revolutionariesrpg.api.Entities
{
    public class PlayerCharacter : BaseModel
    {
        public PlayerCharacter()
        {
            PlayerCharacterActions = new List<PlayerCharacterAction>();
            PlayerCharacterAttributes = new List<PlayerCharacterAttribute>();
            PlayerCharacterEquipment = new List<PlayerCharacterEquipment>();
            PlayerCharacterFeats = new List<PlayerCharacterFeat>();
            PlayerCharacterLanguages = new List<PlayerCharacterLanguage>();
            PlayerCharacterSkills = new List<PlayerCharacterSkill>();
            PlayerCharacterSpells = new List<PlayerCharacterSpell>();
            PlayerCharacterWeapons = new List<PlayerCharacterWeapon>();
        }
        public required string Name { get; set; }
        [ForeignKey("Heritage")]
        public required Guid HeritageId { get; set; }
        public required virtual Heritage Heritage { get; set; }
        [ForeignKey("Class")]
        public required Guid ClassId { get; set; }
        public required virtual Class Class { get; set; }
        public required int Cash { get; set; }
        public required int CurrentHealth { get; set; }
        public required int MaxHealth { get; set; }
        public required int DestinyPoints { get; set; }
        public required int MagicPoints { get; set; }
        public required int Speed { get; set; }
        public required int DashSpeed { get; set; }
        public required int Reflex { get; set; }
        public required int FortDefense { get; set; }
        public required int BaseAttack { get; set; }
        public required int Will { get; set; }
        public virtual List<PlayerCharacterAction> PlayerCharacterActions { get; set; }
        public virtual List<PlayerCharacterAttribute> PlayerCharacterAttributes { get; set; }
        public virtual List<PlayerCharacterEquipment> PlayerCharacterEquipment { get; set; }
        public virtual List<PlayerCharacterFeat> PlayerCharacterFeats { get; set; }
        public virtual List<PlayerCharacterLanguage> PlayerCharacterLanguages { get; set; }
        public virtual List<PlayerCharacterSkill> PlayerCharacterSkills { get; set; }
        public virtual List<PlayerCharacterSpell> PlayerCharacterSpells { get; set; }
        public virtual List<PlayerCharacterWeapon> PlayerCharacterWeapons { get; set; }
    }
}
