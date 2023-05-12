using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the HeadMaster entity
    /// </summary>
    public class BroadcastMessageManager : GenericManager<BroadcastMessageModel>, IBroadcastMessageManager
    {
        private readonly IBroadcastMessageRepository broadcastMessageRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the BroadcastMessage manager.
        /// </summary>
        /// <param name="broadcastMessageRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public BroadcastMessageManager(IBroadcastMessageRepository _broadcastMessageRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.broadcastMessageRepository = _broadcastMessageRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of BroadcastMessages
        /// </summary>
        /// <returns></returns>
        public IQueryable<BroadcastMessageModel> GetBroadcastMessages()
        {
            var broadcastMessages = this.broadcastMessageRepository.GetBroadcastMessages();

            IList<BroadcastMessageModel> broadcastMessageModels = new List<BroadcastMessageModel>();
            broadcastMessages.ForEach((user) => broadcastMessageModels.Add(user.ToModel()));

            return broadcastMessageModels.AsQueryable();
        }

        /// <summary>
        /// Get list of BroadcastMessages by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<BroadcastMessageModel> GetBroadcastMessagesByName(string broadcastMessageName)
        {
            var broadcastMessages = this.broadcastMessageRepository.GetBroadcastMessagesByName(broadcastMessageName);

            IList<BroadcastMessageModel> broadcastMessageModels = new List<BroadcastMessageModel>();
            broadcastMessages.ForEach((user) => broadcastMessageModels.Add(user.ToModel()));

            return broadcastMessageModels.AsQueryable();
        }

        /// <summary>
        /// Get BroadcastMessage by BroadcastMessageId
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        public BroadcastMessageModel GetBroadcastMessageById(Guid broadcastMessageId)
        {
            BroadcastMessage broadcastMessage = this.broadcastMessageRepository.GetBroadcastMessageById(broadcastMessageId);

            return (broadcastMessage != null) ? broadcastMessage.ToModel() : null;
        }

        /// <summary>
        /// Get BroadcastMessage by BroadcastMessageId using async
        /// </summary>
        /// <param name="broadcastMessageId"></param>
        /// <returns></returns>
        public async Task<BroadcastMessageModel> GetBroadcastMessageByIdAsync(Guid broadcastMessageId)
        {
            var broadcastMessage = await this.broadcastMessageRepository.GetBroadcastMessageByIdAsync(broadcastMessageId);

            return (broadcastMessage != null) ? broadcastMessage.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update BroadcastMessage entity
        /// </summary>
        /// <param name="broadcastMessageModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateBroadcastMessageDetails(BroadcastMessageModel broadcastMessageModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            BroadcastMessage broadcastMessage = null;

            //Validate model data
            broadcastMessageModel = broadcastMessageModel.GetModelValidationErrors<BroadcastMessageModel>();

            if (broadcastMessageModel.ErrorMessages.Count > 0)
            {
                response.Errors = broadcastMessageModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (broadcastMessageModel.RequestType == RequestType.Edit)
            {
                broadcastMessage = this.broadcastMessageRepository.GetBroadcastMessageById(broadcastMessageModel.BroadcastMessageId);
            }
            else
            {
                broadcastMessage = new BroadcastMessage();
                broadcastMessageModel.BroadcastMessageId = Guid.NewGuid(); 
            }

            //if (broadcastMessageModel.ErrorMessages.Count == 0 && !(string.Equals(broadcastMessageModel.MessageText.ToLower(), broadcastMessage.MessageText.StringVal().ToLower()) && string.Equals(broadcastMessageModel.Udise.ToLower(), broadcastMessage.Udise.StringVal().ToLower())))
            //{
            //    string isBroadcastMessageExists = this.broadcastMessageRepository.CheckBroadcastMessageExistByName(broadcastMessageModel);

            //    if (isBroadcastMessageExists)
            //    {
            //        response.Errors.Add(Constants.ExistMessage);
            //    }
            //}

            if (response.Errors.Count == 0)
            {
                broadcastMessage.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                broadcastMessage = broadcastMessageModel.FromModel(broadcastMessage);

                //Save Or Update BroadcastMessage details
                bool isSaved = this.broadcastMessageRepository.SaveOrUpdateBroadcastMessageDetails(broadcastMessage);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by BroadcastMessageId
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid broadcastMessageId)
        {
            return this.broadcastMessageRepository.DeleteById(broadcastMessageId);
        }

        /// <summary>
        /// Check duplicate BroadcastMessage by Name
        /// </summary>
        /// <param name="BroadcastMessageModel"></param>
        /// <returns></returns>
        public string CheckBroadcastMessageExistByName(BroadcastMessageModel broadcastMessageModel)
        {
            return this.broadcastMessageRepository.CheckBroadcastMessageExistByName(broadcastMessageModel);
        }

        /// <summary>}
        /// List of BroadcastMessage with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<BroadcastMessageViewModel> GetBroadcastMessagesByCriteria(SearchBroadcastMessageModel searchModel)
        {
            return this.broadcastMessageRepository.GetBroadcastMessagesByCriteria(searchModel);
        }
    }
}
