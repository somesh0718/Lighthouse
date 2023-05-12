using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class StudentCardModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Boys { get; set; }

        public int? Girls { get; set; }

        public int? Total { get; set; }
    }
}