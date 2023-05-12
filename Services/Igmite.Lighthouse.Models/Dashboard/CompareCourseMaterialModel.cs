using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareCourseMaterialModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string JobRoleTaught { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? JobRoleUnits { get; set; }

        public int? Classes { get; set; }

        public int? ClassesWithCM { get; set; }

        public int? ClassesWithoutCM { get; set; }

        public int? StatusNotReported { get; set; }
    }
}