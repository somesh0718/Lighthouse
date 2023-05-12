using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class DataValueViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual string DataValueId { get; set; }

        [DataMember]
        public virtual string DataTypeName { get; set; }

        [DataMember]
        public virtual string ParentName { get; set; }

        [DataMember]
        public virtual string Code { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual int DisplayOrder { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}