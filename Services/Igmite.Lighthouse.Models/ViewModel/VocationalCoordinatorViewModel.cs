using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class VocationalCoordinatorViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid AcademicYearId { get; set; }

        [Key, DataMember]
        public virtual Guid VTPId { get; set; }

        [Key, DataMember]
        public virtual Guid VCId { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string NatureOfAppointment { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string FullName { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string EmailId { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime DateOfJoining { get; set; }

        [DataMember]
        public virtual bool IsResigned { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int TotalRows { get; set; }
    }
}