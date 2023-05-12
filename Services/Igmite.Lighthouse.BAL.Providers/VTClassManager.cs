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
    /// Manager of the VTClass entity
    /// </summary>
    public class VTClassManager : GenericManager<VTClassModel>, IVTClassManager
    {
        private readonly IVTClassRepository vtClassRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtClass manager.
        /// </summary>
        /// <param name="vtClassRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VTClassManager(IVTClassRepository _vtClassRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtClassRepository = _vtClassRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTClasses
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTClassModel> GetVTClasses()
        {
            var vtClasses = this.vtClassRepository.GetVTClasses();

            IList<VTClassModel> vtClassModels = new List<VTClassModel>();
            vtClasses.ForEach((user) => vtClassModels.Add(user.ToModel()));

            return vtClassModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTClasses by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTClassModel> GetVTClassesByName(string vtClassName)
        {
            var vtClasses = this.vtClassRepository.GetVTClassesByName(vtClassName);

            IList<VTClassModel> vtClassModels = new List<VTClassModel>();
            vtClasses.ForEach((user) => vtClassModels.Add(user.ToModel()));

            return vtClassModels.AsQueryable();
        }

        /// <summary>
        /// Get VTClass by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        public VTClassModel GetVTClassById(Guid vtClassId)
        {
            VTClassModel vtClassModel = null;
            VTClass vtClass = this.vtClassRepository.GetVTClassById(vtClassId);

            if (vtClass != null)
            {
                vtClassModel = vtClass.ToModel();
                VocationalTrainer vocationalTrainer = this.commonRepository.GetVocationalTrainerById(vtClass.VTId);

                vtClassModel.VTPId = vocationalTrainer.VCTrainer.VTPId;
                vtClassModel.VCId = vocationalTrainer.VCTrainer.VCId;
                vtClassModel.SectionIds = this.vtClassRepository.GetVTClassSectionsById(vtClassId);
            }

            return vtClassModel;
        }

        /// <summary>
        /// Get VTClass by VTClassId using async
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        public async Task<VTClassModel> GetVTClassByIdAsync(Guid vtClassId)
        {
            var vtClass = await this.vtClassRepository.GetVTClassByIdAsync(vtClassId);

            return (vtClass != null) ? vtClass.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTClass entity
        /// </summary>
        /// <param name="vtClassModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTClassDetails(VTClassModel vtClassModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                VTClass vtClass = null;

                //Validate model data
                vtClassModel = vtClassModel.GetModelValidationErrors<VTClassModel>();

                if (vtClassModel.ErrorMessages.Count > 0)
                {
                    response.Errors = vtClassModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (vtClassModel.RequestType == RequestType.Edit)
                {
                    vtClass = this.vtClassRepository.GetVTClassById(vtClassModel.VTClassId);
                }
                else
                {
                    vtClass = new VTClass();
                    vtClassModel.VTClassId = Guid.NewGuid();
                }

                if (vtClassModel.ErrorMessages.Count == 0)
                {
                    if (vtClassModel.RequestType == RequestType.New)
                    {
                        VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByVTAYId(vtClassModel.VTId, vtClassModel.AcademicYearId);
                        if (vtSchoolSectors != null)
                        {
                            vtClassModel.SchoolId = vtSchoolSectors.SchoolId;
                            vtClass.SchoolId = vtSchoolSectors.SchoolId;
                        }
                        else
                        {
                            response.Errors.Add(Constants.NotMapVTSchoolSectorMessage);
                        }
                    }

                    string existVTClassMessage = this.vtClassRepository.CheckVTClassExistByName(vtClass, vtClassModel);
                    if (!string.IsNullOrEmpty(existVTClassMessage))
                    {
                        response.Errors.Add(existVTClassMessage);
                    }
                }

                if (vtClassModel.RequestType == RequestType.Edit && vtClass.IsActive && !vtClassModel.IsActive)
                {
                    bool canInactiveVTClass = this.vtClassRepository.CheckUserCanInactiveVTClassById(vtClass);

                    if (!canInactiveVTClass)
                        response.Errors.Add("Current VTClass cannot be inactive until all dependencies on that data have been removed or inactive");
                }

                if (response.Errors.Count == 0)
                {
                    vtClass.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    vtClass = vtClassModel.FromModel(vtClass);

                    //Save Or Update vtClass details
                    bool isSaved = this.vtClassRepository.SaveOrUpdateVTClassDetails(vtClass, vtClassModel);

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
                throw new Exception("BAL > SaveOrUpdateVTClassDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtClassId)
        {
            return this.vtClassRepository.DeleteById(vtClassId);
        }

        /// <summary>}
        /// List of VTClass with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTClassViewModel> GetVTClassesByCriteria(SearchVTClassModel searchModel)
        {
            return this.vtClassRepository.GetVTClassesByCriteria(searchModel);
        }
    }
}