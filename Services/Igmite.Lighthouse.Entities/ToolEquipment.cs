using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("ToolEquipments")]
    public partial class ToolEquipment : BaseEntity
    {
        public ToolEquipment()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("ToolEquipmentId", TypeName = "UNIQUEIDENTIFIER", Order = 1)]
        [Display(Name = "Tool Equipment Id", Description = "Tool Equipment Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid ToolEquipmentId { get; set; }

        [DataMember]
        [Column("DivisionId", TypeName = "UNIQUEIDENTIFIER", Order = 2)]
        [Display(Name = "DivisionId", Description = "Division Id", Order = 2)]
        public virtual Guid? DivisionId { get; set; }

        [DataMember]
        [Column("DistrictCode", Order = 3)]
        [Display(Name = "District Code", Description = "District Code", Order = 3)]
        public virtual string DistrictCode { get; set; }

        [DataMember]
        [Column("SchoolId", TypeName = "UNIQUEIDENTIFIER", Order = 4)]
        [Display(Name = "SchoolId", Description = "School Id", Order = 4)]
        public virtual Guid? SchoolId { get; set; }

        [DataMember]
        [Column("VTPId", TypeName = "UNIQUEIDENTIFIER", Order = 5)]
        [Display(Name = "VTPId", Description = "VTP Id", Order = 5)]
        public virtual Guid? VTPId { get; set; }

        [DataMember]
        [Column("VCId", TypeName = "UNIQUEIDENTIFIER", Order = 6)]
        [Display(Name = "VCId", Description = "VC Id", Order = 6)]
        public virtual Guid? VCId { get; set; }

        [DataMember]
        [Column("VTId", TypeName = "UNIQUEIDENTIFIER", Order = 7)]
        [Display(Name = "VT Id", Description = "VT Id", Order = 7)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid VTId { get; set; }

        [DataMember]
        [Column("AcademicYearId", TypeName = "UNIQUEIDENTIFIER", Order = 8)]
        [Display(Name = "Academic Year Id", Description = "Academic Year Id", Order = 8)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        [DataMember]
        [Column("SectorId", TypeName = "UNIQUEIDENTIFIER", Order = 9)]
        [Display(Name = "Sector Id", Description = "Sector Id", Order = 9)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid SectorId { get; set; }

        [DataMember]
        [Column("JobRoleId", TypeName = "UNIQUEIDENTIFIER", Order = 10)]
        [Display(Name = "Job Role Id", Description = "Job Role Id", Order = 10)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid JobRoleId { get; set; }

        [DataMember]
        [Column("ReceiptDate", TypeName = "DATE", Order = 11)]
        [Display(Name = "Receipt Date", Description = "Receipt Date", Order = 11)]
        public virtual DateTime? ReceiptDate { get; set; }

        [DataMember]
        [Column("TEReceiveStatus", Order = 12)]
        [Display(Name = "TE Receive Status", Description = "TE Receive Status", Order = 12)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TEReceiveStatus { get; set; }

        [DataMember]
        [Column("TEStatus", Order = 13)]
        [Display(Name = "TE Status", Description = "TE Status", Order = 13)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TEStatus { get; set; }

        [DataMember]
        [Column("RMStatus", Order = 14)]
        [Display(Name = "RM Status", Description = "RM Status", Order = 14)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RMStatus { get; set; }

        [DataMember]
        [Column("RMFundStatus", Order = 15)]
        [Display(Name = "RM Fund Status", Description = "RM Fund Status", Order = 15)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RMFundStatus { get; set; }

        [DataMember]
        [Column("Details", Order = 16)]
        [Display(Name = "Details", Description = "Details", Order = 16)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Details { get; set; }

        [DataMember]
        [Column("OATEStatus", Order = 17)]
        [Display(Name = "OATEStatus", Description = "Overall Tools & Equipment Status", Order = 17)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OATEStatus { get; set; }

        [DataMember]
        [Column("OFTEStatus", Order = 18)]
        [Display(Name = "OFTEStatus", Description = "Overall functionality status of tools & equipments", Order = 18)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string OFTEStatus { get; set; }

        [DataMember]
        [Column("Reason", Order = 19)]
        [Display(Name = "Reason", Description = "Major reason for patrial availability/functionality of tools", Order = 19)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Reason { get; set; }

        [DataMember]
        [Column("IsSelected", Order = 20)]
        [Display(Name = "IsSelected", Description = "If Selected Not Available", Order = 20)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsSelected { get; set; }

        [DataMember]
        [Column("IsSpecify", Order = 21)]
        [Display(Name = "IsSpecify", Description = "Please Specify", Order = 21)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsSpecify { get; set; }

        [DataMember]
        [Column("RFNReceiveStatus", Order = 22)]
        [Display(Name = "RFNReceiveStatus", Description = "Reason for Not Receiving Status", Order = 22)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RFNReceiveStatus { get; set; }

        [DataMember]
        [Column("IsCommunicated", Order = 23)]
        [Display(Name = "IsCommunicated", Description = "Communicated with Samagra ?", Order = 23)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsCommunicated { get; set; }

        [DataMember]
        [Column("IsSetUpWorkShop", Order = 24)]
        [Display(Name = "IsSetUpWorkShop", Description = "Do school have dedicated room available for set up of workshop?", Order = 24)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsSetUpWorkShop { get; set; }

        [DataMember]
        [Column("RoomType", Order = 25)]
        [Display(Name = "RoomType", Description = "Room type", Order = 25)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RoomType { get; set; }

        [DataMember]
        [Column("AccommodateTools", Order = 26)]
        [Display(Name = "AccommodateTools", Description = "How do you manage to accommodate tools?", Order = 26)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string AccommodateTools { get; set; }

        [DataMember]
        [Column("RoomSize", Order = 27)]
        [Display(Name = "RoomSize", Description = "room size (in sq. ft)", Order = 27)]
        public virtual int? RoomSize { get; set; }

        [DataMember]
        [Column("IsDoorLock", Order = 28)]
        [Display(Name = "IsDoorLock", Description = "Does the room have door which can be locked?", Order = 28)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsDoorLock { get; set; }

        [DataMember]
        [Column("Flooring", Order = 29)]
        [Display(Name = "Flooring", Description = "Condition of the Flooring", Order = 29)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Flooring { get; set; }

        [DataMember]
        [Column("RoomWindows", Order = 30)]
        [Display(Name = "RoomWindows", Description = "Do the Rooms have Windows?", Order = 30)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RoomWindows { get; set; }

        [DataMember]
        [Column("TotalWindowCount", Order = 31)]
        [Display(Name = "TotalWindowCount", Description = "Total Count of window in each room", Order = 31)]
        public virtual int? TotalWindowCount { get; set; }

        [DataMember]
        [Column("IsWindowGrills", Order = 32)]
        [Display(Name = "IsWindowGrills", Description = "Do the Windows have Grills", Order = 32)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsWindowGrills { get; set; }

        [DataMember]
        [Column("IsWindowLocked", Order = 33)]
        [Display(Name = "IsWindowLocked", Description = "Can the windows be locked from inside", Order = 33)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsWindowLocked { get; set; }

        [DataMember]
        [Column("IsRoomActive", Order = 34)]
        [Display(Name = "IsRoomActive", Description = "Does the Room have active Electricity Connection", Order = 34)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string IsRoomActive { get; set; }

        [DataMember]
        [Column("REFInstalled", Order = 35)]
        [Display(Name = "REFInstalled", Description = "Do the Room have Electric fitting installed?", Order = 35)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string REFInstalled { get; set; }

        [DataMember]
        [Column("WorkingSwitchBoard", Order = 36)]
        [Display(Name = "WorkingSwitchBoard", Description = "Does the Room Have Working Switchboard", Order = 36)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string WorkingSwitchBoard { get; set; }

        [DataMember]
        [Column("PSSCount", Order = 37)]
        [Display(Name = "PSSCount", Description = "Count of Plug Socket(s) in the Switchboard", Order = 37)]
        public virtual int? PSSCount { get; set; }

        [DataMember]
        [Column("WLCount", Order = 38)]
        [Display(Name = "WLCount", Description = "Count of Working lights", Order = 38)]
        public virtual int? WLCount { get; set; }

        [DataMember]
        [Column("WFCount", Order = 39)]
        [Display(Name = "WFCount", Description = "Count of Working Fans", Order = 39)]
        public virtual int? WFCount { get; set; }

        [DataMember]
        [Column("RawMaterialRequired", Order = 40)]
        [Display(Name = "RawMaterialRequired", Description = "Raw material required to perform practicals are available?", Order = 40)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RawMaterialRequired { get; set; }

        [DataMember]
        [Column("ToolListId", TypeName = "UNIQUEIDENTIFIER", Order = 41)]
        [Display(Name = "Tool List Id", Description = "Tool List Id", Order = 41)]
        public virtual Guid? ToolListId { get; set; }

        [DataMember]
        [Column("ToolListStatus", Order = 42)]
        [Display(Name = "ToolListStatus", Description = "Tool List Status", Order = 42)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ToolListStatus { get; set; }

        [DataMember]
        [Column("TLActionNeeded1", Order = 43)]
        [Display(Name = "TLActionNeeded1", Description = "Action Needed1", Order = 43)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TLActionNeeded1 { get; set; }

        [DataMember]
        [Column("TLActionNeeded2", Order = 44)]
        [Display(Name = "TLActionNeeded2", Description = "Action Needed2", Order = 44)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TLActionNeeded2 { get; set; }

        [DataMember]
        [Column("RawMaterialId", TypeName = "UNIQUEIDENTIFIER", Order = 45)]
        [Display(Name = "Raw Materia lId", Description = "Raw Material Id", Order = 45)]
        public virtual Guid? RawMaterialId { get; set; }

        [DataMember]
        [Column("RawMaterialStatus", Order = 46)]
        [Display(Name = "RawMaterialStatus", Description = "Raw Material Status", Order = 46)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RawMaterialStatus { get; set; }

        [DataMember]
        [Column("RMLastReceivedDate", TypeName = "DATE", Order = 47)]
        [Display(Name = "RMLastReceivedDate", Description = "Date on which [Raw Material name] last received", Order = 47)]
        public virtual DateTime? RMLastReceivedDate { get; set; }

        [DataMember]
        [Column("RawMaterialAction", Order = 48)]
        [Display(Name = "RawMaterialAction", Description = "Action needed", Order = 48)]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string RawMaterialAction { get; set; }

        [DataMember]
        [Column("QuantityCount", Order = 49)]
        [Display(Name = "QuantityCount", Description = "Quantity Count", Order = 49)]
        public virtual int? QuantityCount { get; set; }

        [DataMember]
        [Column("TLFilePath", Order = 50)]
        [Display(Name = "FilePath", Description = "Upload receiving a copy of Tool List", Order = 50)]
        public string TLFilePath { get; set; }

        [DataMember]
        [Column("LabFilePath", Order = 51)]
        [Display(Name = "LabFilePath", Description = "Upload an image of Lab", Order = 51)]
        public string LabFilePath { get; set; }

        [DataMember]
        [Column("Remarks", Order = 52)]
        [Display(Name = "Remarks", Description = "Remarks", Order = 52)]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string Remarks { get; set; }

        #endregion Public Properties
    }
}