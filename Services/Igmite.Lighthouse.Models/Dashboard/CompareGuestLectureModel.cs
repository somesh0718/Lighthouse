using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareGuestLectureModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? Classes { get; set; }

        public int? TotalGLConductedCount { get; set; }

        public decimal? AverageGLConductedPerClass { get; set; }
    }
}