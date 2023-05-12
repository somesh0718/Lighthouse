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
    /// Manager of the CourseMaterial entity
    /// </summary>
    public class CourseMaterialManager : GenericManager<CourseMaterialModel>, ICourseMaterialManager
    {
        private readonly ICourseMaterialRepository courseMaterialRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the courseMaterial manager.
        /// </summary>
        /// <param name="courseMaterialRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public CourseMaterialManager(ICourseMaterialRepository _courseMaterialRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.courseMaterialRepository = _courseMaterialRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of CourseMaterials
        /// </summary>
        /// <returns></returns>
        public IQueryable<CourseMaterialModel> GetCourseMaterials()
        {
            var courseMaterials = this.courseMaterialRepository.GetCourseMaterials();

            IList<CourseMaterialModel> courseMaterialModels = new List<CourseMaterialModel>();
            courseMaterials.ForEach((user) => courseMaterialModels.Add(user.ToModel()));

            return courseMaterialModels.AsQueryable();
        }

        /// <summary>
        /// Get list of CourseMaterials by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<CourseMaterialModel> GetCourseMaterialsByName(string courseMaterialName)
        {
            var courseMaterials = this.courseMaterialRepository.GetCourseMaterialsByName(courseMaterialName);

            IList<CourseMaterialModel> courseMaterialModels = new List<CourseMaterialModel>();
            courseMaterials.ForEach((user) => courseMaterialModels.Add(user.ToModel()));

            return courseMaterialModels.AsQueryable();
        }

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        public CourseMaterialModel GetCourseMaterialById(Guid courseMaterialId)
        {
            CourseMaterialModel courseMaterialModel = null;
            CourseMaterial courseMaterial = this.courseMaterialRepository.GetCourseMaterialById(courseMaterialId);
            if (courseMaterial != null)
            {
                courseMaterialModel = courseMaterial.ToModel();

                LhUserModel vptAndVCIdModel = this.commonRepository.GetVTPVCAndSchoolIdByVTId(courseMaterial.VTId);
                if (vptAndVCIdModel != null)
                {
                    courseMaterialModel.VTPId = vptAndVCIdModel.VTPId;
                    courseMaterialModel.VCId = vptAndVCIdModel.VCId;
                    courseMaterialModel.SchoolId = vptAndVCIdModel.SchoolId;
                }
            }

            return courseMaterialModel;
        }

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId using async
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        public async Task<CourseMaterialModel> GetCourseMaterialByIdAsync(Guid courseMaterialId)
        {
            var courseMaterial = await this.courseMaterialRepository.GetCourseMaterialByIdAsync(courseMaterialId);

            return (courseMaterial != null) ? courseMaterial.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update CourseMaterial entity
        /// </summary>
        /// <param name="courseMaterialModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateCourseMaterialDetails(CourseMaterialModel courseMaterialModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                CourseMaterial courseMaterial = null;

                //Validate model data
                courseMaterialModel = courseMaterialModel.GetModelValidationErrors<CourseMaterialModel>();

                if (courseMaterialModel.ErrorMessages.Count > 0)
                {
                    response.Errors = courseMaterialModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (courseMaterialModel.RequestType == RequestType.Edit)
                {
                    courseMaterial = this.courseMaterialRepository.GetCourseMaterialById(courseMaterialModel.CourseMaterialId);
                }
                else
                {
                    courseMaterial = new CourseMaterial();
                    courseMaterial.CourseMaterialId = Guid.NewGuid();
                }

                if (courseMaterialModel.ErrorMessages.Count == 0 && courseMaterialModel.RequestType == RequestType.New)
                {
                    bool isCourseMaterialExists = this.courseMaterialRepository.CheckCourseMaterialExistByName(courseMaterialModel);

                    if (isCourseMaterialExists)
                    {
                        response.Errors.Add(Constants.ExistMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    courseMaterial.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                    courseMaterial = courseMaterialModel.FromModel(courseMaterial);

                    //Save Or Update courseMaterial details
                    bool isSaved = this.courseMaterialRepository.SaveOrUpdateCourseMaterialDetails(courseMaterial);

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
                throw new Exception("BAL > SaveOrUpdateCourseMaterialDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by CourseMaterialId
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid courseMaterialId)
        {
            return this.courseMaterialRepository.DeleteById(courseMaterialId);
        }

        /// <summary>
        /// Check duplicate CourseMaterial by Name
        /// </summary>
        /// <param name="courseMaterialModel"></param>
        /// <returns></returns>
        public bool CheckCourseMaterialExistByName(CourseMaterialModel courseMaterialModel)
        {
            return this.courseMaterialRepository.CheckCourseMaterialExistByName(courseMaterialModel);
        }

        /// <summary>}
        /// List of CourseMaterial with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<CourseMaterialViewModel> GetCourseMaterialsByCriteria(SearchCourseMaterialModel searchModel)
        {
            return this.courseMaterialRepository.GetCourseMaterialsByCriteria(searchModel);
        }
    }
}