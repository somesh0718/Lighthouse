using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class JobRoleUnitCardModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? JobRoleUnits { get; set; }
    }
}