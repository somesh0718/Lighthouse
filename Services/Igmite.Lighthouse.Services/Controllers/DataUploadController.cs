using ExcelDataReader;
using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Logging;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Igmite.Lighthouse.Services.Controllers
{
    [Route("LighthouseServices/[controller]"), ApiController]
    public class DataUploadController : ControllerBase
    {
        private readonly ISchoolVEInchargeManager schoolVEInchargeManager;
        private readonly ISchoolManager schoolManager;
        private readonly ISchoolCategoryManager schoolCategoryManager;
        private readonly IAcademicYearManager academicYearManager;
        private readonly IPhaseManager phaseManager;
        private readonly IDivisionManager divisionManager;
        private readonly IDistrictManager districtManager;
        private readonly IBlockManager blockManager;
        private readonly IClusterManager clusterManager;
        private readonly IEmployerManager employerManager;
        private readonly IHeadMasterManager headMasterManager;
        private readonly ISchoolVTPSectorManager schoolVTPSectorManager;
        private readonly ISectorManager sectorManager;
        private readonly IVocationalTrainingProviderManager vocationalTrainingProviderManager;
        private readonly IVCSchoolSectorManager vcSchoolSectorManager;
        private readonly IVocationalCoordinatorManager vocationalCoordinatorManager;
        private readonly IVocationalTrainerManager vocationalTrainerManager;
        private readonly IDataValueManager dataValueManager;
        private readonly IVTClassManager vtClassManager;
        private readonly ISchoolClassManager schoolClassManager;
        private readonly ISectionManager sectionManager;
        private readonly IVTPSectorManager vtpSectorManager;
        private readonly IVTSchoolSectorManager vtSchoolSectorManager;
        private readonly IStudentClassDetailManager studentClassDetailManager;
        private readonly IStudentClassManager studentClassManager;
        private readonly IJobRoleManager jobRoleManager;
        private readonly ICourseModuleManager courseModuleManager;
        private readonly IStudentsForExitFormManager studentsForExitFormManager;

        private Guid schoolCategoryId;
        private Guid academicYearId;
        private Guid phaseId;
        private Guid divisionId;
        private Guid blockId;
        private Guid clusterId;
        private string districtCode, genderId, socialcategoryId, natureAppointmentId, acanadmicQulificationId, professionalQualificationId, schoolTypeId, schoolManagementId, courseModuleId;
        private Guid schoolId;
        private Guid sectorId;
        private Guid VTPId;
        private Guid VCId;
        private Guid schoolVTPSectorId;
        private Guid VTId;
        private Guid classId;
        private Guid sectionId;
        private string stateCode;
        private Guid studentId;
        private Guid jobRoleId;
        private string uploadFolderPath = string.Empty;
        private SingularResponse<string> fileResponse;
        private string errorMessage = string.Empty;
        private bool isValidTemplate = true;
        private List<string> validationErrors = new List<string>();

        /// <summary>
        /// Initializes the Excel controller class.
        /// </summary>
        /// <param name="_excelUploadManager"></param>
        public DataUploadController(ISchoolManager _schoolManager, ISchoolCategoryManager _schoolCategoryManager, IAcademicYearManager _academicYearManager,
                                     IPhaseManager _phaseManager, IDivisionManager _divisionManager, IDistrictManager _districtManager, IBlockManager _blockManager, IClusterManager _clusterManager, ISchoolVEInchargeManager _schoolVEInchargeManager,
                                     IEmployerManager _employerManager, IHeadMasterManager _headMasterManager, ISchoolVTPSectorManager _schoolVTPSectorManager, ISectorManager _sectorManager,
                                     IVocationalTrainingProviderManager _vocationalTrainingProviderManager, IVCSchoolSectorManager _vcSchoolSectorManager, IVocationalCoordinatorManager _vocationalCoordinatorManager,
                                     IVocationalTrainerManager _vocationalTrainerManager, IDataValueManager _dataValueManager, IVTClassManager _vtClassManager, ISchoolClassManager _schoolClassManager,
                                     ISectionManager _sectionManager, IVTPSectorManager _vtpSectorManager, IVTSchoolSectorManager _vtSchoolSectorManager, IStudentClassDetailManager _studentClassDetailManager,
                                     IStudentClassManager _studentClassManager, IJobRoleManager _jobRoleManager, ICourseModuleManager _courseModuleManager, IStudentsForExitFormManager _studentsForExitFormManager)
        {
            this.schoolManager = _schoolManager;
            this.schoolCategoryManager = _schoolCategoryManager;
            this.academicYearManager = _academicYearManager;
            this.phaseManager = _phaseManager;
            this.divisionManager = _divisionManager;
            this.districtManager = _districtManager;
            this.blockManager = _blockManager;
            this.clusterManager = _clusterManager;
            this.schoolVEInchargeManager = _schoolVEInchargeManager;
            this.employerManager = _employerManager;
            this.headMasterManager = _headMasterManager;
            this.schoolVTPSectorManager = _schoolVTPSectorManager;
            this.sectorManager = _sectorManager;
            this.vocationalTrainingProviderManager = _vocationalTrainingProviderManager;
            this.vcSchoolSectorManager = _vcSchoolSectorManager;
            this.vocationalCoordinatorManager = _vocationalCoordinatorManager;
            this.vocationalTrainerManager = _vocationalTrainerManager;
            this.dataValueManager = _dataValueManager;
            this.vtClassManager = _vtClassManager;
            this.schoolClassManager = _schoolClassManager;
            this.sectionManager = _sectionManager;
            this.vtpSectorManager = _vtpSectorManager;
            this.vtSchoolSectorManager = _vtSchoolSectorManager;
            this.studentClassDetailManager = _studentClassDetailManager;
            this.studentClassManager = _studentClassManager;
            this.jobRoleManager = _jobRoleManager;
            this.courseModuleManager = _courseModuleManager;
            this.studentsForExitFormManager = _studentsForExitFormManager;
            this.uploadFolderPath = Path.Combine(Constants.RootPath, "DataUpload");
        }

        [HttpPost, Route("SchoolExcelUpload")]
        public SingularResponse<string> SchoolExcelUpload(IFormFile excelData)
        {
            DataTable dtSchool = new DataTable();

            try
            {
                // Reading School data from excel template
                dtSchool = this.ReadExcelTemplateData(excelData);

                #region Validate Student template format and data type

                List<string> columnNamesInDB = new List<string> { "SchoolName", "SchoolCategory", "SchoolType", "UDISE", "AcademicYear", "Phase", "Division", "District", "Block", "Cluster", "Village", "Panchayat", "Pincode", "Demography", "SchoolManagement", "State" };

                List<string> columnNamesInTemplate = dtSchool.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtSchool.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate Student template format and data type

                #region Get master data for Students

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct Phases
                var phases = this.phaseManager.GetPhases().Select(x => new PhaseModel { PhaseId = x.PhaseId, PhaseName = x.PhaseName }).Distinct().ToList();
                Dictionary<Guid, string> dicPhases = phases.ToDictionary(x => x.PhaseId, x => x.PhaseName.StringVal().ToUpper());

                // Get distinct Divisions
                var divisions = this.divisionManager.GetDivisions().Select(x => new DivisionModel { DivisionId = x.DivisionId, DivisionName = x.DivisionName, StateCode = x.StateCode }).Distinct().ToList();
                Dictionary<Guid, string> dicDivisions = divisions.ToDictionary(x => x.DivisionId, x => x.DivisionName.StringVal().ToUpper());

                // Get distinct Districts
                var districts = this.districtManager.GetDistricts().Select(x => new DistrictModel { DistrictCode = x.DistrictCode, DistrictName = x.DistrictName }).Distinct().ToList();
                Dictionary<string, string> dicDistricts = districts.ToDictionary(x => x.DistrictCode, x => x.DistrictName.StringVal().ToUpper());

                // Get distinct Blocks
                var blocks = this.blockManager.GetBlocks().Select(x => new BlockModel { BlockId = x.BlockId, BlockName = x.BlockName }).Distinct().ToList();
                Dictionary<Guid, string> dicBlocks = blocks.ToDictionary(x => x.BlockId, x => x.BlockName.StringVal().ToUpper());

                // Get distinct Clusters
                var clusters = this.clusterManager.GetClusters().Select(x => new ClusterModel { ClusterId = x.ClusterId, ClusterName = x.ClusterName }).Distinct().ToList();
                Dictionary<Guid, string> dicClusters = clusters.ToDictionary(x => x.ClusterId, x => x.ClusterName.StringVal().ToUpper());

                // Get distinct SchoolCategories
                var schoolCategories = this.schoolCategoryManager.GetSchoolCategories().Select(x => new SchoolCategoryModel { SchoolCategoryId = x.SchoolCategoryId, CategoryName = x.CategoryName }).Distinct().ToList();
                Dictionary<Guid, string> dicSchoolCategories = schoolCategories.ToDictionary(x => x.SchoolCategoryId, x => x.CategoryName.StringVal().ToUpper());

                // Get distinct SchoolTypes
                var schoolTypes = this.dataValueManager.GetDataValuesByType("SchoolType").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicSchoolTypes = schoolTypes.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct SchoolManagements
                var schoolManagements = this.dataValueManager.GetDataValuesByType("SchoolManagement").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicSchoolManagements = schoolManagements.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                #endregion Get master data for Students

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtSchool.Rows.Count; rowIndex++)
                    {
                        DataRow drSchool = dtSchool.Rows[rowIndex];
                        schoolCategoryId = academicYearId = phaseId = divisionId = Guid.Empty;

                        try
                        {
                            string academicYear = drSchool["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string phaseName = drSchool["Phase"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(phaseName))
                            {
                                if (dicPhases.ContainsValue(phaseName))
                                {
                                    phaseId = dicPhases.FirstOrDefault(x => x.Value == phaseName).Key;
                                }
                            }

                            string divisionName = drSchool["Division"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(divisionName))
                            {
                                if (dicDivisions.ContainsValue(divisionName))
                                {
                                    divisionId = dicDivisions.FirstOrDefault(x => x.Value == divisionName).Key;
                                }
                            }

                            string districtName = drSchool["District"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(districtName))
                            {
                                if (dicDistricts.ContainsValue(districtName))
                                {
                                    districtCode = dicDistricts.FirstOrDefault(x => x.Value == districtName).Key;

                                    if (string.IsNullOrEmpty(stateCode))
                                    {
                                        stateCode = divisions.FirstOrDefault(x => x.DivisionId == divisionId).StateCode;
                                    }
                                }
                            }

                            string blockName = drSchool["Block"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(blockName))
                            {
                                if (dicBlocks.ContainsValue(blockName))
                                {
                                    blockId = dicBlocks.FirstOrDefault(x => x.Value == blockName).Key;
                                }
                            }

                            string clusterName = drSchool["Cluster"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(clusterName))
                            {
                                if (dicClusters.ContainsValue(clusterName))
                                {
                                    clusterId = dicClusters.FirstOrDefault(x => x.Value == clusterName).Key;
                                }
                            }

                            string schoolCategoryName = drSchool["SchoolCategory"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(schoolCategoryName))
                            {
                                if (dicSchoolCategories.ContainsValue(schoolCategoryName))
                                {
                                    schoolCategoryId = dicSchoolCategories.FirstOrDefault(x => x.Value == schoolCategoryName).Key;
                                }
                            }

                            string schoolType = drSchool["SchoolType"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(schoolType))
                            {
                                if (dicSchoolTypes.ContainsValue(schoolType))
                                {
                                    schoolTypeId = dicSchoolTypes.FirstOrDefault(x => x.Value == schoolType).Key;
                                }
                            }

                            string schoolManagement = drSchool["SchoolManagement"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(schoolManagement))
                            {
                                if (dicSchoolManagements.ContainsValue(schoolManagement))
                                {
                                    schoolManagementId = dicSchoolManagements.FirstOrDefault(x => x.Value == schoolManagement).Key;
                                }
                            }

                            bool isBlockCusterRequired = true;
                            if (stateCode == "GJ")
                            {
                                if (blockId == Guid.Empty || clusterId == Guid.Empty)
                                    isBlockCusterRequired = false;
                            }

                            if (!string.IsNullOrEmpty(drSchool["SchoolName"].StringVal()) && schoolCategoryId != Guid.Empty && !string.IsNullOrEmpty(schoolTypeId) && !string.IsNullOrEmpty(drSchool["UDISE"].StringVal()) && academicYearId != Guid.Empty && phaseId != Guid.Empty && divisionId != Guid.Empty && !string.IsNullOrEmpty(districtCode) && (stateCode == "GJ" ? isBlockCusterRequired == false : isBlockCusterRequired == true) && !string.IsNullOrEmpty(schoolManagementId) && !string.IsNullOrEmpty(stateCode))
                            {
                                SchoolModel schoolModelEntity = new SchoolModel
                                {
                                    SchoolName = drSchool["SchoolName"].StringVal(),
                                    SchoolCategoryId = schoolCategoryId,
                                    SchoolTypeId = schoolTypeId,
                                    Udise = drSchool["UDISE"].StringVal().ToUpper(),
                                    AcademicYearId = academicYearId,
                                    PhaseId = phaseId,
                                    StateCode = stateCode,
                                    StateName = stateCode,
                                    DivisionId = divisionId,
                                    DistrictCode = districtCode,
                                    BlockId = blockId,
                                    ClusterId = clusterId,
                                    BlockName = drSchool["Block"].StringVal().ToTitleCase(),
                                    Village = drSchool["Village"].StringVal().ToTitleCase(),
                                    Panchayat = drSchool["Panchayat"].StringVal().ToTitleCase(),
                                    Pincode = drSchool["Pincode"].StringVal(),
                                    Demography = drSchool["Demography"].StringVal().ToTitleCase(),
                                    SchoolManagementId = schoolManagementId,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                if (Guid.Equals(blockId, Guid.Empty))
                                    schoolModelEntity.BlockId = null;

                                if (Guid.Equals(clusterId, Guid.Empty))
                                    schoolModelEntity.ClusterId = null;

                                var schoolResponse = schoolManager.SaveOrUpdateSchoolDetails(schoolModelEntity);
                                if (schoolResponse.Errors.Count > 0)
                                {
                                    drSchool["Status"] = "Failed";
                                    drSchool["Exception"] = string.Join(",", schoolResponse.Errors);
                                }
                                else
                                {
                                    drSchool["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (string.IsNullOrEmpty(drSchool["SchoolName"].StringVal()))
                                {
                                    this.errorMessage += "SchoolName is required\n";
                                }
                                if (schoolCategoryId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Category Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(schoolTypeId))
                                {
                                    this.errorMessage += "Invalid School Type Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drSchool["UDISE"].StringVal()))
                                {
                                    this.errorMessage += "UDISE is required\n";
                                }
                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Academic Year Which is not found in Database\n";
                                }
                                if (phaseId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Phase Name Which is not found in Database\n";
                                }
                                if (divisionId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Division Name Which is not found in Database\n";
                                }
                                if (stateCode == "GJ" && blockId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Block Name Which is not found in Database\n";
                                }
                                if (stateCode == "GJ" && clusterId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Cluster Name Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(districtCode))
                                {
                                    this.errorMessage += "Invalid District Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(schoolManagementId))
                                {
                                    this.errorMessage += "Invalid School Management Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(stateCode))
                                {
                                    this.errorMessage += "Invalid State Name Which is not found in Database\n";
                                }

                                drSchool["Status"] = "Failed";
                                drSchool["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drSchool["Status"] = "Failed";
                            drSchool["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("School-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtSchool, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("HeadMastersExcelUpload")]
        public SingularResponse<string> HeadMastersExcelUpload(IFormFile excelData)
        {
            DataTable dtHM = new DataTable();
            try
            {
                // Reading HeadMaster data from excel template
                dtHM = this.ReadExcelTemplateData(excelData);

                #region Validate HeadMaster template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "SchoolName", "FirstName", "MiddleName", "LastName", "Mobile", "Mobile1", "Email", "Gender", "YearsInSchool", "DateOfJoining", "DateOfResignation" };

                List<string> columnNamesInTemplate = dtHM.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtHM.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate HeadMaster template format and data type

                #region Get master data for HeadMasters

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtHM.Select().AsEnumerable().Select(s => s["SchoolName"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise) }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                // Get distinct VocationalTrainers
                List<Guid> schoolIds = schools.Select(x => x.SchoolId).ToList();
                var VTIdsBySchoolIds = this.vocationalTrainerManager.GetVTIdsBySchoolIds(schoolIds).Distinct().ToList();

                // Get distinct Genders
                var genders = this.dataValueManager.GetDataValuesByType("Gender").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicGenders = genders.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                #endregion Get master data for HeadMasters

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtHM.Rows.Count; rowIndex++)
                    {
                        DataRow drHM = dtHM.Rows[rowIndex];
                        schoolId = VTId = Guid.Empty;

                        try
                        {
                            string academicYear = drHM["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string schoolName = string.Format("{0}-{1}", drHM["SchoolName"].StringVal(), drHM["UDISE"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                    VTId = VTIdsBySchoolIds.FirstOrDefault(x => x.Key == schoolId).Value;
                                }
                            }

                            string gender = drHM["Gender"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(gender))
                            {
                                if (dicGenders.ContainsValue(gender))
                                {
                                    genderId = dicGenders.FirstOrDefault(x => x.Value == gender).Key;
                                }
                            }

                            DateTime? dateOfJoining = Constants.GetDateValue(drHM["DateOfJoining"].StringVal());
                            DateTime? dateOfResignation = Constants.GetDateValue(drHM["DateOfResignation"].StringVal());

                            if (academicYearId != Guid.Empty && schoolId != Guid.Empty && !string.IsNullOrEmpty(drHM["FirstName"].StringVal()) && !string.IsNullOrEmpty(drHM["LastName"].StringVal()) && !string.IsNullOrEmpty(drHM["Mobile"].StringVal()) && !string.IsNullOrEmpty(drHM["Email"].StringVal()) && !string.IsNullOrEmpty(genderId) && dateOfJoining.HasValue)
                            {
                                HeadMasterModel headMasterModelEntity = new HeadMasterModel
                                {
                                    AcademicYearId = academicYearId,
                                    SchoolId = schoolId,
                                    FirstName = drHM["FirstName"].StringVal().ToTitleCase(),
                                    MiddleName = drHM["MiddleName"].StringVal().ToTitleCase(),
                                    LastName = drHM["LastName"].StringVal().ToTitleCase(),
                                    Mobile = drHM["Mobile"].StringVal(),
                                    Mobile1 = drHM["Mobile1"].StringVal(),
                                    Email = drHM["Email"].StringVal().ToLower(),
                                    Gender = genderId,
                                    YearsInSchool = Convert.ToInt32(drHM["YearsInSchool"].StringVal()),
                                    DateOfJoining = dateOfJoining.Value,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true
                                };

                                headMasterModelEntity.FullName = string.Format("{0} {1} {2}", headMasterModelEntity.FirstName, headMasterModelEntity.MiddleName, headMasterModelEntity.LastName).TrimSpaces();

                                var hmResponse = headMasterManager.SaveOrUpdateHeadMasterDetails(headMasterModelEntity);

                                if (hmResponse.Errors.Count > 0)
                                {
                                    drHM["Status"] = "Failed";
                                    drHM["Exception"] = string.Join(",", hmResponse.Errors);
                                }
                                else
                                {
                                    drHM["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid AcademicYear Which is not found in Database\n";
                                }
                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Name Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drHM["FirstName"].StringVal()))
                                {
                                    this.errorMessage += "First Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drHM["LastName"].StringVal()))
                                {
                                    this.errorMessage += "Last Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drHM["Mobile"].StringVal()))
                                {
                                    this.errorMessage += "Mobile is required\n";
                                }
                                if (string.IsNullOrEmpty(drHM["Email"].StringVal()))
                                {
                                    this.errorMessage += "Email is required\n";
                                }
                                if (string.IsNullOrEmpty(genderId))
                                {
                                    this.errorMessage += "Invalid Gender Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drHM["DateOfJoining"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Joining is required\n";
                                }
                                if (!string.IsNullOrEmpty(drHM["DateOfJoining"].StringVal()) && !dateOfJoining.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Joining in the format 'DD/MM/YYYY'\n";
                                }
                                if (!string.IsNullOrEmpty(drHM["DateOfResignation"].StringVal()) && !dateOfResignation.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Resignation in the format 'DD/MM/YYYY'\n";
                                }

                                drHM["Status"] = "Failed";
                                drHM["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drHM["Status"] = "Failed";
                            drHM["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("HeadMaster-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtHM, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("SchoolIVEInchargeExcelUpload")]
        public SingularResponse<string> SchoolIVEInchargeExcelUpload(IFormFile excelData)
        {
            DataTable dtSchoolVEIncharge = new DataTable();
            try
            {
                // Reading SchoolVEIncharge data from excel template
                dtSchoolVEIncharge = this.ReadExcelTemplateData(excelData);

                #region Validate Student template format and data type

                List<string> columnNamesInDB = new List<string> { "SchoolName", "VTName", "FirstName", "MiddleName", "LastName", "Mobile", "Mobile1", "Email", "Gender", "DateOfJoining", "DateOfResignation" };

                List<string> columnNamesInTemplate = dtSchoolVEIncharge.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtSchoolVEIncharge.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate Student template format and data type

                #region Get master data for SchoolVEIncharges

                // Get distinct VocationalTrainers
                List<string> vocationalTrainersInTemplate = dtSchoolVEIncharge.Select().AsEnumerable().Select(s => s["VTName"].StringVal()).Distinct().ToList();

                var vocationalTrainers = this.vocationalTrainerManager.GetVocationalTrainersByNames(vocationalTrainersInTemplate).Select(x => new VocationalTrainerModel { VTId = x.VTId, FullName = string.Format("{0}-{1}", x.FullName, x.Email) }).Distinct().ToList();
                Dictionary<Guid, string> dicVocationalTrainers = vocationalTrainers.ToDictionary(x => x.VTId, x => x.FullName.StringVal().ToUpper());

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtSchoolVEIncharge.Select().AsEnumerable().Select(s => s["SchoolName"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise) }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                // Get distinct Genders
                var genders = this.dataValueManager.GetDataValuesByType("Gender").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicGenders = genders.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                #endregion Get master data for SchoolVEIncharges

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtSchoolVEIncharge.Rows.Count; rowIndex++)
                    {
                        DataRow drSchoolVEIncharge = dtSchoolVEIncharge.Rows[rowIndex];
                        schoolId = VTId = Guid.Empty;

                        try
                        {
                            string schoolName = string.Format("{0}-{1}", drSchoolVEIncharge["SchoolName"].StringVal(), drSchoolVEIncharge["UDISE"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                }
                            }

                            string vtName = string.Format("{0}-{1}", drSchoolVEIncharge["VTName"].StringVal(), drSchoolVEIncharge["VTEmailId"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(vtName))
                            {
                                if (dicVocationalTrainers.ContainsValue(vtName))
                                {
                                    VTId = dicVocationalTrainers.FirstOrDefault(x => x.Value == vtName).Key;
                                }
                            }

                            string gender = drSchoolVEIncharge["Gender"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(gender))
                            {
                                if (dicGenders.ContainsValue(gender))
                                {
                                    genderId = dicGenders.FirstOrDefault(x => x.Value == gender).Key;
                                }
                            }

                            DateTime? dateOfJoining = Constants.GetDateValue(drSchoolVEIncharge["DateOfJoining"].StringVal());
                            DateTime? dateOfResignation = Constants.GetDateValue(drSchoolVEIncharge["DateOfResignation"].StringVal());

                            //SchoolName	VTName	FirstName	LastName	Mobile	Email	Gender	DateOfJoining
                            if (schoolId != Guid.Empty && VTId != Guid.Empty && !string.IsNullOrEmpty(drSchoolVEIncharge["FirstName"].StringVal()) && !string.IsNullOrEmpty(drSchoolVEIncharge["LastName"].StringVal()) && !string.IsNullOrEmpty(drSchoolVEIncharge["Mobile"].StringVal()) && !string.IsNullOrEmpty(drSchoolVEIncharge["Email"].StringVal()) && !string.IsNullOrEmpty(genderId) && dateOfJoining.HasValue)
                            {
                                SchoolVEInchargeModel schoolVEInchargeModelEntity = new SchoolVEInchargeModel
                                {
                                    SchoolId = schoolId,
                                    FirstName = drSchoolVEIncharge["FirstName"].StringVal().ToTitleCase(),
                                    MiddleName = drSchoolVEIncharge["MiddleName"].StringVal().ToTitleCase(),
                                    LastName = drSchoolVEIncharge["LastName"].StringVal().ToTitleCase(),
                                    Mobile = drSchoolVEIncharge["Mobile"].StringVal(),
                                    Mobile1 = drSchoolVEIncharge["Mobile1"].StringVal(),
                                    Email = drSchoolVEIncharge["Email"].StringVal().ToLower(),
                                    Gender = genderId,
                                    VTId = VTId,
                                    DateOfJoining = dateOfJoining.Value,
                                    DateOfResignationFromRoleSchool = dateOfResignation,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                schoolVEInchargeModelEntity.FullName = string.Format("{0} {1} {2}", schoolVEInchargeModelEntity.FirstName, schoolVEInchargeModelEntity.MiddleName, schoolVEInchargeModelEntity.LastName).TrimSpaces();

                                var schoolVEInchargeResponse = schoolVEInchargeManager.SaveOrUpdateSchoolVEInchargeDetails(schoolVEInchargeModelEntity);

                                if (schoolVEInchargeResponse.Errors.Count > 0)
                                {
                                    drSchoolVEIncharge["Status"] = "Failed";
                                    drSchoolVEIncharge["Exception"] = string.Join(",", schoolVEInchargeResponse.Errors);
                                }
                                else
                                {
                                    drSchoolVEIncharge["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Name Which is not found in Database\n";
                                }
                                if (VTId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VT Name Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drSchoolVEIncharge["FirstName"].StringVal()))
                                {
                                    this.errorMessage += "First Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drSchoolVEIncharge["LastName"].StringVal()))
                                {
                                    this.errorMessage += "Last Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drSchoolVEIncharge["Mobile"].StringVal()))
                                {
                                    this.errorMessage += "Mobile is required\n";
                                }
                                if (string.IsNullOrEmpty(drSchoolVEIncharge["Email"].StringVal()))
                                {
                                    this.errorMessage += "Email is required\n";
                                }
                                if (string.IsNullOrEmpty(genderId))
                                {
                                    this.errorMessage += "Gender is required\n";
                                }
                                if (string.IsNullOrEmpty(drSchoolVEIncharge["DateOfJoining"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Joining is required\n";
                                }
                                if (!string.IsNullOrEmpty(drSchoolVEIncharge["DateOfJoining"].StringVal()) && !dateOfJoining.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Joining in the format 'DD/MM/YYYY'\n";
                                }
                                if (!string.IsNullOrEmpty(drSchoolVEIncharge["DateOfResignation"].StringVal()) && !dateOfResignation.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Resignation in the format 'DD/MM/YYYY'\n";
                                }

                                drSchoolVEIncharge["Status"] = "Failed";
                                drSchoolVEIncharge["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drSchoolVEIncharge["Status"] = "Failed";
                            drSchoolVEIncharge["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("SchoolIVEIncharge-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtSchoolVEIncharge, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VTSchoolSectorsExcelUpload")]
        public SingularResponse<string> VTSchoolSectorsExcelUpload(IFormFile excelData)
        {
            DataTable dtVTSchoolSector = new DataTable();
            try
            {
                // Reading dtVTSchoolSector data from excel template
                dtVTSchoolSector = this.ReadExcelTemplateData(excelData);

                #region Validate VTSchoolSector template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "VTName", "SchoolName", "SectorName", "DateOfAllocation", "DateOfRemoval", "JobRoleName" };

                List<string> columnNamesInTemplate = dtVTSchoolSector.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtVTSchoolSector.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate VTSchoolSector template format and data type

                #region Get master data for VTSchoolSectors

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName, IsCurrentAcademicYear = x.IsCurrentAcademicYear }).Distinct().ToList();
                Guid academicYearId = academicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true).AcademicYearId;

                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct VocationalCoordinators
                List<string> vocationalCoordinatorsInTemplate = dtVTSchoolSector.Select().AsEnumerable().Select(s => s["VCEmailId"].StringVal()).Distinct().ToList();

                var vocationalCoordinators = this.vocationalCoordinatorManager.GetVocationalCoordinatorsByEmails(academicYearId, vocationalCoordinatorsInTemplate).Distinct().ToList();
                Dictionary<Guid, string> dicVocationalCoordinators = vocationalCoordinators.ToDictionary(x => x.VCId, x => x.EmailId.StringVal().ToUpper());

                // Get distinct VocationalTrainers
                List<string> vocationalTrainersInTemplate = dtVTSchoolSector.Select().AsEnumerable().Select(s => s["VTEmailId"].StringVal()).Distinct().ToList();

                var vocationalTrainers = this.vocationalTrainerManager.GetVocationalTrainersByEmails(academicYearId, vocationalTrainersInTemplate).Distinct().ToList();
                Dictionary<Guid, string> dicVocationalTrainers = vocationalTrainers.ToDictionary(x => x.VTId, x => x.Email.StringVal().ToUpper());

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtVTSchoolSector.Select().AsEnumerable().Select(s => s["SchoolName"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise) }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                // Get distinct Sectors
                var sectors = this.sectorManager.GetSectors().Select(x => new SectorModel { SectorId = x.SectorId, SectorName = x.SectorName }).Distinct().ToList();
                Dictionary<Guid, string> dicSectors = sectors.ToDictionary(x => x.SectorId, x => x.SectorName.StringVal().ToUpper());

                // Get distinct JobRoles
                var jobRoles = this.jobRoleManager.GetJobRoles().Select(x => new JobRoleModel { JobRoleId = x.JobRoleId, JobRoleName = x.JobRoleName }).Distinct().ToList();
                Dictionary<Guid, string> dicJobRoles = jobRoles.ToDictionary(x => x.JobRoleId, x => x.JobRoleName.StringVal().ToUpper());

                #endregion Get master data for VTSchoolSectors

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtVTSchoolSector.Rows.Count; rowIndex++)
                    {
                        DataRow drVTSchoolSector = dtVTSchoolSector.Rows[rowIndex];
                        academicYearId = VTId = schoolId = sectorId = jobRoleId = Guid.Empty;

                        try
                        {
                            string academicYear = drVTSchoolSector["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string vcEmailId = drVTSchoolSector["VCEmailId"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vcEmailId))
                            {
                                if (dicVocationalCoordinators.ContainsValue(vcEmailId))
                                {
                                    VCId = dicVocationalCoordinators.FirstOrDefault(x => x.Value == vcEmailId).Key;
                                }
                            }

                            string vtEmailId = drVTSchoolSector["VTEmailId"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vtEmailId))
                            {
                                if (dicVocationalTrainers.ContainsValue(vtEmailId))
                                {
                                    VTId = dicVocationalTrainers.FirstOrDefault(x => x.Value == vtEmailId).Key;
                                }
                            }

                            string schoolName = string.Format("{0}-{1}", drVTSchoolSector["SchoolName"].StringVal(), drVTSchoolSector["UDISE"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                }
                            }

                            string sectorName = drVTSchoolSector["SectorName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectorName))
                            {
                                if (dicSectors.ContainsValue(sectorName))
                                {
                                    sectorId = dicSectors.FirstOrDefault(x => x.Value == sectorName).Key;
                                }
                            }

                            string jobRoleName = drVTSchoolSector["JobRoleName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(jobRoleName))
                            {
                                if (dicJobRoles.ContainsValue(jobRoleName))
                                {
                                    jobRoleId = dicJobRoles.FirstOrDefault(x => x.Value == jobRoleName).Key;
                                }
                            }

                            DateTime? dateOfAllocation = Constants.GetDateValue(drVTSchoolSector["DateOfAllocation"].StringVal());
                            DateTime? dateOfRemoval = Constants.GetDateValue(drVTSchoolSector["DateOfRemoval"].StringVal());

                            var trainerMappedWithCoordinator = vocationalTrainers.FirstOrDefault(vt => vt.AcademicYearId == academicYearId && vt.VCId == VCId && vt.VTId == VTId);

                            if (trainerMappedWithCoordinator != null && academicYearId != Guid.Empty && VTId != Guid.Empty && schoolId != Guid.Empty && sectorId != Guid.Empty && jobRoleId != Guid.Empty && dateOfAllocation.HasValue)
                            {
                                VTSchoolSectorModel vTSchoolSectorModelEntity = new VTSchoolSectorModel
                                {
                                    AcademicYearId = academicYearId,
                                    VTId = VTId,
                                    SchoolId = schoolId,
                                    SectorId = sectorId,
                                    JobRoleId = jobRoleId,
                                    DateOfAllocation = dateOfAllocation.Value,
                                    DateOfRemoval = dateOfRemoval,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var vtSchoolSectorResponse = vtSchoolSectorManager.SaveOrUpdateVTSchoolSectorDetails(vTSchoolSectorModelEntity);
                                if (vtSchoolSectorResponse.Errors.Count > 0)
                                {
                                    drVTSchoolSector["Status"] = "Failed";
                                    drVTSchoolSector["Exception"] = string.Join(",", vtSchoolSectorResponse.Errors);
                                }
                                else
                                {
                                    drVTSchoolSector["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;

                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage = "Invalid Academic Year Which is not found in Database\n";
                                }
                                if (VTId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VTName Which is not found in Database\n";
                                }
                                if (trainerMappedWithCoordinator == null)
                                {
                                    this.errorMessage += "Invalid VT & VC mapping Which is not found in Database\n";
                                }
                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid SchoolName Which is not found in Database\n";
                                }
                                if (sectorId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Sector Name Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drVTSchoolSector["DateOfAllocation"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Allocation is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTSchoolSector["JobRoleName"].StringVal()))
                                {
                                    this.errorMessage += "Job Role is required\n";
                                }
                                if (!string.IsNullOrEmpty(drVTSchoolSector["DateOfAllocation"].StringVal()) && !dateOfAllocation.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Allocation in the format 'DD/MM/YYYY'\n";
                                }
                                if (!string.IsNullOrEmpty(drVTSchoolSector["DateOfRemoval"].StringVal()) && !dateOfRemoval.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Removal in the format 'DD/MM/YYYY'\n";
                                }

                                drVTSchoolSector["Status"] = "Failed";
                                drVTSchoolSector["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drVTSchoolSector["Status"] = "Failed";
                            drVTSchoolSector["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("VTSchoolSector-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtVTSchoolSector, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("SectorJobRolesExcelUpload")]
        public SingularResponse<string> SectorJobRolesExcelUpload(IFormFile excelData)
        {
            DataTable dtSectorJobRole = new DataTable();
            try
            {
                // Reading SectorJobRoles data from excel template
                dtSectorJobRole = this.ReadExcelTemplateData(excelData);

                #region Validate SectorJobRoles template format and data type

                List<string> columnNamesInDB = new List<string> { "SectorName", "JobRoleName", "QPCode", "DisplayOrder" };

                List<string> columnNamesInTemplate = dtSectorJobRole.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtSectorJobRole.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate SectorJobRoles template format and data type

                #region Get master data for SectorJobRoles

                // Get distinct Sectors
                var sectors = this.sectorManager.GetSectors().Select(x => new SectorModel { SectorId = x.SectorId, SectorName = x.SectorName }).Distinct().ToList();
                Dictionary<Guid, string> dicSectors = sectors.ToDictionary(x => x.SectorId, x => x.SectorName.StringVal().ToUpper());

                #endregion Get master data for SectorJobRoles

                //SectorName	JobRoleName	QPCode	DisplayOrder

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtSectorJobRole.Rows.Count; rowIndex++)
                    {
                        DataRow drSectorJobRole = dtSectorJobRole.Rows[rowIndex];
                        try
                        {
                            string sectorName = drSectorJobRole["SectorName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectorName))
                            {
                                if (dicSectors.ContainsValue(sectorName))
                                {
                                    sectorId = dicSectors.FirstOrDefault(x => x.Value == sectorName).Key;
                                }
                            }

                            int displayOrder = Convert.ToInt32(drSectorJobRole["DisplayOrder"]);

                            if (sectorId != Guid.Empty && !string.IsNullOrEmpty(drSectorJobRole["JobRoleName"].StringVal()) && !string.IsNullOrEmpty(drSectorJobRole["QPCode"].StringVal()))
                            {
                                JobRoleModel jobRoleModel = new JobRoleModel
                                {
                                    JobRoleId = Guid.NewGuid(),
                                    SectorId = sectorId,
                                    JobRoleName = drSectorJobRole["JobRoleName"].StringVal(),
                                    QPCode = drSectorJobRole["QPCode"].StringVal(),
                                    DisplayOrder = Convert.ToInt32(drSectorJobRole["DisplayOrder"].StringVal()),
                                    Remarks = null,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var sectorJobRoleResponse = jobRoleManager.SaveOrUpdateJobRoleDetails(jobRoleModel);

                                if (sectorJobRoleResponse.Errors.Count > 0)
                                {
                                    drSectorJobRole["Status"] = "Failed";
                                    drSectorJobRole["Exception"] = string.Join(",", sectorJobRoleResponse.Errors);
                                }
                                else
                                {
                                    drSectorJobRole["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (sectorId == Guid.Empty)
                                {
                                    this.errorMessage = "Invalid Sector Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drSectorJobRole["JobRoleName"].StringVal()))
                                {
                                    this.errorMessage = "JobRoleName is required\n";
                                }
                                if (string.IsNullOrEmpty(drSectorJobRole["QPCode"].StringVal()))
                                {
                                    this.errorMessage = "QPCode is required\n";
                                }

                                drSectorJobRole["Status"] = "Failed";
                                drSectorJobRole["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drSectorJobRole["Status"] = "Failed";
                            drSectorJobRole["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("JobRole-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtSectorJobRole, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VCSchoolSectorsExcelUpload")]
        public SingularResponse<string> VCSchoolSectorsExcelUpload(IFormFile excelData)
        {
            DataTable dtVCSchoolSector = new DataTable();
            try
            {
                // Reading VCSchoolSector data from excel template
                dtVCSchoolSector = this.ReadExcelTemplateData(excelData);

                #region Validate VCSchoolSector template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "VCName", "SchoolName", "VTPShortName", "SectorName", "DateOfAllocation", "DateOfRemoval" };

                List<string> columnNamesInTemplate = dtVCSchoolSector.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtVCSchoolSector.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate VCSchoolSector template format and data type

                #region Get master data for VCSchoolSectors

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct VTPs
                var vtps = this.vocationalTrainingProviderManager.GetVTPList().Select(x => new VocationalTrainingProviderModel { VTPId = x.Id, VTPShortName = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicVTPs = vtps.ToDictionary(x => x.VTPId, x => x.VTPShortName.StringVal().ToUpper());

                // Get distinct Sectors
                var sectors = this.sectorManager.GetSectors().Select(x => new SectorModel { SectorId = x.SectorId, SectorName = x.SectorName }).Distinct().ToList();
                Dictionary<Guid, string> dicSectors = sectors.ToDictionary(x => x.SectorId, x => x.SectorName.StringVal().ToUpper());

                // Get distinct VocationalCoordinators
                List<string> vcNamesInTemplate = dtVCSchoolSector.Select().AsEnumerable().Select(s => s["VCName"].StringVal()).Distinct().ToList();

                var vocationalCoordinators = this.vocationalCoordinatorManager.GetVocationalCoordinatorsByNames(vcNamesInTemplate).Select(x => new VocationalCoordinatorModel { VCId = x.VCId, FullName = string.Format("{0}-{1}", x.FullName, x.EmailId) }).Distinct().ToList();
                Dictionary<Guid, string> dicVocationalCoordinators = vocationalCoordinators.ToDictionary(x => x.VCId, x => x.FullName.StringVal().ToUpper());

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtVCSchoolSector.Select().AsEnumerable().Select(s => s["SchoolName"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise) }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                // Get distinct VC List
                var vcList = this.vocationalCoordinatorManager.GetVCList().ToList();

                #endregion Get master data for VCSchoolSectors

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtVCSchoolSector.Rows.Count; rowIndex++)
                    {
                        DataRow drVCSchoolSector = dtVCSchoolSector.Rows[rowIndex];
                        academicYearId = VCId = schoolId = VTPId = sectorId = schoolVTPSectorId = Guid.Empty;

                        try
                        {
                            string academicYear = drVCSchoolSector["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string schoolName = string.Format("{0}-{1}", drVCSchoolSector["SchoolName"].StringVal(), drVCSchoolSector["UDISE"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                }
                            }

                            string vtpName = drVCSchoolSector["VTPShortName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vtpName))
                            {
                                if (dicVTPs.ContainsValue(vtpName))
                                {
                                    VTPId = dicVTPs.FirstOrDefault(x => x.Value == vtpName).Key;
                                }
                            }

                            string sectorName = drVCSchoolSector["SectorName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectorName))
                            {
                                if (dicSectors.ContainsValue(sectorName))
                                {
                                    sectorId = dicSectors.FirstOrDefault(x => x.Value == sectorName).Key;
                                }
                            }

                            string vcName = string.Format("{0}-{1}", drVCSchoolSector["VCName"].StringVal().ToUpper(), drVCSchoolSector["VCEmailId"].StringVal().ToUpper());
                            if (!string.IsNullOrEmpty(vcName))
                            {
                                if (dicVocationalCoordinators.ContainsValue(vcName))
                                {
                                    VCId = dicVocationalCoordinators.FirstOrDefault(x => x.Value == vcName).Key;
                                }
                            }

                            if (schoolId != Guid.Empty && VTPId != Guid.Empty && sectorId != Guid.Empty)
                            {
                                SchoolVTPSectorModel schoolVTPSectorModel = this.schoolVTPSectorManager.GetSchoolVTPSectorsBy3Ids(academicYearId, schoolId, VTPId, sectorId);

                                if (schoolVTPSectorModel != null)
                                {
                                    schoolVTPSectorId = schoolVTPSectorModel.SchoolVTPSectorId;
                                }
                            }

                            DateTime? dateOfAllocation = Constants.GetDateValue(drVCSchoolSector["DateOfAllocation"].StringVal());

                            VocationalCoordinatorModel vcItem = vcList.FirstOrDefault(v => v.AcademicYearId == academicYearId && v.VTPId == VTPId);

                            if (academicYearId != Guid.Empty && VCId != Guid.Empty && schoolId != Guid.Empty && VTPId != Guid.Empty && sectorId != Guid.Empty && dateOfAllocation.HasValue && schoolVTPSectorId != Guid.Empty && vcItem != null)
                            {
                                VCSchoolSectorModel vCSchoolSectorModelEntity = new VCSchoolSectorModel
                                {
                                    AcademicYearId = academicYearId,
                                    VCId = VCId,
                                    SchoolVTPSectorId = schoolVTPSectorId,
                                    DateOfAllocation = dateOfAllocation.Value,
                                    DateOfRemoval = null,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var vcSchoolSectorResponse = vcSchoolSectorManager.SaveOrUpdateVCSchoolSectorDetails(vCSchoolSectorModelEntity);
                                if (vcSchoolSectorResponse.Errors.Count > 0)
                                {
                                    drVCSchoolSector["Status"] = "Failed";
                                    drVCSchoolSector["Exception"] = string.Join(",", vcSchoolSectorResponse.Errors);
                                }
                                else
                                {
                                    drVCSchoolSector["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;

                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Academic Year Which is not found in Database\n";
                                }
                                if (VCId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VC Name Which is not found in Database\n";
                                }
                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Name Which is not found in Database\n";
                                }
                                if (VTPId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VTPShortName Which is not found in Database\n";
                                }
                                if (sectorId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Sector Name Which is not found in Database\n";
                                }
                                if (schoolVTPSectorId == Guid.Empty)
                                {
                                    this.errorMessage += "School Name Not Registered in School VTP Sector Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drVCSchoolSector["DateOfAllocation"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Allocation is required\n";
                                }
                                if (!string.IsNullOrEmpty(drVCSchoolSector["DateOfAllocation"].StringVal()) && !dateOfAllocation.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Allocation in the format 'DD/MM/YYYY'\n";
                                }
                                if (vcItem == null)
                                {
                                    this.errorMessage += "VTP & VC mapping does not exists, Please check Vocational Coordinator master page\n";
                                }

                                drVCSchoolSector["Status"] = "Failed";
                                drVCSchoolSector["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drVCSchoolSector["Status"] = "Failed";
                            drVCSchoolSector["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("VCSchoolSectors-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtVCSchoolSector, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VocationalCoordinatorsExcelUpload")]
        public SingularResponse<string> VocationalCoordinatorsExcelUpload(IFormFile excelData)
        {
            DataTable dtVC = new DataTable();

            try
            {
                // Reading Vocational Coordinator data from excel template
                dtVC = this.ReadExcelTemplateData(excelData);

                #region Validate Vocational Coordinator template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "VTPShortName", "FirstName", "MiddleName", "LastName", "Mobile", "Mobile1", "Email", "Gender", "DateOfJoining", "DateOfResignation", "NatureOfAppointment" };

                List<string> columnNamesInTemplate = dtVC.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtVC.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate Vocational Coordinator template format and data type

                #region Get master data for Vocational Coordinators

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct VTPs
                var vtps = this.vocationalTrainingProviderManager.GetVTPList().Select(x => new VocationalTrainingProviderModel { VTPId = x.Id, VTPShortName = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicVTPs = vtps.ToDictionary(x => x.VTPId, x => x.VTPShortName.StringVal().ToUpper());

                // Get distinct SchoolTypes
                var genders = this.dataValueManager.GetDataValuesByType("Gender").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicGenders = genders.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct SchoolManagements
                var natureOfAppointments = this.dataValueManager.GetDataValuesByType("NatureOfAppointment").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicNatureOfAppointments = natureOfAppointments.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                #endregion Get master data for Vocational Coordinators

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtVC.Rows.Count; rowIndex++)
                    {
                        DataRow drVC = dtVC.Rows[rowIndex];
                        VTPId = Guid.Empty;

                        try
                        {
                            string academicYear = drVC["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string vtpName = drVC["VTPShortName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vtpName))
                            {
                                if (dicVTPs.ContainsValue(vtpName))
                                {
                                    VTPId = dicVTPs.FirstOrDefault(x => x.Value == vtpName).Key;
                                }
                            }

                            string gender = drVC["Gender"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(gender))
                            {
                                if (dicGenders.ContainsValue(gender))
                                {
                                    genderId = dicGenders.FirstOrDefault(x => x.Value == gender).Key;
                                }
                            }

                            string natureOfAppointment = drVC["NatureOfAppointment"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(natureOfAppointment))
                            {
                                if (dicNatureOfAppointments.ContainsValue(natureOfAppointment))
                                {
                                    natureAppointmentId = dicNatureOfAppointments.FirstOrDefault(x => x.Value == natureOfAppointment).Key;
                                }
                            }

                            DateTime? dateOfJoining = Constants.GetDateValue(drVC["DateOfJoining"].StringVal());
                            DateTime? dateOfResignation = Constants.GetDateValue(drVC["DateOfResignation"].StringVal());

                            if (academicYearId != Guid.Empty && VTPId != Guid.Empty && !string.IsNullOrEmpty(drVC["FirstName"].StringVal()) && !string.IsNullOrEmpty(drVC["LastName"].StringVal()) && !string.IsNullOrEmpty(drVC["Mobile"].StringVal()) && !string.IsNullOrEmpty(drVC["Email"].StringVal()) && !string.IsNullOrEmpty(genderId) && dateOfJoining.HasValue && !string.IsNullOrEmpty(natureAppointmentId))
                            {
                                VocationalCoordinatorModel vocationalCoordinatorModelEntity = new VocationalCoordinatorModel
                                {
                                    FirstName = drVC["FirstName"].StringVal().ToTitleCase(),
                                    MiddleName = drVC["MiddleName"].StringVal().ToTitleCase(),
                                    LastName = drVC["LastName"].StringVal().ToTitleCase(),
                                    Mobile = drVC["Mobile"].StringVal(),
                                    Mobile1 = drVC["Mobile1"].StringVal(),
                                    EmailId = drVC["Email"].StringVal().ToLower(),
                                    Gender = genderId,
                                    AcademicYearId = academicYearId,
                                    VTPId = VTPId,
                                    DateOfJoining = dateOfJoining.Value,
                                    DateOfResignation = dateOfResignation,
                                    NatureOfAppointment = natureAppointmentId,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                vocationalCoordinatorModelEntity.FullName = string.Format("{0} {1} {2}", vocationalCoordinatorModelEntity.FirstName, vocationalCoordinatorModelEntity.MiddleName, vocationalCoordinatorModelEntity.LastName).TrimSpaces();

                                var vcResponse = vocationalCoordinatorManager.SaveOrUpdateVocationalCoordinatorDetails(vocationalCoordinatorModelEntity);

                                if (vcResponse.Errors.Count > 0)
                                {
                                    drVC["Status"] = "Failed";
                                    drVC["Exception"] = string.Join(",", vcResponse.Errors);
                                }
                                else
                                {
                                    drVC["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (VTPId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VTP Name Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drVC["FirstName"].StringVal()))
                                {
                                    this.errorMessage += "First Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drVC["LastName"].StringVal()))
                                {
                                    this.errorMessage += "Last Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drVC["Mobile"].StringVal()))
                                {
                                    this.errorMessage += "Mobile is required\n";
                                }
                                if (string.IsNullOrEmpty(drVC["Email"].StringVal()))
                                {
                                    this.errorMessage += "Email is required\n";
                                }
                                if (string.IsNullOrEmpty(gender))
                                {
                                    this.errorMessage += "Invalid Gender Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drVC["DateOfJoining"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Joining is required\n";
                                }
                                if (!string.IsNullOrEmpty(drVC["DateOfJoining"].StringVal()) && !dateOfJoining.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Joining in the format 'DD/MM/YYYY'\n";
                                }
                                if (string.IsNullOrEmpty(gender))
                                {
                                    this.errorMessage += "Invalid Gender Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(natureAppointmentId))
                                {
                                    this.errorMessage += "Invalid Nature Appointment Which is not found in Database\n";
                                }
                                if (!string.IsNullOrEmpty(drVC["DateOfResignation"].StringVal()) && !dateOfResignation.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Resignation in the format 'DD/MM/YYYY'\n";
                                }

                                drVC["Status"] = "Failed";
                                drVC["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drVC["Status"] = "Failed";
                            drVC["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("VocationalCoordinators-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtVC, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VocationalTrainingProvidersExcelUpload")]
        public SingularResponse<string> VocationalTrainingProvidersExcelUpload(IFormFile excelData)
        {
            DataTable dtVTP = new DataTable();

            try
            {
                // Reading VTP data from excel template
                dtVTP = this.ReadExcelTemplateData(excelData);

                #region Validate VTP template format and data type

                List<string> columnNamesInDB = new List<string> { "VTPName", "VTPShortName", "ApprovalYear", "CertificationNo", "CertificationAgency", "VTPMobileNo", "VTPEmailId", "VTPAddress", "PrimaryContactPerson", "PrimaryContactNumber", "PrimaryContactEmail", "VTPStateCoordinator", "VTPStateCoordinatorMobile", "VTPStateCoordinatorEmail" };

                List<string> columnNamesInTemplate = dtVTP.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtVTP.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate VTP template format and data type

                #region Get master data for VTP

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                #endregion Get master data for VTP

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtVTP.Rows.Count; rowIndex++)
                    {
                        DataRow drVTP = dtVTP.Rows[rowIndex];

                        try
                        {
                            string academicYear = drVTP["ApprovalYear"].StringVal();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            if (!string.IsNullOrEmpty(drVTP["VTPName"].StringVal()) && !string.IsNullOrEmpty(drVTP["VTPShortName"].StringVal()) && academicYearId != Guid.Empty && !string.IsNullOrEmpty(drVTP["CertificationNo"].StringVal()) && !string.IsNullOrEmpty(drVTP["CertificationAgency"].StringVal()) && !string.IsNullOrEmpty(drVTP["VTPMobileNo"].StringVal()) && !string.IsNullOrEmpty(drVTP["VTPEmailId"].StringVal()) && !string.IsNullOrEmpty(drVTP["VTPAddress"].StringVal()) && !string.IsNullOrEmpty(drVTP["PrimaryContactPerson"].StringVal()) && !string.IsNullOrEmpty(drVTP["PrimaryContactNumber"].StringVal()) && !string.IsNullOrEmpty(drVTP["PrimaryContactEmail"].StringVal()) && !string.IsNullOrEmpty(drVTP["VTPStateCoordinator"].StringVal()) && !string.IsNullOrEmpty(drVTP["VTPStateCoordinatorMobile"].StringVal()) && !string.IsNullOrEmpty(drVTP["VTPStateCoordinatorEmail"].StringVal()))
                            {
                                VocationalTrainingProviderModel vocationalTrainingProviderModelEntity = new VocationalTrainingProviderModel
                                {
                                    VTPName = drVTP["VTPName"].StringVal(),
                                    VTPShortName = drVTP["VTPShortName"].StringVal(),
                                    ApprovalYear = drVTP["ApprovalYear"].StringVal(),
                                    CertificationNo = drVTP["CertificationNo"].StringVal().ToUpper(),
                                    CertificationAgency = drVTP["CertificationAgency"].StringVal().ToTitleCase(),
                                    VTPMobileNo = drVTP["VTPMobileNo"].StringVal(),
                                    VTPEmailId = drVTP["VTPEmailId"].StringVal(),
                                    VTPAddress = drVTP["VTPAddress"].StringVal(),
                                    PrimaryContactPerson = drVTP["PrimaryContactPerson"].StringVal().ToTitleCase(),
                                    PrimaryContactEmail = drVTP["PrimaryContactEmail"].StringVal(),
                                    PrimaryMobileNumber = drVTP["PrimaryContactNumber"].StringVal(),
                                    VTPStateCoordinator = drVTP["VTPStateCoordinator"].StringVal().ToTitleCase(),
                                    VTPStateCoordinatorMobile = drVTP["VTPStateCoordinatorMobile"].StringVal(),
                                    VTPStateCoordinatorEmail = drVTP["VTPStateCoordinatorEmail"].StringVal().ToLower(),
                                    ContractApprovalDate = Constants.GetCurrentDateTime,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var vtpResponse = vocationalTrainingProviderManager.SaveOrUpdateVocationalTrainingProviderDetails(vocationalTrainingProviderModelEntity);

                                if (vtpResponse.Errors.Count > 0)
                                {
                                    drVTP["Status"] = "Failed";
                                    drVTP["Exception"] = string.Join(",", vtpResponse.Errors);
                                }
                                else
                                {
                                    drVTP["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (string.IsNullOrEmpty(drVTP["VTPShortName"].StringVal()))
                                {
                                    this.errorMessage += "VTP Short Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["VTPShortName"].StringVal()))
                                {
                                    this.errorMessage += "VTP Name is required\n";
                                }
                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Academic Year Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["CertificationNo"].StringVal()))
                                {
                                    this.errorMessage += "Certification No is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["CertificationAgency"].StringVal()))
                                {
                                    this.errorMessage += "Certification Agency is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["VTPMobileNo"].StringVal()))
                                {
                                    this.errorMessage += "VTP Mobile No is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["VTPEmailId"].StringVal()))
                                {
                                    this.errorMessage += "VTP EmailId is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["VTPAddress"].StringVal()))
                                {
                                    this.errorMessage += "VTP Address is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["PrimaryContactPerson"].StringVal()))
                                {
                                    this.errorMessage += "Primary Contact Person is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["PrimaryContactNumber"].StringVal()))
                                {
                                    this.errorMessage += "Primary Contact Number is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["PrimaryContactEmail"].StringVal()))
                                {
                                    this.errorMessage += "Primary Contact Email is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["VTPStateCoordinator"].StringVal()))
                                {
                                    this.errorMessage += "VTP State Coordinator is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["VTPStateCoordinatorMobile"].StringVal()))
                                {
                                    this.errorMessage += "VTP State Coordinator Mobile is required\n";
                                }
                                if (string.IsNullOrEmpty(drVTP["VTPStateCoordinatorEmail"].StringVal()))
                                {
                                    this.errorMessage += "VTP State Coordinator Email is required\n";
                                }

                                drVTP["Status"] = "Failed";
                                drVTP["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drVTP["Status"] = "Failed";
                            drVTP["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("VocationalTrainingProviders-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtVTP, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("SchoolVTPSectorsExcelUpload")]
        public SingularResponse<string> SchoolVTPSectorsExcelUpload(IFormFile excelData)
        {
            DataTable dtSchoolVTPSector = new DataTable();

            try
            {
                // Reading SchoolVTPSector data from excel template
                dtSchoolVTPSector = this.ReadExcelTemplateData(excelData);

                #region Validate SchoolVTPSector template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "SectorName", "VTPShortName", "SchoolName", "UDISE" };

                List<string> columnNamesInTemplate = dtSchoolVTPSector.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtSchoolVTPSector.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate SchoolVTPSector template format and data type

                #region Get master data for SchoolVTPSectors

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct Sectors
                var sectors = this.sectorManager.GetSectors().Select(x => new SectorModel { SectorId = x.SectorId, SectorName = x.SectorName }).Distinct().ToList();
                Dictionary<Guid, string> dicSectors = sectors.ToDictionary(x => x.SectorId, x => x.SectorName.StringVal().ToUpper());

                // Get distinct VTPs
                var vtps = this.vocationalTrainingProviderManager.GetVTPList().Select(x => new VocationalTrainingProviderModel { VTPId = x.Id, VTPShortName = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicVTPs = vtps.ToDictionary(x => x.VTPId, x => x.VTPShortName.StringVal().ToUpper());

                // Get distinct VTPSectors
                var vtpSectors = this.vtpSectorManager.GetVTPSectorList();

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtSchoolVTPSector.Select().AsEnumerable().Select(s => s["SchoolName"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise), Udise = x.Udise, Demography = x.SchoolName }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                #endregion Get master data for SchoolVTPSectors

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtSchoolVTPSector.Rows.Count; rowIndex++)
                    {
                        DataRow drSchoolVTPSector = dtSchoolVTPSector.Rows[rowIndex];
                        academicYearId = sectorId = VTPId = schoolId = Guid.Empty;

                        try
                        {
                            string academicYear = drSchoolVTPSector["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string sectorName = drSchoolVTPSector["SectorName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectorName))
                            {
                                if (dicSectors.ContainsValue(sectorName))
                                {
                                    sectorId = dicSectors.FirstOrDefault(x => x.Value == sectorName).Key;
                                }
                            }

                            string vtpName = drSchoolVTPSector["VTPShortName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vtpName))
                            {
                                if (dicVTPs.ContainsValue(vtpName))
                                {
                                    VTPId = dicVTPs.FirstOrDefault(x => x.Value == vtpName).Key;
                                }
                            }

                            string schoolName = string.Format("{0}-{1}", drSchoolVTPSector["SchoolName"].StringVal(), drSchoolVTPSector["UDISE"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                }
                            }

                            var vtpSectorItem = vtpSectors.FirstOrDefault(vs => vs.AcademicYearId == academicYearId && vs.VTPId == VTPId && vs.SectorId == sectorId);

                            if (academicYearId != Guid.Empty && schoolId != Guid.Empty && VTPId != Guid.Empty && sectorId != Guid.Empty && vtpSectorItem != null)
                            {
                                SchoolVTPSectorModel schoolVTPSectorModelEntity = new SchoolVTPSectorModel
                                {
                                    AcademicYearId = academicYearId,
                                    SectorId = sectorId,
                                    VTPId = VTPId,
                                    SchoolId = schoolId,
                                    Remarks = null,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var vtpItem = vtps.FirstOrDefault(x => x.VTPShortName.StringVal().ToUpper() == vtpName);
                                var sectorItem = sectors.FirstOrDefault(x => x.SectorName.StringVal().ToUpper() == sectorName);
                                var schoolItem = schools.FirstOrDefault(x => x.Demography.StringVal().ToUpper() == drSchoolVTPSector["SchoolName"].StringVal().ToUpper());

                                schoolVTPSectorModelEntity.Remarks = string.Format("{0}-{1}-{2}", schoolItem.Udise, vtpItem.VTPShortName, sectorItem.SectorName);

                                var schoolVTPSectorResponse = schoolVTPSectorManager.SaveOrUpdateSchoolVTPSectorDetails(schoolVTPSectorModelEntity);
                                if (schoolVTPSectorResponse.Errors.Count > 0)
                                {
                                    drSchoolVTPSector["Status"] = "Failed";
                                    drSchoolVTPSector["Exception"] = string.Join(",", schoolVTPSectorResponse.Errors);
                                }
                                else
                                {
                                    drSchoolVTPSector["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;

                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Academic Year Which is not found in Database\n";
                                }
                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Name Which is not found in Database\n";
                                }
                                if (VTPId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VTPShortName Which is not found in Database\n";
                                }
                                if (sectorId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Sector Name Which is not found in Database\n";
                                }
                                if (vtpSectorItem == null)
                                {
                                    this.errorMessage += "VTP & Sector mapping does not exists, Please check in the VTP Sector Master\n";
                                }

                                drSchoolVTPSector["Status"] = "Failed";
                                drSchoolVTPSector["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);
                            drSchoolVTPSector["Status"] = "Failed";
                            drSchoolVTPSector["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("SchoolVTPSectors-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtSchoolVTPSector, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VocationalTrainersExcelUpload")]
        public SingularResponse<string> VocationalTrainersExcelUpload(IFormFile excelData)
        {
            DataTable dtVT = new DataTable();

            try
            {
                // Reading VocationalTrainer data from excel template
                dtVT = this.ReadExcelTemplateData(excelData);

                #region Validate VocationalTrainer template format and data type

                List<string> columnNamesInDB = new List<string> { "VTPShortName", "FirstName", "MiddleName", "LastName", "Mobile", "Mobile1", "Email", "Gender", "DateOfBirth", "SocialCategory", "NatureOfAppointment", "AcademicQualification", "ProfessionalQualification", "ProfessionalQualificationDetails", "IndustryExperienceMonths", "TrainingExperienceMonths", "AadhaarNumber", "DateOfJoining", "DateOfResignation", "VCName" };

                List<string> columnNamesInTemplate = dtVT.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtVT.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate VocationalTrainer template format and data type

                #region Get master data for VocationalTrainers

                // Get distinct VocationalCoordinators
                List<string> vcNamesInTemplate = dtVT.Select().AsEnumerable().Select(s => s["VCName"].StringVal()).Distinct().ToList();

                var vocationalCoordinators = this.vocationalCoordinatorManager.GetVocationalCoordinatorsByNames(vcNamesInTemplate).Select(x => new VocationalCoordinatorModel { VCId = x.VCId, FullName = x.FullName }).Distinct().ToList();
                Dictionary<Guid, string> dicVocationalCoordinators = vocationalCoordinators.ToDictionary(x => x.VCId, x => x.FullName.StringVal().ToUpper());

                // Get distinct VTPs
                var vtps = this.vocationalTrainingProviderManager.GetVTPList().Select(x => new VocationalTrainingProviderModel { VTPId = x.Id, VTPShortName = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicVTPs = vtps.ToDictionary(x => x.VTPId, x => x.VTPShortName.StringVal().ToUpper());

                // Get distinct Genders
                var genders = this.dataValueManager.GetDataValuesByType("Gender").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicGenders = genders.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct SocialCategores
                var socialCategories = this.dataValueManager.GetDataValuesByType("SocialCategory").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicSocialCategories = socialCategories.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct NatureOfAppointments
                var natureOfAppointments = this.dataValueManager.GetDataValuesByType("NatureOfAppointment").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicNatureOfAppointments = natureOfAppointments.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct AcademicQualifications
                var academicQualifications = this.dataValueManager.GetDataValuesByType("AcademicQualification").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicAcademicQualifications = academicQualifications.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct ProfessionalQualifications
                var professionalQualifications = this.dataValueManager.GetDataValuesByType("ProfessionalQualification").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicProfessionalQualifications = professionalQualifications.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                #endregion Get master data for VocationalTrainers

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtVT.Rows.Count; rowIndex++)
                    {
                        DataRow drVT = dtVT.Rows[rowIndex];
                        VTPId = VCId = Guid.Empty;

                        try
                        {
                            string vtpName = drVT["VTPShortName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vtpName))
                            {
                                if (dicVTPs.ContainsValue(vtpName))
                                {
                                    VTPId = dicVTPs.FirstOrDefault(x => x.Value == vtpName).Key;
                                }
                            }

                            string vcName = drVT["VCName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vcName))
                            {
                                if (dicVocationalCoordinators.ContainsValue(vcName))
                                {
                                    VCId = dicVocationalCoordinators.FirstOrDefault(x => x.Value == vcName).Key;
                                }
                            }

                            string gender = drVT["Gender"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(gender))
                            {
                                if (dicGenders.ContainsValue(gender))
                                {
                                    genderId = dicGenders.FirstOrDefault(x => x.Value == gender).Key;
                                }
                            }

                            string socialCategory = drVT["SocialCategory"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(socialCategory))
                            {
                                if (dicSocialCategories.ContainsValue(socialCategory))
                                {
                                    socialcategoryId = dicSocialCategories.FirstOrDefault(x => x.Value == socialCategory).Key;
                                }
                            }

                            string natureOfAppointment = drVT["NatureOfAppointment"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(natureOfAppointment))
                            {
                                if (dicNatureOfAppointments.ContainsValue(natureOfAppointment))
                                {
                                    natureAppointmentId = dicNatureOfAppointments.FirstOrDefault(x => x.Value == natureOfAppointment).Key;
                                }
                            }

                            string academicQualification = drVT["AcademicQualification"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicQualification))
                            {
                                if (dicAcademicQualifications.ContainsValue(academicQualification))
                                {
                                    acanadmicQulificationId = dicAcademicQualifications.FirstOrDefault(x => x.Value == academicQualification).Key;
                                }
                            }

                            string professionalQualification = drVT["ProfessionalQualification"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(professionalQualification))
                            {
                                if (dicProfessionalQualifications.ContainsValue(professionalQualification))
                                {
                                    professionalQualificationId = dicProfessionalQualifications.FirstOrDefault(x => x.Value == professionalQualification).Key;
                                }
                            }

                            DateTime? dateOfBirth = Constants.GetDateValue(drVT["DateOfBirth"].StringVal());
                            DateTime? dateOfJoining = Constants.GetDateValue(drVT["DateOfJoining"].StringVal());
                            DateTime? dateOfResignation = Constants.GetDateValue(drVT["DateOfResignation"].StringVal());

                            if (VTPId != Guid.Empty && VCId != Guid.Empty && !string.IsNullOrEmpty(drVT["FirstName"].StringVal()) && !string.IsNullOrEmpty(drVT["LastName"].StringVal()) && !string.IsNullOrEmpty(drVT["Mobile"].StringVal()) && !string.IsNullOrEmpty(drVT["Email"].StringVal()) && !string.IsNullOrEmpty(genderId) && dateOfBirth.HasValue && !string.IsNullOrEmpty(socialcategoryId) && !string.IsNullOrEmpty(natureAppointmentId) && !string.IsNullOrEmpty(acanadmicQulificationId) && !string.IsNullOrEmpty(professionalQualificationId) && dateOfJoining.HasValue)
                            {
                                VocationalTrainerModel vocationalTrainerModelEntity = new VocationalTrainerModel
                                {
                                    VTPId = VTPId,
                                    VCId = VCId,
                                    FirstName = drVT["FirstName"].StringVal().ToTitleCase(),
                                    MiddleName = drVT["MiddleName"].StringVal().ToTitleCase(),
                                    LastName = drVT["LastName"].StringVal().ToTitleCase(),
                                    Mobile = drVT["Mobile"].StringVal(),
                                    Mobile1 = drVT["Mobile1"].StringVal(),
                                    Email = drVT["Email"].StringVal(),
                                    Gender = genderId,
                                    DateOfBirth = dateOfBirth.Value,
                                    SocialCategory = socialcategoryId,
                                    NatureOfAppointment = natureAppointmentId,
                                    AcademicQualification = acanadmicQulificationId,
                                    ProfessionalQualification = professionalQualificationId,
                                    ProfessionalQualificationDetails = drVT["ProfessionalQualificationDetails"].StringVal(),
                                    IndustryExperienceMonths = Convert.ToInt32(drVT["IndustryExperienceMonths"].StringVal()),
                                    TrainingExperienceMonths = Convert.ToInt32(drVT["TrainingExperienceMonths"].StringVal()),
                                    AadhaarNumber = drVT["AadhaarNumber"].StringVal(),
                                    DateOfJoining = dateOfJoining.Value,
                                    DateOfResignation = dateOfResignation,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                vocationalTrainerModelEntity.FullName = string.Format("{0} {1} {2}", vocationalTrainerModelEntity.FirstName, vocationalTrainerModelEntity.MiddleName, vocationalTrainerModelEntity.LastName).TrimSpaces();

                                var vtResponse = vocationalTrainerManager.SaveOrUpdateVocationalTrainerDetails(vocationalTrainerModelEntity);
                                if (vtResponse.Errors.Count > 0)
                                {
                                    drVT["Status"] = "Failed";
                                    drVT["Exception"] = string.Join(",", vtResponse.Errors);
                                }
                                else
                                {
                                    drVT["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (VTPId == Guid.Empty)
                                {
                                    this.errorMessage = "Invalid VTPShortName Which is not found in Database";
                                }
                                if (VCId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VCName Which is not found in Database";
                                }

                                if (string.IsNullOrEmpty(drVT["FirstName"].StringVal()))
                                {
                                    this.errorMessage += "First Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drVT["LastName"].StringVal()))
                                {
                                    this.errorMessage += "Last Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drVT["Mobile"].StringVal()))
                                {
                                    this.errorMessage += "Mobile is required\n";
                                }
                                if (string.IsNullOrEmpty(drVT["Email"].StringVal()))
                                {
                                    this.errorMessage += "Email is required\n";
                                }
                                if (string.IsNullOrEmpty(genderId))
                                {
                                    this.errorMessage += "Invalid Gender Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drVT["DateOfBirth"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Birth is required\n";
                                }
                                if (!string.IsNullOrEmpty(drVT["DateOfBirth"].StringVal()) && !dateOfBirth.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Birth in the format 'DD/MM/YYYY'\n";
                                }
                                if (string.IsNullOrEmpty(socialcategoryId))
                                {
                                    this.errorMessage += "Invalid Social Category Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(natureAppointmentId))
                                {
                                    this.errorMessage += "Invalid Nature Of Appointment Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(acanadmicQulificationId))
                                {
                                    this.errorMessage += "Invalid Acanadmic Qulification Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(professionalQualificationId))
                                {
                                    this.errorMessage += "Invalid Professional Qualification Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drVT["AadhaarNumber"].StringVal()))
                                {
                                    this.errorMessage += "Aadhaar Number is required\n";
                                }
                                if (string.IsNullOrEmpty(drVT["DateOfJoining"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Joining is required\n";
                                }
                                if (!string.IsNullOrEmpty(drVT["DateOfJoining"].StringVal()) && !dateOfJoining.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Joining in the format 'DD/MM/YYYY'\n";
                                }
                                if (!string.IsNullOrEmpty(drVT["DateOfResignation"].StringVal()) && !dateOfResignation.HasValue)
                                {
                                    this.errorMessage += "Error! Invalid date format. Please enter the Date Of Resignation in the format 'DD/MM/YYYY'\n";
                                }

                                drVT["Status"] = "Failed";
                                drVT["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);
                            drVT["Status"] = "Failed";
                            drVT["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("VocationalTrainers-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtVT, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VTClassesExcelUpload")]
        public SingularResponse<string> VTClassesExcelUpload(IFormFile excelData)
        {
            DataTable dtVTClass = new DataTable();
            try
            {
                // Reading VTClass data from excel template
                dtVTClass = this.ReadExcelTemplateData(excelData);

                #region Validate VTClass template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "VTName", "SchoolName", "ClassName", "SectionName" };

                List<string> columnNamesInTemplate = dtVTClass.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtVTClass.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate VTClass template format and data type

                #region Get master data for VTClasses

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct Sections
                var sections = this.sectionManager.GetSections().Select(x => new SectionModel { SectionId = x.SectionId, Name = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicSections = sections.ToDictionary(x => x.SectionId, x => x.Name.StringVal().ToUpper());

                // Get distinct Classes
                var classes = this.schoolClassManager.GetSchoolClasses().Select(x => new SchoolClassModel { ClassId = x.ClassId, Name = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicClasses = classes.ToDictionary(x => x.ClassId, x => x.Name.StringVal().ToUpper());

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtVTClass.Select().AsEnumerable().Select(s => s["SchoolName"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise) }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                // Get distinct VocationalTrainers
                List<string> vocationalTrainerInTemplate = dtVTClass.Select().AsEnumerable().Select(s => s["VTName"].StringVal()).Distinct().ToList();

                var vocationalTrainers = this.vocationalTrainerManager.GetVocationalTrainersByNames(vocationalTrainerInTemplate).Select(x => new VocationalTrainerModel { VTId = x.VTId, FullName = string.Format("{0}-{1}", x.FullName, x.Email) }).Distinct().ToList();
                Dictionary<Guid, string> dicVocationalTrainers = vocationalTrainers.ToDictionary(x => x.VTId, x => x.FullName.StringVal().ToUpper());

                #endregion Get master data for VTClasses

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtVTClass.Rows.Count; rowIndex++)
                    {
                        DataRow drVTClass = dtVTClass.Rows[rowIndex];
                        academicYearId = VTId = schoolId = classId = sectionId = Guid.Empty;

                        try
                        {
                            string academicYear = drVTClass["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string vtName = string.Format("{0}-{1}", drVTClass["VTName"].StringVal(), drVTClass["VTEmailId"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(vtName))
                            {
                                if (dicVocationalTrainers.ContainsValue(vtName))
                                {
                                    VTId = dicVocationalTrainers.FirstOrDefault(x => x.Value == vtName).Key;
                                }
                            }

                            string schoolName = string.Format("{0}-{1}", drVTClass["SchoolName"].StringVal(), drVTClass["UDISE"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                }
                            }

                            string className = drVTClass["ClassName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(className))
                            {
                                if (dicClasses.ContainsValue(className))
                                {
                                    classId = dicClasses.FirstOrDefault(x => x.Value == className).Key;
                                }
                            }

                            string sectionName = drVTClass["SectionName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectionName))
                            {
                                if (dicSections.ContainsValue(sectionName))
                                {
                                    sectionId = dicSections.FirstOrDefault(x => x.Value == sectionName).Key;
                                }
                            }

                            IList<Guid> sectionIds = new List<Guid>() { sectionId };

                            //AcademicYear	VTName	SchoolName	ClassName	SectionName

                            if (academicYearId != Guid.Empty && VTId != Guid.Empty && schoolId != Guid.Empty && classId != Guid.Empty && sectionId != Guid.Empty)
                            {
                                VTClassModel vTClassModelEntity = new VTClassModel
                                {
                                    AcademicYearId = academicYearId,
                                    VTId = VTId,
                                    ClassId = classId,
                                    SectionId = sectionId,
                                    SchoolId = schoolId,
                                    SectionIds = sectionIds.ToList(),
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var vtClassResponse = vtClassManager.SaveOrUpdateVTClassDetails(vTClassModelEntity);

                                if (vtClassResponse.Errors.Count > 0)
                                {
                                    drVTClass["Status"] = "Failed";
                                    drVTClass["Exception"] = string.Join(",", vtClassResponse.Errors);
                                }
                                else
                                {
                                    drVTClass["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;

                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Academic Year Which is not found in Database\n";
                                }
                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Name Which is not found in Database\n";
                                }
                                if (VTId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VTName Which is not found in Database\n";
                                }
                                if (classId == Guid.Empty)
                                {
                                    this.errorMessage = "Invalid Class Name Which is not found in Database\n";
                                }
                                if (sectionId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Section Which is not found in Database\n";
                                }

                                drVTClass["Status"] = "Failed";
                                drVTClass["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drVTClass["Status"] = "Failed";
                            drVTClass["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("VTClasses-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtVTClass, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VTPSectorsExcelUpload")]
        public SingularResponse<string> VTPSectorsExcelUpload(IFormFile excelData)
        {
            DataTable dtVTPSector = new DataTable();

            try
            {
                // Reading VTPSector data from excel template
                dtVTPSector = this.ReadExcelTemplateData(excelData);

                #region Validate VTPSector template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "VTPShortName", "SectorName" };

                List<string> columnNamesInTemplate = dtVTPSector.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtVTPSector.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate VTPSector template format and data type

                #region Get master data for VTPSectors

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct VTPs
                var vtps = this.vocationalTrainingProviderManager.GetVTPList().Select(x => new VocationalTrainingProviderModel { VTPId = x.Id, VTPShortName = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicVTPs = vtps.ToDictionary(x => x.VTPId, x => x.VTPShortName.StringVal().ToUpper());

                // Get distinct Sectors
                var sectors = this.sectorManager.GetSectors().Select(x => new SectorModel { SectorId = x.SectorId, SectorName = x.SectorName }).Distinct().ToList();
                Dictionary<Guid, string> dicSectors = sectors.ToDictionary(x => x.SectorId, x => x.SectorName.StringVal().ToUpper());

                #endregion Get master data for VTPSectors

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtVTPSector.Rows.Count; rowIndex++)
                    {
                        DataRow drVTPSector = dtVTPSector.Rows[rowIndex];
                        academicYearId = VTPId = sectorId = Guid.Empty;

                        try
                        {
                            string academicYear = drVTPSector["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string vtpName = drVTPSector["VTPShortName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(vtpName))
                            {
                                if (dicVTPs.ContainsValue(vtpName))
                                {
                                    VTPId = dicVTPs.FirstOrDefault(x => x.Value == vtpName).Key;
                                }
                            }

                            string sectorName = drVTPSector["SectorName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectorName))
                            {
                                if (dicSectors.ContainsValue(sectorName))
                                {
                                    sectorId = dicSectors.FirstOrDefault(x => x.Value == sectorName).Key;
                                }
                            }

                            if (academicYearId != Guid.Empty && VTPId != Guid.Empty && sectorId != Guid.Empty)
                            {
                                VTPSectorModel vtpSectorModelEntity = new VTPSectorModel
                                {
                                    AcademicYearId = academicYearId,
                                    SectorId = sectorId,
                                    VTPId = VTPId,
                                    Remarks = null,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var vtpSectorResponse = vtpSectorManager.SaveOrUpdateVTPSectorDetails(vtpSectorModelEntity);

                                if (vtpSectorResponse.Errors.Count > 0)
                                {
                                    drVTPSector["Status"] = "Failed";
                                    drVTPSector["Exception"] = string.Join(",", vtpSectorResponse.Errors);
                                }
                                else
                                {
                                    drVTPSector["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (VTPId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VTPShortName Which is not found in Database\n";
                                }

                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Academic Year Which is not found in Database\n";
                                }

                                if (sectorId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Sector Name Which is not found in Database\n";
                                }

                                drVTPSector["Status"] = "Failed";
                                drVTPSector["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drVTPSector["Status"] = "Failed";
                            drVTPSector["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("VTPSectors-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtVTPSector, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("StudentsExcelUpload")]
        public SingularResponse<string> StudentsExcelUpload(IFormFile excelData)
        {
            DataTable dtStudent = new DataTable();

            try
            {
                // Reading Student data from excel template
                dtStudent = this.ReadExcelTemplateData(excelData);

                #region Validate Student template format and data type

                List<string> columnNamesInDB = new List<string> { "AcademicYear", "SchoolName", "VTName", "ClassName", "SectionName", "DateOfEnrollment", "FirstName", "MiddleName", "LastName", "Gender", "MobileNumber", "DateOfDropout", "FatherName", "MotherName", "GuardianName", "DateOfBirth", "AadhaarNumber", "StudentRollNumber", "SocialCategory", "CWSNStatus", "FirstMobileNumber", "SecondMobileNumber", "DropoutReason" };

                List<string> columnNamesInTemplate = dtStudent.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtStudent.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate Student template format and data type

                #region Get master data for Students

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtStudent.Select().AsEnumerable().Select(s => s["SchoolName"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise) }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                // Get distinct Classes
                var classes = this.schoolClassManager.GetSchoolClasses().Select(x => new SchoolClassModel { ClassId = x.ClassId, Name = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicClasses = classes.ToDictionary(x => x.ClassId, x => x.Name.StringVal().ToUpper());

                // Get distinct Sections
                var sections = this.sectionManager.GetSections().Select(x => new SectionModel { SectionId = x.SectionId, Name = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicSections = sections.ToDictionary(x => x.SectionId, x => x.Name.StringVal().ToUpper());

                // Get distinct Genders
                var genders = this.dataValueManager.GetDataValuesByType("Gender").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicGenders = genders.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct SocialCategories
                var socialCategories = this.dataValueManager.GetDataValuesByType("SocialCategory").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicSocialCategories = socialCategories.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct VocationalTrainers
                List<string> vocationalTrainersInTemplate = dtStudent.Select().AsEnumerable().Select(s => s["VTName"].StringVal()).Distinct().ToList();

                var vocationalTrainers = this.vocationalTrainerManager.GetVocationalTrainersByNames(vocationalTrainersInTemplate).Select(x => new VocationalTrainerModel { VTId = x.VTId, FullName = string.Format("{0}-{1}", x.FullName, x.Email) }).Distinct().ToList();
                Dictionary<Guid, string> dicVocationalTrainers = vocationalTrainers.ToDictionary(x => x.VTId, x => x.FullName.StringVal().ToUpper());

                // Get distinct Students
                List<string> studentsInTemplate = dtStudent.Select().AsEnumerable().Select(s => string.Format("{0} {1} {2}", s["FirstName"], s["MiddleName"], s["LastName"]).StringVal()).Distinct().ToList();

                var students = this.studentClassManager.GetStudentsByNames(studentsInTemplate).Select(x => new StudentClassModel { StudentId = x.StudentId, FullName = x.FullName }).Distinct().ToList();
                Dictionary<Guid, string> dicStudents = students.ToDictionary(x => x.StudentId, x => x.FullName.StringVal().ToUpper());

                #endregion Get master data for Students

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtStudent.Rows.Count; rowIndex++)
                    {
                        DataRow drStudent = dtStudent.Rows[rowIndex];
                        academicYearId = classId = sectionId = VTId = schoolId = Guid.Empty;

                        try
                        {
                            string schoolName = string.Format("{0}-{1}", drStudent["SchoolName"].StringVal(), drStudent["UDISE"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                }
                            }

                            string vtName = string.Format("{0}-{1}", drStudent["VTName"].StringVal(), drStudent["VTEmailId"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(vtName))
                            {
                                if (dicVocationalTrainers.ContainsValue(vtName))
                                {
                                    VTId = dicVocationalTrainers.FirstOrDefault(x => x.Value == vtName).Key;
                                }
                            }

                            string academicYear = drStudent["AcademicYear"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(academicYear))
                            {
                                if (dicAcademicYears.ContainsValue(academicYear))
                                {
                                    academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                                }
                            }

                            string className = drStudent["ClassName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(className))
                            {
                                if (dicClasses.ContainsValue(className))
                                {
                                    classId = dicClasses.FirstOrDefault(x => x.Value == className).Key;
                                }
                            }

                            string sectionName = drStudent["SectionName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectionName))
                            {
                                if (dicSections.ContainsValue(sectionName))
                                {
                                    sectionId = dicSections.FirstOrDefault(x => x.Value == sectionName).Key;
                                }
                            }

                            string gender = drStudent["Gender"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(gender))
                            {
                                if (dicGenders.ContainsValue(gender))
                                {
                                    genderId = dicGenders.FirstOrDefault(x => x.Value == gender).Key;
                                }
                            }

                            string socialCategory = drStudent["SocialCategory"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(socialCategory))
                            {
                                if (dicSocialCategories.ContainsValue(socialCategory))
                                {
                                    socialcategoryId = dicSocialCategories.FirstOrDefault(x => x.Value == socialCategory).Key;
                                }
                            }

                            DateTime? dateOfDropout;
                            string dateOfDropoutText = drStudent["DateOfDropout"].StringVal().ToUpper();

                            if (!string.IsNullOrEmpty(dateOfDropoutText))
                            {
                                dateOfDropout = DateTime.Parse(dateOfDropoutText, new System.Globalization.CultureInfo("pt-BR"));
                            }
                            else
                            {
                                dateOfDropout = null;
                            }

                            if (academicYearId != Guid.Empty && schoolId != Guid.Empty && VTId != Guid.Empty && classId != Guid.Empty && sectionId != Guid.Empty && !string.IsNullOrEmpty(drStudent["DateOfEnrollment"].StringVal()) && !string.IsNullOrEmpty(drStudent["FirstName"].StringVal()) && !string.IsNullOrEmpty(drStudent["LastName"].StringVal()) && !string.IsNullOrEmpty(genderId))
                            {
                                StudentClassModel studentClassModelEntity = new StudentClassModel
                                {
                                    VTId = VTId,
                                    SchoolId = schoolId,
                                    AcademicYearId = academicYearId,
                                    ClassId = classId,
                                    SectionId = sectionId,
                                    DateOfEnrollment = Convert.ToDateTime(drStudent["DateOfEnrollment"].StringVal()),
                                    FirstName = drStudent["FirstName"].StringVal().ToTitleCase(),
                                    MiddleName = drStudent["MiddleName"].StringVal().ToTitleCase(),
                                    LastName = drStudent["LastName"].StringVal().ToTitleCase(),
                                    Gender = genderId,
                                    Mobile = drStudent["MobileNumber"].StringVal(),
                                    DateOfDropout = dateOfDropout,
                                    DropoutReason = drStudent["DropoutReason"].StringVal(),
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };
                                studentClassModelEntity.FullName = string.Format("{0} {1} {2}", studentClassModelEntity.FirstName, studentClassModelEntity.MiddleName, studentClassModelEntity.LastName).TrimSpaces();

                                var studentResponse = studentClassManager.SaveOrUpdateStudentClassDetails(studentClassModelEntity);

                                if (studentResponse.Errors.Count > 0)
                                {
                                    drStudent["Status"] = "Failed";
                                    drStudent["Exception"] = string.Join(",", studentResponse.Errors);
                                }
                                else
                                {
                                    drStudent["Status"] = "Success";

                                    // Add student in List
                                    dicStudents.Add(studentClassModelEntity.StudentId, studentClassModelEntity.FullName);
                                }

                                if (!string.IsNullOrEmpty(studentClassModelEntity.FullName))
                                {
                                    if (dicStudents.ContainsValue(studentClassModelEntity.FullName))
                                    {
                                        studentId = dicStudents.FirstOrDefault(x => x.Value == studentClassModelEntity.FullName).Key;
                                    }
                                }

                                StudentClassDetailModel studentClassDetailModelEntity = new StudentClassDetailModel
                                {
                                    StudentId = studentId,
                                    FatherName = drStudent["FatherName"].StringVal().ToTitleCase(),
                                    MotherName = drStudent["MotherName"].StringVal().ToTitleCase(),
                                    GuardianName = drStudent["GuardianName"].StringVal().ToTitleCase(),
                                    DateOfBirth = Convert.ToDateTime(drStudent["DateOfBirth"].StringVal()),
                                    AadhaarNumber = drStudent["AadhaarNumber"].StringVal(),
                                    Mobile = drStudent["FirstMobileNumber"].StringVal(),
                                    Mobile1 = drStudent["SecondMobileNumber"].StringVal(),
                                    StudentRollNumber = drStudent["StudentRollNumber"].StringVal().ToUpper(),
                                    SocialCategory = socialcategoryId,
                                    Religion = null,
                                    CWSNStatus = drStudent["CWSNStatus"].StringVal(),
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var studentParentResponse = studentClassDetailManager.SaveOrUpdateStudentClassDetailDetails(studentClassDetailModelEntity);

                                if (studentParentResponse.Errors.Count > 0)
                                {
                                    drStudent["Status"] += "Student Details: Failed";
                                    drStudent["Exception"] += "\n" + string.Join(",", studentParentResponse.Errors);
                                }
                                else
                                {
                                    drStudent["Status"] += "Student Details: Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;

                                if (academicYearId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Academic Year Which is not found in Database\n";
                                }
                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Name Which is not found in Database\n";
                                }
                                if (VTId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VT Name Which is not found in Database\n";
                                }
                                if (classId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Class Name Which is not found in Database\n";
                                }
                                if (sectionId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Section Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drStudent["DateOfEnrollment"].StringVal()))
                                {
                                    this.errorMessage += "Date Of Enrollment is required\n";
                                }
                                if (string.IsNullOrEmpty(drStudent["FirstName"].StringVal()))
                                {
                                    this.errorMessage += "First Name is required\n";
                                }
                                if (string.IsNullOrEmpty(drStudent["LastName"].StringVal()))
                                {
                                    this.errorMessage += "Last Name is required\n";
                                }
                                if (string.IsNullOrEmpty(genderId))
                                {
                                    this.errorMessage += "Gender is required\n";
                                }

                                drStudent["Status"] += "Student Details: Failed";
                                drStudent["Exception"] += this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drStudent["Status"] = "Failed";
                            drStudent["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("StudentRegistration-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtStudent, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("EmployerExcelUpload")]
        public SingularResponse<string> EmployerExcelUpload(IFormFile excelData)
        {
            DataTable dtEmployer = new DataTable();
            try
            {
                // Reading Employer data from excel template
                dtEmployer = this.ReadExcelTemplateData(excelData);

                #region Validate Student template format and data type

                List<string> columnNamesInDB = new List<string> { "Division", "District", "Block", "Address", "City", "Pincode", "BusinessType", "EmployeeCount", "Outlets", "Contact1", "Mobile1", "Designation1", "EmailId1", "Contact2", "Mobile2", "Designation2", "EmailId2", "State" };

                List<string> columnNamesInTemplate = dtEmployer.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtEmployer.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate Student template format and data type

                #region Get master data for Employers

                // Get distinct AcademicYears
                var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct Divisions
                var divisions = this.divisionManager.GetDivisions().Select(x => new DivisionModel { DivisionId = x.DivisionId, DivisionName = x.DivisionName.ToUpper(), StateCode = x.StateCode }).Distinct().ToList();
                Dictionary<Guid, string> dicDivisions = divisions.ToDictionary(x => x.DivisionId, x => x.DivisionName.StringVal().ToUpper());

                // Get distinct Districts
                var districts = this.districtManager.GetDistricts().Select(x => new DistrictModel { DistrictCode = x.DistrictCode, DistrictName = x.DistrictName }).Distinct().ToList();
                Dictionary<string, string> dicDistricts = districts.ToDictionary(x => x.DistrictCode, x => x.DistrictName.StringVal().ToUpper());

                // Get distinct Blocks
                var blocks = this.blockManager.GetBlocks().Select(x => new BlockModel { BlockId = x.BlockId, BlockName = x.BlockName }).Distinct().ToList();
                Dictionary<Guid, string> dicBlocks = blocks.ToDictionary(x => x.BlockId, x => x.BlockName.StringVal().ToUpper());

                #endregion Get master data for Employers

                if (isValidTemplate)
                {
                    for (int rowIndex = 1; rowIndex < dtEmployer.Rows.Count; rowIndex++)
                    {
                        DataRow drEmployer = dtEmployer.Rows[rowIndex];
                        try
                        {
                            divisionId = Guid.Empty;
                            districtCode = string.Empty;

                            string divisionName = drEmployer["Division"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(divisionName))
                            {
                                if (dicDivisions.ContainsValue(divisionName))
                                {
                                    divisionId = dicDivisions.FirstOrDefault(x => x.Value == divisionName).Key;

                                    if (string.IsNullOrEmpty(stateCode))
                                    {
                                        stateCode = divisions.FirstOrDefault(x => x.DivisionName == divisionName).StateCode;
                                    }
                                }
                            }

                            string districtName = drEmployer["District"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(districtName))
                            {
                                if (dicDistricts.ContainsValue(districtName))
                                {
                                    districtCode = dicDistricts.FirstOrDefault(x => x.Value == districtName).Key;
                                }
                            }

                            //string blockName = drEmployer["Block"].StringVal().ToUpper();
                            //if (!string.IsNullOrEmpty(blockName))
                            //{
                            //    if (dicBlocks.ContainsValue(blockName))
                            //    {
                            //        blockId = dicBlocks.FirstOrDefault(x => x.Value == blockName).Key;
                            //    }
                            //}

                            if (divisionId != Guid.Empty && !string.IsNullOrEmpty(districtCode) && !string.IsNullOrEmpty(stateCode) && !string.IsNullOrEmpty(drEmployer["Block"].StringVal()))
                            {
                                EmployerModel employerModelEntity = new EmployerModel
                                {
                                    StateCode = stateCode,
                                    DivisionId = divisionId,
                                    DistrictCode = districtCode,
                                    BlockName = drEmployer["Block"].StringVal().ToTitleCase(),
                                    Address = drEmployer["Address"].StringVal(),
                                    City = drEmployer["City"].StringVal().ToTitleCase(),
                                    Pincode = drEmployer["Pincode"].StringVal(),
                                    BusinessType = drEmployer["BusinessType"].StringVal(),
                                    EmployeeCount = Convert.ToInt32(drEmployer["EmployeeCount"].StringVal()),
                                    Outlets = drEmployer["Outlets"].StringVal(),
                                    Contact1 = drEmployer["Contact1"].StringVal().ToTitleCase(),
                                    Mobile1 = drEmployer["Mobile1"].StringVal(),
                                    Designation1 = drEmployer["Designation1"].StringVal().ToTitleCase(),
                                    EmailId1 = drEmployer["EmailId1"].StringVal().ToLower(),
                                    Contact2 = drEmployer["Contact2"].StringVal().ToTitleCase(),
                                    Mobile2 = drEmployer["Mobile2"].StringVal(),
                                    Designation2 = drEmployer["Designation2"].StringVal().ToTitleCase(),
                                    EmailId2 = drEmployer["EmailId2"].StringVal().ToLower(),
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var employerResponse = employerManager.SaveOrUpdateEmployerDetails(employerModelEntity);

                                if (employerResponse.Errors.Count > 0)
                                {
                                    drEmployer["Status"] = "Failed";
                                    drEmployer["Exception"] = string.Join(",", employerResponse.Errors);
                                }
                                else
                                {
                                    drEmployer["Status"] = "Success";
                                }
                            }
                            else
                            {
                                this.errorMessage = string.Empty;
                                if (divisionId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Division Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(districtCode))
                                {
                                    this.errorMessage += "Invalid District Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(stateCode))
                                {
                                    this.errorMessage += "Invalid State Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(drEmployer["BlockName"].StringVal()))
                                {
                                    this.errorMessage += "Block Name is required\n";
                                }

                                drEmployer["Status"] = "Failed";
                                drEmployer["Exception"] = this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drEmployer["Status"] = "Failed";
                            drEmployer["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("Employer-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtEmployer, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("VocationalCourseModuleExcelUpload")]
        public SingularResponse<string> VocationalCourseModuleExcelUpload(IFormFile excelData)
        {
            List<SingularResponse<string>> responses = new List<SingularResponse<string>>();
            List<CourseModuleModel> courseModuleModels = new List<CourseModuleModel>();
            DataTable dtCourseModule = new DataTable();

            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new MemoryStream())
                {
                    #region Read CourseModule data from excel template

                    excelData.CopyTo(stream);
                    stream.Position = 0;

                    IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    DataSet result = excelReader.AsDataSet(conf);
                    dtCourseModule = result.Tables[0];

                    dtCourseModule.Columns.Add(new DataColumn("Status", typeof(string)));
                    dtCourseModule.Columns.Add(new DataColumn("Exception", typeof(string)));

                    #endregion Read CourseModule data from excel template

                    #region Validate CourseModule template format and data type

                    if (!dtCourseModule.Columns.Contains("Sector"))
                    {
                        dtCourseModule.Rows[0]["Exception"] = "Invalid Sector name";
                        isValidTemplate = false;
                    }

                    if (!dtCourseModule.Columns.Contains("JobRole"))
                    {
                        dtCourseModule.Rows[1]["Exception"] = "Invalid JobRole name";
                        isValidTemplate = false;
                    }

                    if (!dtCourseModule.Columns.Contains("Class"))
                    {
                        dtCourseModule.Rows[2]["Exception"] = "Invalid Class name";
                        isValidTemplate = false;
                    }

                    if (!dtCourseModule.Columns.Contains("CourseModules"))
                    {
                        dtCourseModule.Rows[3]["Exception"] = "Invalid CourseModules name";
                        isValidTemplate = false;
                    }

                    if (!dtCourseModule.Columns.Contains("Units"))
                    {
                        dtCourseModule.Rows[4]["Exception"] = "Invalid Units name";
                        isValidTemplate = false;
                    }

                    if (!dtCourseModule.Columns.Contains("Sessions"))
                    {
                        dtCourseModule.Rows[4]["Exception"] = "Invalid Sessions name";
                        isValidTemplate = false;
                    }

                    if (!dtCourseModule.Columns.Contains("DisplayOrder"))
                    {
                        dtCourseModule.Rows[5]["Exception"] = "Invalid DisplayOrder name";
                        isValidTemplate = false;
                    }

                    #endregion Validate CourseModule template format and data type

                    #region Get master data for Course Modules

                    // Get distinct Sectors
                    var sectors = this.sectorManager.GetSectors().Select(x => new SectorModel { SectorId = x.SectorId, SectorName = x.SectorName }).Distinct().ToList();
                    Dictionary<Guid, string> dicSectors = sectors.ToDictionary(x => x.SectorId, x => x.SectorName.StringVal().ToUpper());

                    // Get distinct JobRoles
                    var jobRoles = this.jobRoleManager.GetJobRoles().Select(x => new JobRoleModel { JobRoleId = x.JobRoleId, JobRoleName = x.JobRoleName }).Distinct().ToList();
                    Dictionary<Guid, string> dicJobRoles = jobRoles.ToDictionary(x => x.JobRoleId, x => x.JobRoleName.StringVal().ToUpper());

                    // Get distinct Classes
                    var classes = this.schoolClassManager.GetSchoolClasses().Select(x => new SchoolClassModel { ClassId = x.ClassId, Name = x.Name }).Distinct().ToList();
                    Dictionary<Guid, string> dicClasses = classes.ToDictionary(x => x.ClassId, x => x.Name.StringVal().ToUpper());

                    // Get distinct CourseModules
                    List<DataValueModel> courseModules = this.dataValueManager.GetDataValues().Where(x => x.DataTypeId == "CourseModule").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                    Dictionary<string, string> dicCourseModules = courseModules.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                    #endregion Get master data for Course Modules

                    if (isValidTemplate)
                    {
                        for (int rowIndex = 1; rowIndex < dtCourseModule.Rows.Count; rowIndex++)
                        {
                            DataRow drCourseModule = dtCourseModule.Rows[rowIndex];
                            sectorId = jobRoleId = classId = Guid.Empty;

                            try
                            {
                                string sectorName = Convert.ToString(drCourseModule["Sector"].StringVal().ToUpper());
                                if (!string.IsNullOrEmpty(sectorName))
                                {
                                    if (dicSectors.ContainsValue(sectorName))
                                    {
                                        sectorId = dicSectors.FirstOrDefault(x => x.Value == sectorName).Key;
                                    }
                                }

                                string jobRoleName = Convert.ToString(drCourseModule["JobRole"].StringVal().ToUpper());
                                if (!string.IsNullOrEmpty(jobRoleName))
                                {
                                    if (dicJobRoles.ContainsValue(jobRoleName))
                                    {
                                        jobRoleId = dicJobRoles.FirstOrDefault(x => x.Value == jobRoleName).Key;
                                    }
                                }

                                string className = Convert.ToString(drCourseModule["Class"].StringVal().ToUpper());
                                if (!string.IsNullOrEmpty(className))
                                {
                                    if (dicClasses.ContainsValue(className))
                                    {
                                        classId = dicClasses.FirstOrDefault(x => x.Value == className).Key;
                                    }
                                }

                                string courseModuleName = Convert.ToString(drCourseModule["CourseModules"].StringVal().ToUpper());
                                if (!string.IsNullOrEmpty(courseModuleName))
                                {
                                    if (dicCourseModules.ContainsValue(courseModuleName))
                                    {
                                        courseModuleId = dicCourseModules.FirstOrDefault(x => x.Value == courseModuleName).Key;
                                    }
                                }

                                string unitName = Convert.ToString(drCourseModule["Units"]);
                                string sessionName = Convert.ToString(drCourseModule["Sessions"]);
                                sessionName = string.IsNullOrEmpty(sessionName) ? "Others" : sessionName;

                                if ((courseModuleId == "122" && sectorId != Guid.Empty && jobRoleId != Guid.Empty && classId != Guid.Empty && !string.IsNullOrEmpty(unitName)) || (courseModuleId == "121" && classId != Guid.Empty && !string.IsNullOrEmpty(unitName)))
                                {
                                    CourseModuleModel courseModuleModel = courseModuleModels.FirstOrDefault(s => s.SectorId == sectorId && s.JobRoleId == jobRoleId && s.ModuleTypeId == courseModuleId && s.ClassId == classId && s.UnitName == unitName);

                                    if (courseModuleModel == null)
                                    {
                                        courseModuleModel = new CourseModuleModel
                                        {
                                            ClassId = classId,
                                            ModuleTypeId = courseModuleId,
                                            SectorId = Guid.Equals(sectorId, Guid.Empty) ? (Guid?)null : sectorId,
                                            JobRoleId = Guid.Equals(jobRoleId, Guid.Empty) ? (Guid?)null : jobRoleId,
                                            UnitName = unitName,
                                            DisplayOrder = 1,
                                            Remarks = null,
                                            CreatedBy = "System",
                                            CreatedOn = Constants.GetCurrentDateTime,
                                            UpdatedBy = "System",
                                            UpdatedOn = Constants.GetCurrentDateTime,
                                            IsActive = true,
                                            RowIndex = rowIndex
                                        };

                                        courseModuleModel.Sessions.Add(new UnitSessionModel { SessionName = sessionName, DisplayOrder = 1 });
                                        courseModuleModels.Add(courseModuleModel);
                                    }
                                    else
                                    {
                                        int displayOrder = courseModuleModel.Sessions.Max(s => s.DisplayOrder);
                                        courseModuleModel.Sessions.Add(new UnitSessionModel { SessionName = sessionName, DisplayOrder = (displayOrder + 1) });
                                    }
                                }
                                else
                                {
                                    this.errorMessage = string.Empty;
                                    if (sectorId == Guid.Empty)
                                        this.errorMessage += "Invalid sector Which is not found in Database, ";

                                    if (jobRoleId == Guid.Empty)
                                        this.errorMessage += "Invalid jobRole Which is not found in Database, ";

                                    if (classId == Guid.Empty)
                                        this.errorMessage += "Invalid class Which is not found in Database, ";

                                    if (string.IsNullOrEmpty(courseModuleName))
                                        this.errorMessage += "Invalid Course Module Name Which is not found in Database, ";

                                    if (string.IsNullOrEmpty(unitName))
                                        this.errorMessage += "Unit name is required, ";

                                    dtCourseModule.Rows[rowIndex]["Status"] = "Failed";
                                    dtCourseModule.Rows[rowIndex]["Exception"] += this.errorMessage;
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                                dtCourseModule.Rows[rowIndex]["Status"] = "Failed";
                                dtCourseModule.Rows[rowIndex]["Exception"] += ex.Message + ", ";
                            }
                        }
                    }

                    excelReader.Close();
                    excelReader.Dispose();
                }

                if (isValidTemplate)
                {
                    for (int cmIndex = 1; cmIndex < courseModuleModels.Count; cmIndex++)
                    {
                        var courseModuleItem = courseModuleModels[cmIndex];
                        int displayOrder = courseModuleItem.Sessions.Count + 1;

                        courseModuleItem.DisplayOrder = (cmIndex + 1);
                        courseModuleItem.Sessions.Add(new UnitSessionModel { SessionName = string.Format("{0}. Others", displayOrder), DisplayOrder = displayOrder });

                        SingularResponse<string> response = courseModuleManager.SaveOrUpdateCourseModuleDetails(courseModuleModels[cmIndex], true);
                        responses.Add(response);

                        DataRow drCourseModule = dtCourseModule.Rows[courseModuleItem.RowIndex];
                        if (response.Errors.Count > 0)
                        {
                            drCourseModule["Status"] = "Failed";
                            drCourseModule["Exception"] = string.Join(",", response.Errors);
                        }
                        else
                        {
                            drCourseModule["Status"] = "Success";
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("CourseModules-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtCourseModule, fileName);

                if (!string.IsNullOrEmpty(fileUrl) && responses.Count > 0)
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { Constants.ExcelTemplateErrorMessage },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("UploadStudentsForExitSurvey")]
        public SingularResponse<string> UploadStudentsForExitSurvey(IFormFile excelData)
        {
            DataTable dtStudent = new DataTable();

            try
            {
                // Reading Student data from excel template
                dtStudent = this.ReadExcelTemplateData(excelData);

                #region Validate Student template format and data type

                List<string> columnNamesInDB = new List<string> { "StudentFullName", "FatherName", "StudentUniqueId", "NameOfSchool", "UdiseCode", "District", "Class", "Gender", "DOB", "Category", "Sector", "JobRole", "VTPName" };

                List<string> columnNamesInTemplate = dtStudent.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

                columnNamesInDB.ForEach(columnName =>
                {
                    if (!columnNamesInTemplate.Contains(columnName))
                        validationErrors.Add(columnName);
                });

                if (validationErrors.Count > 0)
                {
                    dtStudent.Rows[1]["Exception"] = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    isValidTemplate = false;
                }

                #endregion Validate Student template format and data type

                #region Get master data for Students

                //// Get distinct AcademicYears
                //var academicYears = this.academicYearManager.GetAcademicYears().Select(x => new AcademicYearModel { AcademicYearId = x.AcademicYearId, YearName = x.YearName }).Distinct().ToList();
                //Dictionary<Guid, string> dicAcademicYears = academicYears.ToDictionary(x => x.AcademicYearId, x => x.YearName.StringVal().ToUpper());

                // Get distinct Schools
                List<string> schoolNamesInTemplate = dtStudent.Select().AsEnumerable().Select(s => s["NameOfSchool"].StringVal()).Distinct().ToList();

                var schools = this.schoolManager.GetSchoolsByNames(schoolNamesInTemplate).Select(x => new SchoolModel { SchoolId = x.SchoolId, SchoolName = string.Format("{0}-{1}", x.SchoolName, x.Udise) }).Distinct().ToList();
                Dictionary<Guid, string> dicSchools = schools.ToDictionary(x => x.SchoolId, x => x.SchoolName.StringVal().ToUpper());

                // Get distinct Classes
                var classes = this.schoolClassManager.GetSchoolClasses().Select(x => new SchoolClassModel { ClassId = x.ClassId, Name = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicClasses = classes.ToDictionary(x => x.ClassId, x => x.Name.StringVal().ToUpper());

                // Get distinct Sectors
                var sectors = this.sectorManager.GetSectors().Select(x => new SectorModel { SectorId = x.SectorId, SectorName = x.SectorName }).Distinct().ToList();
                Dictionary<Guid, string> dicSectors = sectors.ToDictionary(x => x.SectorId, x => x.SectorName.StringVal().ToUpper());

                // Get distinct VTPs
                var vtps = this.vocationalTrainingProviderManager.GetVTPList().Select(x => new VocationalTrainingProviderModel { VTPId = x.Id, VTPShortName = x.Name }).Distinct().ToList();
                Dictionary<Guid, string> dicVtps = vtps.ToDictionary(x => x.VTPId, x => x.VTPName.StringVal().ToUpper());

                // Get distinct Genders
                var genders = this.dataValueManager.GetDataValuesByType("StudentGender").Select(x => new DataValueModel { DataValueId = x.DataValueId, Name = x.Name }).Distinct().ToList();
                Dictionary<string, string> dicGenders = genders.ToDictionary(x => x.DataValueId, x => x.Name.StringVal().ToUpper());

                // Get distinct Students
                List<string> studentsInTemplate = dtStudent.Select().AsEnumerable().Select(s => s["StudentFullName"].StringVal()).Distinct().ToList();

                var students = this.studentClassManager.GetStudentsByNames(studentsInTemplate).Select(x => new StudentClassModel { StudentId = x.StudentId, FullName = x.FullName }).Distinct().ToList();
                List<string> dicStudents = students.Select(x => x.FullName).ToList();

                #endregion Get master data for Students

                if (isValidTemplate)
                {
                    for (int rowIndex = 0; rowIndex < dtStudent.Rows.Count; rowIndex++)
                    {
                        DataRow drStudent = dtStudent.Rows[rowIndex];
                        academicYearId = classId = sectionId = VTId = schoolId = Guid.Empty;

                        try
                        {
                            string schoolName = string.Format("{0}-{1}", drStudent["NameOfSchool"].StringVal(), drStudent["UdiseCode"].StringVal()).ToUpper();
                            if (!string.IsNullOrEmpty(schoolName))
                            {
                                if (dicSchools.ContainsValue(schoolName))
                                {
                                    schoolId = dicSchools.FirstOrDefault(x => x.Value == schoolName).Key;
                                }
                            }

                            //string academicYear = drStudent["AcademicYear"].StringVal().ToUpper();
                            //if (!string.IsNullOrEmpty(academicYear))
                            //{
                            //    if (dicAcademicYears.ContainsValue(academicYear))
                            //    {
                            //        academicYearId = dicAcademicYears.FirstOrDefault(x => x.Value == academicYear).Key;
                            //    }
                            //}

                            string className = drStudent["Class"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(className))
                            {
                                if (dicClasses.ContainsValue(className))
                                {
                                    classId = dicClasses.FirstOrDefault(x => x.Value == className).Key;
                                }
                            }

                            string sectorName = drStudent["Sector"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(sectorName))
                            {
                                if (dicSectors.ContainsValue(sectorName))
                                {
                                    sectorId = dicSectors.FirstOrDefault(x => x.Value == sectorName).Key;
                                }
                            }

                            string VTPName = drStudent["VTPName"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(VTPName))
                            {
                                if (dicVtps.ContainsValue(VTPName))
                                {
                                    VTPId = dicVtps.FirstOrDefault(x => x.Value == VTPName).Key;
                                }
                            }

                            VocationalTrainerModel vocationalTrainerModel = new VocationalTrainerModel();
                            VocationalCoordinatorModel vocationalCoordinator = new VocationalCoordinatorModel();
                            VTSchoolSectorModel vtSchoolSectorModel = new VTSchoolSectorModel();
                            vtSchoolSectorModel = this.vtSchoolSectorManager.GetVTSchoolSectorBySchoolIdANDSectorId(schoolId, sectorId);

                            if (vtSchoolSectorModel != null)
                            {
                                vocationalTrainerModel = this.vocationalTrainerManager.GetVocationalTrainerById(new DataRequest { DataId = vtSchoolSectorModel.AcademicYearId.ToString(), DataId1 = vocationalTrainerModel.VTPId.ToString(), DataId2 = vocationalTrainerModel.VCId.ToString(), DataId3 = vocationalTrainerModel.VTId.ToString() });
                                if (vocationalTrainerModel != null)
                                {
                                    VTId = vocationalTrainerModel.VTId;
                                    vocationalCoordinator = this.vocationalCoordinatorManager.GetVocationalCoordinatorById(new DataRequest { DataId = vtSchoolSectorModel.AcademicYearId.ToString(), DataId1 = vocationalTrainerModel.VTPId.ToString(), DataId2 = vocationalTrainerModel.VCId.ToString() });
                                }
                            }

                            string gender = drStudent["Gender"].StringVal().ToUpper();
                            if (!string.IsNullOrEmpty(gender))
                            {
                                if (dicGenders.ContainsValue(gender))
                                {
                                    genderId = dicGenders.FirstOrDefault(x => x.Value == gender).Key;
                                }
                            }

                            //string socialCategory = drStudent["Category"].StringVal().ToUpper();
                            //if (!string.IsNullOrEmpty(socialCategory))
                            //{
                            //    if (dicSocialCategories.ContainsValue(socialCategory))
                            //    {
                            //        socialcategoryId = dicSocialCategories.FirstOrDefault(x => x.Value == socialCategory).Key;
                            //    }
                            //}

                            if (schoolId != Guid.Empty && VTId != Guid.Empty && classId != Guid.Empty && sectorId != Guid.Empty && !string.IsNullOrEmpty(drStudent["StudentFullName"].StringVal()) && !string.IsNullOrEmpty(genderId))
                            {
                                StudentsForExitFormModel studentForExitFormModelEntity = new StudentsForExitFormModel
                                {
                                    AcademicYearId = vtSchoolSectorModel.AcademicYearId,
                                    StudentFullName = drStudent["StudentFullName"].StringVal().ToTitleCase(),
                                    FatherName = drStudent["FatherName"].StringVal().ToTitleCase(),
                                    StudentUniqueId = drStudent["StudentUniqueId"].StringVal().ToTitleCase(),
                                    NameOfSchool = drStudent["NameOfSchool"].StringVal().ToTitleCase(),
                                    UdiseCode = drStudent["UdiseCode"].StringVal(),
                                    District = drStudent["District"].StringVal().ToTitleCase(),
                                    Class = drStudent["Class"].StringVal(),
                                    Gender = drStudent["Gender"].StringVal().ToTitleCase(),
                                    DOB = Convert.ToDateTime(drStudent["DOB"]),
                                    Category = drStudent["Category"].StringVal().ToTitleCase(),
                                    Sector = drStudent["Sector"].StringVal().ToTitleCase(),
                                    JobRole = drStudent["JobRole"].StringVal().ToTitleCase(),
                                    VTPId = VTPId,
                                    VTPName = drStudent["VTPName"].StringVal().ToTitleCase(),
                                    VTId = VTId,
                                    VTName = vocationalTrainerModel.FullName,
                                    VCId = vocationalTrainerModel.VCId,
                                    VCName = vocationalCoordinator.FullName,
                                    CreatedBy = "System",
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = "System",
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = true,
                                };

                                var studentResponse = this.studentsForExitFormManager.SaveOrUpdateStudentsForExitFormDetails(studentForExitFormModelEntity);

                                if (studentResponse.Errors.Count > 0)
                                {
                                    drStudent["Status"] = "Failed";
                                    drStudent["Exception"] = string.Join(",", studentResponse.Errors);
                                }
                                else
                                {
                                    drStudent["Status"] = "Success";

                                    // Add student in List
                                    dicStudents.Add(studentForExitFormModelEntity.StudentFullName);
                                }

                                //if (!string.IsNullOrEmpty(studentForExitFormModelEntity.StudentFullName))
                                //{
                                //    if (dicStudents.Contains(studentForExitFormModelEntity.StudentFullName))
                                //    {
                                //       // studentId = dicStudents.FirstOrDefault(x => x.Value == studentClassModelEntity.FullName).Key;
                                //    }
                                //}
                            }
                            else
                            {
                                this.errorMessage = string.Empty;

                                if (schoolId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid School Name Which is not found in Database\n";
                                }
                                if (VTId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid VT Name Which is not found in Database\n";
                                }
                                if (classId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Class Name Which is not found in Database\n";
                                }
                                if (sectorId == Guid.Empty)
                                {
                                    this.errorMessage += "Invalid Sector Which is not found in Database\n";
                                }
                                if (string.IsNullOrEmpty(genderId))
                                {
                                    this.errorMessage += "Gender is required\n";
                                }

                                drStudent["Status"] += "Student Details: Failed";
                                drStudent["Exception"] += this.errorMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorManager.Instance.WriteErrorLogsInFile(ex);

                            drStudent["Status"] = "Failed";
                            drStudent["Exception"] = ex.Message;
                        }
                    }
                }

                //file name to be created
                string fileName = string.Format("StudentsForExitForm-Upload-{0}.xlsx", Constants.GetCurrentDateTime.ToString("yyyyMMddHHmmss"));
                string fileUrl = this.WriteExcelFromDataTable(dtStudent, fileName);

                if (isValidTemplate && !string.IsNullOrEmpty(fileUrl))
                {
                    this.fileResponse = new SingularResponse<string>
                    {
                        Messages = new List<string> { fileUrl },
                    };
                }
                else if (isValidTemplate == false)
                {
                    string errorMessages = string.Format("Invalid {0} field in excel template", string.Join(",", validationErrors));
                    this.fileResponse = new SingularResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { string.Format(Constants.ExcelTemplateErrorMessage, errorMessages) },
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                this.fileResponse = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return fileResponse;
        }

        [HttpPost, Route("UploadExcelData")]
        public SingularResponse<string> UploadExcelData([FromBody] FileUploadModel uploadModel)
        {
            SingularResponse<string> responses = new SingularResponse<string>();

            ErrorManager.Instance.WriteErrorLogsInFile("Step 1 : Started bulk data uploading");

            try
            {
                if (!string.IsNullOrEmpty(uploadModel.Base64Data))
                {
                    string base64Data = uploadModel.Base64Data.Split(',').LastOrDefault();
                    //string base64Data = uploadModel.Base64Data.Split(new string[] { "base64," }, StringSplitOptions.None).LastOrDefault();

                    byte[] excelBytes = Convert.FromBase64String(base64Data);
                    Stream ms = new MemoryStream(excelBytes);
                    uploadModel.UploadFile = new FormFile(ms, 0, ms.Length, Path.GetFileNameWithoutExtension(uploadModel.FileName), uploadModel.FileName);
                }

                switch (uploadModel.ContentType)
                {
                    case "Schools":
                        responses = this.SchoolExcelUpload(uploadModel.UploadFile);
                        break;

                    case "HeadMasters":
                        responses = this.HeadMastersExcelUpload(uploadModel.UploadFile);
                        break;

                    case "SchoolVEIncharges":
                        responses = this.SchoolIVEInchargeExcelUpload(uploadModel.UploadFile);
                        break;

                    case "VTSchoolSectors":
                        responses = this.VTSchoolSectorsExcelUpload(uploadModel.UploadFile);
                        break;

                    case "SectorJobRoles":
                        responses = this.SectorJobRolesExcelUpload(uploadModel.UploadFile);
                        break;

                    case "VCSchoolSectors":
                        responses = this.VCSchoolSectorsExcelUpload(uploadModel.UploadFile);
                        break;

                    case "VocationalCoordinators":
                        responses = this.VocationalCoordinatorsExcelUpload(uploadModel.UploadFile);
                        break;

                    case "VocationalTrainingProviders":
                        responses = this.VocationalTrainingProvidersExcelUpload(uploadModel.UploadFile);
                        break;

                    case "SchoolVTPSectors":
                        responses = this.SchoolVTPSectorsExcelUpload(uploadModel.UploadFile);
                        break;

                    case "VocationalTrainers":
                        responses = this.VocationalTrainersExcelUpload(uploadModel.UploadFile);
                        break;

                    case "VTClasses":
                        responses = this.VTClassesExcelUpload(uploadModel.UploadFile);
                        break;

                    case "VTPSectors":
                        responses = this.VTPSectorsExcelUpload(uploadModel.UploadFile);
                        break;

                    case "Students":
                        responses = this.StudentsExcelUpload(uploadModel.UploadFile);
                        break;

                    case "Employer":
                        responses = this.EmployerExcelUpload(uploadModel.UploadFile);
                        break;

                    case "CourseModules":
                        responses = this.VocationalCourseModuleExcelUpload(uploadModel.UploadFile);
                        break;

                    case "ExitSurvey":
                        responses = this.UploadStudentsForExitSurvey(uploadModel.UploadFile);
                        break;

                    default:
                        break;
                }

                ErrorManager.Instance.WriteErrorLogsInFile("Step 3 : Finished bulk data uploading\n\n");
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile("Step Z : " + ex.Message);
                ErrorManager.Instance.WriteErrorLogsInFile(ex);

                responses = new SingularResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { string.Format("Error : Please check uploaded excel template data\n{0}", ex.Message) },
                };
            }

            return responses;
        }

        private string WriteExcelFromDataTable(DataTable dtExcelData, string fileName)
        {
            string excelFolderPath = Path.Combine(Constants.RootPath, Constants.DocumentPath, "BulkUpload");
            if (!Directory.Exists(excelFolderPath))
                Directory.CreateDirectory(excelFolderPath);

            try
            {
                using (var fs = new FileStream(Path.Combine(excelFolderPath, fileName), FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet excelSheet = workbook.CreateSheet(dtExcelData.TableName);

                    List<String> columns = new List<string>();
                    IRow row = excelSheet.CreateRow(0);
                    int columnIndex = 0;

                    foreach (System.Data.DataColumn column in dtExcelData.Columns)
                    {
                        columns.Add(column.ColumnName);
                        row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                        columnIndex++;
                    }

                    int rowIndex = 1;
                    foreach (DataRow dsrow in dtExcelData.Rows)
                    {
                        row = excelSheet.CreateRow(rowIndex);
                        int cellIndex = 0;
                        foreach (String col in columns)
                        {
                            row.CreateCell(cellIndex).SetCellValue(dsrow[col].StringVal());
                            cellIndex++;
                        }

                        rowIndex++;
                    }
                    workbook.Write(fs);
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.WriteErrorLogsInFile("Step Y : Saving upload excel results\n" + ex.Message + "\n" + ex.StackTrace);
                throw ex;
            }

            return Path.Combine(Constants.DocumentPath, "BulkUpload", fileName);
        }

        /// <summary>
        /// Get excel template data in DataTable format
        /// </summary>
        /// <param name="excelData"></param>
        /// <returns></returns>
        private DataTable ReadExcelTemplateData(IFormFile excelData)
        {
            try
            {
                DataTable dtExcelData = new DataTable();

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new MemoryStream())
                {
                    excelData.CopyTo(stream);
                    stream.Position = 0;

                    IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    DataSet result = excelReader.AsDataSet(conf);
                    excelReader.Close();
                    excelReader.Dispose();

                    dtExcelData = result.Tables[0];
                    dtExcelData.Columns.Add(new DataColumn("Status", typeof(string)));
                    dtExcelData.Columns.Add(new DataColumn("Exception", typeof(string)));

                    return dtExcelData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}