using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardSchoolModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Count { get; set; }
    }
}