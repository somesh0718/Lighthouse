﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract]
    public class VTSchoolSectorReport
    {
        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string SchoolAllottedYear { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string PhaseName { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string VTEmail { get; set; }

        [DataMember]
        public virtual DateTime VTDateOfJoining { get; set; }

        [DataMember]
        public virtual DateTime DateOfAllocation { get; set; }

        [DataMember]
        public virtual DateTime? DateOfRemoval { get; set; }
    }
}