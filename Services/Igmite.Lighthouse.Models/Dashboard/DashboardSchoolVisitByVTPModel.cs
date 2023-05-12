using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardSchoolVisitByVTPModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? VisitedSchools { get; set; }
    }
}