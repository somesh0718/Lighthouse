using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class MessageTemplateMapper
    {
        public static MessageTemplateModel ToModel(this MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                return null;

            MessageTemplateModel messageTemplateModel = new MessageTemplateModel
            {
                MessageTemplateId = messageTemplate.MessageTemplateId,
                TemplateName = messageTemplate.TemplateName,
                TemplateFlowId = messageTemplate.TemplateFlowId,
                MessageTypeId = messageTemplate.MessageTypeId,
                MessageSubTypeId = messageTemplate.MessageSubTypeId,
                SMSMessage = messageTemplate.SMSMessage,
                WhatsappMessage = messageTemplate.WhatsappMessage,
                EmailMessage = messageTemplate.EmailMessage,
                CreatedBy = messageTemplate.CreatedBy,
                CreatedOn = messageTemplate.CreatedOn,
                UpdatedBy = messageTemplate.UpdatedBy,
                UpdatedOn = messageTemplate.UpdatedOn,
                IsActive = messageTemplate.IsActive
            };

            messageTemplateModel.MessageFieldIds = messageTemplate.MessageFields.Split(',').ToList();
            messageTemplateModel.ApplicableForIds = messageTemplate.ApplicableFor.Split(',').ToList();

            return messageTemplateModel;
        }

        public static MessageTemplate FromModel(this MessageTemplateModel messageTemplateModel, MessageTemplate messageTemplate)
        {
            messageTemplate.TemplateName = messageTemplateModel.TemplateName;
            messageTemplate.TemplateFlowId = messageTemplateModel.TemplateFlowId;
            messageTemplate.MessageTypeId = messageTemplateModel.MessageTypeId;
            messageTemplate.MessageSubTypeId = messageTemplateModel.MessageSubTypeId;
            messageTemplate.MessageFields = string.Join(",", messageTemplateModel.MessageFieldIds);
            messageTemplate.SMSMessage = messageTemplateModel.SMSMessage;
            messageTemplate.WhatsappMessage = messageTemplateModel.WhatsappMessage;
            messageTemplate.EmailMessage = messageTemplateModel.EmailMessage;
            messageTemplate.ApplicableFor = string.Join(",", messageTemplateModel.ApplicableForIds);
            messageTemplate.IsActive = messageTemplateModel.IsActive;
            messageTemplate.RequestType = messageTemplateModel.RequestType;
            messageTemplate.SetAuditValues(messageTemplateModel.RequestType);

            return messageTemplate;
        }
    }
}