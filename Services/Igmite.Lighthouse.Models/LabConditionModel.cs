using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class LabConditionModel
    {
        public LabConditionModel()
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
        public virtual string TEReceiveStatus { get; set; }

        [DataMember]
        public virtual DateTime? ReceiptDate { get; set; }

        [DataMember]
        public virtual string OATEStatus { get; set; }

        [DataMember]
        public virtual string OFTEStatus { get; set; }

        [DataMember]
        public virtual string Reason { get; set; }

        [DataMember]
        public virtual string IsSelected { get; set; }

        [DataMember]
        public virtual string IsSpecify { get; set; }

        [DataMember]
        public virtual string RFNReceiveStatus { get; set; }

        [DataMember]
        public virtual string IsCommunicated { get; set; }

        [DataMember]
        public virtual string IsSetUpWorkShop { get; set; }

        [DataMember]
        public virtual string RoomType { get; set; }

        [DataMember]
        public virtual string AccommodateTools { get; set; }

        [DataMember]
        public virtual int RoomSize { get; set; }

        [DataMember]
        public virtual string IsDoorLock { get; set; }

        [DataMember]
        public virtual string Flooring { get; set; }

        [DataMember]
        public virtual string RoomWindows { get; set; }

        [DataMember]
        public virtual int TotalWindowCount { get; set; }

        [DataMember]
        public virtual string IsWindowGrills { get; set; }

        [DataMember]
        public virtual string IsWindowLocked { get; set; }

        [DataMember]
        public virtual string IsRoomActive { get; set; }

        [DataMember]
        public virtual string REFInstalled { get; set; }

        [DataMember]
        public virtual string WorkingSwitchBoard { get; set; }

        [DataMember]
        public virtual int PSSCount { get; set; }

        [DataMember]
        public virtual int WLCount { get; set; }

        [DataMember]
        public virtual int WFCount { get; set; }

        [DataMember]
        public virtual string RoomDamaged { get; set; }

        [DataMember]
        public virtual string RawMaterialRequired { get; set; }

        [DataMember]
        public virtual string ImgToolList { get; set; }

        [DataMember]
        public virtual string ImgLab { get; set; }

        [DataMember]
        public virtual string Remark { get; set; }

    }
}
