using Igmite.Lighthouse.Entities;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VocationalTrainingProviderModel : VocationalTrainingProvider
    {
        public VocationalTrainingProviderModel()
        {
        }

        [DataMember]
        public FileUploadModel MOUDocumentFile { get; set; }

        [DataMember]
        public virtual Guid AcademicYearId { get; set; }

        [DataMember]
        public virtual DateTime DateOfJoining { get; set; }

        [DataMember]
        public virtual DateTime? DateOfResignation { get; set; }

        public class VTPTransferRequest
        {
            public Guid AcademicYearId { get; set; }
            public Guid VTPId { get; set; }
            public Guid SectorId { get; set; }            
        }
    }
}