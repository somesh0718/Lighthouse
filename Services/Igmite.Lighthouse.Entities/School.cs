using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Schools")]
    public partial class School : BaseEntity
    {
        public School()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "School Id", Description = "School Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolId { get; set; }

        [DataMember]
        [Column("SchoolName", Order = 2)]
        [Display(Name = "School Name", Description = "School Name", Order = 2)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolName { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolCategoryId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "School Category", Description = "School Category", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SchoolCategoryId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SchoolTypeId", Order = 4)]
        [Display(Name = "School Type", Description = "School Type", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(45, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolTypeId { get; set; }

        [DataMember]
        [Column("UDISE", Order = 5)]
        [Display(Name = "UDISE", Description = "UDISE", Order = 5)]
        [MaxLength(11, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual string Udise { get; set; }

        // Foreign key
        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 6)]
        [Display(Name = "Academic Year", Description = "Academic Year", Order = 6)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Foreign key
        [DataMember]
        [Column("PhaseId", TypeName = "UNIQUEIDENTIFIER", Order = 7)]
        [Display(Name = "Phase", Description = "Phase", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid PhaseId { get; set; }

        // Foreign key
        [DataMember]
        [Column("StateCode", Order = 8)]
        [Display(Name = "State", Description = "State", Order = 8)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StateCode { get; set; }

        // Foreign key
        [DataMember]
        [Column("DivisionId", TypeName = "UNIQUEIDENTIFIER", Order = 9)]
        [Display(Name = "Division", Description = "Division", Order = 9)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid DivisionId { get; set; }

        // Foreign key
        [DataMember]
        [Column("DistrictCode", Order = 10)]
        [Display(Name = "District", Description = "District", Order = 10)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DistrictCode { get; set; }

        // Foreign key
        [DataMember]
        [Column("BlockId", TypeName = "UNIQUEIDENTIFIER", Order = 11)]
        [Display(Name = "Block", Description = "Block", Order = 11)]
        public virtual Guid? BlockId { get; set; }

        // Foreign key
        [DataMember]
        [Column("ClusterId", TypeName = "UNIQUEIDENTIFIER", Order = 12)]
        [Display(Name = "Cluster", Description = "Cluster", Order = 12)]
        public virtual Guid? ClusterId { get; set; }

        [DataMember]
        [Column("BlockName", Order = 13)]
        [Display(Name = "Block Name", Description = "Block Name", Order = 13)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string BlockName { get; set; }

        [DataMember]
        [Column("Village", Order = 14)]
        [Display(Name = "Village", Description = "Village", Order = 14)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Village { get; set; }

        [DataMember]
        [Column("Panchayat", Order = 15)]
        [Display(Name = "Panchayat", Description = "Panchayat", Order = 15)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Panchayat { get; set; }

        [DataMember]
        [Column("Pincode", Order = 16)]
        [Display(Name = "Pincode", Description = "Pincode", Order = 16)]
        [MaxLength(6, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Pincode { get; set; }

        [DataMember]
        [Column("Demography", Order = 17)]
        [Display(Name = "Demography", Description = "Demography", Order = 17)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Demography { get; set; }

        [DataMember]
        [Column("SchoolManagementId", Order = 18)]
        [Display(Name = "School Management", Description = "School Management", Order = 18)]
        [MaxLength(20, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SchoolManagementId { get; set; }

        [DataMember]
        [Column("IsImplemented", TypeName = "BIT", Order = 19)]
        [Display(Name = "Is School Implemented?", Description = "Is School Implemented?", Order = 19)]
        public virtual bool IsImplemented { get; set; }

        #endregion Public Properties
    }
}