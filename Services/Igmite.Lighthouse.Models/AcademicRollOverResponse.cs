using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class AcademicRollOverResponse
    {
        [Key]
        public int DataTypeId { get; set; }

        public string Result { get; set; }

        public bool success { get; set; }
    }
}