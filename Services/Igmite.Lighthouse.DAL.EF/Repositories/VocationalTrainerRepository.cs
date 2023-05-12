using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the VocationalTrainer entity
    /// </summary>
    public class VocationalTrainerRepository : GenericRepository<VocationalTrainer>, IVocationalTrainerRepository
    {
        /// <summary>
        /// Get list of VocationalTrainer
        /// </summary>
        /// <returns></returns>
        public IQueryable<VocationalTrainer> GetVocationalTrainers()
        {
            return this.Context.VocationalTrainers.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalTrainer by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainer> GetVocationalTrainersByName(string name)
        {
            var vocationalTrainers = (from v in this.Context.VocationalTrainers
                                      where v.FirstName.Contains(name)
                                      select v).AsQueryable();

            return vocationalTrainers;
        }

        /// <summary>
        /// Get list of VocationalTrainer by vtNames
        /// </summary>
        /// <param name="vtNames"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainer> GetVocationalTrainersByNames(List<string> vtNames)
        {
            var vocationalTrainers = (from v in this.Context.VocationalTrainers
                                      where vtNames.Contains(v.FullName)
                                      select v).AsQueryable();

            return vocationalTrainers;
        }

        /// <summary>
        /// Get list of VocationalTrainer by EmailIds
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="emailIds"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainerModel> GetVocationalTrainersByEmails(Guid academicYearId, List<string> emailIds)
        {
            var vocationalTrainers = (from vtm in this.Context.VCTrainersMap
                                      join vt in this.Context.VocationalTrainers on vtm.VTId equals vt.VTId
                                      where vtm.AcademicYearId == academicYearId && vtm.IsActive == true && vt.IsActive == true && emailIds.Contains(vt.Email)
                                      select new VocationalTrainerModel
                                      {
                                          AcademicYearId = vtm.AcademicYearId,
                                          VTPId = vtm.VTPId,
                                          VCId = vtm.VCId,
                                          VTId = vtm.VTId,
                                          FullName = vt.FullName,
                                          Email = vt.Email
                                      }).AsQueryable();

            return vocationalTrainers;
        }

        /// <summary>
        /// Get VocationalTrainer by VTId
        /// </summary>
        /// <param name="VTId"></param>
        /// <returns></returns>
        public VocationalTrainer GetVocationalTrainerById(Guid vtId)
        {
            VocationalTrainer vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.VTId == vtId);
            if (vocationalTrainer != null)
            {
                AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true);
                VCTrainerMap vcTrainers = this.Context.VCTrainersMap.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.VTId == vtId && v.DateOfResignation == null && v.IsActive == true);

                if (vcTrainers != null)
                {
                    vocationalTrainer.VCTrainer = vcTrainers;
                }
            }

            return vocationalTrainer;
        }

        /// <summary>
        /// Get VocationalTrainer by VTId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <param name="VTId"></param>
        /// <returns></returns>
        public VocationalTrainer GetVocationalTrainerById(DataRequest vtRequest)
        {
            Guid academicYearId = Guid.Parse(vtRequest.DataId);
            Guid vtpId = Guid.Parse(vtRequest.DataId1);
            Guid vcId = Guid.Parse(vtRequest.DataId2);
            Guid vtId = Guid.Parse(vtRequest.DataId3);

            VocationalTrainer vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.VTId == vtId);
            if (vocationalTrainer != null)
            {
                VCTrainerMap vcTrainer = this.Context.VCTrainersMap.FirstOrDefault(v => v.AcademicYearId == academicYearId && v.VTPId == vtpId && v.VCId == vcId && v.VTId == vtId);

                if (vcTrainer != null)
                {
                    vocationalTrainer.VCTrainer = vcTrainer;
                }
            }

            return vocationalTrainer;
        }

        /// <summary>
        /// Get VocationalTrainer by VTId using async
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public async Task<VocationalTrainer> GetVocationalTrainerByIdAsync(Guid vtId)
        {
            var vocationalTrainer = await (from v in this.Context.VocationalTrainers
                                           where v.VTId == vtId
                                           select v).FirstOrDefaultAsync();

            return vocationalTrainer;
        }

        /// <summary>
        /// Insert/Update VocationalTrainer entity
        /// </summary>
        /// <param name="vocationalTrainer"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVocationalTrainerDetails(VocationalTrainer vocationalTrainer)
        {
            try
            {
                if (RequestType.New == vocationalTrainer.RequestType)
                {
                    Context.VocationalTrainers.Add(vocationalTrainer);
                    Context.SaveChanges();

                    AcademicYear academicYear = Context.AcademicYears.FirstOrDefault(a => a.IsCurrentAcademicYear == true);
                    vocationalTrainer.VCTrainer.AcademicYearId = academicYear.AcademicYearId;

                    Context.VCTrainersMap.Add(vocationalTrainer.VCTrainer);
                    Context.SaveChanges();
                }
                else
                {
                    Context.Entry<VocationalTrainer>(vocationalTrainer).State = EntityState.Modified;
                    Context.SaveChanges();
                    Context.Entry<VCTrainerMap>(vocationalTrainer.VCTrainer).State = EntityState.Modified;
                    Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVocationalTrainerDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtId)
        {
            try
            {
                VocationalTrainer vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.VTId == vtId);

                if (vocationalTrainer != null)
                {
                    StudentClassMapping student = this.Context.StudentClassMapping.FirstOrDefault(v => v.VTId == vtId);
                    VTDailyReporting vtDailyReporting = this.Context.VTDailyReportings.FirstOrDefault(v => v.VTId == vtId);

                    if (student == null && vtDailyReporting == null)
                    {
                        Account account = this.Context.Accounts.FirstOrDefault(v => v.AccountId == vtId);

                        if (account != null)
                        {
                            AccountRole accountRole = this.Context.AccountRoles.FirstOrDefault(v => v.AccountId == account.AccountId);
                            if (accountRole != null)
                            {
                                Context.Entry<AccountRole>(accountRole).State = EntityState.Deleted;
                            }

                            Context.Entry<Account>(account).State = EntityState.Deleted;
                        }

                        List<VTClass> vtClasses = this.Context.VTClasses.Where(v => v.VTId == vtId).ToList();
                        vtClasses.ForEach(vtClassItem =>
                        {
                            List<VTClassSection> vtClassSections = this.Context.VTClassSections.Where(v => v.VTClassId == vtClassItem.VTClassId).ToList();
                            vtClassSections.ForEach(vtClassSectionItem =>
                            {
                                Context.Entry<VTClassSection>(vtClassSectionItem).State = EntityState.Deleted;
                            });

                            Context.Entry<VTClass>(vtClassItem).State = EntityState.Deleted;
                        });

                        List<VTSchoolSector> vtSchoolSectors = this.Context.VTSchoolSectors.Where(v => v.VTId == vtId).ToList();
                        vtSchoolSectors.ForEach(vtSchoolSectorItem =>
                        {
                            Context.Entry<VTSchoolSector>(vtSchoolSectorItem).State = EntityState.Deleted;
                        });

                        List<VCTrainerMap> vcTrainers = this.Context.VCTrainersMap.Where(v => v.VTId == vtId).ToList();
                        vcTrainers.ForEach(vcTrainerItem =>
                        {
                            Context.Entry<VCTrainerMap>(vcTrainerItem).State = EntityState.Deleted;
                        });

                        Context.Entry<VocationalTrainer>(vocationalTrainer).State = EntityState.Deleted;

                        Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > DeletedVocationalTrainerById", ex);
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VocationalTrainer by Name
        /// </summary>
        /// <param name="vocationalTrainerModel"></param>
        /// <returns></returns>
        public List<string> CheckVocationalTrainerExistByName(VocationalTrainerModel vocationalTrainerModel)
        {
            var errorMessages = new List<string>();

            try
            {
                AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(v => v.IsCurrentAcademicYear == true);

                if (vocationalTrainerModel.RequestType == RequestType.New)
                {
                    VocationalTrainer vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.Email == vocationalTrainerModel.Email && v.IsActive == true);

                    if (vocationalTrainer != null)
                    {
                        errorMessages.Add(string.Format("EmailId is already exists for another VT ({0})", vocationalTrainer.FullName));
                    }

                    vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.Mobile == vocationalTrainerModel.Mobile && v.IsActive == true);

                    if (vocationalTrainer != null)
                    {
                        errorMessages.Add(string.Format("Mobile no is already exists for another VT ({0})", vocationalTrainer.FullName));
                    }

                    vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.AadhaarNumber == vocationalTrainerModel.AadhaarNumber && v.IsActive == true);

                    if (vocationalTrainer != null)
                    {
                        errorMessages.Add(string.Format("Aadhaar Number is already exists for another VT ({0})", vocationalTrainer.FullName));
                    }

                    VTPCoordinatorsMap vtpCoordinatorsMap = this.Context.VTPCoordinatorMap.FirstOrDefault(a => a.AcademicYearId == academicYear.AcademicYearId && a.VTPId == vocationalTrainerModel.VTPId && a.VCId == vocationalTrainerModel.VCId && a.IsActive == true);
                    if (vtpCoordinatorsMap == null)
                    {
                        VocationalTrainingProvider vtp = this.Context.VocationalTrainingProviders.FirstOrDefault(v => v.VTPId == vocationalTrainerModel.VTPId);
                        if (vocationalTrainer != null)
                            errorMessages.Add(string.Format("This VC does not Mapped with this VTP ({0})", vtp.VTPName));
                    }

                    IList<VCTrainerMap> vCTrainerMap = this.Context.VCTrainersMap.Where(a => a.AcademicYearId == academicYear.AcademicYearId && a.VTPId == vocationalTrainerModel.VTPId && a.VCId == vocationalTrainerModel.VCId && a.IsActive == true).ToList();
                    if (vCTrainerMap.Count > 0)
                    {
                        vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.Email == vocationalTrainerModel.Email && v.IsActive == true);
                        if (vocationalTrainer != null)
                        {
                            VCTrainerMap vcTrainerItem = vCTrainerMap.FirstOrDefault(vtpm => vtpm.VTId == vocationalTrainer.VTId);
                            if (vcTrainerItem != null)
                            {
                                errorMessages.Add(string.Format("This VC is already Mapped with this VT ({0})", vocationalTrainer.FullName));
                            }
                        }
                    }

                    Account account = this.Context.Accounts.FirstOrDefault(a => a.LoginId == vocationalTrainerModel.Email && a.IsActive == true);
                    if (account != null)
                    {
                        errorMessages.Add(string.Format("Account is already registered with this email ({0})", account.EmailId));
                    }
                }
                else if (vocationalTrainerModel.RequestType == RequestType.Edit && !vocationalTrainerModel.DateOfResignation.HasValue)
                {
                    VocationalTrainer vocationalTrainer = this.Context.VocationalTrainers.FirstOrDefault(v => v.VTId == vocationalTrainerModel.VTId);

                    if (vocationalTrainer.IsActive && !vocationalTrainerModel.IsActive)
                    {
                        var vtSchoolSectors = this.Context.VTSchoolSectors.Where(v => v.AcademicYearId == vocationalTrainerModel.AcademicYearId && v.VTId == vocationalTrainerModel.VTId && v.IsActive == true).ToList();
                        if (vtSchoolSectors.Count > 0)
                        {
                            errorMessages.Add("VT School Sector is already active, Please inactive VTSchoolSector mapping first!");
                        }

                        var vtClasses = this.Context.VTClasses.Where(v => v.AcademicYearId == vocationalTrainerModel.AcademicYearId && v.VTId == vocationalTrainerModel.VTId && v.IsActive == true).ToList();
                        if (vtClasses.Count > 0)
                        {
                            errorMessages.Add("VT Classes are already active, Please inactive all classes first!");
                        }
                    }

                    if (!vocationalTrainer.IsActive && vocationalTrainerModel.IsActive)
                    {
                        var studentItem = this.Context.StudentClassMapping.FirstOrDefault(s => s.AcademicYearId == vocationalTrainerModel.AcademicYearId && s.VTId == vocationalTrainerModel.VTId && s.IsActive == true);

                        if (studentItem != null)
                        {
                            errorMessages.Add("You are not authorized to change this due to students are mapped with this trainer, Please contact the PMU Admin");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVocationalTrainerExistByName", ex);
            }

            return errorMessages;
        }

        /// <summary>
        /// Check duplicate Vocational Trainer by Aadhaar Number
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public VocationalTrainer CheckVocationalTrainerExistByAadhaarNumber(string aadhaarNumber)
        {
            return this.Context.VocationalTrainers.FirstOrDefault(v => v.AadhaarNumber == aadhaarNumber && v.IsActive == true);
        }

        /// <summary>
        /// List of VocationalTrainer with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<VocationalTrainerViewModel> GetVocationalTrainersByCriteria(SearchVocationalTrainerModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[10];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "hmId", MySqlDbType = MySqlDbType.Int32, Value = searchModel.HMId };
            sqlParams[4] = new MySqlParameter { ParameterName = "socialCategoryId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.SocialCategoryId };
            sqlParams[5] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[6] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[7] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[8] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[9] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.VocationalTrainerViewModels.FromSql<VocationalTrainerViewModel>("CALL GetVocationalTrainersByCriteria (@academicYearId, @vtpId, @vcId, @hmId, @socialCategoryId, @status, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of VT Ids by School Ids
        /// </summary>
        /// <param name="schoolIds"></param>
        /// <returns></returns>
        public Dictionary<Guid, Guid> GetVTIdsBySchoolIds(List<Guid> schoolIds)
        {
            Dictionary<Guid, Guid> VTIdsBySchoolIds = new Dictionary<Guid, Guid>();
            AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(y => y.IsCurrentAcademicYear == true);

            foreach (Guid schoolId in schoolIds)
            {
                var vtSchoolSector = this.Context.VTSchoolSectors.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.SchoolId == schoolId);
                if (vtSchoolSector != null)
                {
                    VTIdsBySchoolIds.Add(schoolId, vtSchoolSector.VTId);
                }
            }

            return VTIdsBySchoolIds;
        }

        /// <summary>
        /// Inactive VT Related Data When Resigned
        /// </summary>
        /// <param name="vocationalTrainer"></param>
        /// <param name="isActivate"></param>
        /// <returns></returns>
        public bool InactiveVTRelatedDataWhenResigned(VocationalTrainer vocationalTrainer)
        {
            AcademicYear academicYears = this.Context.AcademicYears.FirstOrDefault(v => v.IsCurrentAcademicYear == true);

            VTSchoolSector vtSchoolSector = this.Context.VTSchoolSectors.Where(v => v.AcademicYearId == academicYears.AcademicYearId && v.VTId == vocationalTrainer.VTId && v.DateOfRemoval == null && v.IsActive == true).OrderByDescending(x => x.UpdatedOn).FirstOrDefault();

            if (vtSchoolSector != null)
            {
                if (!vocationalTrainer.IsActive && vocationalTrainer.VCTrainer.DateOfResignation.HasValue)
                {
                    vtSchoolSector.DateOfRemoval = vocationalTrainer.VCTrainer.DateOfResignation;
                }
                else if (vocationalTrainer.IsActive && !vocationalTrainer.VCTrainer.DateOfResignation.HasValue)
                {
                    vtSchoolSector.DateOfRemoval = null;
                }

                vtSchoolSector.UpdatedBy = vocationalTrainer.UpdatedBy;
                vtSchoolSector.UpdatedOn = Constants.GetCurrentDateTime;
                vtSchoolSector.IsActive = vocationalTrainer.IsActive;

                Context.Entry<VTSchoolSector>(vtSchoolSector).State = EntityState.Modified;

                List<VTClass> vtClasses = this.Context.VTClasses.Where(v => v.AcademicYearId == vtSchoolSector.AcademicYearId && v.SchoolId == vtSchoolSector.SchoolId && v.VTId == vtSchoolSector.VTId && v.IsActive == true).ToList();
                for (int classIndex = 0; classIndex < vtClasses.Count; classIndex++)
                {
                    vtClasses[classIndex].UpdatedBy = vocationalTrainer.UpdatedBy;
                    vtClasses[classIndex].UpdatedOn = Constants.GetCurrentDateTime;
                    vtClasses[classIndex].IsActive = vocationalTrainer.IsActive;

                    Context.Entry<VTClass>(vtClasses[classIndex]).State = EntityState.Modified;

                    List<StudentClassMapping> students = this.Context.StudentClassMapping.Where(v => v.AcademicYearId == vtSchoolSector.AcademicYearId && v.SchoolId == vtSchoolSector.SchoolId && v.ClassId == vtClasses[classIndex].ClassId && v.VTId == vtSchoolSector.VTId && v.IsActive == true).ToList();
                    for (int studentIndex = 0; studentIndex < students.Count; studentIndex++)
                    {
                        students[studentIndex].UpdatedBy = vocationalTrainer.UpdatedBy;
                        students[studentIndex].UpdatedOn = Constants.GetCurrentDateTime;
                        students[studentIndex].IsActive = vocationalTrainer.IsActive;

                        Context.Entry<StudentClassMapping>(students[studentIndex]).State = EntityState.Modified;
                    }
                }

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// List of School Students by VT
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<SchoolStudentModel> GetSchoolStudentsByVTId(Guid academicYearId, Guid vtId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.Guid, Value = vtId };

            return Context.SchoolStudentModels.FromSql<SchoolStudentModel>("CALL GetSchoolStudentsByVTId (@AcademicYearId, @VTId)", sqlParams).ToList();
        }

        /// <summary>
        /// List of School Student Details by VT
        /// </summary>
        /// <param name="vTTransferRequest"></param>
        /// <returns></returns>
        public string TransferOldVTToNewVT(VTTransferRequest vTTransferRequest)
        {
            try
            {
                using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                {
                    DateTime creationDate = Constants.GetCurrentDateTime;

                    VTSchoolSector vTSchoolSectorOld = Context.VTSchoolSectors.FirstOrDefault(vtss => vtss.VTId == vTTransferRequest.FromVTId && vtss.AcademicYearId == vTTransferRequest.AcademicYearId && vtss.IsActive == true);
                    VTSchoolSector vTSchoolSectorNew = Context.VTSchoolSectors.FirstOrDefault(vtsn => vtsn.VTId == vTTransferRequest.ToVTId && vtsn.AcademicYearId == vTTransferRequest.AcademicYearToId && vtsn.IsActive == true);

                    if ((vTTransferRequest.IsVTResigned == 1 || vTTransferRequest.IsVTResigned == 2) && vTTransferRequest.FromSchoolId != vTTransferRequest.ToSchoolId)
                    {
                        vTTransferRequest.Remarks = "VT School Sector  does not exists";
                    }
                    else if ((vTTransferRequest.IsVTResigned == 1 || vTTransferRequest.IsVTResigned == 2) && vTSchoolSectorNew != null)
                    {
                        vTTransferRequest.Remarks = "This VT already mapped with School";
                    }
                    else if (vTSchoolSectorOld != null)
                    {
                        // shuffling Old VT
                        if (vTTransferRequest.IsVTResigned == 3)
                        {
                            // Create new VTSchoolSector mapping
                            VTSchoolSector vTSchoolSector = new VTSchoolSector();
                            vTSchoolSector.VTSchoolSectorId = new Guid();
                            vTSchoolSector.VTId = vTTransferRequest.FromVTId;
                            vTSchoolSector.AcademicYearId = vTTransferRequest.AcademicYearId;
                            vTSchoolSector.SchoolId = vTTransferRequest.ToSchoolId;
                            vTSchoolSector.SectorId = vTSchoolSectorOld.SectorId;
                            vTSchoolSector.JobRoleId = vTSchoolSectorOld.JobRoleId;
                            vTSchoolSector.DateOfAllocation = creationDate;
                            vTSchoolSector.DateOfRemoval = null;
                            vTSchoolSector.CreatedBy = vTTransferRequest.UserId;
                            vTSchoolSector.CreatedOn = creationDate;
                            vTSchoolSector.UpdatedBy = vTTransferRequest.UserId;
                            vTSchoolSector.UpdatedOn = creationDate;
                            vTSchoolSector.IsActive = true;

                            Context.VTSchoolSectors.Add(vTSchoolSector);

                            // Create new VTClasses mapping
                            List<VTClass> vTClasses = Context.VTClasses.Where(vtc => vtc.VTId == vTTransferRequest.FromVTId && vtc.AcademicYearId == vTTransferRequest.AcademicYearId && vtc.SchoolId == vTTransferRequest.ToSchoolId && vtc.IsActive == true).ToList();

                            foreach (VTClass vTClassOld in vTClasses)
                            {
                                // Create new VTClasses mapping
                                VTClass vTClass = new VTClass();
                                vTClass.VTClassId = new Guid();
                                vTClass.AcademicYearId = vTTransferRequest.AcademicYearId;
                                vTClass.SchoolId = vTTransferRequest.ToSchoolId;
                                vTClass.SectionId = vTClassOld.SectionId;
                                vTClass.VTId = vTTransferRequest.FromVTId;
                                vTClass.ClassId = vTClassOld.ClassId;
                                vTClass.CreatedBy = vTTransferRequest.UserId;
                                vTClass.CreatedOn = creationDate;
                                vTClass.UpdatedBy = vTTransferRequest.UserId;
                                vTClass.UpdatedOn = creationDate;
                                vTClass.IsActive = true;

                                Context.VTClasses.Add(vTClass);

                                // Inactive old VTClass mapping
                                vTClassOld.UpdatedBy = vTTransferRequest.UserId;
                                vTClassOld.UpdatedOn = creationDate;
                                vTClassOld.IsActive = false;
                                Context.Entry<VTClass>(vTClassOld).State = EntityState.Modified;

                            }

                            // Create new VTClasses mapping
                            List<StudentClassMapping> StudentClassMappings = Context.StudentClassMapping.Where(scm => scm.VTId == vTTransferRequest.FromVTId && scm.AcademicYearId == vTTransferRequest.AcademicYearId && scm.SchoolId == vTTransferRequest.ToSchoolId && scm.IsActive == true).ToList();

                            foreach (StudentClassMapping studentClassMappingOld in StudentClassMappings)
                            {                             

                                // Create new StudentClassMapping mapping
                                StudentClassMapping studentClassMapping = new StudentClassMapping();
                                studentClassMapping.StudentClassMappingId = new Guid();
                                studentClassMapping.AcademicYearId = vTTransferRequest.AcademicYearId;
                                studentClassMapping.ClassId = studentClassMappingOld.ClassId;
                                studentClassMapping.SectionId = studentClassMappingOld.SectionId;
                                studentClassMapping.SchoolId = vTTransferRequest.ToSchoolId;
                                studentClassMapping.StudentId = studentClassMappingOld.StudentId;
                                studentClassMappingOld.StudentRollNumber = studentClassMappingOld.StudentRollNumber;
                                studentClassMapping.VTId = vTTransferRequest.FromVTId;
                                studentClassMapping.CreatedBy = vTTransferRequest.UserId;
                                studentClassMapping.CreatedOn = creationDate;
                                studentClassMapping.UpdatedBy = vTTransferRequest.UserId;
                                studentClassMapping.UpdatedOn = creationDate;
                                studentClassMapping.IsActive = true;

                                Context.StudentClassMapping.Add(studentClassMapping);

                                // Inactive old StudentClassMapping mapping
                                studentClassMappingOld.UpdatedBy = vTTransferRequest.UserId;
                                studentClassMappingOld.UpdatedOn = creationDate;
                                studentClassMappingOld.IsActive = false;
                                Context.Entry<StudentClassMapping>(studentClassMappingOld).State = EntityState.Modified;

                            }

                        }
                        else
                        {
                            // Create new VTSchoolSector mapping
                            VTSchoolSector vTSchoolSector = new VTSchoolSector();
                            vTSchoolSector.VTSchoolSectorId = new Guid();
                            vTSchoolSector.VTId = vTTransferRequest.ToVTId;
                            vTSchoolSector.AcademicYearId = vTTransferRequest.AcademicYearId;
                            vTSchoolSector.SchoolId = vTTransferRequest.ToSchoolId;
                            vTSchoolSector.SectorId = vTSchoolSectorOld.SectorId;
                            vTSchoolSector.JobRoleId = vTSchoolSectorOld.JobRoleId;
                            vTSchoolSector.DateOfAllocation = vTTransferRequest.DateOfAllocation;
                            vTSchoolSector.DateOfRemoval = null;
                            vTSchoolSector.CreatedBy = vTTransferRequest.UserId;
                            vTSchoolSector.CreatedOn = creationDate;
                            vTSchoolSector.UpdatedBy = vTTransferRequest.UserId;
                            vTSchoolSector.UpdatedOn = creationDate;
                            vTSchoolSector.IsActive = true;

                            Context.VTSchoolSectors.Add(vTSchoolSector);

                            // Create new VTClasses mapping
                            List<VTClass> vTClasses = Context.VTClasses.Where(vtc => vtc.VTId == vTTransferRequest.FromVTId && vtc.AcademicYearId == vTTransferRequest.AcademicYearId && vtc.SchoolId == vTTransferRequest.FromSchoolId && vtc.IsActive == true).ToList();

                            foreach (VTClass vTClassOld in vTClasses)
                            {

                                // Create new VCTrainerMap mapping
                                VTClass vTClass = new VTClass();
                                vTClass.VTClassId = new Guid();
                                vTClass.AcademicYearId = vTTransferRequest.AcademicYearId;
                                vTClass.SchoolId = vTTransferRequest.ToSchoolId;
                                vTClass.SectionId = vTClassOld.SectionId;
                                vTClass.VTId = vTTransferRequest.ToVTId;
                                vTClass.ClassId = vTClassOld.ClassId;
                                vTClass.CreatedBy = vTTransferRequest.UserId;
                                vTClass.CreatedOn = creationDate;
                                vTClass.UpdatedBy = vTTransferRequest.UserId;
                                vTClass.UpdatedOn = creationDate;
                                vTClass.IsActive = true;

                                Context.VTClasses.Add(vTClass);

                                List<VTClassSection> vTClassSectionOld = Context.VTClassSections.Where(vtcs => vtcs.VTClassId == vTClassOld.VTClassId && vtcs.IsActive == true).ToList();

                                // Create VTClassSections mapping
                                vTClassSectionOld.ForEach(vtClassSectionItem =>
                                {

                                    VTClassSection vTClassSection = new VTClassSection();
                                    vTClassSection.VTClassSectionId = new Guid();
                                    vTClassSection.VTClassId = vTClass.VTClassId;
                                    vTClassSection.SectionId = vtClassSectionItem.SectionId;
                                    vTClassSection.Remarks = vtClassSectionItem.Remarks;
                                    vTClassSection.CreatedBy = vTTransferRequest.UserId;
                                    vTClassSection.CreatedOn = creationDate;
                                    vTClassSection.IsActive = true;

                                    Context.VTClassSections.Add(vTClassSection);

                                    // Inactive old VTClass mapping
                                    vtClassSectionItem.IsActive = false;
                                    Context.Entry<VTClassSection>(vtClassSectionItem).State = EntityState.Modified;
                                });
                              
                                // Inactive old VTClass mapping
                                vTClassOld.UpdatedBy = vTTransferRequest.UserId;
                                vTClassOld.UpdatedOn = creationDate;
                                vTClassOld.IsActive = false;
                                Context.Entry<VTClass>(vTClassOld).State = EntityState.Modified;                            

                            }

                            // Create new StudentClass mapping
                            List<StudentClassMapping> StudentClassMappings = Context.StudentClassMapping.Where(scm => scm.VTId == vTTransferRequest.FromVTId && scm.AcademicYearId == vTTransferRequest.AcademicYearId && scm.SchoolId == vTTransferRequest.FromSchoolId && scm.IsActive == true).ToList();

                            foreach (StudentClassMapping studentClassMappingOld in StudentClassMappings)
                            {            

                                // Create new StudentClassMapping mapping
                                StudentClassMapping studentClassMapping = new StudentClassMapping();
                                studentClassMapping.StudentClassMappingId = new Guid();
                                studentClassMapping.AcademicYearId = vTTransferRequest.AcademicYearId;
                                studentClassMapping.ClassId = studentClassMappingOld.ClassId;
                                studentClassMapping.SectionId = studentClassMappingOld.SectionId;
                                studentClassMapping.SchoolId = vTTransferRequest.ToSchoolId;
                                studentClassMapping.StudentId = studentClassMappingOld.StudentId;
                                studentClassMapping.StudentRollNumber = studentClassMappingOld.StudentRollNumber;
                                studentClassMapping.VTId = vTTransferRequest.ToVTId;
                                studentClassMapping.CreatedBy = vTTransferRequest.UserId;
                                studentClassMapping.CreatedOn = creationDate;
                                studentClassMapping.UpdatedBy = vTTransferRequest.UserId;
                                studentClassMapping.UpdatedOn = creationDate;
                                studentClassMapping.IsActive = true;

                                Context.StudentClassMapping.Add(studentClassMapping);

                                // Inactive old StudentClassMapping mapping
                                studentClassMappingOld.UpdatedBy = vTTransferRequest.UserId;
                                studentClassMappingOld.UpdatedOn = creationDate;
                                studentClassMappingOld.IsActive = false;
                                Context.Entry<StudentClassMapping>(studentClassMappingOld).State = EntityState.Modified;

                            }

                        }                    

                        // Resign VT
                        if (vTTransferRequest.IsVTResigned == 1)
                        {

                            VocationalTrainer vocationalTrainer = Context.VocationalTrainers.FirstOrDefault(vtss => vtss.VTId == vTTransferRequest.FromVTId);
                            vocationalTrainer.UpdatedBy = vTTransferRequest.UserId;
                            vocationalTrainer.UpdatedOn = creationDate;
                            vocationalTrainer.IsActive = false;
                            Context.Entry<VocationalTrainer>(vocationalTrainer).State = EntityState.Modified;

                            VCTrainerMap vCTrainerMap = Context.VCTrainersMap.FirstOrDefault(vtss => vtss.VTId == vTTransferRequest.FromVTId && vtss.AcademicYearId == vTTransferRequest.AcademicYearId);
                            vCTrainerMap.DateOfResignation = vTTransferRequest.DateOfResignation;
                            vCTrainerMap.UpdatedBy = vTTransferRequest.UserId;
                            vCTrainerMap.UpdatedOn = creationDate;
                            vCTrainerMap.IsActive = false;
                            Context.Entry<VCTrainerMap>(vCTrainerMap).State = EntityState.Modified;


                            Account account = Context.Accounts.FirstOrDefault(vtss => vtss.AccountId == vTTransferRequest.FromVTId);
                            account.PasswordExpiredOn = creationDate;
                            account.IsLocked =true;
                            account.UpdatedBy = vTTransferRequest.UserId;
                            account.UpdatedOn = creationDate;
                            account.IsActive = false;
                            Context.Entry<Account>(account).State = EntityState.Modified;
                         
                        }

                       // shuffling New VT
                        if (vTTransferRequest.IsVTResigned == 3)
                        {
                            VTSchoolSector oldVTSchoolSector = Context.VTSchoolSectors.FirstOrDefault(vtss => vtss.VTId == vTTransferRequest.ToVTId && vtss.AcademicYearId == vTTransferRequest.AcademicYearToId);

                            if(oldVTSchoolSector != null)
                            {
                           
                                // Create new VTSchoolSector mapping
                                VTSchoolSector newVTSchoolSector = new VTSchoolSector();
                                newVTSchoolSector.VTSchoolSectorId = new Guid();
                                newVTSchoolSector.VTId = vTTransferRequest.ToVTId;
                                newVTSchoolSector.AcademicYearId = vTTransferRequest.AcademicYearId;
                                newVTSchoolSector.SchoolId = vTTransferRequest.FromSchoolId;
                                newVTSchoolSector.SectorId = vTSchoolSectorOld.SectorId;
                                newVTSchoolSector.JobRoleId = vTSchoolSectorOld.JobRoleId;
                                newVTSchoolSector.DateOfAllocation = creationDate; 
                                newVTSchoolSector.DateOfRemoval = null;
                                newVTSchoolSector.CreatedBy = vTTransferRequest.UserId;
                                newVTSchoolSector.CreatedOn = creationDate;
                                newVTSchoolSector.UpdatedBy = vTTransferRequest.UserId;
                                newVTSchoolSector.UpdatedOn = creationDate;
                                newVTSchoolSector.IsActive = true;

                                Context.VTSchoolSectors.Add(newVTSchoolSector);
                                // Create new VTClasses mapping
                                List<VTClass> vTClasses = Context.VTClasses.Where(vtc => vtc.VTId == vTTransferRequest.ToVTId && vtc.AcademicYearId == vTTransferRequest.AcademicYearToId && vtc.SchoolId == vTTransferRequest.ToSchoolId && vtc.IsActive == true).ToList();

                                foreach (VTClass oldVTClass in vTClasses)
                                {

                                    // Create new VTClasses mapping
                                    VTClass newVTClass = new VTClass();
                                    newVTClass.VTClassId = new Guid();
                                    newVTClass.AcademicYearId = vTTransferRequest.AcademicYearId;
                                    newVTClass.SchoolId = vTTransferRequest.FromSchoolId;
                                    newVTClass.SectionId = oldVTClass.SectionId;
                                    newVTClass.VTId = vTTransferRequest.ToVTId;
                                    newVTClass.ClassId = oldVTClass.ClassId;
                                    newVTClass.CreatedBy = vTTransferRequest.UserId;
                                    newVTClass.CreatedOn = creationDate;
                                    newVTClass.UpdatedBy = vTTransferRequest.UserId;
                                    newVTClass.UpdatedOn = creationDate;
                                    newVTClass.IsActive = true;

                                    Context.VTClasses.Add(newVTClass);
                                    
                                    // Inactive old VTClass mapping
                                    oldVTClass.UpdatedBy = vTTransferRequest.UserId;
                                    oldVTClass.UpdatedOn = creationDate;
                                    oldVTClass.IsActive = false;
                                    Context.Entry<VTClass>(oldVTClass).State = EntityState.Modified;

                                }

                                // Create new VTClasses mapping
                                List<StudentClassMapping> StudentClassMappings = Context.StudentClassMapping.Where(scm => scm.VTId == vTTransferRequest.ToVTId && scm.AcademicYearId == vTTransferRequest.AcademicYearToId && scm.SchoolId == vTTransferRequest.ToSchoolId && scm.IsActive == true).ToList();

                                foreach (StudentClassMapping oldStudentClassMapping in StudentClassMappings)
                                {                               

                                    // Create new StudentClassMapping mapping
                                    StudentClassMapping newStudentClassMapping = new StudentClassMapping();
                                    newStudentClassMapping.StudentClassMappingId = new Guid();
                                    newStudentClassMapping.AcademicYearId = vTTransferRequest.AcademicYearId;
                                    newStudentClassMapping.ClassId = oldStudentClassMapping.ClassId;
                                    newStudentClassMapping.SectionId = oldStudentClassMapping.SectionId;
                                    newStudentClassMapping.SchoolId = vTTransferRequest.FromSchoolId;
                                    newStudentClassMapping.StudentId = oldStudentClassMapping.StudentId;
                                    newStudentClassMapping.StudentRollNumber = oldStudentClassMapping.StudentRollNumber;
                                    newStudentClassMapping.VTId = vTTransferRequest.ToVTId;
                                    newStudentClassMapping.CreatedBy = vTTransferRequest.UserId;
                                    newStudentClassMapping.CreatedOn = creationDate;
                                    newStudentClassMapping.UpdatedBy = vTTransferRequest.UserId;
                                    newStudentClassMapping.UpdatedOn = creationDate;
                                    newStudentClassMapping.IsActive = true;

                                    Context.StudentClassMapping.Add(newStudentClassMapping);

                                    // Inactive old StudentClassMapping mapping
                                    oldStudentClassMapping.UpdatedBy = vTTransferRequest.UserId;
                                    oldStudentClassMapping.UpdatedOn = creationDate;
                                    oldStudentClassMapping.IsActive = false;
                                    Context.Entry<StudentClassMapping>(oldStudentClassMapping).State = EntityState.Modified;
                                }

                                //Inactive Old VT School Sector
                                oldVTSchoolSector.DateOfRemoval = vTTransferRequest.ToDateOfRemoval;
                                oldVTSchoolSector.UpdatedBy = vTTransferRequest.UserId;
                                oldVTSchoolSector.UpdatedOn = creationDate;
                                oldVTSchoolSector.IsActive = false;
                                Context.Entry<VTSchoolSector>(oldVTSchoolSector).State = EntityState.Modified;


                            }
                            else
                            {
                                vTTransferRequest.Remarks = "New VT School Sector does not exists";
                            }

                        }

                        // Inactive old VTSchoolSector mapping
                        if (vTTransferRequest.IsVTResigned == 2 || vTTransferRequest.IsVTResigned == 3)
                        {
                            vTSchoolSectorOld.DateOfRemoval = vTTransferRequest.DateOfRemoval;
                        }
                        vTSchoolSectorOld.UpdatedBy = vTTransferRequest.UserId;
                        vTSchoolSectorOld.UpdatedOn = creationDate;
                        vTSchoolSectorOld.IsActive = false;
                        Context.Entry<VTSchoolSector>(vTSchoolSectorOld).State = EntityState.Modified;

                        vTTransferRequest.Remarks = "Transfer Completed";

                        // Commit all changes for VTP Transfers
                        this.Context.SaveChanges();
                        transaction.Commit();

                    }
                    else 
                    {
                        vTTransferRequest.Remarks = "VT School Sector does not exists";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return vTTransferRequest.Remarks;
        }

        /// <summary>
        /// Save Or Update VCTrainerMap by VTId
        /// </summary>
        /// <param name="vTTransferRequest"></param>
        /// <returns></returns>
        public VCTrainerMap SaveOrUpdateVCTrainerMapById(VTTransferRequest vtTransferRequest)
        {
            VCTrainerMap vcTrainerMap = this.Context.VCTrainersMap.FirstOrDefault(v => v.AcademicYearId == vtTransferRequest.AcademicYearToId && v.VTPId == vtTransferRequest.ToVTPId && v.VCId == vtTransferRequest.ToVCId && v.VTId == vtTransferRequest.ToVTId);
            if (vcTrainerMap == null)
            {
                vcTrainerMap.VCTrainerId = new Guid();
                vcTrainerMap.VTId = vtTransferRequest.ToVTId;
                vcTrainerMap.AcademicYearId = vtTransferRequest.AcademicYearToId;
                vcTrainerMap.VTPId = vtTransferRequest.ToVTPId;
                vcTrainerMap.VCId = vtTransferRequest.ToVCId;
                vcTrainerMap.DateOfJoining = Constants.GetCurrentDateTime;
                vcTrainerMap.NatureOfAppointment = "58";
                vcTrainerMap.IsActive = true;

                Context.VCTrainersMap.Add(vcTrainerMap);
                Context.SaveChanges();
            }
            else
            {
                vcTrainerMap.DateOfJoining = Constants.GetCurrentDateTime;
                vcTrainerMap.DateOfResignation = null;
                vcTrainerMap.IsActive = true;
                vcTrainerMap.UpdatedOn = Constants.GetCurrentDateTime;
                Context.Entry<VCTrainerMap>(vcTrainerMap).State = EntityState.Modified;
                Context.SaveChanges();
            }

            return vcTrainerMap;
        }
    }
}