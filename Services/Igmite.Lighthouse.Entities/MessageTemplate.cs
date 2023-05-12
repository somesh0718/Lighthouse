using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("MessageTemplates")]
    public partial class MessageTemplate : BaseEntity
    {
        public MessageTemplate()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("MessageTemplateId", TypeName = "INT", Order = 1)]
        [Display(Name = "Message Template Id", Description = "Message Template Id", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual int MessageTemplateId { get; set; }

        [DataMember]
        [Column("TemplateName", Order = 2)]
        [Display(Name = "Template Name", Description = "Template Name", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(150, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TemplateName { get; set; }

        [DataMember]
        [Column("TemplateFlowId", Order = 3)]
        [Display(Name = "Template FlowId", Description = "Template FlowId", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(200, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string TemplateFlowId { get; set; }

        [DataMember]
        [Column("MessageTypeId", Order = 4)]
        [Display(Name = "Message TypeId", Description = "Message TypeId", Order = 4)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MessageTypeId { get; set; }

        [DataMember]
        [Column("MessageSubTypeId", Order = 5)]
        [Display(Name = "Message SubTypeId", Description = "Message SubTypeId", Order = 5)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(15, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MessageSubTypeId { get; set; }

        [DataMember, JsonIgnore]
        [Column("MessageFields", Order = 6)]
        [Display(Name = "Message Fields", Description = "Message Fields", Order = 6)]
        [MaxLength(250, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string MessageFields { get; set; }

        [DataMember]
        [Column("SMSMessage", Order = 7)]
        [Display(Name = "SMS Message", Description = "SMS Message", Order = 7)]
        [MaxLength(750, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string SMSMessage { get; set; }

        [DataMember]
        [Column("WhatsappMessage", Order = 8)]
        [Display(Name = "Whatsapp Message", Description = "Whatsapp Message", Order = 8)]
        public virtual string WhatsappMessage { get; set; }

        [DataMember]
        [Column("EmailMessage", Order = 9)]
        [Display(Name = "Email Message", Description = "Email Message", Order = 9)]
        public virtual string EmailMessage { get; set; }

        [DataMember, JsonIgnore]
        [Column("ApplicableFor", Order = 10)]
        [Display(Name = "Applicable For", Description = "Applicable For", Order = 10)]
        public virtual string ApplicableFor { get; set; }

        #endregion Public Properties
    }
}