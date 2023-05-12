using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareStudentModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? Classes { get; set; }

        public int? EnrollmentStudents { get; set; }

        public int? StudentAttendance { get; set; }
    }
}