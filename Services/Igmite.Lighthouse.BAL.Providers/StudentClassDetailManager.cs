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
    /// Manager of the StudentClassDetail entity
    /// </summary>
    public class StudentClassDetailManager : GenericManager<StudentClassDetailModel>, IStudentClassDetailManager
    {
        private readonly IStudentClassDetailRepository studentClassDetailRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the studentClassDetail manager.
        /// </summary>
        /// <param name="studentClassDetailRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public StudentClassDetailManager(IStudentClassDetailRepository _studentClassDetailRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.studentClassDetailRepository = _studentClassDetailRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of StudentClassDetails
        /// </summary>
        /// <returns></returns>
        public IQueryable<StudentClassDetailModel> GetStudentClassDetails()
        {
            var studentClassDetails = this.studentClassDetailRepository.GetStudentClassDetails();

            IList<StudentClassDetailModel> studentClassDetailModels = new List<StudentClassDetailModel>();
            studentClassDetails.ForEach((user) => studentClassDetailModels.Add(user.ToModel()));

            return studentClassDetailModels.AsQueryable();
        }

        /// <summary>
        /// Get list of StudentClassDetails by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<StudentClassDetailModel> GetStudentClassDetailsByName(string studentClassDetailName)
        {
            var studentClassDetails = this.studentClassDetailRepository.GetStudentClassDetailsByName(studentClassDetailName);

            IList<StudentClassDetailModel> studentClassDetailModels = new List<StudentClassDetailModel>();
            studentClassDetails.ForEach((user) => studentClassDetailModels.Add(user.ToModel()));

            return studentClassDetailModels.AsQueryable();
        }

        /// <summary>
        /// Get StudentClassDetail by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentClassDetailModel GetStudentClassDetailById(Guid studentId)
        {
            StudentClassDetailModel studentClassDetailModel = null;
            StudentClassDetail studentClassDetail = this.studentClassDetailRepository.GetStudentClassDetailById(studentId);
            if (studentClassDetail != null)
            {
                studentClassDetailModel = studentClassDetail.ToModel();

                var currentYear = this.commonRepository.GetAcademicYear();
                var studentClassMapping = this.commonRepository.GetSchoolClassMappingBySchoolId(currentYear.AcademicYearId, studentId);
                var vcTraineMap = this.commonRepository.GetVCTrainerMapByVTId(studentClassMapping.AcademicYearId, studentClassMapping.VTId);

                studentClassDetailModel.VTPId = vcTraineMap.VTPId;
                studentClassDetailModel.VCId = vcTraineMap.VCId;
                studentClassDetailModel.VTId = vcTraineMap.VTId;
                studentClassDetailModel.SchoolId = studentClassMapping.SchoolId;
                studentClassDetailModel.ClassId = studentClassMapping.ClassId;
                studentClassDetailModel.SectionId = studentClassMapping.SectionId;
                studentClassDetailModel.AcademicYearId = studentClassMapping.AcademicYearId;
            }

            return studentClassDetailModel;
        }

        /// <summary>
        /// Get StudentClassDetail by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentClassDetailModel> GetStudentClassDetailByIdAsync(Guid studentId)
        {
            var studentClassDetail = await this.studentClassDetailRepository.GetStudentClassDetailByIdAsync(studentId);

            return (studentClassDetail != null) ? studentClassDetail.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update StudentClassDetail entity
        /// </summary>
        /// <param name="studentClassDetailModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateStudentClassDetailDetails(StudentClassDetailModel studentClassDetailModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                StudentClassDetail studentClassDetail = null;

                //Validate model data
                studentClassDetailModel = studentClassDetailModel.GetModelValidationErrors<StudentClassDetailModel>();

                if (studentClassDetailModel.ErrorMessages.Count > 0)
                {
                    response.Errors = studentClassDetailModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (studentClassDetailModel.RequestType == RequestType.Edit)
                {
                    studentClassDetail = this.studentClassDetailRepository.GetStudentClassDetailById(studentClassDetailModel.StudentId);
                }
                else
                {
                    studentClassDetail = new StudentClassDetail();
                }

                if (studentClassDetailModel.ErrorMessages.Count == 0)
                {
                    IList<string> errorMessages = this.studentClassDetailRepository.CheckStudentClassDetailExistByName(studentClassDetailModel);

                    if (errorMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", errorMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    studentClassDetail.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    studentClassDetail = studentClassDetailModel.FromModel(studentClassDetail);

                    //Save Or Update studentClassDetail details
                    bool isSaved = this.studentClassDetailRepository.SaveOrUpdateStudentClassDetailDetails(studentClassDetail);

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
                throw new Exception("BAL > SaveOrUpdateStudentClassDetailDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid studentId)
        {
            return this.studentClassDetailRepository.DeleteById(studentId);
        }

        /// <summary>
        /// Check duplicate StudentClassDetail by Name
        /// </summary>
        /// <param name="studentClassDetailModel"></param>
        /// <returns></returns>
        public IList<string> CheckStudentClassDetailExistByName(StudentClassDetailModel studentClassDetailModel)
        {
            return this.studentClassDetailRepository.CheckStudentClassDetailExistByName(studentClassDetailModel);
        }

        /// <summary>}
        /// List of StudentClassDetail with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<StudentClassDetailViewModel> GetStudentClassDetailsByCriteria(SearchStudentClassDetailModel searchModel)
        {
            return this.studentClassDetailRepository.GetStudentClassDetailsByCriteria(searchModel);
        }

        /// <summary>
        /// Get VocationalEducationAssessment with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public VocationalEducationAssessmentModel GetVocationalEducationAssessmentBySchoolAndVTId(SearchStudentClassDetailModel searchModel)
        {
            return this.studentClassDetailRepository.GetVocationalEducationAssessmentBySchoolAndVTId(searchModel);
        }
    }
}