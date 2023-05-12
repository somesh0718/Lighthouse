using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTStudentVEResults")]
    public partial class VTStudentVEResult : BaseEntity
    {
        public VTStudentVEResult()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTStudentVEResultId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTStudent VEResult Id", Description = "VTStudent VEResult Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTStudentVEResultId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTClass Id", Description = "VTClass Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassId { get; set; }

        // Foreign key
        [DataMember]
        [Column("StudentId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Student Id", Description = "Student Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid StudentId { get; set; }

        [DataMember]
        [Column("DateIssuence", TypeName = "DATETIME", Order = 4)]
        [Display(Name = "Date Issuence", Description = "Date Issuence", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime DateIssuence { get; set; }

        [DataMember]
        [Column("ExternalMarks", TypeName = "INT", Order = 5)]
        [Display(Name = "External Marks", Description = "External Marks", Order = 5)]
        public virtual int ExternalMarks { get; set; }

        [DataMember]
        [Column("TheoryMarks", TypeName = "INT", Order = 6)]
        [Display(Name = "Theory Marks", Description = "Theory Marks", Order = 6)]
        public virtual int TheoryMarks { get; set; }

        [DataMember]
        [Column("InternalMarks", TypeName = "INT", Order = 7)]
        [Display(Name = "Internal Marks", Description = "Internal Marks", Order = 7)]
        public virtual int InternalMarks { get; set; }

        [DataMember]
        [Column("TotalMarks", TypeName = "INT", Order = 8)]
        [Display(Name = "Total Marks", Description = "Total Marks", Order = 8)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int TotalMarks { get; set; }

        [DataMember]
        [Column("Grade", Order = 9)]
        [Display(Name = "Grade", Description = "Grade", Order = 9)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Grade { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Student Classes", Description = "Student Classes")]
        //public virtual StudentClass StudentClass { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTClasses", Description = "VTClasses")]
        //public virtual VTClass VTClass { get; set; }
        #endregion Public Properties
    }
}
