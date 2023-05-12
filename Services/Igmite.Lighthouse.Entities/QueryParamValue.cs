using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("LighthouseParams")]
    public partial class LighthouseParams
    {
        public LighthouseParams()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("LighthouseParamId", TypeName = "INT")]
        public virtual int LighthouseParamId { get; set; }

        [DataMember]
        [Column("Param1")]
        public virtual string Param1 { get; set; }

        [DataMember]
        [Column("Param2")]
        public virtual string Param2 { get; set; }

        [DataMember]
        [Column("Param3")]
        public virtual string Param3 { get; set; }

        [DataMember]
        [Column("Param4")]
        public virtual string Param4 { get; set; }

        [DataMember]
        [Column("Param5")]
        public virtual string Param5 { get; set; }

        [DataMember]
        [Column("CreatedBy")]
        public virtual string CreatedBy { get; set; }

        [DataMember]
        [Column("CreatedOn")]
        public virtual DateTime CreatedOn { get; set; }

        #endregion Public Properties
    }
}