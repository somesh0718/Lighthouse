using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardSchoolVisitByMonthModel
    {
        [Key]
        public int Id { get; set; }

        public string ReportMonth { get; set; }

        public int? TotalVC { get; set; }

        public int? TotalSchools { get; set; }

        public int? SchoolVisited { get; set; }

        public int? SchoolNotVisited { get { return (this.TotalSchools - this.SchoolVisited); } }

        public int? NoOfVisitedSchools { get; set; }
    }
}