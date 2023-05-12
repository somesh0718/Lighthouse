using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareSchoolModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? ApprovedSchools { get; set; }

        public int? JobRoleUnits { get; set; }

        public int? Class09 { get; set; }

        public int? Class10 { get; set; }

        public int? Class11 { get; set; }

        public int? Class12 { get; set; }

        public int? StudentMale { get; set; }

        public int? StudentFemale { get; set; }
    }
}