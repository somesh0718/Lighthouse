using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class CompareCoordinatorModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Districts { get; set; }

        public int? SchoolsCovered { get; set; }

        public int? CoordinatorsPlaced { get; set; }

        public int? CoordinatorsReporting { get; set; }

        public int? NoOfSchoolVisits { get; set; }

        public int? NoOfMeetingsHeld { get; set; }

        public int? NoOfOutreachActivities { get; set; }
    }
}