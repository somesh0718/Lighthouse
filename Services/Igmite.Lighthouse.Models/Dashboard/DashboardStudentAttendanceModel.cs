using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.Models
{
    public class DashboardStudentAttendanceModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? EnrolledBoys { get; set; }

        public int? EnrolledGirls { get; set; }

        public int? EnrolledStudents { get; set; }

        public int? AttendanceBoys { get; set; }

        public int? AttendanceGirls { get; set; }

        public int? StudentAttendances { get; set; }

        public double? PercentageAttendanceBoys { get; set; }

        public double? PercentageAttendanceGirls { get; set; }

        public double? Percentage { get; set; }
    }
}