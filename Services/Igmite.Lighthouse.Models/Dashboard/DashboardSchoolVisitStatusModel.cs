using System;
using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardSchoolVisitStatusModel
    {
        [Key]
        public int Id { get; set; }

        public string ReportMonth { get; set; }

        public int? TotalVC { get; set; }

        public int? PlacedVC { get; set; }

        public int? TotalSchools { get; set; }

        public int? SchoolVisits { get; set; }

        public int? NoOfVisitedSchools { get; set; }

        public decimal? AvgVisitsPerCordinatorPerMonth
        {
            get
            {
                return ((PlacedVC > 0 && NoOfVisitedSchools > 0) ? Math.Round(Convert.ToDecimal(NoOfVisitedSchools.Value) / PlacedVC.Value, 2) : 0);
            }
        }
    }
}