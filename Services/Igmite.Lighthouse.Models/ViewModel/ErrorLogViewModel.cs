using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    public class ErrorLogViewModel
    {
        // Primary key
        [Key, DataMember]
        public virtual Guid ErrorLogId { get; set; }

        [DataMember]
        public virtual string ModuleName { get; set; }

        [DataMember]
        public virtual string ErrorCode { get; set; }

        [DataMember]
        public virtual int? ErrorSeverity { get; set; }

        [DataMember]
        public virtual int? ErrorState { get; set; }

        [DataMember]
        public virtual string ErrorProcedure { get; set; }

        [DataMember]
        public virtual int? ErrorLine { get; set; }

        [DataMember]
        public virtual DateTime ErrorTime { get; set; }

        [DataMember]
        public virtual string ErrorType { get; set; }

        [DataMember]
        public virtual string ErrorLocation { get; set; }

        [DataMember]
        public virtual string ErrorMessage { get; set; }

        [DataMember]
        public virtual string StackTrace { get; set; }

        [DataMember]
        public virtual string ErrorStatus { get; set; }

        [DataMember]
        public virtual bool IsResolved { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }
}
