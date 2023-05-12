using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTPSchoolModel
    {
        public VTPSchoolModel()
        {
        }

        [Key, DataMember]
        public Guid VCSchoolSectorId { get; set; }

        [DataMember]
        public Guid SchoolVTPSectorId { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public Guid VTPId { get; set; }

        [DataMember]
        public Guid SectorId { get; set; }

        [DataMember]
        public Guid SchoolId { get; set; }

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public Guid? ToVTPId { get; set; }

        [DataMember]
        public Guid? ToVCId { get; set; }

        [DataMember]
        public DateTime? DateOfAllocation { get; set; }

        [DataMember]
        public DateTime? DateOfJoining { get; set; }

        [DataMember]
        public Guid? ToSectorId { get; set; }

        [DataMember]
        public string Remarks { get; set; }
    }

    [DataContract, Serializable]
    public class VTPSchoolTransferModel
    {
        public VTPSchoolTransferModel()
        {
        }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public List<VTPSchoolModel> VTPSchoolModels { get; set; }
    }
}