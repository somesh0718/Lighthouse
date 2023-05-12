using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareNewEnrolmentAndDropoutStudentModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? Classes { get; set; }

        public int? NewEnrolments { get; set; }

        public int? Dropouts { get; set; }

        public int? CurrentStudents { get; set; }
    }
}