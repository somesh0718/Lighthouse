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
    /// Manager of the SchoolVEIncharge entity
    /// </summary>
    public class SchoolVEInchargeManager : GenericManager<SchoolVEInchargeModel>, ISchoolVEInchargeManager
    {
        private readonly ISchoolVEInchargeRepository schoolVEInchargeRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the schoolVEIncharge manager.
        /// </summary>
        /// <param name="schoolVEInchargeRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SchoolVEInchargeManager(ISchoolVEInchargeRepository _schoolVEInchargeRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.schoolVEInchargeRepository = _schoolVEInchargeRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of SchoolVEIncharges
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolVEInchargeModel> GetSchoolVEIncharges()
        {
            var schoolVEIncharges = this.schoolVEInchargeRepository.GetSchoolVEIncharges();

            IList<SchoolVEInchargeModel> schoolVEInchargeModels = new List<SchoolVEInchargeModel>();
            schoolVEIncharges.ForEach((user) => schoolVEInchargeModels.Add(user.ToModel()));

            return schoolVEInchargeModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolVEIncharges by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolVEInchargeModel> GetSchoolVEInchargesByName(string schoolVEInchargeName)
        {
            var schoolVEIncharges = this.schoolVEInchargeRepository.GetSchoolVEInchargesByName(schoolVEInchargeName);

            IList<SchoolVEInchargeModel> schoolVEInchargeModels = new List<SchoolVEInchargeModel>();
            schoolVEIncharges.ForEach((user) => schoolVEInchargeModels.Add(user.ToModel()));

            return schoolVEInchargeModels.AsQueryable();
        }

        /// <summary>
        /// Get SchoolVEIncharge by VEIId
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        public SchoolVEInchargeModel GetSchoolVEInchargeById(Guid veiId)
        {
            SchoolVEInchargeModel schoolVEInchargeModel = null;
            SchoolVEIncharge schoolVEIncharge = this.schoolVEInchargeRepository.GetSchoolVEInchargeById(veiId);

            if (schoolVEIncharge != null)
            {
                schoolVEInchargeModel = schoolVEIncharge.ToModel();

                LhUserModel vptAndVCIdModel = this.commonRepository.GetVTPandVCIdBySchoolId(schoolVEIncharge.SchoolId);
                schoolVEInchargeModel.VTPId = vptAndVCIdModel.VTPId;
                schoolVEInchargeModel.VCId = vptAndVCIdModel.VCId;
            }

            return schoolVEInchargeModel;
        }

        /// <summary>
        /// Get SchoolVEIncharge by VEIId using async
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        public async Task<SchoolVEInchargeModel> GetSchoolVEInchargeByIdAsync(Guid veiId)
        {
            var schoolVEIncharge = await this.schoolVEInchargeRepository.GetSchoolVEInchargeByIdAsync(veiId);

            return (schoolVEIncharge != null) ? schoolVEIncharge.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update SchoolVEIncharge entity
        /// </summary>
        /// <param name="schoolVEInchargeModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSchoolVEInchargeDetails(SchoolVEInchargeModel schoolVEInchargeModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                SchoolVEIncharge schoolVEIncharge = null;

                //Validate model data
                schoolVEInchargeModel = schoolVEInchargeModel.GetModelValidationErrors<SchoolVEInchargeModel>();

                if (schoolVEInchargeModel.ErrorMessages.Count > 0)
                {
                    response.Errors = schoolVEInchargeModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (schoolVEInchargeModel.RequestType == RequestType.Edit)
                {
                    schoolVEIncharge = this.schoolVEInchargeRepository.GetSchoolVEInchargeById(schoolVEInchargeModel.VEIId);
                }
                else
                {
                    schoolVEIncharge = new SchoolVEIncharge();
                    schoolVEIncharge.VEIId = Guid.NewGuid();
                }

                if (schoolVEInchargeModel.ErrorMessages.Count == 0)
                {
                    schoolVEInchargeModel.FullName = string.Format("{0} {1} {2}", schoolVEInchargeModel.FirstName, schoolVEInchargeModel.MiddleName, schoolVEInchargeModel.LastName).TrimSpaces();

                    string errorMessage = this.schoolVEInchargeRepository.CheckSchoolVEInchargeExistByName(schoolVEInchargeModel);

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        response.Errors.Add(errorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    schoolVEIncharge.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    schoolVEIncharge = schoolVEInchargeModel.FromModel(schoolVEIncharge);

                    //Save Or Update schoolVEIncharge details
                    bool isSaved = this.schoolVEInchargeRepository.SaveOrUpdateSchoolVEInchargeDetails(schoolVEIncharge);

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
                throw new Exception("BAL > SaveOrUpdateSchoolVEInchargeDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by VEIId
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid veiId)
        {
            return this.schoolVEInchargeRepository.DeleteById(veiId);
        }

        /// <summary>
        /// Check duplicate SchoolVEIncharge by Name
        /// </summary>
        /// <param name="schoolVEInchargeModel"></param>
        /// <returns></returns>
        public string CheckSchoolVEInchargeExistByName(SchoolVEInchargeModel schoolVEInchargeModel)
        {
            return this.schoolVEInchargeRepository.CheckSchoolVEInchargeExistByName(schoolVEInchargeModel);
        }

        /// <summary>}
        /// List of SchoolVEIncharge with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolVEInchargeViewModel> GetSchoolVEInchargesByCriteria(SearchSchoolVEInchargeModel searchModel)
        {
            return this.schoolVEInchargeRepository.GetSchoolVEInchargesByCriteria(searchModel);
        }
    }
}