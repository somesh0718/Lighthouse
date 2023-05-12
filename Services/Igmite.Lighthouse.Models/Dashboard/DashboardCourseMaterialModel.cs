using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardCourseMaterialModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ReportedReceived { get; set; }

        public int? NotReported { get; set; }

        public int? ReportedNotReceived { get; set; }

        public int? TotalReportedClasses { get; set; }
    }
}