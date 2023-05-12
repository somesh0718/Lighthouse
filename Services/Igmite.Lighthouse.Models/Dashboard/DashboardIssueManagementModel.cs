using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardIssueManagementModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? High { get; set; }

        public int? Medium { get; set; }

        public int? Low { get; set; }

        public int? Open { get; set; }

        public int? Closed { get; set; }

        public int? Hold { get; set; }

        public int? Discard { get; set; }

        public int? InProgress { get; set; }

        public int? Total { get; set; }

        public string IssueRaisedBy { get; set; }

        public string IssuePriority { get; set; }
    }
}