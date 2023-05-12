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
    /// Repository of the VocationalCoordinator entity
    /// </summary>
    public class VocationalCoordinatorRepository : GenericRepository<VocationalCoordinator>, IVocationalCoordinatorRepository
    {
        /// <summary>
        /// Get list of VocationalCoordinator
        /// </summary>
        /// <returns></returns>
        public IQueryable<VocationalCoordinator> GetVocationalCoordinators()
        {
            return this.Context.VocationalCoordinators.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalCoordinator by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VocationalCoordinator> GetVocationalCoordinatorsByName(string name)
        {
            var vocationalCoordinators = (from v in this.Context.VocationalCoordinators
                                          where v.FirstName.Contains(name)
                                          select v).AsQueryable();

            return vocationalCoordinators;
        }

        /// <summary>
        /// Get list of VocationalCoordinators by VC Names
        /// </summary>
        /// <param name="vcNames"></param>
        /// <returns></returns>
        public IQueryable<VocationalCoordinator> GetVocationalCoordinatorsByNames(List<string> vcNames)
        {
            var vocationalCoordinators = (from v in this.Context.VocationalCoordinators
                                          where vcNames.Contains(v.FullName)
                                          select v).AsQueryable();

            return vocationalCoordinators;
        }

        /// <summary>
        /// Get list of VocationalCoordinators by EmailIds
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="emailIds"></param>
        /// <returns></returns>
        public IQueryable<VocationalCoordinatorModel> GetVocationalCoordinatorsByEmails(Guid academicYearId, List<string> emailIds)
        {
            var vocationalCoordinators = (from vcm in this.Context.VTPCoordinatorMap
                                          join vc in this.Context.VocationalCoordinators on vcm.VCId equals vc.VCId
                                          where vcm.AcademicYearId == academicYearId && vcm.IsActive == true && vc.IsActive == true && emailIds.Contains(vc.EmailId)
                                          select new VocationalCoordinatorModel
                                          {
                                              AcademicYearId = vcm.AcademicYearId,
                                              VTPId = vcm.VTPId,
                                              VCId = vcm.VCId,
                                              FullName = vc.FullName,
                                              EmailId = vc.EmailId
                                          }).AsQueryable();

            return vocationalCoordinators;
        }

        /// <summary>
        /// Get VocationalCoordinator by VCId
        /// </summary>
        /// <param name="VCId"></param>
        /// <returns></returns>
        public VocationalCoordinator GetVocationalCoordinatorById(Guid vcId)
        {
            VocationalCoordinator vocationalCoordinator = this.Context.VocationalCoordinators.FirstOrDefault(v => v.VCId == vcId);
            if (vocationalCoordinator != null)
            {
                AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true);
                VTPCoordinatorsMap vtpCoordinators = this.Context.VTPCoordinatorMap.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.VCId == vcId && v.DateOfResignation == null && v.IsActive == true);

                if (vtpCoordinators != null)
                {
                    vocationalCoordinator.VTPCoordinator = vtpCoordinators;
                }
            }

            return vocationalCoordinator;
        }

        /// <summary>
        /// Get VocationalCoordinator by VCId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        public VocationalCoordinator GetVocationalCoordinatorById(DataRequest vcRequest)
        {
            Guid academicYearId = Guid.Parse(vcRequest.DataId);
            Guid vtpId = Guid.Parse(vcRequest.DataId1);
            Guid vcId = Guid.Parse(vcRequest.DataId2);

            VocationalCoordinator vocationalCoordinator = this.Context.VocationalCoordinators.FirstOrDefault(v => v.VCId == vcId);
            if (vocationalCoordinator != null)
            {
                VTPCoordinatorsMap vtpCoordinators = this.Context.VTPCoordinatorMap.FirstOrDefault(v => v.AcademicYearId == academicYearId && v.VTPId == vtpId && v.VCId == vcId);

                if (vtpCoordinators != null)
                {
                    vocationalCoordinator.VTPCoordinator = vtpCoordinators;
                }
            }

            return vocationalCoordinator;
        }

        /// <summary>
        /// Get VocationalCoordinator by VCId using async
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public async Task<VocationalCoordinator> GetVocationalCoordinatorByIdAsync(Guid vcId)
        {
            var vocationalCoordinator = await (from v in this.Context.VocationalCoordinators
                                               where v.VCId == vcId
                                               select v).FirstOrDefaultAsync();

            return vocationalCoordinator;
        }

        /// <summary>
        /// Insert/Update VocationalCoordinator entity
        /// </summary>
        /// <param name="vocationalCoordinator"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVocationalCoordinatorDetails(VocationalCoordinator vocationalCoordinator)
        {
            try
            {
                if (RequestType.New == vocationalCoordinator.RequestType)
                {
                    Context.VocationalCoordinators.Add(vocationalCoordinator);
                    Context.SaveChanges();
                    AcademicYear academicYear = Context.AcademicYears.FirstOrDefault(a => a.IsCurrentAcademicYear == true);
                    vocationalCoordinator.VTPCoordinator.AcademicYearId = academicYear.AcademicYearId;
                    Context.VTPCoordinatorMap.Add(vocationalCoordinator.VTPCoordinator);
                    Context.SaveChanges();
                }
                else
                {
                    Context.Entry<VocationalCoordinator>(vocationalCoordinator).State = EntityState.Modified;
                    Context.SaveChanges();
                    Context.Entry<VTPCoordinatorsMap>(vocationalCoordinator.VTPCoordinator).State = EntityState.Modified;
                    Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVocationalCoordinatorDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VCId
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcId)
        {
            try
            {
                VocationalCoordinator vocationalCoordinator = this.Context.VocationalCoordinators.FirstOrDefault(v => v.VCId == vcId);

                if (vocationalCoordinator != null)
                {
                    VCTrainerMap vocationalTrainer = this.Context.VCTrainersMap.FirstOrDefault(v => v.VCId == vcId);
                    VCDailyReporting vcDailyReporting = this.Context.VCDailyReportings.FirstOrDefault(v => v.VCId == vcId);

                    if (vocationalTrainer == null && vcDailyReporting == null)
                    {
                        Account account = this.Context.Accounts.FirstOrDefault(v => v.AccountId == vcId);

                        if (account != null)
                        {
                            AccountRole accountRole = this.Context.AccountRoles.FirstOrDefault(v => v.AccountId == account.AccountId);

                            if (accountRole != null)
                            {
                                Context.Entry<AccountRole>(accountRole).State = EntityState.Deleted;
                            }

                            Context.Entry<Account>(account).State = EntityState.Deleted;
                        }

                        List<VCSchoolSector> vcSchoolSectors = this.Context.VCSchoolSectors.Where(v => v.VCId == vcId).ToList();
                        vcSchoolSectors.ForEach(vcSchoolSectorItem =>
                        {
                            Context.Entry<VCSchoolSector>(vcSchoolSectorItem).State = EntityState.Deleted;
                        });

                        Context.Entry<VocationalCoordinator>(vocationalCoordinator).State = EntityState.Deleted;

                        Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > DeletedVocationalCoordinatorById", ex);
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VocationalCoordinator by Name
        /// </summary>
        /// <param name="vocationalCoordinatorModel"></param>
        /// <returns></returns>
        public List<string> CheckVocationalCoordinatorExistByName(VocationalCoordinatorModel vocationalCoordinatorModel)
        {
            var errorMessages = new List<string>();

            try
            {
                if (vocationalCoordinatorModel.RequestType == RequestType.New)
                {
                    var vocationalCoordinator = this.Context.VocationalCoordinators.FirstOrDefault(v => v.EmailId == vocationalCoordinatorModel.EmailId && v.IsActive == true);

                    if (vocationalCoordinator != null)
                    {
                        errorMessages.Add(string.Format("EmailId is already exists for another VC ({0})", vocationalCoordinator.FullName));
                    }

                    vocationalCoordinator = this.Context.VocationalCoordinators.FirstOrDefault(v => v.Mobile == vocationalCoordinatorModel.Mobile && v.IsActive == true);

                    if (vocationalCoordinator != null)
                    {
                        errorMessages.Add(string.Format("Mobile is already exists for another VC ({0})", vocationalCoordinator.FullName));
                    }

                    Account account = this.Context.Accounts.FirstOrDefault(a => a.LoginId == vocationalCoordinatorModel.EmailId && a.IsActive == true);
                    if (account != null)
                    {
                        errorMessages.Add(string.Format("Account is already registered with this email ({0})", account.EmailId));
                    }
                }
                else if (vocationalCoordinatorModel.RequestType == RequestType.Edit && !vocationalCoordinatorModel.DateOfResignation.HasValue)
                {
                    VocationalCoordinator vocationalCoordinator = this.Context.VocationalCoordinators.FirstOrDefault(v => v.VCId == vocationalCoordinatorModel.VCId);
                    VTPCoordinatorsMap VTPCoordinatorsMap = this.Context.VTPCoordinatorMap.FirstOrDefault(v => v.AcademicYearId == vocationalCoordinatorModel.AcademicYearId && v.VCId == vocationalCoordinatorModel.VCId && v.IsActive == true);

                    if (vocationalCoordinator.IsActive && !vocationalCoordinatorModel.IsActive)
                    {
                        var vcSchoolSectors = this.Context.VCSchoolSectors.Where(v => v.AcademicYearId == vocationalCoordinatorModel.AcademicYearId && v.VCId == vocationalCoordinatorModel.VCId && v.IsActive == true).ToList();
                        if (vcSchoolSectors.Count > 0)
                        {
                            errorMessages.Add("VC School Sector is already active, Please inactive VCSchoolSector mapping first!");
                        }

                        var vocationalTrainers = this.Context.VCTrainersMap.Where(v => v.VCId == vocationalCoordinatorModel.VCId && v.IsActive == true).ToList();
                        if (vocationalTrainers.Count > 0)
                        {
                            errorMessages.Add("Vocational Trainers are already active, Please inactive all Vocational Trainers first!");
                        }
                    }

                    if (!Guid.Equals(vocationalCoordinator.VTPCoordinator.VTPId, vocationalCoordinatorModel.VTPId))
                    {
                        var vcSchoolSectors = (from vcss in this.Context.VCSchoolSectors
                                               join svs in this.Context.SchoolVTPSectors on new { a = vcss.AcademicYearId, b = vcss.SchoolVTPSectorId, c = vcss.IsActive } equals new { a = svs.AcademicYearId, b = svs.SchoolVTPSectorId, c = svs.IsActive }
                                               where vcss.AcademicYearId == vocationalCoordinatorModel.AcademicYearId && vcss.VCId == vocationalCoordinatorModel.VCId && svs.VTPId == vocationalCoordinator.VTPCoordinator.VTPId && vcss.IsActive == true
                                               select vcss).ToList();

                        if (vcSchoolSectors.Count > 0)
                        {
                            errorMessages.Add("VC School Sectors are active, Please inactive VCSchoolSector mapping first before changing VTP Name");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVocationalCoordinatorExistByName", ex);
            }

            return errorMessages;
        }

        /// <summary>
        /// List of VocationalCoordinator with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<VocationalCoordinatorViewModel> GetVocationalCoordinatorsByCriteria(SearchVocationalCoordinatorModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[8];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "natureOfAppointmentId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.NatureOfAppointmentId };
            sqlParams[3] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[4] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[5] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[6] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[7] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.VocationalCoordinatorViewModels.FromSql<VocationalCoordinatorViewModel>("CALL GetVocationalCoordinatorsByCriteria (@academicYearId, @vtpId, @natureOfAppointmentId, @status, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Inactive VC Related Data When Resigned
        /// </summary>
        /// <param name="vocationalCoordinator"></param>
        /// <param name="isActivate"></param>
        /// <returns></returns>
        public bool InactiveVCRelatedDataWhenResigned(VocationalCoordinator vocationalCoordinator)
        {
            AcademicYear academicYears = this.Context.AcademicYears.FirstOrDefault(v => v.IsCurrentAcademicYear == true);

            var vcSchoolSectors = this.Context.VCSchoolSectors.Where(v => v.AcademicYearId == academicYears.AcademicYearId && v.VCId == vocationalCoordinator.VCId && v.DateOfRemoval == null && v.IsActive == true).ToList();

            foreach (var schoolSectorItem in vcSchoolSectors)
            {
                if (!vocationalCoordinator.IsActive && vocationalCoordinator.VTPCoordinator.DateOfResignation.HasValue)
                {
                    schoolSectorItem.DateOfRemoval = vocationalCoordinator.VTPCoordinator.DateOfResignation;
                }
                else if (vocationalCoordinator.IsActive && !vocationalCoordinator.VTPCoordinator.DateOfResignation.HasValue)
                {
                    schoolSectorItem.DateOfRemoval = null;
                }

                schoolSectorItem.UpdatedBy = vocationalCoordinator.UpdatedBy;
                schoolSectorItem.UpdatedOn = Constants.GetCurrentDateTime;
                schoolSectorItem.IsActive = vocationalCoordinator.IsActive;

                Context.Entry<VCSchoolSector>(schoolSectorItem).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Get list of VocationalTrainingProvider
        /// </summary>
        /// <returns></returns>
        public IList<VocationalCoordinatorModel> GetVCList()
        {
            var vtpList = (from vcm in this.Context.VTPCoordinatorMap
                           join vtpm in this.Context.VTPAcademicYearMap on new { a = vcm.AcademicYearId, b = vcm.VTPId } equals new { a = vtpm.AcademicYearId, b = vtpm.VTPId }
                           join ay in this.Context.AcademicYears on vtpm.AcademicYearId equals ay.AcademicYearId
                           join vc in this.Context.VocationalCoordinators on vcm.VCId equals vc.VCId
                           where vcm.IsActive == true && vtpm.IsActive == true && vc.IsActive == true
                           && ay.IsCurrentAcademicYear == true && vcm.DateOfResignation == null
                           select new VocationalCoordinatorModel
                           {
                               AcademicYearId = vcm.AcademicYearId,
                               VCId = vcm.VCId,
                               VTPId = vcm.VTPId,
                               FullName = vc.FullName
                           }).ToList();

            return vtpList;
        }

        /// <summary>
        /// Get VC Schools By VTP And VC Id
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        public IList<VCSchoolModel> GetVCSchoolsByVTPAndVCId(DataRequest schoolRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.VarChar, Value = schoolRequest.DataId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.VarChar, Value = schoolRequest.DataId1 };
            sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.VarChar, Value = schoolRequest.DataId2 };

            return Context.VCSchoolModels.FromSql<VCSchoolModel>("CALL GetVCSchoolsByVTPAndVCId (@AcademicYearId, @VTPId, @VCId)", sqlParams).ToList();
        }

        /// <summary>
        /// Save VC Transfers data
        /// </summary>
        /// <param name="vcSchoolTransferRequest"></param>
        /// <returns></returns>
        public VCSchoolTransferModel SaveVCTransfers(VCSchoolTransferModel vcSchoolTransferRequest)
        {
            foreach (VCSchoolModel vcSchoolItem in vcSchoolTransferRequest.VCSchoolModels)
            {
                try
                {
                    using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                    {
                        DateTime creationDate = Constants.GetCurrentDateTime;

                        SchoolVTPSector schoolVTPSectorOld = Context.SchoolVTPSectors.FirstOrDefault(svs => svs.SchoolVTPSectorId == vcSchoolItem.SchoolVTPSectorId);
                        VCSchoolSector vcSchoolSectorOld = Context.VCSchoolSectors.FirstOrDefault(vcs => vcs.VCSchoolSectorId == vcSchoolItem.VCSchoolSectorId && vcs.IsActive == true);

                        if (schoolVTPSectorOld != null && vcSchoolSectorOld != null)
                        {
                            SchoolVTPSector schoolVTPSectorNew = Context.SchoolVTPSectors.FirstOrDefault(svs => svs.AcademicYearId == schoolVTPSectorOld.AcademicYearId && svs.SectorId == schoolVTPSectorOld.SectorId && svs.SchoolId == schoolVTPSectorOld.SchoolId && svs.VTPId == vcSchoolItem.ToVTPId && svs.IsActive == true);

                            if (schoolVTPSectorNew != null)
                            {
                                // Create new VCSchoolSector mapping
                                VCSchoolSector vcSchoolSector = new VCSchoolSector();
                                vcSchoolSector.VCSchoolSectorId = Guid.NewGuid();
                                vcSchoolSector.AcademicYearId = schoolVTPSectorNew.AcademicYearId;
                                vcSchoolSector.VCId = vcSchoolItem.ToVCId.Value;
                                vcSchoolSector.SchoolVTPSectorId = schoolVTPSectorNew.SchoolVTPSectorId;
                                vcSchoolSector.DateOfAllocation = vcSchoolItem.DateOfAllocation.Value;
                                vcSchoolSector.DateOfRemoval = null;
                                vcSchoolSector.CreatedBy = vcSchoolTransferRequest.UserId;
                                vcSchoolSector.CreatedOn = creationDate;
                                vcSchoolSector.UpdatedBy = vcSchoolTransferRequest.UserId;
                                vcSchoolSector.UpdatedOn = creationDate;
                                vcSchoolSector.IsActive = true;

                                Context.VCSchoolSectors.Add(vcSchoolSector);

                                List<VCTrainerMap> vcTrainers = (from vtss in Context.VTSchoolSectors
                                                                 join vtm in Context.VCTrainersMap on new { a = vtss.AcademicYearId, b = vtss.VTId, c = vtss.IsActive } equals new { a = vtm.AcademicYearId, b = vtm.VTId, c = vtm.IsActive }
                                                                 where vtss.AcademicYearId == schoolVTPSectorNew.AcademicYearId && vtss.SchoolId == schoolVTPSectorNew.SchoolId && vtm.VTPId == vcSchoolItem.VTPId && vtm.VCId == vcSchoolItem.VCId && vtm.IsActive == true
                                                                 select vtm).ToList();

                                foreach (VCTrainerMap vcTrainerOld in vcTrainers)
                                {
                                    // Create new VCTrainerMap mapping
                                    VCTrainerMap vcTrainer = new VCTrainerMap();
                                    vcTrainer.VCTrainerId = Guid.NewGuid();
                                    vcTrainer.AcademicYearId = schoolVTPSectorNew.AcademicYearId;
                                    vcTrainer.VTId = vcTrainerOld.VTId;
                                    vcTrainer.VCId = vcSchoolItem.ToVCId.Value;
                                    vcTrainer.VTPId = vcSchoolItem.ToVTPId.Value;
                                    vcTrainer.DateOfJoining = vcSchoolItem.DateOfJoining.Value;
                                    vcTrainer.DateOfResignation = null;
                                    vcTrainer.NatureOfAppointment = vcTrainerOld.NatureOfAppointment;
                                    vcTrainer.CreatedBy = vcSchoolTransferRequest.UserId;
                                    vcTrainer.CreatedOn = creationDate;
                                    vcTrainer.UpdatedBy = vcSchoolTransferRequest.UserId;
                                    vcTrainer.UpdatedOn = creationDate;
                                    vcTrainer.IsActive = true;

                                    Context.VCTrainersMap.Add(vcTrainer);

                                    // Inactive old VCTrainerMap mapping
                                    vcTrainerOld.DateOfResignation = creationDate;
                                    vcTrainerOld.UpdatedBy = vcSchoolTransferRequest.UserId;
                                    vcTrainerOld.UpdatedOn = creationDate;
                                    vcTrainerOld.IsActive = false;
                                    Context.Entry<VCTrainerMap>(vcTrainerOld).State = EntityState.Modified;
                                }

                                // Inactive old VCSchoolSector mapping
                                vcSchoolSectorOld.DateOfRemoval = creationDate;
                                vcSchoolSectorOld.UpdatedBy = vcSchoolTransferRequest.UserId;
                                vcSchoolSectorOld.UpdatedOn = creationDate;
                                vcSchoolSectorOld.IsActive = false;
                                Context.Entry<VCSchoolSector>(vcSchoolSectorOld).State = EntityState.Modified;

                                // Inactive old SchoolVTPSector mapping
                                if (!(schoolVTPSectorOld.AcademicYearId == vcSchoolItem.AcademicYearId && schoolVTPSectorOld.SectorId == vcSchoolItem.SectorId && schoolVTPSectorOld.VTPId == vcSchoolItem.ToVTPId && schoolVTPSectorOld.SchoolId == schoolVTPSectorOld.SchoolId))
                                {
                                    schoolVTPSectorOld.UpdatedBy = vcSchoolTransferRequest.UserId;
                                    schoolVTPSectorOld.UpdatedOn = creationDate;
                                    schoolVTPSectorOld.IsActive = false;
                                    Context.Entry<SchoolVTPSector>(schoolVTPSectorOld).State = EntityState.Modified;
                                }

                                vcSchoolItem.Remarks = "Transfer Completed";

                                // Commit all changes for VC Transfers
                                this.Context.SaveChanges();
                                transaction.Commit();
                            }
                            else
                            {
                                vcSchoolItem.Remarks = "School VTP Sector does not exists";
                            }
                        }
                        else if (vcSchoolSectorOld != null)
                        {
                            vcSchoolItem.Remarks = "VC School Sector does not exists";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return vcSchoolTransferRequest;
        }
    }
}