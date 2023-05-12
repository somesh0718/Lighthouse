using Igmite.Lighthouse.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class AccountWorkLocationModel
    {
        [DataMember, Key]
        [Display(Name = "Account Work Location Id", Description = "Account Work Location Id")]
        public virtual Guid AccountWorkLocationId { get; set; }

        [DataMember]
        [Display(Name = "Account Id", Description = "Account Id")]
        public virtual Guid AccountId { get; set; }

        [DataMember]
        [Display(Name = "State Code", Description = "State Code")]
        public virtual string StateCode { get; set; }

        [DataMember]
        [Display(Name = "State Name", Description = "State Name")]
        public virtual string StateName { get; set; }

        [DataMember]
        [Display(Name = "Division Id", Description = "Division Id")]
        public virtual Guid DivisionId { get; set; }

        [DataMember]
        [Display(Name = "Division Name", Description = "Division Name")]
        public virtual string DivisionName { get; set; }

        [DataMember]
        [Display(Name = "District Id", Description = "District Id")]
        public virtual string DistrictId { get; set; }

        [DataMember]
        [Display(Name = "District Name", Description = "District Name")]
        public virtual string DistrictName { get; set; }

        [DataMember]
        [Display(Name = "Block Id", Description = "Block Id")]
        public virtual string BlockId { get; set; }

        [DataMember]
        [Display(Name = "Block Name", Description = "Block Name")]
        public virtual string BlockName { get; set; }

        [DataMember]
        [Display(Name = "Cluster Id", Description = "Cluster Id")]
        public virtual string ClusterId { get; set; }

        [DataMember]
        [Display(Name = "Cluster Name", Description = "Cluster Name")]
        public virtual string ClusterName { get; set; }

        [DataMember]
        [Display(Name = "Remarks", Description = "Remarks")]
        public virtual string Remarks { get; set; }

        [DataMember]
        [Display(Name = "Is Active?", Description = "Is Active?")]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual RequestType RequestType { get; set; }
    }
}