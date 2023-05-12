using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{

    [DataContract, Serializable]
    public class MaterialListModel
    {
        public MaterialListModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string Composite { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VCEmail { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTEmail { get; set; }

        [DataMember]
        public virtual string RawMaterialRequired { get; set; }

        [DataMember]
        public virtual DateTime? ReceiptDate { get; set; }

        [DataMember]
        public virtual string MaterialName { get; set; }

        [DataMember]
        public virtual string RawMaterialStatus { get; set; }

        [DataMember]
        public virtual string Action { get; set; }

        [DataMember]
        public virtual int QuantityCount { get; set; }

    }
}
