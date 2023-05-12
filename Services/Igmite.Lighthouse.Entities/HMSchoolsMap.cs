using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("HMSchoolsMap")]
    public partial class HMSchoolsMap : BaseEntity
    {
        public HMSchoolsMap()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("HMSchoolId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "HMSchoolId", Description = "HMSchoolId")]
        public virtual Guid HMSchoolId { get; set; }


        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "AcademicYearId", Description = "AcademicYearId")]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "School", Description = "School")]
        public virtual Guid SchoolId { get; set; }

        // Foreign key
        [DataMember]
        [Column("HMId", TypeName = "UNIQUEIDENTIFIER")]
        [Display(Name = "HMId", Description = "HMId")]
        public virtual Guid HMId { get; set; }

        [DataMember]
        [Column("DateOfJoining", TypeName = "DATETIME")]
        [Display(Name = "Date Of Joining", Description = "Date Of Joining")]
        public virtual DateTime DateOfJoining { get; set; }

        [DataMember]
        [Column("DateOfResignation", TypeName = "DATETIME")]
        [Display(Name = "Date Of Resignation", Description = "Date Of Resignation")]
        public virtual DateTime? DateOfResignation { get; set; }

        #endregion Public Properties
    }
}