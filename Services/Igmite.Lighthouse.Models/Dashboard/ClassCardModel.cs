using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class ClassCardModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Class9 { get; set; }

        public int? Class10 { get; set; }

        public int? Class11 { get; set; }

        public int? Class12 { get; set; }

        public int? Total { get; set; }
    }
}