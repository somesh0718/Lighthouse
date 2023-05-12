using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SearchSchoolModel : BaseSearchModel
    {
        [DataMember]
        public Guid? DivisionId { get; set; }

        [DataMember]
        public string DistrictId { get; set; }

        [DataMember]
        public string SchoolCategoryId { get; set; }

        [DataMember]
        public string SchoolManagementId { get; set; }

        [DataMember]
        public bool? IsImplemented { get; set; }
    }
}