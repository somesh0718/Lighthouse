using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareFieldVisitModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? Classes { get; set; }

        public int? TotalFVConductedCount { get; set; }

        public decimal? AverageFVConductedPerClass { get; set; }
    }
}