using Microsoft.EntityFrameworkCore;
using revolutionariesrpg.api.Entities;

namespace revolutionariesrpg.api
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Entities.Action> Actions { get; set; }
		public DbSet<ActionType> ActionTypes { get; set; }
		public DbSet<Entities.Attribute> Attributes { get; set; }
		public DbSet<AttributeEffect> AttributeEffects { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<ClassAction> ClassActions { get; set; }
		public DbSet<ClassFeat> ClassFeats { get; set; }
		public DbSet<ClassItem> ClassItems { get; set; }
		public DbSet<Die> Dice { get; set; }
		public DbSet<Equipment> Equipment { get; set; }
		public DbSet<Feat> Feats { get; set; }
		public DbSet<FeatPrerequisite> FeatPrerequisites { get; set; }
		public DbSet<FeatPrerequisiteType> FeatPrerequisiteTypes { get; set; }
		public DbSet<Heritage> Heritages { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<PlayerCharacter> PlayerCharacters { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<Spell> Spells { get; set; }
		public DbSet<SpellEffect> SpellEffects { get; set; }
		public DbSet<SpellType> SpellTypes { get; set; }
		public DbSet<Weapon> Weapons { get; set; }
		public DbSet<WeaponType> WeaponTypes { get; set; }
	}
}