using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igmite.Lighthouse.Mappers
{
    public static class BroadcastMessageMapper
    {
        public static BroadcastMessageModel ToModel(this BroadcastMessage broadcastMessage)
        {
            if (broadcastMessage == null)
                return null;

            BroadcastMessageModel broadcastMessageModel = new BroadcastMessageModel
            {
                BroadcastMessageId = broadcastMessage.BroadcastMessageId,
                MessageText = broadcastMessage.MessageText,
                FromDate = broadcastMessage.FromDate,
                ToDate = broadcastMessage.ToDate,
                ApplicableFor = broadcastMessage.ApplicableFor,
                CreatedBy = broadcastMessage.CreatedBy,
                CreatedOn = broadcastMessage.CreatedOn,
                UpdatedBy = broadcastMessage.UpdatedBy,
                UpdatedOn = broadcastMessage.UpdatedOn,
                IsActive = broadcastMessage.IsActive
            };

            return broadcastMessageModel;
        }

        public static BroadcastMessage FromModel(this BroadcastMessageModel broadcastMessageModel, BroadcastMessage broadcastMessage)
        {
            broadcastMessage.BroadcastMessageId = broadcastMessageModel.BroadcastMessageId;
            broadcastMessage.MessageText = broadcastMessageModel.MessageText;
            broadcastMessage.FromDate = broadcastMessageModel.FromDate;
            broadcastMessage.ToDate = broadcastMessageModel.ToDate;
            broadcastMessage.ApplicableFor = broadcastMessageModel.ApplicableFor;
            broadcastMessage.IsActive = broadcastMessageModel.IsActive;
            broadcastMessage.RequestType = broadcastMessageModel.RequestType;
            broadcastMessage.SetAuditValues(broadcastMessageModel.RequestType);

            return broadcastMessage;
        }
    }
}
