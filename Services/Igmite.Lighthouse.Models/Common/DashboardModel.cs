using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models.Common
{
    public class DashboardModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public decimal Percentage { get; set; }
    }
}