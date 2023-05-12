using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardCardModel
    {
        [Key]
        public int Id { get; set; }

        public int? ApprovedCount { get; set; }

        public int? ImplementedCount { get; set; }
    }
}