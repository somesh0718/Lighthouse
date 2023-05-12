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
    /// Manager of the VTStudentResultOtherSubject entity
    /// </summary>
    public class VTStudentResultOtherSubjectManager : GenericManager<VTStudentResultOtherSubjectModel>, IVTStudentResultOtherSubjectManager
    {
        private readonly IVTStudentResultOtherSubjectRepository vtStudentResultOtherSubjectRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtStudentResultOtherSubject manager.
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTStudentResultOtherSubjectManager(IVTStudentResultOtherSubjectRepository _vtStudentResultOtherSubjectRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtStudentResultOtherSubjectRepository = _vtStudentResultOtherSubjectRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTStudentResultOtherSubjects
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentResultOtherSubjectModel> GetVTStudentResultOtherSubjects()
        {
            var vtStudentResultOtherSubjects = this.vtStudentResultOtherSubjectRepository.GetVTStudentResultOtherSubjects();

            IList<VTStudentResultOtherSubjectModel> vtStudentResultOtherSubjectModels = new List<VTStudentResultOtherSubjectModel>();
            vtStudentResultOtherSubjects.ForEach((user) => vtStudentResultOtherSubjectModels.Add(user.ToModel()));

            return vtStudentResultOtherSubjectModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentResultOtherSubjects by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentResultOtherSubjectModel> GetVTStudentResultOtherSubjectsByName(string vtStudentResultOtherSubjectName)
        {
            var vtStudentResultOtherSubjects = this.vtStudentResultOtherSubjectRepository.GetVTStudentResultOtherSubjectsByName(vtStudentResultOtherSubjectName);

            IList<VTStudentResultOtherSubjectModel> vtStudentResultOtherSubjectModels = new List<VTStudentResultOtherSubjectModel>();
            vtStudentResultOtherSubjects.ForEach((user) => vtStudentResultOtherSubjectModels.Add(user.ToModel()));

            return vtStudentResultOtherSubjectModels.AsQueryable();
        }

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        public VTStudentResultOtherSubjectModel GetVTStudentResultOtherSubjectById(Guid vtStudentResultOtherSubjectId)
        {
            VTStudentResultOtherSubject vtStudentResultOtherSubject = this.vtStudentResultOtherSubjectRepository.GetVTStudentResultOtherSubjectById(vtStudentResultOtherSubjectId);

            return (vtStudentResultOtherSubject != null) ? vtStudentResultOtherSubject.ToModel() : null;
        }

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId using async
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        public async Task<VTStudentResultOtherSubjectModel> GetVTStudentResultOtherSubjectByIdAsync(Guid vtStudentResultOtherSubjectId)
        {
            var vtStudentResultOtherSubject = await this.vtStudentResultOtherSubjectRepository.GetVTStudentResultOtherSubjectByIdAsync(vtStudentResultOtherSubjectId);

            return (vtStudentResultOtherSubject != null) ? vtStudentResultOtherSubject.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTStudentResultOtherSubject entity
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTStudentResultOtherSubjectDetails(VTStudentResultOtherSubjectModel vtStudentResultOtherSubjectModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTStudentResultOtherSubject vtStudentResultOtherSubject = null;

            //Validate model data
            vtStudentResultOtherSubjectModel = vtStudentResultOtherSubjectModel.GetModelValidationErrors<VTStudentResultOtherSubjectModel>();

            if (vtStudentResultOtherSubjectModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtStudentResultOtherSubjectModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtStudentResultOtherSubjectModel.RequestType == RequestType.Edit)
            {
                vtStudentResultOtherSubject = this.vtStudentResultOtherSubjectRepository.GetVTStudentResultOtherSubjectById(vtStudentResultOtherSubjectModel.VTStudentResultOtherSubjectId);
            }
            else
            {
                vtStudentResultOtherSubject = new VTStudentResultOtherSubject();
                vtStudentResultOtherSubjectModel.VTStudentResultOtherSubjectId = Guid.NewGuid();
            }

            if (vtStudentResultOtherSubjectModel.ErrorMessages.Count == 0 && !(Guid.Equals(vtStudentResultOtherSubject.StudentId, vtStudentResultOtherSubjectModel.StudentId) && string.Equals(vtStudentResultOtherSubjectModel.SubjectName.ToLower(), vtStudentResultOtherSubject.SubjectName.StringVal().ToLower())))
            {
                bool isVTStudentResultOtherSubjectExists = this.vtStudentResultOtherSubjectRepository.CheckVTStudentResultOtherSubjectExistByName(vtStudentResultOtherSubjectModel);

                if (isVTStudentResultOtherSubjectExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtStudentResultOtherSubject.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtStudentResultOtherSubjectModel.RequestType == RequestType.New)
                {
                    VTClass vtClass = this.commonRepository.GetVTClassByUserId(vtStudentResultOtherSubject.AuthUserId);
                    if (vtClass != null)
                        vtStudentResultOtherSubjectModel.VTClassId = vtClass.VTClassId;
                }

                vtStudentResultOtherSubject = vtStudentResultOtherSubjectModel.FromModel(vtStudentResultOtherSubject);

                //Save Or Update vtStudentResultOtherSubject details
                bool isSaved = this.vtStudentResultOtherSubjectRepository.SaveOrUpdateVTStudentResultOtherSubjectDetails(vtStudentResultOtherSubject);

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
        /// Delete a record by VTStudentResultOtherSubjectId
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentResultOtherSubjectId)
        {
            return this.vtStudentResultOtherSubjectRepository.DeleteById(vtStudentResultOtherSubjectId);
        }

        /// <summary>
        /// Check duplicate VTStudentResultOtherSubject by Name
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentResultOtherSubjectExistByName(VTStudentResultOtherSubjectModel vtStudentResultOtherSubjectModel)
        {
            return this.vtStudentResultOtherSubjectRepository.CheckVTStudentResultOtherSubjectExistByName(vtStudentResultOtherSubjectModel);
        }

        /// <summary>}
        /// List of VTStudentResultOtherSubject with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentResultOtherSubjectViewModel> GetVTStudentResultOtherSubjectsByCriteria(SearchVTStudentResultOtherSubjectModel searchModel)
        {
            return this.vtStudentResultOtherSubjectRepository.GetVTStudentResultOtherSubjectsByCriteria(searchModel);
        }
    }
}