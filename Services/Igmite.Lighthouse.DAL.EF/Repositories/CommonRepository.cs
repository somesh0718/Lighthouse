using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igmite.Lighthouse.DAL.EF
{
    public class CommonRepository : GenericRepository<Account>, ICommonRepository
    {
        public IList<DropdownModel<Guid>> GetRoles()
        {
            var roles = from r in this.Context.Roles
                        where r.IsActive == true && r.Code != "SUR"
                        select new DropdownModel<Guid>
                        {
                            Id = r.RoleId,
                            Name = r.Name
                        };

            return roles.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<Guid>> GetAllRoles()
        {
            var roles = from r in this.Context.Roles
                        select new DropdownModel<Guid>
                        {
                            Id = r.RoleId,
                            Name = r.Name
                        };

            return roles.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<Guid>> GetAccounts()
        {
            var users = from r in this.Context.Accounts
                        where r.IsActive == true
                        select new DropdownModel<Guid>
                        {
                            Id = r.AccountId,
                            Name = r.FirstName + " " + r.LastName
                        };

            return users.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<Guid>> GetSiteHeaders()
        {
            var siteHeaders = from r in this.Context.SiteHeaders
                              where r.IsActive == true
                              select new DropdownModel<Guid>
                              {
                                  Id = r.SiteHeaderId,
                                  Name = r.ShortName
                              };

            return siteHeaders.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<Guid>> GetTransactions()
        {
            var transactions = from r in this.Context.Transactions
                               where r.IsActive == true
                               select new DropdownModel<Guid>
                               {
                                   Id = r.TransactionId,
                                   Name = r.Name
                               };

            return transactions.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<string>> GetCountries()
        {
            var countries = from r in this.Context.Countries
                            where r.IsActive == true
                            select new DropdownModel<string>
                            {
                                Id = r.CountryCode,
                                Name = r.CountryName
                            };

            return countries.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<string>> GetStates(string countryCode)
        {
            IList<DropdownModel<string>> stateList = new List<DropdownModel<string>>();
            stateList.Add(new DropdownModel<string> { Id = string.Empty, Name = "Select State" });

            var states = (from s in this.Context.States
                          where s.IsActive == true
                          select new DropdownModel<string>
                          {
                              Id = s.StateCode,
                              Name = s.StateId + "-" + s.StateName + " (" + s.StateCode + ")"
                          }).OrderBy(s => s.Name).ToList();

            stateList = stateList.Concat(states).ToList();
            return stateList;
        }

        public IList<DropdownModel<string>> GetDistricts(string stateCode)
        {
            IList<DropdownModel<string>> districtList = new List<DropdownModel<string>>();
            districtList.Add(new DropdownModel<string> { Id = string.Empty, Name = "Select District" });

            var districts = (from c in this.Context.Districts
                             where c.IsActive == true && c.StateCode == stateCode
                             select new DropdownModel<string>
                             {
                                 Id = c.DistrictCode,
                                 Name = c.DistrictName
                             }).OrderBy(r => r.Name).ToList(); ;

            districtList = districtList.Concat(districts).ToList();
            return districtList;
        }

        public IList<DropdownModel<string>> GetDropdownDataById(string dataTypeId)
        {
            var dropDownData = (from r in this.Context.DataValues
                                where r.IsActive == true &&
                                r.DataTypeId == dataTypeId
                                select new DropdownModel<string>
                                {
                                    Id = r.DataValueId.ToString(),
                                    Name = r.Name
                                }).ToList();

            return dropDownData.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<string>> GetMasterDataForDropdown(MasterDataRequest dataRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "dataType", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "roleId", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.RoleId };
            sqlParams[2] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.UserId };
            sqlParams[3] = new MySqlParameter { ParameterName = "parentId", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.ParentId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetMasterDataForDropdown (@dataType, @roleId, @userId, @parentId)", sqlParams).ToList();
        }

        public IList<DropdownModel<string>> GetTargetVocationalTrainers(MasterDataForAcademicRollover request)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.VarChar, Value = request.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.VarChar, Value = request.SchoolId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.VarChar, Value = request.VTId };
            sqlParams[3] = new MySqlParameter { ParameterName = "classId", MySqlDbType = MySqlDbType.VarChar, Value = request.ClassId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetTargetVocationalTrainers (@academicYearId, @schoolId, @vtId, @classId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of class against Vocational Trainer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetClassesByVTId(string userId, Guid vtId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetClassesByVTId (@userId, @vtId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of sections against class for Vocational Trainer
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSectionsByVTClassId(Guid vtId, Guid classId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };
            sqlParams[1] = new MySqlParameter { ParameterName = "classId", MySqlDbType = MySqlDbType.Guid, Value = classId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSectionsByVTClassId (@vtId, @classId)", sqlParams).ToList();
        }

        public IList<DropdownModel<string>> GetStudentsByUserId(string userType, string userId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "userType", MySqlDbType = MySqlDbType.VarChar, Value = userType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetStudentsByUserId (@dataType, @userId)", sqlParams).ToList();
        }

        public IList<DropdownModel<string>> GetSchoolVTPSectorsByUserId(string userId, string userTypeId, string academicYearId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "userTypeId", MySqlDbType = MySqlDbType.VarChar, Value = userTypeId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId.StringVal() };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolVTPSectorsByUserId (@userId, @userTypeId, @academicYearId)", sqlParams).ToList();
        }

        public IList<HeaderMenuModel> GetAccountTransactions(string accountId, string action)
        {
            IList<HeaderMenuModel> transactions = new List<HeaderMenuModel>();

            using (var dbContext = ContextFactory.CreateContext())
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = accountId };
                sqlParams[1] = new MySqlParameter { ParameterName = "action", MySqlDbType = MySqlDbType.VarChar, Value = action };

                transactions = Context.Query<HeaderMenuModel>().FromSql("EXEC dbo.GetTransactionsByUserId @userId, @action", sqlParams).ToList();
            }

            return transactions;
        }

        public TermsCondition GetLatestTermsCondition()
        {
            return this.Context.TermsConditions.FirstOrDefault(t => t.IsActive == true);
        }

        public IList<DropdownModel<Guid>> GetAdministrator()
        {
            var adminRole = from a in this.Context.Accounts
                            join arm in this.Context.AccountRoles on a.AccountId equals arm.AccountId
                            join r in this.Context.Roles on arm.RoleId equals r.RoleId
                            where r.Code == "ADM" && a.IsActive == true
                            select new DropdownModel<Guid>
                            {
                                Id = a.AccountId,
                                Name = a.FirstName + " " + a.LastName
                            };

            return adminRole.OrderBy(r => r.Name).ToList();
        }

        public IList<DropdownModel<Guid>> GetEmployees()
        {
            Guid mdmRoleId = Guid.Parse("B3610158-37E0-4411-9157-F6F9530B409E");

            IList<string> mdmEmails = (from a in Context.Accounts
                                       join r in Context.AccountRoles on a.AccountId equals r.AccountId
                                       where r.RoleId == mdmRoleId
                                       select a.EmailId
                            ).ToList();

            var employees = (from s in this.Context.Employees
                             where s.IsActive == true && !mdmEmails.Contains(s.EmailId)
                             select new DropdownModel<Guid>
                             {
                                 Id = s.AccountId,
                                 Name = s.FirstName + " " + s.LastName
                             }).OrderBy(s => s.Name).ToList();

            return employees;
        }

        public string GetCommercialContractNameById(Guid employeeId)
        {
            var employee = this.Context.Employees.FirstOrDefault(e => e.IsActive == true && e.AccountId == employeeId);

            return employee != null ? string.Format("{0} {1}", employee.FirstName, employee.LastName) : string.Empty;
        }

        /// <summary>
        /// Get VCSchoolSector by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public VCSchoolSector GetVCSchoolSectorsByUserId(string userId)
        {
            AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(a => a.IsCurrentAcademicYear == true);

            VCSchoolSector vcSchoolSectors = (from vsc in this.Context.VCSchoolSectors
                                              join vc in this.Context.VocationalCoordinators on vsc.VCId equals vc.VCId
                                              join ac in this.Context.Accounts on vc.EmailId equals ac.LoginId
                                              where vsc.AcademicYearId == academicYear.AcademicYearId && ac.IsActive == true && ac.UserId == userId
                                              select vsc).FirstOrDefault();

            return vcSchoolSectors;
        }

        /// <summary>
        /// Get VTSchoolSector by VTId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public VTSchoolSector GetVTSchoolSectorsByVTId(Guid vtId)
        {
            AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(y => y.IsCurrentAcademicYear == true);
            return this.Context.VTSchoolSectors.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.VTId == vtId && v.IsActive == true);
        }

        /// <summary>
        /// Get VTSchoolSector by VTId & AcademicYearId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public VTSchoolSector GetVTSchoolSectorsByVTAYId(Guid vtId, Guid academicYearId)
        {
            return this.Context.VTSchoolSectors.FirstOrDefault(v => v.AcademicYearId == academicYearId && v.VTId == vtId && v.IsActive == true);
        }

        /// <summary>
        /// Get VTSchoolSector by VTId & Current AcademicYearId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public VTSchoolSector GetVTSchoolSectorsByCurrentAYVTId(Guid vtId)
        {
            VTSchoolSector vtSchoolSector = (from vss in this.Context.VTSchoolSectors
                                             join ay in this.Context.AcademicYears on vss.AcademicYearId equals ay.AcademicYearId
                                             where vss.IsActive == true && vss.VTId == vtId && ay.IsCurrentAcademicYear == true
                                             select vss).FirstOrDefault();

            return vtSchoolSector;
        }

        /// <summary>
        /// Get VTClass by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public VTClass GetVTClassByUserId(string userId)
        {
            VTClass vtClass = (from vsc in this.Context.VTClasses
                               join vt in this.Context.VocationalTrainers on vsc.VTId equals vt.VTId
                               join ac in this.Context.Accounts on vt.Email equals ac.LoginId
                               join vct in this.Context.VCTrainersMap on new { a = vsc.AcademicYearId, b = vsc.VTId } equals new { a = vct.AcademicYearId, b = vct.VTId }
                               where ac.IsActive == true && vct.IsActive == true && ac.UserId == userId
                               select vsc).FirstOrDefault();

            return vtClass;
        }

        /// <summary>
        /// Get Current Academic Year
        /// </summary>
        /// <returns></returns>
        public AcademicYear GetAcademicYear()
        {
            return this.Context.AcademicYears.FirstOrDefault(y => y.IsCurrentAcademicYear == true);
        }

        /// <summary>
        /// Get SchoolClassMapping by SchoolId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentClassMapping GetSchoolClassMappingBySchoolId(Guid academicYearId, Guid studentId)
        {
            return this.Context.StudentClassMapping.FirstOrDefault(v => v.AcademicYearId == academicYearId && v.StudentId == studentId && v.IsActive == true);
        }

        /// <summary>
        /// Get VCTrainerMap by VTId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public VCTrainerMap GetVCTrainerMapByVTId(Guid academicYearId, Guid vtId)
        {
            return this.Context.VCTrainersMap.FirstOrDefault(v => v.AcademicYearId == academicYearId && v.VTId == vtId && v.IsActive == true);
        }

        /// <summary>
        /// Get List of Units by Class, Module and JobRole
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="moduleTypeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetUnitsByClassAndModuleId(Guid classId, string moduleTypeId, Guid userId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "classId", MySqlDbType = MySqlDbType.Guid, Value = classId };
            sqlParams[1] = new MySqlParameter { ParameterName = "moduleTypeId", MySqlDbType = MySqlDbType.VarChar, Value = moduleTypeId };
            sqlParams[2] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = userId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetUnitsByClassAndModuleId (@classId, @moduleTypeId, @userId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get List of Sessions by Unit
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSessionsByUnitId(Guid unitId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "unitId", MySqlDbType = MySqlDbType.Guid, Value = unitId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSessionsByUnitId (@unitId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get List of Students by Class and Section
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public IList<StudentAttendanceModel> GetStudentsByClassIdForVT(Guid userId, Guid classId, Guid sectionId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = userId };
            sqlParams[1] = new MySqlParameter { ParameterName = "classId", MySqlDbType = MySqlDbType.Guid, Value = classId };
            sqlParams[2] = new MySqlParameter { ParameterName = "sectionId", MySqlDbType = MySqlDbType.Guid, Value = sectionId };

            return Context.StudentAttendanceModels.FromSql<StudentAttendanceModel>("CALL GetStudentsByClassIdForVT (@userId, @classId, @sectionId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of school by VC
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolsByVCId(string userId, Guid vcId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = vcId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolsByVCId (@userId, @vcId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of school by DRP
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="drpId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolsByDRPId(string userId, Guid drpId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "drpId", MySqlDbType = MySqlDbType.Guid, Value = drpId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolsByDRPId (@userId, @drpId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of Course Module >> Units >> Sessions by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public IList<ModuleUnitSessionModel> GetCourseModuleUnitSessions(string userId, Guid vtId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };

            return Context.ModuleUnitSessionModels.FromSql<ModuleUnitSessionModel>("CALL GetCourseModuleUnitSessions (@userId, @vtId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of Master Data
        /// </summary>
        /// <returns></returns>
        public IList<MasterDataModel> GetCommonMasterData(string userId)
        {
            //MySqlParameter[] sqlParams = new MySqlParameter[1];
            //sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };
            //return Context.MasterDataModels.FromSql<MasterDataModel>("CALL GetCommonMasterData(@userId)").ToList();

            IList<MasterDataModel> masterData = new List<MasterDataModel>();
            masterData = this.Context.DataValues.Where(x => x.IsActive == true).Select(m => new MasterDataModel
            {
                DataValueId = m.DataValueId,
                DataTypeId = m.DataTypeId,
                ParentId = m.ParentId,
                Code = m.Code,
                Name = m.Name,
                Description = m.Description,
                DisplayOrder = m.DisplayOrder
            }).ToList();

            return masterData;
        }

        /// <summary>
        /// Get list of Class Sections by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public IList<ClassSectionModel> GetClassSectionsByVTId(string userId, Guid vtId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };

            return Context.ClassSectionModels.FromSql<ClassSectionModel>("CALL GetClassSectionsByVTId (@userId, @vtId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of student by VT
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public IList<StudentByVTModel> GetStudentsByVTId(Guid vtId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };

            return Context.StudentByVTModels.FromSql<StudentByVTModel>("CALL GetStudentsByVTId (@vtId)", sqlParams).ToList();
        }

        /// <summary>
        /// Save Lighthouse Settings
        /// </summary>
        /// <param name="settingModel"></param>
        /// <returns></returns>
        public SettingModel SaveLighthouseSettings(SettingModel settingModel)
        {
            Account account = this.Context.Accounts.FirstOrDefault(a => a.IsActive == true && a.AccountId == settingModel.UserId);

            return new SettingModel
            {
                RoleId = settingModel.RoleId,
                UserId = settingModel.UserId,
                LoginId = account.LoginId,
                Password = account.Password
            };
        }

        /// <summary>
        /// Get Vocational Trainer by VTId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public VocationalTrainer GetVocationalTrainerById(Guid vtId)
        {
            VocationalTrainer vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.VTId == vtId);
            if (vocationalTrainer != null)
            {
                AcademicYear academicYear = Context.AcademicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true);
                if (academicYear != null)
                {
                    VCTrainerMap vcTrainer = this.Context.VCTrainersMap.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.VTId == vocationalTrainer.VTId && v.IsActive == true);

                    if (vcTrainer != null)
                    {
                        vocationalTrainer.VCTrainer = vcTrainer;
                    }
                }
            }

            return vocationalTrainer;
        }

        /// <summary>
        /// Get Vocational Trainer by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public VocationalTrainer GetVocationalTrainerBySchoolId(Guid schoolId)
        {
            AcademicYear academicYear = Context.AcademicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true);

            VocationalTrainer vocationalTrainer = (from vt in this.Context.VocationalTrainers
                                                   join vtss in this.Context.VTSchoolSectors on vt.VTId equals vtss.VTId
                                                   where vtss.AcademicYearId == academicYear.AcademicYearId && vtss.SchoolId == schoolId
                                                   select vt).FirstOrDefault();

            if (vocationalTrainer != null)
            {
                if (academicYear != null)
                {
                    VCTrainerMap vcTrainer = this.Context.VCTrainersMap.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.VTId == vocationalTrainer.VTId && v.IsActive == true);

                    if (vcTrainer != null)
                    {
                        vocationalTrainer.VCTrainer = vcTrainer;
                    }
                }
            }

            return vocationalTrainer;
        }

        /// <summary>
        /// Get VTP and VC Id by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public LhUserModel GetVTPandVCIdBySchoolId(Guid schoolId)
        {
            var vtpVCId = (from svs in this.Context.SchoolVTPSectors
                           join vcss in this.Context.VCSchoolSectors on new { a = svs.AcademicYearId, b = svs.SchoolVTPSectorId } equals new { a = vcss.AcademicYearId, b = vcss.SchoolVTPSectorId }
                           join ay in this.Context.AcademicYears on svs.AcademicYearId equals ay.AcademicYearId
                           where svs.SchoolId == schoolId && svs.IsActive == true && ay.IsCurrentAcademicYear == true
                           select new LhUserModel
                           {
                               AcademicYearId = svs.AcademicYearId,
                               VTPId = svs.VTPId,
                               VCId = vcss.VCId
                           }).FirstOrDefault();

            return vtpVCId;
        }

        /// <summary>
        /// Get VTP,School and VC Id by VTID
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public LhUserModel GetVTPVCAndSchoolIdByVTId(Guid vtId)
        {
            var vtpVCId = (from vss in this.Context.VTSchoolSectors
                           join vt in this.Context.VCTrainersMap on new { a = vss.AcademicYearId, b = vss.VTId } equals new { a = vt.AcademicYearId, b = vt.VTId }
                           join ay in this.Context.AcademicYears on vss.AcademicYearId equals ay.AcademicYearId
                           where vss.IsActive == true && vt.IsActive == true && ay.IsCurrentAcademicYear == true && vt.VTId == vtId
                           select new LhUserModel
                           {
                               AcademicYearId = vt.AcademicYearId,
                               VTPId = vt.VTPId,
                               VCId = vt.VCId,
                               VTId = vt.VTId,
                               SchoolId = vss.SchoolId
                           }).FirstOrDefault();

            return vtpVCId;
        }

        /// <summary>
        /// Get VTP, VC & School Id by HMId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public LhUserModel GetVTPVCSchoolIdByHMId(Guid hmId)
        {
            var vtpVCId = (from hms in this.Context.HMSchoolMap
                           join hm in this.Context.HeadMasters on hms.HMId equals hm.HMId
                           join svs in this.Context.SchoolVTPSectors on hms.SchoolId equals svs.SchoolId
                           join vcss in this.Context.VCSchoolSectors on svs.SchoolVTPSectorId equals vcss.SchoolVTPSectorId
                           join ay in this.Context.AcademicYears on hms.AcademicYearId equals ay.AcademicYearId
                           where hm.IsActive == true && svs.IsActive == true && ay.IsCurrentAcademicYear == true && hm.HMId == hmId
                           select new LhUserModel
                           {
                               AcademicYearId = hms.AcademicYearId,
                               VTPId = svs.VTPId,
                               VCId = vcss.VCId,
                               SchoolId = hms.SchoolId
                           }).FirstOrDefault();

            return vtpVCId;
        }

        /// <summary>
        /// Send AppVersion to Users
        /// </summary>
        /// <param name="appVersionModel"></param>
        /// <returns></returns>
        public dynamic GetAppVersion(AppVersionModel appVersionModel)
        {
            var dataValue = (from dv in this.Context.DataValues
                             where dv.DataTypeId == appVersionModel.PlatformName
                             select new { AppVersion = dv.Description, UpdatedVersionDate = dv.UpdatedOn }).FirstOrDefault();

            return dataValue;
        }

        /// <summary>
        /// Get VTP by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTPByHMId(Guid academicYearId, Guid hmId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.VarChar, Value = hmId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTPByHMId (@AcademicYearId, @HMId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VC by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVCByHMId(Guid academicYearId, Guid hmId, Guid vtpId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.VarChar, Value = hmId };
            sqlParams[2] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVCByHMId (@AcademicYearId, @HMId, @VTPId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VT by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByHMId(Guid academicYearId, Guid hmId, Guid vcId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.VarChar, Value = hmId };
            sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTByHMId (@AcademicYearId, @HMId, @VCId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get School by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public dynamic GetSchoolByHMId(Guid academicYearId, Guid hmId, Guid vcId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.VarChar, Value = hmId };
            sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolByHMId (@AcademicYearId, @HMId, @VCId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VT by SchoolIdHMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public dynamic GetVTBySchoolIdHMId(Guid academicYearId, Guid hmId, Guid vcId, Guid schoolId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.VarChar, Value = hmId };
            sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };
            sqlParams[3] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.VarChar, Value = schoolId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTBySchoolIdHMId (@AcademicYearId, @HMId, @VCId, @SchoolId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VTP by AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public dynamic GetVTPByAYId(string roleId, Guid userId, Guid academicYearId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "RoleId", MySqlDbType = MySqlDbType.VarChar, Value = roleId };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTPByAYId (@RoleId, @UserId, @AcademicYearId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VC by AcademicYearId And VTPId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public dynamic GetVCByAYAndVTPId(string roleId, Guid userId, Guid academicYearId, Guid vtpId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "RoleId", MySqlDbType = MySqlDbType.VarChar, Value = roleId };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVCByAYAndVTPId (@RoleId, @UserId, @AcademicYearId, @VTPId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VT by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYAndVCId(DataRequest dataRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "RoleId", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.DataId };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.DataId1 };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.DataId2 };
            sqlParams[3] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = dataRequest.DataId3 };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTByAYAndVCId (@RoleId, @UserId, @AcademicYearId, @VCId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get School by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolByAYAndVCId(string roleId, Guid userId, Guid academicYearId, Guid vtpId, Guid vcId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "RoleId", MySqlDbType = MySqlDbType.VarChar, Value = roleId };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };
            sqlParams[4] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolByAYAndVCId (@RoleId, @UserId, @AcademicYearId, @VTPId, @VCId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="vtRequest"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYAndSchoolId(DataRequest vtRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[6];
            sqlParams[0] = new MySqlParameter { ParameterName = "RoleId", MySqlDbType = MySqlDbType.VarChar, Value = vtRequest.DataId };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = vtRequest.DataId1 };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = vtRequest.DataId2 };
            sqlParams[3] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = vtRequest.DataId3 };
            sqlParams[4] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.Guid, Value = vtRequest.DataId4 };
            sqlParams[5] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.Guid, Value = vtRequest.DataId5 };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTByAYAndSchoolId (@RoleId, @UserId, @AcademicYearId, @VTPId, @VCId, @SchoolId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VT by AcademicYearId,VTPId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYAndVTPIdVCId(string roleId, Guid userId, Guid academicYearId, Guid vtpId, Guid vcId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "RoleId", MySqlDbType = MySqlDbType.VarChar, Value = roleId };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };
            sqlParams[4] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTByAYAndVTPIdVCId (@RoleId, @UserId, @AcademicYearId, @VTPId, @VCId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get JobRole by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetJobRoleByVTIdAyIdSchoolId(Guid vtId, Guid academicYearId, Guid schoolId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.VarChar, Value = vtId };
            sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[2] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.VarChar, Value = schoolId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetJobRoleByVTIdAyIdSchoolId (@VTId, @AcademicYearId, @SchoolId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSectorByVTIdAyIdSchoolId(Guid vtId, Guid academicYearId, Guid schoolId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.VarChar, Value = vtId };
            sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[2] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.VarChar, Value = schoolId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSectorByVTIdAyIdSchoolId (@VTId, @AcademicYearId, @SchoolId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VTPId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSectorByAyIdVTPId(Guid academicYearId, Guid vtpId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSectorByAyIdVTPId ( @AcademicYearId, @VTPId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VCId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSectorByAyIdVCId(Guid academicYearId, Guid vcId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSectorByAyIdVCId ( @AcademicYearId, @VCId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get Student by ClassId And SectionId
        /// </summary>
        /// <param name="studentRequest"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetStudentsByClassIdSectionId(DataRequest studentRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = studentRequest.DataId };
            sqlParams[1] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.VarChar, Value = studentRequest.DataId1 };
            sqlParams[2] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.VarChar, Value = studentRequest.DataId2 };
            sqlParams[3] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = studentRequest.DataId3 };
            sqlParams[4] = new MySqlParameter { ParameterName = "SectionId", MySqlDbType = MySqlDbType.VarChar, Value = studentRequest.DataId4 };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetStudentsByClassIdSectionId (@AcademicYearId, @SchoolId, @VTId, @ClassId, @SectionId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get School by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolsByAcademicYearId(Guid academicYearId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[1];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolsByAYId (@AcademicYearId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVocationalTrainersByAcademicYearIdAndSchoolId(Guid academicYearId, Guid schoolId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.VarChar, Value = schoolId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVocationalTrainersByAYIdAndSchoolId (@AcademicYearId, @SchoolId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get School by RoleId And UserId And AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolByAYIdAndRoleId(string roleId, Guid userId, Guid academicYearId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "RoleId", MySqlDbType = MySqlDbType.VarChar, Value = roleId };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolByAYIdAndRoleId ( @RoleId, @UserId ,@AcademicYearId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get VC by AcademicYearId And VTPId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVCByVTPIdSectorId(Guid academicYearId, Guid vtpId, Guid sectorId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };
            sqlParams[2] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = sectorId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVCByVTPIdSectorId ( @AcademicYearId, @VTPId ,@SectorId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VTP by AcademicYearId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTPByAYIdSectorId(Guid academicYearId, Guid sectorId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = sectorId };


            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTPByAYIdSectorId (@AcademicYearId, @SectorId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get VTP by AcademicYearId And VTPId And VCId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolByVTPIdVCIdSectorId(Guid academicYearId, Guid vtpId, Guid vcId, Guid sectorId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };
            sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };
            sqlParams[3] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = sectorId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetSchoolByVTPIdVCIdSectorId ( @AcademicYearId, @VTPId ,@VCId, @SectorId)", sqlParams).ToList();
        }

        /// <summary>
        ///  Get VT by AcademicYearId And VTPId And VCId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYIdAndVTPIdVCId(Guid academicYearId, Guid vtpId, Guid vcId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = vtpId };
            sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = vcId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetVTByAYIdAndVTPIdVCId ( @AcademicYearId, @VTPId ,@VCId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of MessageTemplates By Type
        /// </summary>
        /// <returns></returns>
        public IList<MessageTemplate> GetMessageTemplates(string messageTypeId = "")
        {
            return Context.MessageTemplates.Where(m => m.IsActive == true).ToList();
        }

        /// <summary>
        /// Get Android Settings
        /// </summary>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetAndroidSettings()
        {
            var lh2Settings = (from dv in this.Context.DataValues
                               where dv.DataTypeId == "AndroidVersion"
                               select new DropdownModel<string>
                               {
                                   Id = dv.DataValueId,
                                   Name = dv.Name,
                                   Description = dv.Description,
                                   SequenceNo = dv.DisplayOrder
                               }).OrderBy(x => x.SequenceNo).ToList();

            return lh2Settings;
        }

        /// <summary>
        /// Get Lighthouse Settings
        /// </summary>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetLighthouseSettings()
        {
            var lh2Settings = (from dv in this.Context.DataValues
                               where dv.DataTypeId == "LH2Settings"
                               select new DropdownModel<string>
                               {
                                   Id = dv.DataValueId,
                                   Name = dv.Name,
                                   Description = dv.Description,
                                   SequenceNo = dv.DisplayOrder
                               }).OrderBy(x => x.SequenceNo).ToList();

            return lh2Settings;
        }

        public IList<DropdownModel<string>> GetDashboardData(DashboardDataRequest para)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "dataType", MySqlDbType = MySqlDbType.VarChar, Value = para.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = para.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "parentId", MySqlDbType = MySqlDbType.VarChar, Value = para.ParentId };
            sqlParams[3] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = para.AcademicYearId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = para.DivisionId };
            sqlParams[5] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = para.DistrictCode };
            sqlParams[6] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = para.SectorId };
            sqlParams[7] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = para.JobRoleId };
            sqlParams[8] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = para.VTPId };
            sqlParams[9] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = para.ClassId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.VarChar, Value = para.SchoolManagementId };

            return Context.DropdownModels.FromSql<DropdownModel<string>>("CALL GetDashboardData (@dataType, @userId, @parentId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @SchoolManagementId)", sqlParams).ToList();
        }

        public DashboardCardModel GetDashboardCardData(DashboardDataRequest dashboardRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[10];
            sqlParams[0] = new MySqlParameter { ParameterName = "dataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SchoolManagementId };

            return Context.DashboardCardModels.FromSql<DashboardCardModel>("CALL GetDashboardCardData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @SchoolManagementId)", sqlParams).FirstOrDefault();
        }

        public IList<DashboardModel> GetDashboardVTAttendanceChartData(DashboardDataRequest para)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = para.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = para.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = para.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = para.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = para.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = para.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = para.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = para.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = para.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = para.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = para.SchoolManagementId };

            return Context.DashboardModels.FromSql<DashboardModel>("CALL GetDashboardVTAttendanceChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();
        }

        public IList<DashboardModel> GetDashboardVCAttendanceChartData(DashboardDataRequest para)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = para.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = para.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = para.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = para.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = para.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = para.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = para.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = para.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = para.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = para.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = para.SchoolManagementId };

            return Context.DashboardModels.FromSql<DashboardModel>("CALL GetDashboardVCAttendanceChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();
        }

        public IList<DashboardStudentAttendanceModel> GetDashboardStudentAttendanceChartData(DashboardDataRequest dashboardRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.SchoolManagementId };

            return Context.DashboardStudentAttendanceModels.FromSql<DashboardStudentAttendanceModel>("CALL GetDashboardStudentAttendanceChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();
        }

        public DashboardSchoolVisitStatusModel GetDashboardSchoolVisitStatusChartData(DashboardDataRequest dashboardRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.SchoolManagementId };

            return Context.DashboardSchoolVisitStatusModels.FromSql<DashboardSchoolVisitStatusModel>("CALL GetDashboardSchoolVisitStatusChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).FirstOrDefault();
        }

        public IList<DashboardIssueManagementModel> GetDashboardIssueManagementStatusChartData(DashboardDataRequest dashboardRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.SchoolManagementId };

            return Context.DashboardIssueManagementModels.FromSql<DashboardIssueManagementModel>("CALL GetDashboardIssueManagementStatusChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();
        }

        public IList<DashboardIssueManagementModel> GetDashboardIssueManagementChartData(DashboardDataRequest dashboardRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[12];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.SchoolManagementId };
            sqlParams[11] = new MySqlParameter { ParameterName = "IssueId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.IssueId };

            return Context.DashboardIssueManagementModels.FromSql<DashboardIssueManagementModel>("CALL GetDashboardIssueManagementChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @IssueId)", sqlParams).ToList();
        }

        public IList<JobRoleUnitCardModel> GetDashboardJobRoleUnitsCardData(DashboardDataRequest dashboardRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.SchoolManagementId };

            return Context.JobRoleUnitCardModels.FromSql<JobRoleUnitCardModel>("CALL GetDashboardJobRoleUnitsCardData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();
        }

        /// <summary>
        /// Get Lighthouse dashboard data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        public IList<T> GetLighthouseDashboards<T>(DashboardDataRequest dashboardRequest)
        {
            #region "Report Parameters"

            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.SchoolManagementId };

            #endregion "Report Parameters"

            try
            {
                if (typeof(T) == typeof(DashboardSchoolVisitByMonthModel))
                {
                    IList<DashboardSchoolVisitByMonthModel> schoolVisitByMonthModel = Context.DashboardSchoolVisitByMonthModels.FromSql<DashboardSchoolVisitByMonthModel>("CALL GetDashboardSchoolVisitDrilldownData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return schoolVisitByMonthModel as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardSchoolVisitByVTPModel))
                {
                    IList<DashboardSchoolVisitByVTPModel> courseMaterialStatusReports = Context.DashboardSchoolVisitByVTPModels.FromSql<DashboardSchoolVisitByVTPModel>("CALL GetDashboardSchoolVisitDrilldownData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return courseMaterialStatusReports as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardSchoolModel))
                {
                    IList<DashboardSchoolModel> schoolModel = Context.DashboardSchoolModels.FromSql<DashboardSchoolModel>("CALL GetDashboardSchoolChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return schoolModel as IList<T>;
                }
                else if (typeof(T) == typeof(VocationalTrainersCardModel))
                {
                    IList<VocationalTrainersCardModel> vocationalTrainersCardModels = Context.VocationalTrainersCardModels.FromSql<VocationalTrainersCardModel>("CALL GetDashboardVocationalTrainersCardData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return vocationalTrainersCardModels as IList<T>;
                }
                else if (typeof(T) == typeof(ClassCardModel))
                {
                    IList<ClassCardModel> classModels = Context.ClassCardModels.FromSql<ClassCardModel>("CALL GetDashboardClassesCardData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return classModels as IList<T>;
                }
                else if (typeof(T) == typeof(StudentCardModel))
                {
                    IList<StudentCardModel> studentModels = Context.StudentCardModels.FromSql<StudentCardModel>("CALL GetDashboardStudentsCardData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return studentModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardGuestLectureModel))
                {
                    IList<DashboardGuestLectureModel> guestLectureModel = Context.DashboardGuestLectureModels.FromSql<DashboardGuestLectureModel>("CALL GetDashboardGuestLectureChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return guestLectureModel as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardFieldVisitModel))
                {
                    IList<DashboardFieldVisitModel> dashboardFieldVisitModels = Context.DashboardFieldVisitModels.FromSql<DashboardFieldVisitModel>("CALL GetDashboardFieldVisitChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return dashboardFieldVisitModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardCourseMaterialModel))
                {
                    IList<DashboardCourseMaterialModel> courseMaterialModels = Context.DashboardCourseMaterialModels.FromSql<DashboardCourseMaterialModel>("CALL GetDashboardCourseMaterialChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return courseMaterialModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardToolEquipmentModel))
                {
                    IList<DashboardToolEquipmentModel> toolEquipmentModels = Context.DashboardToolEquipmentModels.FromSql<DashboardToolEquipmentModel>("CALL GetDashboardToolsAndEquipmentChartData (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return toolEquipmentModels as IList<T>;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new List<T>();
        }
    }
}