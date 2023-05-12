using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Phases")]
    public partial class Phase : BaseEntity
    {
        public Phase()
        {
            //this.Schools = new List<School>();
            //this.SectorJobRoles = new List<SectorJobRole>();

            //this.DeletedSchoolIds = new List<Guid>();
            //this.DeletedSectorJobRoleIds = new List<Guid>();
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("PhaseId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Phase Id", Description = "Phase Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid PhaseId { get; set; }

        [DataMember]
        [Column("PhaseName", Order = 2)]
        [Display(Name = "Phase Name", Description = "Phase Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string PhaseName { get; set; }

        [DataMember]
        [Column("Description", Order = 3)]
        [Display(Name = "Description", Description = "Description", Order = 3)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Schools", Description = "Schools")]
        //public virtual IList<School> Schools { get; set; }

        //// Navigation properties
        //[DataMember, JsonIgnore]
        //[Display(Name = "Sector Job Roles", Description = "Sector Job Roles")]
        //public virtual IList<SectorJobRole> SectorJobRoles { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedSchoolIds { get; set; }

        //[NotMapped, JsonIgnore]
        //public IList<Guid> DeletedSectorJobRoleIds { get; set; }

        #endregion Public Properties
    }
}
