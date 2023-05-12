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
    /// Manager of the SchoolVTPSector entity
    /// </summary>
    public class SchoolVTPSectorManager : GenericManager<SchoolVTPSectorModel>, ISchoolVTPSectorManager
    {
        private readonly ISchoolVTPSectorRepository schoolVTPSectorRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the schoolVTPSector manager.
        /// </summary>
        /// <param name="schoolVTPSectorRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SchoolVTPSectorManager(ISchoolVTPSectorRepository _schoolVTPSectorRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.schoolVTPSectorRepository = _schoolVTPSectorRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of SchoolVTPSectors
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolVTPSectorModel> GetSchoolVTPSectors()
        {
            var schoolVTPSectors = this.schoolVTPSectorRepository.GetSchoolVTPSectors();

            IList<SchoolVTPSectorModel> schoolVTPSectorModels = new List<SchoolVTPSectorModel>();
            schoolVTPSectors.ForEach((user) => schoolVTPSectorModels.Add(user.ToModel()));

            return schoolVTPSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolVTPSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolVTPSectorModel> GetSchoolVTPSectorsByName(string schoolVTPSectorName)
        {
            var schoolVTPSectors = this.schoolVTPSectorRepository.GetSchoolVTPSectorsByName(schoolVTPSectorName);

            IList<SchoolVTPSectorModel> schoolVTPSectorModels = new List<SchoolVTPSectorModel>();
            schoolVTPSectors.ForEach((user) => schoolVTPSectorModels.Add(user.ToModel()));

            return schoolVTPSectorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolVTPSector by VTP & Sector
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public SchoolVTPSectorModel GetSchoolVTPSectorsBy3Ids(Guid academicYearId, Guid schoolId, Guid vtpId, Guid sectorId)
        {
            var schoolVTPSector = this.schoolVTPSectorRepository.GetSchoolVTPSectorsBy3Ids(academicYearId, schoolId, vtpId, sectorId);

            return (schoolVTPSector != null) ? schoolVTPSector.ToModel() : null;
        }

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        public SchoolVTPSectorModel GetSchoolVTPSectorById(Guid schoolVTPSectorId)
        {
            SchoolVTPSector schoolVTPSector = this.schoolVTPSectorRepository.GetSchoolVTPSectorById(schoolVTPSectorId);

            return (schoolVTPSector != null) ? schoolVTPSector.ToModel() : null;
        }

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId using async
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        public async Task<SchoolVTPSectorModel> GetSchoolVTPSectorByIdAsync(Guid schoolVTPSectorId)
        {
            var schoolVTPSector = await this.schoolVTPSectorRepository.GetSchoolVTPSectorByIdAsync(schoolVTPSectorId);

            return (schoolVTPSector != null) ? schoolVTPSector.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update SchoolVTPSector entity
        /// </summary>
        /// <param name="schoolVTPSectorModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSchoolVTPSectorDetails(SchoolVTPSectorModel schoolVTPSectorModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                SchoolVTPSector schoolVTPSector = null;

                //Validate model data
                schoolVTPSectorModel = schoolVTPSectorModel.GetModelValidationErrors<SchoolVTPSectorModel>();

                if (schoolVTPSectorModel.ErrorMessages.Count > 0)
                {
                    response.Errors = schoolVTPSectorModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (schoolVTPSectorModel.RequestType == RequestType.Edit)
                {
                    schoolVTPSector = this.schoolVTPSectorRepository.GetSchoolVTPSectorById(schoolVTPSectorModel.SchoolVTPSectorId);
                }
                else
                {
                    schoolVTPSector = new SchoolVTPSector();
                    schoolVTPSectorModel.SchoolVTPSectorId = Guid.NewGuid();
                }

                if (schoolVTPSectorModel.ErrorMessages.Count == 0)
                {
                    string existSchoolVTPSectorMessage = this.schoolVTPSectorRepository.CheckSchoolVTPSectorExistByName(schoolVTPSector, schoolVTPSectorModel);

                    if (!string.IsNullOrEmpty(existSchoolVTPSectorMessage))
                    {
                        response.Errors.Add(existSchoolVTPSectorMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    schoolVTPSector.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    schoolVTPSector = schoolVTPSectorModel.FromModel(schoolVTPSector);

                    //Save Or Update schoolVTPSector details
                    bool isSaved = this.schoolVTPSectorRepository.SaveOrUpdateSchoolVTPSectorDetails(schoolVTPSector);

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
                throw new Exception("BAL > SaveOrUpdateSchoolVTPSectorDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid schoolVTPSectorId)
        {
            return this.schoolVTPSectorRepository.DeleteById(schoolVTPSectorId);
        }

        /// <summary>
        /// List of SchoolVTPSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<SchoolVTPSectorViewModel> GetSchoolVTPSectorsByCriteria(SearchSchoolVTPSectorModel searchModel)
        {
            return this.schoolVTPSectorRepository.GetSchoolVTPSectorsByCriteria(searchModel);
        }
    }
}