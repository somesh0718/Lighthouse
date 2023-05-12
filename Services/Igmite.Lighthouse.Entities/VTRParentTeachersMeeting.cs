using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRParentTeachersMeetings")]
    public partial class VTRParentTeachersMeeting : BaseEntityCreation
    {
        public VTRParentTeachersMeeting()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRParentTeachersMeetingId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRParent Teachers Meeting Id", Description = "VTRParent Teachers Meeting Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRParentTeachersMeetingId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("VocationalParentsCount", TypeName = "INT", Order = 3)]
        [Display(Name = "Vocational Parents Count", Description = "Vocational Parents Count", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int VocationalParentsCount { get; set; }

        [DataMember]
        [Column("OtherParentsCount", TypeName = "INT", Order = 4)]
        [Display(Name = "Other Parents Count", Description = "Other Parents Count", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int OtherParentsCount { get; set; }

        [DataMember]
        [Column("PTADetails", Order = 5)]
        [Display(Name = "PTADetails", Description = "PTADetails", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PTADetails { get; set; }

        #endregion Public Properties
    }
}