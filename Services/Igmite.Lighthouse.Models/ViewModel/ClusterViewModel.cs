using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ClusterViewModel
    {
        [DataMember, Key]
        public virtual Guid ClusterId { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string BlockName { get; set; }

        [DataMember]
        public virtual string ClusterName { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}