using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTPMonthlyModel
    {
        public VTPMonthlyModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string Block { get; set; }

        [DataMember]
        public virtual string Village { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string SubjectCode { get; set; }

        [DataMember]
        public virtual string JobRoleStd9thWithQPCode { get; set; }

        [DataMember]
        public virtual string JobRoleStd10thWithQPCode { get; set; }

        [DataMember]
        public virtual string JobRoleStd11thWithQPCode { get; set; }

        [DataMember]
        public virtual string JobRoleStd12thWithQPCode { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VCEmailId { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string VTGender { get; set; }

        [DataMember]
        public virtual DateTime? VTDateOfJoining { get; set; }

        [DataMember]
        public virtual string HMName { get; set; }

        [DataMember]
        public virtual string HMMobile { get; set; }     

        [DataMember]
        public virtual string NoOfVisitsByVCInReportingMonth { get; set; }      

        [DataMember]
        public virtual string TotalNoOfVisitsByVCInAY { get; set; }      

        [DataMember]
        public virtual string StudentEnrollment9thGirls { get; set; }
     
        [DataMember]
        public virtual string StudentEnrollment9thBoys { get; set; }   
        
        [DataMember]
        public virtual string StudentEnrollment9thTotal { get; set; }

        [DataMember]
        public virtual string StudentEnrollment10thGirls { get; set; }

        [DataMember]
        public virtual string StudentEnrollment10thBoys { get; set; }

        [DataMember]
        public virtual string StudentEnrollment10thTotal { get; set; }

        [DataMember]
        public virtual string StudentEnrollment11thGirls { get; set; }

        [DataMember]
        public virtual string StudentEnrollment11thBoys { get; set; }

        [DataMember]
        public virtual string StudentEnrollment11thTotal { get; set; }

        [DataMember]
        public virtual string StudentEnrollment12thGirls { get; set; }

        [DataMember]
        public virtual string StudentEnrollment12thBoys { get; set; }

        [DataMember]
        public virtual string StudentEnrollment12thTotal { get; set; }

        [DataMember]
        public virtual string StudentsDropped9InReportingMonth { get; set; }

        [DataMember]
        public virtual string StudentsDropped10InReportingMonth { get; set; }

        [DataMember]
        public virtual string StudentsDropped11InReportingMonth { get; set; }

        [DataMember]
        public virtual string StudentsDropped12InReportingMonth { get; set; }

        [DataMember]
        public virtual string GL9thReportingMonth { get; set; }

        [DataMember]
        public virtual string GL9thTotalInAY { get; set; }

        [DataMember]
        public virtual string GL10thReportingMonth { get; set; }

        [DataMember]
        public virtual string GL10thTotalInAY { get; set; }

        [DataMember]
        public virtual string GL11thReportingMonth { get; set; }

        [DataMember]
        public virtual string GL11thTotalInAY { get; set; }

        [DataMember]
        public virtual string GL12thReportingMonth { get; set; }

        [DataMember]
        public virtual string GL12thTotalInAY { get; set; }

        [DataMember]
        public virtual string FV9thReportingMonth { get; set; }

        [DataMember]
        public virtual string FV9thTotalInAY { get; set; }

        [DataMember]
        public virtual string FV10thReportingMonth { get; set; }

        [DataMember]
        public virtual string FV10thTotalInAY { get; set; }

        [DataMember]
        public virtual string FV11thReportingMonth { get; set; }

        [DataMember]
        public virtual string FV11thTotalInAY { get; set; }

        [DataMember]
        public virtual string FV12thReportingMonth { get; set; }

        [DataMember]
        public virtual string FV12thTotalInAY { get; set; }

        [DataMember]
        public virtual string Student9thAttendanceInPerc { get; set; }

        [DataMember]
        public virtual string Student10thAttendanceInPerc { get; set; }

        [DataMember]
        public virtual string Student11thAttendanceInPerc { get; set; }

        [DataMember]
        public virtual string Student12thAttendanceInPerc { get; set; }

        [DataMember]
        public virtual string Remark { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}