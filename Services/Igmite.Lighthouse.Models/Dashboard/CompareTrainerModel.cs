using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareTrainerModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ImplementedSchools { get; set; }

        public int? TrainersPlaced { get; set; }

        public int? TrainersReporting { get; set; }

        public decimal? TrainerAttendance { get; set; }
    }
}