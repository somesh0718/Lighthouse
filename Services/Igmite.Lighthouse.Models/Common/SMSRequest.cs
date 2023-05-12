using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class SMSRequest
    {
        [DataMember]
        [Display(Name = "Message Type", Description = "Message Type")]
        public virtual string MessageType { get; set; }

        [DataMember]
        [Display(Name = "Message Type", Description = "Message Type")]
        public virtual string SendTo { get; set; }
    }

    [DataContract, Serializable]
    public class OTPRequest : SMSRequest
    {
        [DataMember]
        [Display(Name = "OTP Number", Description = "OTP Number")]
        public virtual string OTPNumber { get; set; }

        [DataMember]
        [Display(Name = "OTP Date Time", Description = "OTP Date Time")]
        public virtual string OTPDateTime { get; set; }
    }

    [DataContract, Serializable]
    public class VTRequest : SMSRequest
    {
        [DataMember]
        [Display(Name = "VT Name", Description = "VT Name")]
        public virtual string VTName { get; set; }

        [DataMember]
        [Display(Name = "VT EmailId", Description = "VT EmailId")]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        [Display(Name = "Reporting Date", Description = "Reporting Date")]
        public virtual string ReportingDate { get; set; }
    }

    [DataContract, Serializable]
    public class GLRequest : SMSRequest
    {
        [DataMember]
        [Display(Name = "VT Name", Description = "VT Name")]
        public virtual string VTName { get; set; }

        [DataMember]
        [Display(Name = "VT EmailId", Description = "VT EmailId")]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        [Display(Name = "Reporting Date", Description = "Reporting Date")]
        public virtual string ReportingDate { get; set; }
    }

    [DataContract, Serializable]
    public class FVRequest : SMSRequest
    {
        [DataMember]
        [Display(Name = "VT Name", Description = "VT Name")]
        public virtual string VTName { get; set; }

        [DataMember]
        [Display(Name = "VT EmailId", Description = "VT EmailId")]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        [Display(Name = "Reporting Date", Description = "Reporting Date")]
        public virtual string ReportingDate { get; set; }
    }
}