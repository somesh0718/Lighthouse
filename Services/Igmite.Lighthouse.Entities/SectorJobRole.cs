using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("SectorJobRoles")]
    public partial class SectorJobRole : BaseEntity
    {
        public SectorJobRole()
        {
            //this.VTPSectors = new List<VTPSector>();

            //this.DeletedVTPIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("SectorJobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Sector Job Role Id", Description = "Sector Job Role Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorJobRoleId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Job Role Id", Description = "Job Role Id", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid JobRoleId { get; set; }

        [DataMember]
        [Column("QPCode", Order = 4)]
        [Display(Name = "QP Code", Description = "QP Code", Order = 4)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        [RegularExpression(EntityConstants.RegxQPCodePattern, ErrorMessage = EntityConstants.QPCodeErrorMessage)]
        public virtual string QPCode { get; set; }

        [DataMember]
        [Column("Remarks", Order = 5)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 5)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "VTPSectors", Description = "VTPSectors")]
        //public virtual IList<VTPSector> VTPSectors { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Academic Years", Description = "Academic Years")]
        //public virtual AcademicYear AcademicYear { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Job Roles", Description = "Job Roles")]
        //public virtual JobRole Role { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Phases", Description = "Phases")]
        //public virtual Phase Phase { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Sectors", Description = "Sectors")]
        //public virtual Sector Sector { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedVTPIds { get; set; }

        #endregion Public Properties
    }
}