using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VocationalTrainingProviders")]
    public partial class VocationalTrainingProvider : BaseEntity
    {
        public VocationalTrainingProvider()
        {
            this.VTPAcademicYear = new VTPAcademicYearsMap();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTPId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTPId", Description = "VTPId", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPId { get; set; }

        [DataMember]
        [Column("VTPShortName", Order = 2)]
        [Display(Name = "VTPShort Name", Description = "VTPShort Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPShortName { get; set; }

        [DataMember]
        [Column("VTPName", Order = 3)]
        [Display(Name = "VTPName", Description = "VTPName", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPName { get; set; }

        [DataMember]
        [Column("ApprovalYear", Order = 4)]
        [Display(Name = "Approval Year", Description = "Approval Year", Order = 4)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ApprovalYear { get; set; }

        [DataMember]
        [Column("CertificationNo", Order = 5)]
        [Display(Name = "Certification No", Description = "Certification No", Order = 5)]
        [MaxLength(30, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CertificationNo { get; set; }

        [DataMember]
        [Column("CertificationAgency", Order = 6)]
        [Display(Name = "Certification Agency", Description = "Certification Agency", Order = 6)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string CertificationAgency { get; set; }

        [DataMember]
        [Column("VTPMobileNo", Order = 7)]
        [Display(Name = "Mobile Number", Description = "Mobile Number", Order = 7)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPMobileNo { get; set; }

        [DataMember]
        [Column("VTPEmailId", Order = 8)]
        [Display(Name = "Certification No", Description = "Certification No", Order = 8)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPEmailId { get; set; }

        [DataMember]
        [Column("VTPAddress", Order = 9)]
        [Display(Name = "VTP Address", Description = "VTP Address", Order = 9)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPAddress { get; set; }

        [DataMember]
        [Column("PrimaryContactPerson", Order = 10)]
        [Display(Name = "Primary Contact Person", Description = "Primary Contact Person", Order = 10)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PrimaryContactPerson { get; set; }

        [DataMember]
        [Column("PrimaryMobileNumber", Order = 11)]
        [Display(Name = "Primary Mobile Number", Description = "Primary Mobile Number", Order = 11)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PrimaryMobileNumber { get; set; }

        [DataMember]
        [Column("PrimaryContactEmail", Order = 12)]
        [Display(Name = "Primary Contact Email", Description = "Primary Contact Email", Order = 12)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PrimaryContactEmail { get; set; }

        [DataMember]
        [Column("VTPStateCoordinator", Order = 13)]
        [Display(Name = "VTP State Coordinator", Description = "VTP State Coordinator", Order = 13)]
        [MaxLength(120, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPStateCoordinator { get; set; }

        [DataMember]
        [Column("VTPStateCoordinatorMobile", Order = 14)]
        [Display(Name = "VTP State Coordinator Mobile", Description = "VTP State Coordinator Mobile", Order = 14)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPStateCoordinatorMobile { get; set; }

        [DataMember]
        [Column("VTPStateCoordinatorEmail", Order = 15)]
        [Display(Name = "VTP State Coordinator Email", Description = "VTP State Coordinator Email", Order = 15)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPStateCoordinatorEmail { get; set; }

        [DataMember]
        [Column("ContractApprovalDate", TypeName = "DATETIME", Order = 16)]
        [Display(Name = "Contract Approval Date", Description = "Contract Approval Date", Order = 16)]
        public virtual DateTime? ContractApprovalDate { get; set; }

        [DataMember]
        [Column("ContractEndDate", TypeName = "DATETIME", Order = 17)]
        [Display(Name = "Contract End Date", Description = "Contract End Date", Order = 17)]
        public virtual DateTime? ContractEndDate { get; set; }

        [DataMember]
        [Column("MOUDocUpload", Order = 18)]
        [Display(Name = "MOU Document Upload", Description = "MOU Document Upload", Order = 18)]        
        public virtual string MOUDocUpload { get; set; }

        #endregion Public Properties

        [NotMapped, JsonIgnore]
        public virtual VTPAcademicYearsMap VTPAcademicYear { get; set; }
    }
}