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
    /// Manager of the Sector entity
    /// </summary>
    public class SectorManager : GenericManager<SectorModel>, ISectorManager
    {
        private readonly ISectorRepository sectorRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the sector manager.
        /// </summary>
        /// <param name="sectorRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SectorManager(ISectorRepository _sectorRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.sectorRepository = _sectorRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Sectors
        /// </summary>
        /// <returns></returns>
        public IQueryable<SectorModel> GetSectors()
        {
            var sectors = this.sectorRepository.GetSectors();

            IList<SectorModel> sectorModels = new List<SectorModel>();
            sectors.ForEach((user) => sectorModels.Add(user.ToModel()));

            return sectorModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Sectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SectorModel> GetSectorsByName(string sectorName)
        {
            var sectors = this.sectorRepository.GetSectorsByName(sectorName);

            IList<SectorModel> sectorModels = new List<SectorModel>();
            sectors.ForEach((user) => sectorModels.Add(user.ToModel()));

            return sectorModels.AsQueryable();
        }

        /// <summary>
        /// Get Sector by SectorId
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public SectorModel GetSectorById(Guid sectorId)
        {
            Sector sector = this.sectorRepository.GetSectorById(sectorId);

            return (sector != null) ? sector.ToModel() : null;
        }

        /// <summary>
        /// Get Sector by SectorId using async
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public async Task<SectorModel> GetSectorByIdAsync(Guid sectorId)
        {
            var sector = await this.sectorRepository.GetSectorByIdAsync(sectorId);

            return (sector != null) ? sector.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Sector entity
        /// </summary>
        /// <param name="sectorModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSectorDetails(SectorModel sectorModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Sector sector = null;

            //Validate model data
            sectorModel = sectorModel.GetModelValidationErrors<SectorModel>();

            if (sectorModel.ErrorMessages.Count > 0)
            {
                response.Errors = sectorModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (sectorModel.RequestType == RequestType.Edit)
            {
                sector = this.sectorRepository.GetSectorById(sectorModel.SectorId);

                //sector.AcademicYearSchoolVTPJobRoles.ForEach((oldAcademicYearSchoolVTPJobRoleItem) =>
                //{
                //    var valAcademicYearSchoolVTPJobRoleItem = sectorModel.AcademicYearSchoolVTPJobRoleModels.FirstOrDefault(a => a.AcademicYearSchoolVTPSectorJobRoleId == oldAcademicYearSchoolVTPJobRoleItem.AcademicYearSchoolVTPSectorJobRoleId);
                //    if (valAcademicYearSchoolVTPJobRoleItem == null)
                //    {
                //        sector.Deleted//s.Add(oldAcademicYearSchoolVTPJobRoleIdItem.AcademicYearSchoolVTPJobRole);
                //    }
                //});

                //sector.JobRoles.ForEach((oldJobRoleItem) =>
                //{
                //    var valJobRoleItem = sectorModel.JobRoleModels.FirstOrDefault(a => a.SectorJobRoleId == oldJobRoleItem.SectorJobRoleId);
                //    if (valJobRoleItem == null)
                //    {
                //        sector.Deleted//s.Add(oldJobRoleIdItem.JobRole);
                //    }
                //});

                //sector.VTPJobRoles.ForEach((oldVTPJobRoleItem) =>
                //{
                //    var valVTPJobRoleItem = sectorModel.VTPJobRoleModels.FirstOrDefault(a => a.VTPSectorJobRoleId == oldVTPJobRoleItem.VTPSectorJobRoleId);
                //    if (valVTPJobRoleItem == null)
                //    {
                //        sector.Deleted//s.Add(oldVTPJobRoleIdItem.VTPJobRole);
                //    }
                //});
            }
            else
            {
                sector = new Sector();
                sectorModel.SectorId = Guid.NewGuid();
            }

            if (sectorModel.ErrorMessages.Count == 0 && (sectorModel.SectorName.StringVal().ToLower() != sector.SectorName.StringVal().ToLower()))
            {
                bool isSectorExists = this.sectorRepository.CheckSectorExistByName(sectorModel);

                if (isSectorExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                sector.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                sector = sectorModel.FromModel(sector);

                //Save Or Update sector details
                bool isSaved = this.sectorRepository.SaveOrUpdateSectorDetails(sector);

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
        /// Delete a record by SectorId
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid sectorId)
        {
            return this.sectorRepository.DeleteById(sectorId);
        }

        /// <summary>
        /// Check duplicate Sector by Name
        /// </summary>
        /// <param name="sectorModel"></param>
        /// <returns></returns>
        public bool CheckSectorExistByName(SectorModel sectorModel)
        {
            return this.sectorRepository.CheckSectorExistByName(sectorModel);
        }

        /// <summary>}
        /// List of Sector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SectorViewModel> GetSectorsByCriteria(SearchSectorModel searchModel)
        {
            return this.sectorRepository.GetSectorsByCriteria(searchModel);
        }
    }
}