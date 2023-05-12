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
    /// Manager of the AcademicYear entity
    /// </summary>
    public class AcademicYearManager : GenericManager<AcademicYearModel>, IAcademicYearManager
    {
        private readonly IAcademicYearRepository academicYearRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the academicYear manager.
        /// </summary>
        /// <param name="academicYearRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public AcademicYearManager(IAcademicYearRepository _academicYearRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.academicYearRepository = _academicYearRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of AcademicYears
        /// </summary>
        /// <returns></returns>
        public IQueryable<AcademicYearModel> GetAcademicYears()
        {
            var academicYears = this.academicYearRepository.GetAcademicYears();

            IList<AcademicYearModel> academicYearModels = new List<AcademicYearModel>();
            academicYears.ForEach((user) => academicYearModels.Add(user.ToModel()));

            return academicYearModels.AsQueryable();
        }

        /// <summary>
        /// Get list of AcademicYears by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<AcademicYearModel> GetAcademicYearsByName(string academicYearName)
        {
            var academicYears = this.academicYearRepository.GetAcademicYearsByName(academicYearName);

            IList<AcademicYearModel> academicYearModels = new List<AcademicYearModel>();
            academicYears.ForEach((user) => academicYearModels.Add(user.ToModel()));

            return academicYearModels.AsQueryable();
        }

        /// <summary>
        /// Get AcademicYear by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public AcademicYearModel GetAcademicYearById(Guid academicYearId)
        {
            AcademicYear academicYear = this.academicYearRepository.GetAcademicYearById(academicYearId);

            return (academicYear != null) ? academicYear.ToModel() : null;
        }

        /// <summary>
        /// Get AcademicYear by AcademicYearId using async
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public async Task<AcademicYearModel> GetAcademicYearByIdAsync(Guid academicYearId)
        {
            var academicYear = await this.academicYearRepository.GetAcademicYearByIdAsync(academicYearId);

            return (academicYear != null) ? academicYear.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update AcademicYear entity
        /// </summary>
        /// <param name="academicYearModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateAcademicYearDetails(AcademicYearModel academicYearModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            AcademicYear academicYear = null;

            //Validate model data
            academicYearModel = academicYearModel.GetModelValidationErrors<AcademicYearModel>();

            if (academicYearModel.ErrorMessages.Count > 0)
            {
                response.Errors = academicYearModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (academicYearModel.RequestType == RequestType.Edit)
            {
                academicYear = this.academicYearRepository.GetAcademicYearById(academicYearModel.AcademicYearId);

                //academicYear.SchoolVTPSectorJobRoles.ForEach((oldSchoolVTPSectorJobRoleItem) =>
                //{
                //    var valSchoolVTPSectorJobRoleItem = academicYearModel.SchoolVTPSectorJobRoleModels.FirstOrDefault(a => a.AcademicYearSchoolVTPSectorJobRoleId == oldSchoolVTPSectorJobRoleItem.AcademicYearSchoolVTPSectorJobRoleId);
                //    if (valSchoolVTPSectorJobRoleItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldSchoolVTPSectorJobRoleIdItem.SchoolVTPSectorJobRole);
                //    }
                //});

                //academicYear.HeadMasters.ForEach((oldHeadMasterItem) =>
                //{
                //    var valHeadMasterItem = academicYearModel.HeadMasterModels.FirstOrDefault(a => a.HMId == oldHeadMasterItem.HMId);
                //    if (valHeadMasterItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldHMIdItem.HeadMaster);
                //    }
                //});

                //academicYear.Schools.ForEach((oldSchoolItem) =>
                //{
                //    var valSchoolItem = academicYearModel.SchoolModels.FirstOrDefault(a => a.SchoolId == oldSchoolItem.SchoolId);
                //    if (valSchoolItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldSchoolIdItem.School);
                //    }
                //});

                //academicYear.SchoolVEIncharges.ForEach((oldSchoolVEInchargeItem) =>
                //{
                //    var valSchoolVEInchargeItem = academicYearModel.SchoolVEInchargeModels.FirstOrDefault(a => a.VEIId == oldSchoolVEInchargeItem.VEIId);
                //    if (valSchoolVEInchargeItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldVEIIdItem.SchoolVEIncharge);
                //    }
                //});

                //academicYear.SchoolVTPSectors.ForEach((oldSchoolVTPSectorItem) =>
                //{
                //    var valSchoolVTPSectorItem = academicYearModel.SchoolVTPSectorModels.FirstOrDefault(a => a.SchoolVTPSectorId == oldSchoolVTPSectorItem.SchoolVTPSectorId);
                //    if (valSchoolVTPSectorItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldSchoolVTPSectorIdItem.SchoolVTPSector);
                //    }
                //});

                //academicYear.SectorJobRoles.ForEach((oldSectorJobRoleItem) =>
                //{
                //    var valSectorJobRoleItem = academicYearModel.SectorJobRoleModels.FirstOrDefault(a => a.SectorJobRoleId == oldSectorJobRoleItem.SectorJobRoleId);
                //    if (valSectorJobRoleItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldSectorJobRoleIdItem.SectorJobRole);
                //    }
                //});

                //academicYear.StudentClassDetails.ForEach((oldStudentClassDetailItem) =>
                //{
                //    var valStudentClassDetailItem = academicYearModel.StudentClassDetailModels.FirstOrDefault(a => a.StudentId == oldStudentClassDetailItem.StudentId);
                //    if (valStudentClassDetailItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldStudentIdItem.StudentClassDetail);
                //    }
                //});

                //academicYear.StudentClasses.ForEach((oldStudentClassItem) =>
                //{
                //    var valStudentClassItem = academicYearModel.StudentClassModels.FirstOrDefault(a => a.StudentId == oldStudentClassItem.StudentId);
                //    if (valStudentClassItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldStudentIdItem.StudentClass);
                //    }
                //});

                //academicYear.VCSchoolSectors.ForEach((oldVCSchoolSectorItem) =>
                //{
                //    var valVCSchoolSectorItem = academicYearModel.VCSchoolSectorModels.FirstOrDefault(a => a.VCSchoolSectorId == oldVCSchoolSectorItem.VCSchoolSectorId);
                //    if (valVCSchoolSectorItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldVCSchoolSectorIdItem.VCSchoolSector);
                //    }
                //});

                //academicYear.VTClasses.ForEach((oldVTClassItem) =>
                //{
                //    var valVTClassItem = academicYearModel.VTClassModels.FirstOrDefault(a => a.VTClassId == oldVTClassItem.VTClassId);
                //    if (valVTClassItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldVTClassIdItem.VTClass);
                //    }
                //});

                //academicYear.VTPSectors.ForEach((oldVTPSectorItem) =>
                //{
                //    var valVTPSectorItem = academicYearModel.VTPSectorModels.FirstOrDefault(a => a.VTPSectorId == oldVTPSectorItem.VTPSectorId);
                //    if (valVTPSectorItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldVTPSectorIdItem.VTPSector);
                //    }
                //});

                //academicYear.VTSchoolSectors.ForEach((oldVTSchoolSectorItem) =>
                //{
                //    var valVTSchoolSectorItem = academicYearModel.VTSchoolSectorModels.FirstOrDefault(a => a.VTSchoolSectorId == oldVTSchoolSectorItem.VTSchoolSectorId);
                //    if (valVTSchoolSectorItem == null)
                //    {
                //        academicYear.Deleted//s.Add(oldVTSchoolSectorIdItem.VTSchoolSector);
                //    }
                //});
            }
            else
            {
                academicYear = new AcademicYear();
                academicYearModel.AcademicYearId = Guid.NewGuid();
            }

            if (academicYearModel.ErrorMessages.Count == 0 && (academicYearModel.YearName.StringVal().ToLower() != academicYear.YearName.StringVal().ToLower()))
            {
                bool isAcademicYearExists = this.academicYearRepository.CheckAcademicYearExistByName(academicYearModel);

                if (isAcademicYearExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                academicYear.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                academicYear = academicYearModel.FromModel(academicYear);

                //Save Or Update academicYear details
                bool isSaved = this.academicYearRepository.SaveOrUpdateAcademicYearDetails(academicYear);

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
        /// Delete a record by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid academicYearId)
        {
            return this.academicYearRepository.DeleteById(academicYearId);
        }

        /// <summary>
        /// Check duplicate AcademicYear by Name
        /// </summary>
        /// <param name="academicYearModel"></param>
        /// <returns></returns>
        public bool CheckAcademicYearExistByName(AcademicYearModel academicYearModel)
        {
            return this.academicYearRepository.CheckAcademicYearExistByName(academicYearModel);
        }

        /// <summary>}
        /// List of AcademicYear with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<AcademicYearViewModel> GetAcademicYearsByCriteria(SearchAcademicYearModel searchModel)
        {
            return this.academicYearRepository.GetAcademicYearsByCriteria(searchModel);
        }
    }
}