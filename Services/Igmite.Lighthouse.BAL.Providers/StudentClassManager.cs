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
    /// Manager of the StudentClass entity
    /// </summary>
    public class StudentClassManager : GenericManager<StudentClassModel>, IStudentClassManager
    {
        private readonly IStudentClassRepository studentClassRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the studentClass manager.
        /// </summary>
        /// <param name="studentClassRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public StudentClassManager(IStudentClassRepository _studentClassRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.studentClassRepository = _studentClassRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of StudentClasses
        /// </summary>
        /// <returns></returns>
        public IQueryable<StudentClassModel> GetStudentClasses()
        {
            var studentClasses = this.studentClassRepository.GetStudentClasses();

            IList<StudentClassModel> studentClassModels = new List<StudentClassModel>();
            studentClasses.ForEach((user) => studentClassModels.Add(user.ToModel()));

            return studentClassModels.AsQueryable();
        }

        /// <summary>
        /// Get list of StudentClasses by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<StudentClassModel> GetStudentClassesByName(string studentClassName)
        {
            var studentClasses = this.studentClassRepository.GetStudentClassesByName(studentClassName);

            IList<StudentClassModel> studentClassModels = new List<StudentClassModel>();
            studentClasses.ForEach((user) => studentClassModels.Add(user.ToModel()));

            return studentClassModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        public IQueryable<StudentClassModel> GetStudentsByNames(List<string> studentNames)
        {
            var studentClasses = this.studentClassRepository.GetStudentsByNames(studentNames);

            IList<StudentClassModel> studentClassModels = new List<StudentClassModel>();
            studentClasses.ForEach((user) => studentClassModels.Add(user.ToModel()));

            return studentClassModels.AsQueryable();
        }

        /// <summary>
        /// Get StudentClass by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentClassModel GetStudentClassById(Guid studentId)
        {
            StudentClassModel studentClassModel = null;
            StudentClass studentClass = this.studentClassRepository.GetStudentClassById(studentId);

            if (studentClass != null)
            {
                studentClassModel = studentClass.ToModel();

                if (studentClass.VTId.HasValue)
                {
                    VocationalTrainer vocationalTrainer = this.commonRepository.GetVocationalTrainerById(studentClass.VTId.Value);

                    studentClassModel.VTPId = vocationalTrainer.VCTrainer.VTPId;
                    studentClassModel.VCId = vocationalTrainer.VCTrainer.VCId;
                }
            }

            return studentClassModel;
        }

        /// <summary>
        /// Get StudentClass by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentClassModel> GetStudentClassByIdAsync(Guid studentId)
        {
            var studentClass = await this.studentClassRepository.GetStudentClassByIdAsync(studentId);

            return (studentClass != null) ? studentClass.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update StudentClass entity
        /// </summary>
        /// <param name="studentClassModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateStudentClassDetails(StudentClassModel studentClassModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                StudentClass studentClass = null;

                //Validate model data
                studentClassModel = studentClassModel.GetModelValidationErrors<StudentClassModel>();

                if (studentClassModel.ErrorMessages.Count > 0)
                {
                    response.Errors = studentClassModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (studentClassModel.RequestType == RequestType.Edit)
                {
                    studentClass = this.studentClassRepository.GetStudentClassById(studentClassModel.StudentId);
                }
                else
                {
                    studentClass = new StudentClass();
                    studentClass.StudentId = Guid.NewGuid();
                    studentClassModel.StudentId = studentClass.StudentId;
                }

                if (studentClassModel.ErrorMessages.Count == 0)
                {
                    studentClassModel.FullName = string.Format("{0} {1} {2}", studentClassModel.FirstName, studentClassModel.MiddleName, studentClassModel.LastName).TrimSpaces();
                    string existStudentMessage = this.studentClassRepository.CheckStudentClassExistByName(studentClass, studentClassModel);

                    if (!string.IsNullOrEmpty(existStudentMessage))
                    {
                        response.Errors.Add(existStudentMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    studentClass.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    studentClass = studentClassModel.FromModel(studentClass);

                    if (studentClassModel.RequestType == RequestType.New)
                    {
                        VTSchoolSector vtSchoolSectors = this.commonRepository.GetVTSchoolSectorsByVTId(studentClassModel.VTId.Value);
                        if (vtSchoolSectors != null)
                            studentClass.SchoolId = vtSchoolSectors.SchoolId;
                    }

                    //Save Or Update studentClass details
                    bool isSaved = this.studentClassRepository.SaveOrUpdateStudentClassDetails(studentClass);

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
                throw new Exception("BAL > SaveOrUpdateStudentClassDetails", ex);
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
            string authUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

            return this.studentClassRepository.DeleteById(studentId, authUserId);
        }

        /// <summary>}
        /// List of StudentClass with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<StudentClassViewModel> GetStudentClassesByCriteria(SearchStudentClassModel searchModel)
        {
            return this.studentClassRepository.GetStudentClassesByCriteria(searchModel);
        }
    }
}