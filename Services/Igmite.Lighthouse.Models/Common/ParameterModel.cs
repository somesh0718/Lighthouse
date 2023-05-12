using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class MasterDataRequest
    {
        [DataMember]
        public string DataType { get; set; }

        [DataMember]
        public string RoleId { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string ParentId { get; set; }
    }

    [DataContract, Serializable]
    public class MasterDataForAcademicRollover
    {
        [DataMember]
        public string AcademicYearId { get; set; }

        [DataMember]
        public string SchoolId { get; set; }

        [DataMember]
        public string VTId { get; set; }

        [DataMember]
        public string ClassId { get; set; }
    }

    [DataContract, Serializable]
    public class DashboardDataRequest
    {
        [DataMember]
        public string DataType { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string ParentId { get; set; }

        [DataMember]
        public string AcademicYearId { get; set; }

        [DataMember]
        public virtual string MonthId { get; set; }

        [DataMember]
        public string DivisionId { get; set; }

        [DataMember]
        public string DistrictCode { get; set; }

        [DataMember]
        public string SectorId { get; set; }

        [DataMember]
        public string JobRoleId { get; set; }

        [DataMember]
        public string VTPId { get; set; }

        [DataMember]
        public string ClassId { get; set; }

        [DataMember]
        public string SchoolManagementId { get; set; }

        [DataMember]
        public string IssueId { get; set; }

        [DataMember]
        public string IssuePriority { get; set; }

        [DataMember]
        public string RaisedBy { get; set; }
    }
}