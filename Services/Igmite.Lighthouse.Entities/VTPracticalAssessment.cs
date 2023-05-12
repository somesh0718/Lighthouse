using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTPracticalAssessments")]
    public partial class VTPracticalAssessment : BaseEntity
    {
        public VTPracticalAssessment()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTPracticalAssessmentId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTPractical Assessment Id", Description = "VTPractical Assessment Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPracticalAssessmentId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTClass Id", Description = "VTClass Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassId { get; set; }

        [DataMember]
        [Column("AssessmentDate", TypeName = "DATETIME", Order = 3)]
        [Display(Name = "Assessment Date", Description = "Assessment Date", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime AssessmentDate { get; set; }

        [DataMember]
        [Column("BoysPresent", TypeName = "INT", Order = 4)]
        [Display(Name = "Boys Present", Description = "Boys Present", Order = 4)]
        public virtual int? BoysPresent { get; set; }

        [DataMember]
        [Column("GirlsPresent", TypeName = "INT", Order = 5)]
        [Display(Name = "Girls Present", Description = "Girls Present", Order = 5)]
        public virtual int? GirlsPresent { get; set; }

        [DataMember]
        [Column("AssessorName", Order = 6)]
        [Display(Name = "Assessor Name", Description = "Assessor Name", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorName { get; set; }

        [DataMember]
        [Column("AssessorMobile", Order = 7)]
        [Display(Name = "Assessor Mobile", Description = "Assessor Mobile", Order = 7)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorMobile { get; set; }

        [DataMember]
        [Column("AssessorEmail", Order = 8)]
        [Display(Name = "Assessor Email", Description = "Assessor Email", Order = 8)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorEmail { get; set; }

        [DataMember]
        [Column("AssessorQualification", Order = 9)]
        [Display(Name = "Assessor Qualification", Description = "Assessor Qualification", Order = 9)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorQualification { get; set; }

        [DataMember]
        [Column("AssessorTimeReached", TypeName = "DATETIME", Order = 10)]
        [Display(Name = "Assessor Time Reached", Description = "Assessor Time Reached", Order = 10)]
        public virtual DateTime? AssessorTimeReached { get; set; }

        [DataMember]
        [Column("AssessorIdCheck", Order = 11)]
        [Display(Name = "Assessor Id Check", Description = "Assessor Id Check", Order = 11)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorIdCheck { get; set; }

        [DataMember]
        [Column("AssessorIdType", Order = 12)]
        [Display(Name = "Assessor Id Type", Description = "Assessor Id Type", Order = 12)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorIdType { get; set; }

        [DataMember]
        [Column("AssessorSSCLetter", Order = 13)]
        [Display(Name = "Assessor SSCLetter", Description = "Assessor SSCLetter", Order = 13)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorSSCLetter { get; set; }

        [DataMember]
        [Column("AssessorBehaviour", Order = 14)]
        [Display(Name = "Assessor Behaviour", Description = "Assessor Behaviour", Order = 14)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorBehaviour { get; set; }

        [DataMember]
        [Column("AssessorDemands", Order = 15)]
        [Display(Name = "Assessor Demands", Description = "Assessor Demands", Order = 15)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorDemands { get; set; }

        [DataMember]
        [Column("AssessorBehaiourFormality", Order = 16)]
        [Display(Name = "Assessor Behaiour Formality", Description = "Assessor Behaiour Formality", Order = 16)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AssessorBehaiourFormality { get; set; }

        [DataMember]
        [Column("AssessorGroupPhoto", Order = 17)]
        [Display(Name = "Assessor Group Photo", Description = "Assessor Group Photo", Order = 17)]
        public virtual string AssessorGroupPhoto { get; set; }

        [DataMember]
        [Column("VCPMUNameVisit", Order = 18)]
        [Display(Name = "VCPMUName Visit", Description = "VCPMUName Visit", Order = 18)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VCPMUNameVisit { get; set; }

        [DataMember]
        [Column("RemarksDetails", Order = 19)]
        [Display(Name = "Remarks Details", Description = "Remarks Details", Order = 19)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RemarksDetails { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTClasses", Description = "VTClasses")]
        //public virtual VTClass VTClass { get; set; }

        #endregion Public Properties
    }
}