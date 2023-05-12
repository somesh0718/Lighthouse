using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("VTPSectorJobRoles")]
    public partial class VTPSectorJobRole : BaseEntity
    {
        public VTPSectorJobRole()
        {
            //this.SchoolVTPSectors = new List<SchoolVTPSector>();

            //this.DeletedSchoolIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("VTPSectorJobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "VTPSector Job Role Id", Description = "VTPSector Job Role Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTPSectorJobRoleId { get; set; }

        // Foreign key
        [DataMember]
        [Column("VTPId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "VTPId", Description = "VTPId", Order = 2)]
        public virtual Guid? VTPId { get; set; }

        // Foreign key
        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 3)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 3)]
        public virtual Guid? SectorId { get; set; }

        // Foreign key
        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "Job Role Id", Description = "Job Role Id", Order = 4)]
        public virtual Guid? JobRoleId { get; set; }

        [DataMember]
        [Column("VTPSectorJobRoleName", Order = 5)]
        [Display(Name = "VTP Sector JobRole Name", Description = "VTP Sector JobRole Name", Order = 5)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string VTPSectorJobRoleName { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "School VTPSectors", Description = "School VTPSectors")]
        //public virtual IList<SchoolVTPSector> SchoolVTPSectors { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Job Roles", Description = "Job Roles")]
        //public virtual JobRole Role { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Sectors", Description = "Sectors")]
        //public virtual Sector Sector { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Vocational Training Providers", Description = "Vocational Training Providers")]
        //public virtual VocationalTrainingProvider VocationalTrainingProvider { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedSchoolIds { get; set; }

        #endregion Public Properties
    }
}