using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VTPracticalAssessmentViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid VTPracticalAssessmentId { get; set; }

        // Foreign key
        [DataMember]
        public virtual string ClassName { get; set; }

        [DataMember]
        public virtual DateTime AssessmentDate { get; set; }

        [DataMember]
        public virtual int? BoysPresent { get; set; }

        [DataMember]
        public virtual int? GirlsPresent { get; set; }

        [DataMember]
        public virtual string AssessorName { get; set; }

        [DataMember]
        public virtual string AssessorMobile { get; set; }

        [DataMember]
        public virtual string AssessorEmail { get; set; }

        [DataMember]
        public virtual string AssessorQualification { get; set; }

        [DataMember]
        public virtual DateTime? AssessorTimeReached { get; set; }

        [DataMember]
        public virtual string AssessorIdCheck { get; set; }

        [DataMember]
        public virtual string AssessorIdType { get; set; }

        [DataMember]
        public virtual string AssessorSSCLetter { get; set; }

        [DataMember]
        public virtual string AssessorBehaviour { get; set; }

        [DataMember]
        public virtual string AssessorDemands { get; set; }

        [DataMember]
        public virtual string AssessorBehaiourFormality { get; set; }

        [DataMember]
        public virtual string AssessorGroupPhoto { get; set; }

        [DataMember]
        public virtual string VCPMUNameVisit { get; set; }

        [DataMember]
        public virtual string RemarksDetails { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
