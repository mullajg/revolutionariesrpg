using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Entities.BaseClasses
{
    public class BaseModel : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
