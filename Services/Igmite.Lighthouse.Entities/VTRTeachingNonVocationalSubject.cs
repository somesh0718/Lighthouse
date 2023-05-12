using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTRTeachingNonVocationalSubjects")]
    public partial class VTRTeachingNonVocationalSubject : BaseEntityCreation
    {
        public VTRTeachingNonVocationalSubject()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTRTeachingNonVocationalSubjectId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTRTeaching Non Vocational Subject Id", Description = "VTRTeaching Non Vocational Subject Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTRTeachingNonVocationalSubjectId { get; set; }

        [DataMember]
        [Column("VTDailyReportingId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTDaily Reporting Id", Description = "VTDaily Reporting Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTDailyReportingId { get; set; }

        [DataMember]
        [Column("OtherClassTakenDetails", Order = 3)]
        [Display(Name = "Other Class Taken Details", Description = "Other Class Taken Details", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OtherClassTakenDetails { get; set; }

        [DataMember]
        [Column("OtherClassTime", TypeName = "INT", Order = 4)]
        [Display(Name = "Other Class Time", Description = "Other Class Time", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int OtherClassTime { get; set; }

        #endregion Public Properties
    }
}