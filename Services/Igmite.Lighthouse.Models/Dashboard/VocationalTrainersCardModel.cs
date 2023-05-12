using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class VocationalTrainersCardModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double? TotalVT { get; set; }

        public double? PlacedVT { get; set; }

        public double? ReportedVT { get; set; }
    }
}