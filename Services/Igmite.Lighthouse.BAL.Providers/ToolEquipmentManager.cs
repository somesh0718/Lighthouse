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
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the ToolEquipment entity
    /// </summary>
    public class ToolEquipmentManager : GenericManager<ToolEquipmentModel>, IToolEquipmentManager
    {
        private readonly IToolEquipmentRepository toolEquipmentRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the toolEquipment manager.
        /// </summary>
        /// <param name="toolEquipmentRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public ToolEquipmentManager(IToolEquipmentRepository _toolEquipmentRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.toolEquipmentRepository = _toolEquipmentRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of ToolEquipments
        /// </summary>
        /// <returns></returns>
        public IQueryable<ToolEquipmentModel> GetToolEquipments()
        {
            var toolEquipments = this.toolEquipmentRepository.GetToolEquipments();

            IList<ToolEquipmentModel> toolEquipmentModels = new List<ToolEquipmentModel>();
            toolEquipments.ForEach((user) => toolEquipmentModels.Add(user.ToModel()));

            return toolEquipmentModels.AsQueryable();
        }

        /// <summary>
        /// Get list of ToolEquipments by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ToolEquipmentModel> GetToolEquipmentsByName(string toolEquipmentName)
        {
            var toolEquipments = this.toolEquipmentRepository.GetToolEquipmentsByName(toolEquipmentName);

            IList<ToolEquipmentModel> toolEquipmentModels = new List<ToolEquipmentModel>();
            toolEquipments.ForEach((user) => toolEquipmentModels.Add(user.ToModel()));

            return toolEquipmentModels.AsQueryable();
        }

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        public ToolEquipmentModel GetToolEquipmentById(Guid toolEquipmentId)
        {
            ToolEquipmentModel toolEquipmentModel = null;
            ToolEquipment toolEquipment = this.toolEquipmentRepository.GetToolEquipmentById(toolEquipmentId);

            if (toolEquipment != null)
            {
                toolEquipmentModel = toolEquipment.ToModel();

                LhUserModel vptAndVCIdModel = this.commonRepository.GetVTPVCAndSchoolIdByVTId(toolEquipment.VTId);
                if (vptAndVCIdModel != null)
                {
                    toolEquipmentModel.VTPId = vptAndVCIdModel.VTPId;
                    toolEquipmentModel.VCId = vptAndVCIdModel.VCId;
                    toolEquipmentModel.SchoolId = vptAndVCIdModel.SchoolId;
                }

                toolEquipmentModel.RoomDamaged = this.toolEquipmentRepository.GetRoomDamagedById(toolEquipmentId);
                toolEquipmentModel.TEMaterialList = this.toolEquipmentRepository.GetMaterialListById(toolEquipmentId);
                toolEquipmentModel.TEToolList = this.toolEquipmentRepository.GetToolListById(toolEquipmentId);
            }

            return toolEquipmentModel;
        }

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId using async
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        public async Task<ToolEquipmentModel> GetToolEquipmentByIdAsync(Guid toolEquipmentId)
        {
            var toolEquipment = await this.toolEquipmentRepository.GetToolEquipmentByIdAsync(toolEquipmentId);

            return (toolEquipment != null) ? toolEquipment.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update ToolEquipment entity
        /// </summary>
        /// <param name="toolEquipmentModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateToolEquipmentDetails(ToolEquipmentModel toolEquipmentModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                ToolEquipment toolEquipment = null;

                //Validate model data
                toolEquipmentModel = toolEquipmentModel.GetModelValidationErrors<ToolEquipmentModel>();

                if (toolEquipmentModel.ErrorMessages.Count > 0)
                {
                    response.Errors = toolEquipmentModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (toolEquipmentModel.RequestType == RequestType.Edit)
                {
                    toolEquipment = this.toolEquipmentRepository.GetToolEquipmentById(toolEquipmentModel.ToolEquipmentId);
                }
                else
                {
                    toolEquipment = new ToolEquipment();
                    toolEquipment.ToolEquipmentId = Guid.NewGuid();
                }

                if (toolEquipmentModel.ErrorMessages.Count == 0 && toolEquipmentModel.RequestType == RequestType.New)
                {
                    bool isToolEquipmentExists = this.toolEquipmentRepository.CheckToolEquipmentExistByName(toolEquipmentModel);

                    if (isToolEquipmentExists)
                    {
                        response.Errors.Add(Constants.ExistMessage);
                    }

                    bool isAssignVTSchoolExists = this.toolEquipmentRepository.CheckVTSchoolSectorExistByVTId(toolEquipmentModel);
                    if (!isAssignVTSchoolExists)
                    {
                        response.Errors.Add("VT School Sector is not assign for this VT");
                    }
                }

                if (toolEquipmentModel.TLPhotoFile != null)
                {
                    toolEquipmentModel.TLPhotoFile.ContentId = toolEquipmentModel.ToolEquipmentId;
                    var tLFile = UtilityManager.UploadFileInPostByMobile(toolEquipmentModel.TLPhotoFile);

                    toolEquipmentModel.TLFilePath = tLFile.FilePath;

                    if (tLFile.Exception != null)
                        Logging.ErrorManager.Instance.WriteErrorLogsInFile(tLFile.Exception);
                }

                if (toolEquipmentModel.LabPhotoFile != null)
                {
                    toolEquipmentModel.LabPhotoFile.ContentId = toolEquipmentModel.ToolEquipmentId;
                    var labFile = UtilityManager.UploadFileInPostByMobile(toolEquipmentModel.LabPhotoFile);

                    toolEquipmentModel.LabFilePath = labFile.FilePath;

                    if (labFile.Exception != null)
                        Logging.ErrorManager.Instance.WriteErrorLogsInFile(labFile.Exception);
                }

                if (response.Errors.Count == 0)
                {
                    toolEquipment.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    toolEquipment = toolEquipmentModel.FromModel(toolEquipment);

                    //Save Or Update toolEquipment details
                    bool isSaved = this.toolEquipmentRepository.SaveOrUpdateToolEquipmentDetails(toolEquipment, toolEquipmentModel);

                    response.Result = isSaved ? "Success" : "Failed";
                }
                else
                {
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateToolEquipmentDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid toolEquipmentId)
        {
            return this.toolEquipmentRepository.DeleteById(toolEquipmentId);
        }

        /// <summary>
        /// Check duplicate ToolEquipment by Name
        /// </summary>
        /// <param name="toolEquipmentModel"></param>
        /// <returns></returns>
        public bool CheckToolEquipmentExistByName(ToolEquipmentModel toolEquipmentModel)
        {
            return this.toolEquipmentRepository.CheckToolEquipmentExistByName(toolEquipmentModel);
        }

        /// <summary>}
        /// List of ToolEquipment with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ToolEquipmentViewModel> GetToolEquipmentsByCriteria(SearchToolEquipmentModel searchModel)
        {
            return this.toolEquipmentRepository.GetToolEquipmentsByCriteria(searchModel);
        }

        /// <summary>
        /// Get list of TEAndRMList
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEAndRMListModel> GetTEAndRMList()
        {
            var tEAndRMLists = this.toolEquipmentRepository.GetTEAndRMList();

            IList<TEAndRMListModel> tEAndRMListModels = new List<TEAndRMListModel>();
            tEAndRMLists.ForEach((user) => tEAndRMListModels.Add(user.ToModel()));

            return tEAndRMListModels.AsQueryable();
        }
    }
}