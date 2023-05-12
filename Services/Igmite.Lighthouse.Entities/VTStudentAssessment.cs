using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTStudentAssessments")]
    public partial class VTStudentAssessment : BaseEntity
    {
        public VTStudentAssessment()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTStudentAssessmentId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTStudent Assessment Id", Description = "VTStudent Assessment Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTStudentAssessmentId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTClassId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTClass Id", Description = "VTClass Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTClassId { get; set; }

        [DataMember]
        [Column("TestimonialType", Order = 3)]
        [Display(Name = "Testimonial Type", Description = "Testimonial Type", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TestimonialType { get; set; }

        [DataMember]
        [Column("StudentName", Order = 4)]
        [Display(Name = "Student Name", Description = "Student Name", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StudentName { get; set; }

        [DataMember]
        [Column("StudentGender", Order = 5)]
        [Display(Name = "Student Gender", Description = "Student Gender", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(10, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string StudentGender { get; set; }

        [DataMember]
        [Column("StudentPhoto", Order = 6)]
        [Display(Name = "Student Photo", Description = "Student Photo", Order = 6)]
        public virtual string StudentPhoto { get; set; }

        [DataMember]
        [Column("OJTCompany", Order = 7)]
        [Display(Name = "OJTCompany", Description = "OJTCompany", Order = 7)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OJTCompany { get; set; }

        [DataMember]
        [Column("OJTCompanyAddress", Order = 8)]
        [Display(Name = "OJTCompany Address", Description = "OJTCompany Address", Order = 8)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OJTCompanyAddress { get; set; }

        [DataMember]
        [Column("OJTFieldSuperName", Order = 9)]
        [Display(Name = "OJTField Super Name", Description = "OJTField Super Name", Order = 9)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OJTFieldSuperName { get; set; }

        [DataMember]
        [Column("OJTFieldSuperMobile", Order = 10)]
        [Display(Name = "OJTField Super Mobile", Description = "OJTField Super Mobile", Order = 10)]
        [RegularExpression(EntityConstants.RegxMobilePattern, ErrorMessage = EntityConstants.MobileErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OJTFieldSuperMobile { get; set; }

        [DataMember]
        [Column("OJTFieldSuperEmail", Order = 11)]
        [Display(Name = "OJTField Super Email", Description = "OJTField Super Email", Order = 11)]
        [RegularExpression(EntityConstants.RegxEmailPattern, ErrorMessage = EntityConstants.EmailErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OJTFieldSuperEmail { get; set; }

        [DataMember]
        [Column("GroupPhoto", Order = 12)]
        [Display(Name = "Group Photo", Description = "Group Photo", Order = 12)]
        public virtual string GroupPhoto { get; set; }

        [DataMember]
        [Column("TestimonialTitle", Order = 13)]
        [Display(Name = "Testimonial Title", Description = "Testimonial Title", Order = 13)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TestimonialTitle { get; set; }

        [DataMember]
        [Column("TestimonialDetails", Order = 14)]
        [Display(Name = "Testimonial Details", Description = "Testimonial Details", Order = 14)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TestimonialDetails { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTClasses", Description = "VTClasses")]
        //public virtual VTClass VTClass { get; set; }

        #endregion Public Properties
    }
}