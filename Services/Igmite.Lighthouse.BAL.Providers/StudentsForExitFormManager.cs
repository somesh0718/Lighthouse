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
    /// Manager of the StudentsForExitForm entity
    /// </summary>
    public class StudentsForExitFormManager : GenericManager<StudentsForExitFormModel>, IStudentsForExitFormManager
    {
        private readonly IStudentsForExitFormRepository studentsForExitFormRepository;
        private readonly IExitSurveyDetailsRepository exitSurveyDetailsRepository;
        private readonly IStudentClassRepository studentClassRepository;
        private readonly IStudentClassDetailRepository studentClassDetailRepository;
        private readonly ISchoolRepository schoolRepository;
        private readonly ISchoolClassRepository schoolClassRepository;
        private readonly ISectorRepository sectorRepository;
        private readonly IDataValueRepository dataValueRepository;
        private readonly IVocationalTrainerRepository vtRepository;
        private readonly IVTSchoolSectorRepository vtSchoolSectorRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the studentsForExitForm manager.
        /// </summary>
        /// <param name="studentsForExitFormRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public StudentsForExitFormManager(IStudentsForExitFormRepository _studentsForExitFormRepository,
            IHttpContextAccessor _httpContextAccessor,
            ICommonRepository _commonRepository,
            IStudentClassRepository _studentClassRepository,
            IStudentClassDetailRepository _studentClassDetailRepository,
            ISchoolRepository _schoolRepository,
            ISchoolClassRepository _schoolClassRepository,
            ISectorRepository _sectorRepository,
            IVTSchoolSectorRepository _vtSchoolSectorRepository,
            IVocationalTrainerRepository _vtRepository,
            IDataValueRepository _dataValueRepository,
            IExitSurveyDetailsRepository _exitSurveyDetailsRepository)
        {
            this.studentsForExitFormRepository = _studentsForExitFormRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
            this.studentClassRepository = _studentClassRepository;
            this.studentClassDetailRepository = _studentClassDetailRepository;
            this.schoolRepository = _schoolRepository;
            this.schoolClassRepository = _schoolClassRepository;
            this.sectorRepository = _sectorRepository;
            this.vtSchoolSectorRepository = _vtSchoolSectorRepository;
            this.vtRepository = _vtRepository;
            this.dataValueRepository = _dataValueRepository;
            this.exitSurveyDetailsRepository = _exitSurveyDetailsRepository;
        }

        /// <summary>
        /// Get list of StudentsForExitFormes
        /// </summary>
        /// <returns></returns>
        public IList<StudentsForExitSurveyViewModel> GetStudentsForExitForm(ExitSurveyRequestModel exitSurveyDetailsModel)
        {
            IList<StudentsForExitSurveyViewModel> studentsForExitForm = this.studentsForExitFormRepository.GetStudentsForExitForm(exitSurveyDetailsModel);

            //if(exitSurveyDetailsModel.AcademicYear == "2020-2021")
            //{
            //    var students = this.studentsForExitFormRepository.GetStudentsForExitForm();
            //    if(students != null && students.Count() > 0)
            //    studentsForExitForm = students.ToList();
            //}
            //else
            //{
            //var students = this.studentClassRepository.GetStudentClassesByYear(exitSurveyDetailsModel);
            //if(students != null && students.Count() > 0)
            //{
            //foreach(var student in students)
            //{
            //    StudentsForExitForm studentForExitForm = MapStudentClassToStudentForExitForm(student);
            //    if(studentForExitForm != null && !studentsForExitForm.Any(x=>x.ExitStudentId == studentForExitForm.ExitStudentId
            //    && x.StudentFullName == studentForExitForm.StudentFullName))
            //    {
            //        studentsForExitForm.Add(studentForExitForm);
            //    }
            //}

            // }
            // }

            //IList<StudentsForExitFormModel> studentsForExitFormModels = new List<StudentsForExitFormModel>();
            //if(studentsForExitForm != null && studentsForExitForm.Count() > 0)
            //{
            //    foreach(var user in studentsForExitForm)
            //    {
            //        StudentsForExitFormModel studentsForExitFormModel = new StudentsForExitFormModel();
            //        studentsForExitFormModel = user.ToModel();
            //        var checkIfSurveyFilled = this.exitSurveyDetailsRepository.GetExitSurveyDetailsByStudentId(studentsForExitFormModel.ExitStudentId);
            //        if (checkIfSurveyFilled != null)
            //            studentsForExitFormModel.IsExitSurveyFilled = 1;
            //        else
            //            studentsForExitFormModel.IsExitSurveyFilled = 0;
            //        studentsForExitFormModels.Add(studentsForExitFormModel);
            //    }
            //}
            //studentsForExitForm.ForEach((user) => studentsForExitFormModels.Add(user.ToModel()));
            return studentsForExitForm;
        }

        public IQueryable<ExitSurveyReportModel> GetExitSurveyReport(ExitSurveyRequestModel exitSurveyDetailsModel)
        {
            List<ExitSurveyReportModel> exitSurveyDetails = new List<ExitSurveyReportModel>();

            exitSurveyDetails = this.studentsForExitFormRepository.GetExitSurveyReport(exitSurveyDetailsModel).ToList();

            if (exitSurveyDetails != null && exitSurveyDetails.Count() > 0)
                return exitSurveyDetails.AsQueryable();
            else
                return null;
        }

        public StudentsForExitForm MapStudentClassToStudentForExitForm(StudentClass studentClass)
        {
            StudentsForExitForm studentForExitForm = null;
            var studentClassDetails = this.studentClassDetailRepository.GetStudentClassDetailById(studentClass.StudentId);
            var schoolDetails = this.schoolRepository.GetSchoolById(studentClass.SchoolId);
            var schoolClass = this.schoolClassRepository.GetSchoolClassById(studentClass.ClassId);
            var genderDetails = this.dataValueRepository.GetDataValueById(studentClass.Gender);
            var categoryDetails = (studentClassDetails != null) ? this.dataValueRepository.GetDataValueById(studentClassDetails.SocialCategory) : null;
            //var VTDetails = this.vtRepository.GetVocationalTrainerById();
            // var vtSchoolSectors = this.vtSchoolSectorRepository.GetVTSchoolSectorBySchoolIdANDSectorId()
            if (studentClass != null)
            {
                studentForExitForm = new StudentsForExitForm();
                studentForExitForm.StudentFullName = studentClass.FullName;
                studentForExitForm.ExitStudentId = studentClass.StudentId;
                studentForExitForm.FatherName = (studentClassDetails != null) ? studentClassDetails.FatherName : null;
                studentForExitForm.StudentUniqueId = (studentClassDetails != null) ? studentClassDetails.StudentRollNumber : null;
                studentForExitForm.NameOfSchool = (schoolDetails != null) ? schoolDetails.SchoolName : null;
                studentForExitForm.UdiseCode = (schoolDetails != null) ? schoolDetails.Udise : null;
                studentForExitForm.District = (schoolDetails != null) ? schoolDetails.DistrictCode : null;
                studentForExitForm.Class = (schoolClass != null) ? schoolClass.Name : null;
                studentForExitForm.Gender = (genderDetails != null) ? genderDetails.Name : null;
                studentForExitForm.DOB = studentClassDetails.DateOfBirth;
                studentForExitForm.Category = (categoryDetails != null) ? categoryDetails.Name : null;
            }
            return studentForExitForm;
        }

        /// <summary>
        /// Get list of StudentsForExitFormes by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<StudentsForExitFormModel> GetStudentsForExitFormByName(string studentsForExitFormName)
        {
            var studentsForExitFormes = this.studentsForExitFormRepository.GetStudentsForExitFormByName(studentsForExitFormName);

            IList<StudentsForExitFormModel> studentsForExitFormModels = new List<StudentsForExitFormModel>();
            studentsForExitFormes.ForEach((user) => studentsForExitFormModels.Add(user.ToModel()));

            return studentsForExitFormModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        public IQueryable<StudentsForExitFormModel> GetStudentsByNames(List<string> studentNames)
        {
            var studentsForExitFormes = this.studentsForExitFormRepository.GetStudentsByNames(studentNames);

            IList<StudentsForExitFormModel> studentsForExitFormModels = new List<StudentsForExitFormModel>();
            studentsForExitFormes.ForEach((user) => studentsForExitFormModels.Add(user.ToModel()));

            return studentsForExitFormModels.AsQueryable();
        }

        /// <summary>
        /// Get StudentsForExitForm by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentsForExitFormModel GetStudentsForExitFormById(Guid studentId)
        {
            StudentsForExitForm studentsForExitForm = this.studentsForExitFormRepository.GetStudentsForExitFormById(studentId);

            return (studentsForExitForm != null) ? studentsForExitForm.ToModel() : null;
        }

        /// <summary>
        /// Get StudentsForExitForm by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentsForExitFormModel> GetStudentsForExitFormByIdAsync(Guid studentId)
        {
            var studentsForExitForm = await this.studentsForExitFormRepository.GetStudentsForExitFormByIdAsync(studentId);

            return (studentsForExitForm != null) ? studentsForExitForm.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update StudentsForExitForm entity
        /// </summary>
        /// <param name="studentsForExitFormModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateStudentsForExitFormDetails(StudentsForExitFormModel studentsForExitFormModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                StudentsForExitForm studentsForExitForm = null;

                //Validate model data
                studentsForExitFormModel = studentsForExitFormModel.GetModelValidationErrors<StudentsForExitFormModel>();

                if (studentsForExitFormModel.ErrorMessages.Count > 0)
                {
                    response.Errors = studentsForExitFormModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (studentsForExitFormModel.RequestType == RequestType.Edit)
                {
                    studentsForExitForm = this.studentsForExitFormRepository.GetStudentsForExitFormById(studentsForExitFormModel.ExitStudentId);
                }
                else
                {
                    studentsForExitForm = new StudentsForExitForm();
                    studentsForExitForm.ExitStudentId = Guid.NewGuid();
                    studentsForExitFormModel.ExitStudentId = studentsForExitForm.ExitStudentId;
                }

                if (studentsForExitFormModel.ErrorMessages.Count == 0)
                {
                    bool isStudentsForExitFormExists = this.studentsForExitFormRepository.CheckStudentsForExitFormExistByName(studentsForExitFormModel);

                    if (isStudentsForExitFormExists)
                    {
                        response.Errors.Add(Constants.ExistMessage);
                    }
                }

                if (response.Errors.Count == 0)
                {
                    studentsForExitForm.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    studentsForExitForm = studentsForExitFormModel.FromModel(studentsForExitForm);

                    //Save Or Update studentsForExitForm details
                    bool isSaved = this.studentsForExitFormRepository.SaveOrUpdateStudentsForExitFormDetails(studentsForExitForm);

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
                throw new Exception("BAL > SaveOrUpdateStudentsForExitFormDetails", ex);
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

            return this.studentsForExitFormRepository.DeleteById(studentId);
        }

        /// <summary>
        /// Check duplicate StudentsForExitForm by Name
        /// </summary>
        /// <param name="studentsForExitFormModel"></param>
        /// <returns></returns>
        public bool CheckStudentsForExitFormExistByName(StudentsForExitFormModel studentsForExitFormModel)
        {
            return this.studentsForExitFormRepository.CheckStudentsForExitFormExistByName(studentsForExitFormModel);
        }
    }
}