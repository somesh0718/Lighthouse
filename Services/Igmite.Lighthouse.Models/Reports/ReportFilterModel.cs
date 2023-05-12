using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ReportFilterModel
    {
        [DataMember]
        public virtual string UserId { get; set; }

        [DataMember]
        public virtual Guid? AcademicYearId { get; set; }

        [DataMember]
        public virtual string MonthId { get; set; }

        [DataMember]
        public virtual Guid? DivisionId { get; set; }

        [DataMember]
        public virtual string DistrictId { get; set; }

        [DataMember]
        public virtual Guid? SectorId { get; set; }

        [DataMember]
        public virtual Guid? JobRoleId { get; set; }

        [DataMember]
        public virtual Guid? VTPId { get; set; }

        [DataMember]
        public virtual Guid? VTId { get; set; }

        [DataMember]
        public virtual Guid? VCId { get; set; }

        [DataMember]
        public virtual Guid? HMId { get; set; }

        [DataMember]
        public virtual Guid? SchoolId { get; set; }

        [DataMember]
        public virtual Guid? ClassId { get; set; }

        [DataMember]
        public virtual Guid? SectionId { get; set; }

        [DataMember]
        public virtual Guid? GenderId { get; set; }

        [DataMember]
        public virtual string SchoolManagementId { get; set; }

        [DataMember]
        public virtual DateTime? ReportDate { get; set; }

        [DataMember]
        public virtual DateTime? FromDate { get; set; }

        [DataMember]
        public virtual DateTime? ToDate { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual int PageIndex { get; set; }

        [DataMember]
        public virtual int PageSize { get; set; }
    }
}