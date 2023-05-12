using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("Clusters")]
    public partial class Cluster : BaseEntity
    {
        public Cluster()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("ClusterId", Order = 1)]
        [Display(Name = "Cluster Id", Description = "Cluster Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ClusterId { get; set; }

        [DataMember]
        [Column("BlockId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "Block", Description = "Block", Order = 2)]
        public virtual Guid BlockId { get; set; }

        [DataMember]
        [Column("ClusterName", Order = 3)]
        [Display(Name = "Cluster Name", Description = "Cluster Name", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClusterName { get; set; }

        [DataMember]
        [Column("Description", Order = 4)]
        [Display(Name = "Description", Description = "Description", Order = 4)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Description { get; set; }

        [DataMember, NotMapped]
        public Guid? DivisionId { get; set; }

        [DataMember, NotMapped]
        public string DistrictId { get; set; }

        #endregion Public Properties
    }
}