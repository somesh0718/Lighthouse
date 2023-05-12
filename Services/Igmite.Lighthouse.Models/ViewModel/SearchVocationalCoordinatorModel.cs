using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SearchVocationalCoordinatorModel : BaseSearchModel
    {
        [DataMember]
        public string NatureOfAppointmentId { get; set; }
    }
}