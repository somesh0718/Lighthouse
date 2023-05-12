using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareVTVCReportingModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? TrainersPlaced { get; set; }

        public int? TotalVT { get; set; }

        public int? TrainersReporting { get; set; }

        public int? CoordinatorsPlaced { get; set; }

        public int? TotalVC { get; set; }

        public int? CoordinatorsReporting { get; set; }
    }
}