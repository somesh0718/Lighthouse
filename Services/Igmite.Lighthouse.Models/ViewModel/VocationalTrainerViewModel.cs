using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VocationalTrainerViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AcademicYearId { get; set; }

        [Key, DataMember]
        public virtual Guid VTPId { get; set; }

        [Key, DataMember]
        public virtual Guid VCId { get; set; }

        [Key, DataMember]
        public virtual Guid VTId { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string Email { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime? DateOfBirth { get; set; }

        [DataMember]
        public virtual DateTime? DateOfJoining { get; set; }

        [DataMember]
        public virtual string AcademicQualification { get; set; }

        [DataMember]
        public virtual string ProfessionalQualification { get; set; }

        [DataMember]
        public virtual string AadhaarNumber { get; set; }

        [DataMember]
        public virtual string SocialCategory { get; set; }

        [DataMember]
        public virtual string NatureOfAppointment { get; set; }

        [DataMember]
        public virtual bool IsResigned { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}