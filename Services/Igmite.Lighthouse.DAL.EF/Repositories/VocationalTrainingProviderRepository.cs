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
    /// Repository of the VocationalTrainingProvider entity
    /// </summary>
    public class VocationalTrainingProviderRepository : GenericRepository<VocationalTrainingProvider>, IVocationalTrainingProviderRepository
    {
        /// <summary>
        /// Get list of VocationalTrainingProvider
        /// </summary>
        /// <returns></returns>
        public IQueryable<VocationalTrainingProvider> GetVocationalTrainingProviders()
        {
            return this.Context.VocationalTrainingProviders.AsQueryable();
        }

        /// <summary>
        /// Get list of VocationalTrainingProvider by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VocationalTrainingProvider> GetVocationalTrainingProvidersByName(string name)
        {
            var vocationalTrainingProviders = (from v in this.Context.VocationalTrainingProviders
                                               where v.VTPShortName.Contains(name)
                                               select v).AsQueryable();

            return vocationalTrainingProviders;
        }

        /// <summary>
        /// Get list of TEAndRMList
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEAndRMList> GetTEAndRMList()
        {
            return this.Context.TEAndRMList.AsQueryable();
        }

        /// <summary>
        /// Get VocationalTrainingProvider by VTPId
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public VocationalTrainingProvider GetVocationalTrainingProviderById(Guid vtpId)
        {
            VocationalTrainingProvider vocationalTrainingProvider = this.Context.VocationalTrainingProviders.FirstOrDefault(v => v.VTPId == vtpId);
            if (vocationalTrainingProvider != null)
            {
                AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true);
                if (academicYear != null)
                {
                    VTPAcademicYearsMap vtpAcademicYears = this.Context.VTPAcademicYearMap.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.VTPId == vtpId && v.IsActive == true);

                    if (vtpAcademicYears != null)
                    {
                        vocationalTrainingProvider.VTPAcademicYear = vtpAcademicYears;
                    }
                }
            }
            return vocationalTrainingProvider;
        }

        /// <summary>
        /// Get VocationalTrainingProvider by VTPId using async
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public async Task<VocationalTrainingProvider> GetVocationalTrainingProviderByIdAsync(Guid vtpId)
        {
            var vocationalTrainingProvider = await (from v in this.Context.VocationalTrainingProviders
                                                    where v.VTPId == vtpId
                                                    select v).FirstOrDefaultAsync();

            return vocationalTrainingProvider;
        }

        /// <summary>
        /// Insert/Update VocationalTrainingProvider entity
        /// </summary>
        /// <param name="vocationalTrainingProvider"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVocationalTrainingProviderDetails(VocationalTrainingProvider vocationalTrainingProvider)
        {
            try
            {
                if (RequestType.New == vocationalTrainingProvider.RequestType)
                {
                    Context.VocationalTrainingProviders.Add(vocationalTrainingProvider);
                    Context.SaveChanges();

                    AcademicYear academicYear = Context.AcademicYears.FirstOrDefault(a => a.IsCurrentAcademicYear == true);
                    vocationalTrainingProvider.VTPAcademicYear.AcademicYearId = academicYear.AcademicYearId;
                    vocationalTrainingProvider.VTPAcademicYear.DateOfJoining = Constants.GetCurrentDateTime;

                    Context.VTPAcademicYearMap.Add(vocationalTrainingProvider.VTPAcademicYear);
                    Context.SaveChanges();
                }
                else
                {
                    Context.Entry<VocationalTrainingProvider>(vocationalTrainingProvider).State = EntityState.Modified;
                    Context.SaveChanges();
                    Context.Entry<VTPAcademicYearsMap>(vocationalTrainingProvider.VTPAcademicYear).State = EntityState.Modified;
                    Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVocationalTrainingProviderDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTPId
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpId)
        {
            VocationalTrainingProvider vocationalTrainingProvider = this.Context.VocationalTrainingProviders.FirstOrDefault(v => v.VTPId == vtpId);

            if (vocationalTrainingProvider != null)
            {
                Context.Entry<VocationalTrainingProvider>(vocationalTrainingProvider).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VocationalTrainingProvider by Name
        /// </summary>
        /// <param name="vocationalTrainingProviderModel"></param>
        /// <returns></returns>
        public bool CheckVocationalTrainingProviderExistByName(VocationalTrainingProviderModel vocationalTrainingProviderModel)
        {
            VocationalTrainingProvider vocationalTrainingProvider = this.Context.VocationalTrainingProviders.FirstOrDefault(v => v.VTPShortName == vocationalTrainingProviderModel.VTPShortName);

            return vocationalTrainingProvider != null;
        }

        /// <summary>
        /// Get list of VocationalTrainingProvider
        /// </summary>
        /// <returns></returns>
        public IList<DropdownModel<Guid>> GetVTPList()
        {
            var vtpList = (from vtpm in this.Context.VTPAcademicYearMap
                           join ay in this.Context.AcademicYears on vtpm.AcademicYearId equals ay.AcademicYearId
                           join vtp in this.Context.VocationalTrainingProviders on vtpm.VTPId equals vtp.VTPId
                           where vtpm.IsActive == true && vtp.IsActive == true && ay.IsCurrentAcademicYear == true && vtpm.DateOfResignation == null
                           select new DropdownModel<Guid>
                           {
                               Id = vtp.VTPId,
                               Name = vtp.VTPShortName,
                               Description = vtp.VTPName,
                               IsDisabled = false,
                               IsSelected = false,
                               SequenceNo = 1
                           }).ToList();

            return vtpList;
        }

        /// <summary>}
        /// List of VocationalTrainingProvider with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VocationalTrainingProviderViewModel> GetVocationalTrainingProvidersByCriteria(SearchVocationalTrainingProviderModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[6];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[2] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[3] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[5] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.VocationalTrainingProviderViewModels.FromSql<VocationalTrainingProviderViewModel>("CALL GetVocationalTrainingProvidersByCriteria (@academicYearId, @status, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// List of School Students by VT
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<VTPSchoolModel> GetSchoolByVTPIdSectorId(Guid academicYearId, Guid vtpId, Guid sectorId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = vtpId };
            sqlParams[2] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = sectorId };

            return Context.VTPSchoolModels.FromSql<VTPSchoolModel>("CALL GetSchoolByVTPIdSectorId (@AcademicYearId, @VTPId, @SectorId)", sqlParams).ToList();
        }


        /// <summary>
        /// Save VTP Transfers data
        /// </summary>
        /// <param name="vtpSchoolTransferRequest"></param>
        /// <returns></returns>
        public VTPSchoolTransferModel SaveVTPTransfers(VTPSchoolTransferModel vtpSchoolTransferRequest)
        {
            foreach (VTPSchoolModel vtpSchoolItem in vtpSchoolTransferRequest.VTPSchoolModels)
            {
                try
                {
                    using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                    {
                        DateTime creationDate = Constants.GetCurrentDateTime;

                        VTPSector vtpSectorOld = Context.VTPSectors.FirstOrDefault(sv => sv.AcademicYearId == vtpSchoolItem.AcademicYearId && sv.SectorId == vtpSchoolItem.SectorId);
                        VTPCoordinatorsMap vtpCoordinatorsMapOld = Context.VTPCoordinatorMap.FirstOrDefault(vtp => vtp.AcademicYearId == vtpSchoolItem.AcademicYearId && vtp.VTPId == vtpSchoolItem.ToVTPId && vtp.VCId == vtpSchoolItem.ToVCId);                   
                        SchoolVTPSector schoolVTPSectorOld = Context.SchoolVTPSectors.FirstOrDefault(svtp => svtp.AcademicYearId == vtpSchoolItem.AcademicYearId && svtp.SchoolVTPSectorId == vtpSchoolItem.SchoolVTPSectorId);
                        VCSchoolSector vcSchoolSectorOld = Context.VCSchoolSectors.FirstOrDefault(vcs => vcs.AcademicYearId == vtpSchoolItem.AcademicYearId && vcs.VCSchoolSectorId == vtpSchoolItem.VCSchoolSectorId && vcs.IsActive == true);
                        if (vtpSectorOld != null && vtpCoordinatorsMapOld != null)
                        {
                            //Create new SchoolVTPSector mapping
                            SchoolVTPSector schoolVTPSector = new SchoolVTPSector();
                            schoolVTPSector.SchoolVTPSectorId = Guid.NewGuid();
                            schoolVTPSector.AcademicYearId = vtpSectorOld.AcademicYearId;
                            schoolVTPSector.SectorId = vtpSchoolItem.SectorId;
                            schoolVTPSector.VTPId = vtpSchoolItem.ToVTPId.Value;
                            schoolVTPSector.SchoolId = vtpSchoolItem.SchoolId;
                            schoolVTPSector.Remarks = vtpSchoolItem.Remarks;
                            schoolVTPSector.CreatedBy = vtpSchoolTransferRequest.UserId;
                            schoolVTPSector.CreatedOn = creationDate;
                            schoolVTPSector.UpdatedBy = vtpSchoolTransferRequest.UserId;
                            schoolVTPSector.UpdatedOn = creationDate;
                            schoolVTPSector.IsActive = true;

                            Context.SchoolVTPSectors.Add(schoolVTPSector);

                            // Create new VCSchoolSector mapping
                            VCSchoolSector vcSchoolSector = new VCSchoolSector();
                            vcSchoolSector.VCSchoolSectorId = Guid.NewGuid();
                            vcSchoolSector.AcademicYearId = vtpSectorOld.AcademicYearId;
                            vcSchoolSector.VCId = vtpSchoolItem.ToVCId.Value;
                            vcSchoolSector.SchoolVTPSectorId = schoolVTPSector.SchoolVTPSectorId;
                            vcSchoolSector.DateOfAllocation = vtpSchoolItem.DateOfAllocation.Value;
                            vcSchoolSector.DateOfRemoval = null;
                            vcSchoolSector.CreatedBy = vtpSchoolTransferRequest.UserId;
                            vcSchoolSector.CreatedOn = creationDate;
                            vcSchoolSector.UpdatedBy = vtpSchoolTransferRequest.UserId;
                            vcSchoolSector.UpdatedOn = creationDate;
                            vcSchoolSector.IsActive = true;

                            Context.VCSchoolSectors.Add(vcSchoolSector);

                            // Create new VTPCoordinatorsMap mapping
                            VTPCoordinatorsMap vTPCoordinatorsMap = new VTPCoordinatorsMap();
                            vTPCoordinatorsMap.VTPCoordinatorId = Guid.NewGuid();
                            vTPCoordinatorsMap.AcademicYearId = vtpSectorOld.AcademicYearId;
                            vTPCoordinatorsMap.VTPId = vtpSchoolItem.ToVTPId.Value;
                            vTPCoordinatorsMap.VCId = vtpSchoolItem.ToVCId.Value;
                            vTPCoordinatorsMap.DateOfJoining = vtpSchoolItem.DateOfJoining.Value;
                            vTPCoordinatorsMap.NatureOfAppointment = "58";
                            vTPCoordinatorsMap.CreatedBy = vtpSchoolTransferRequest.UserId;
                            vTPCoordinatorsMap.CreatedOn = creationDate;
                            vTPCoordinatorsMap.UpdatedBy = vtpSchoolTransferRequest.UserId;
                            vTPCoordinatorsMap.UpdatedOn = creationDate;
                            vTPCoordinatorsMap.IsActive = true;

                            Context.VTPCoordinatorMap.Add(vTPCoordinatorsMap);

                            List<VCTrainerMap> vcTrainers = (from vtss in Context.VTSchoolSectors
                                                                join vtm in Context.VCTrainersMap on new { a = vtss.AcademicYearId, b = vtss.VTId, c = vtss.IsActive } equals new { a = vtm.AcademicYearId, b = vtm.VTId, c = vtm.IsActive }
                                                                where vtss.AcademicYearId == schoolVTPSectorOld.AcademicYearId && vtss.SchoolId == schoolVTPSectorOld.SchoolId && vtss.SectorId == schoolVTPSectorOld.SectorId && vtm.IsActive == true
                                                                select vtm).ToList();

                            foreach (VCTrainerMap vcTrainerOld in vcTrainers)
                            {
                                // Create new VCTrainerMap mapping
                                VCTrainerMap vcTrainer = new VCTrainerMap();
                                vcTrainer.VCTrainerId = Guid.NewGuid();
                                vcTrainer.AcademicYearId = vtpSectorOld.AcademicYearId;
                                vcTrainer.VTId = vcTrainerOld.VTId;
                                vcTrainer.VCId = vtpSchoolItem.ToVCId.Value;
                                vcTrainer.VTPId = vtpSchoolItem.ToVTPId.Value;
                                vcTrainer.DateOfJoining = vtpSchoolItem.DateOfJoining.Value;
                                vcTrainer.DateOfResignation = null;
                                vcTrainer.NatureOfAppointment = vcTrainerOld.NatureOfAppointment;
                                vcTrainer.CreatedBy = vtpSchoolTransferRequest.UserId;
                                vcTrainer.CreatedOn = creationDate;
                                vcTrainer.UpdatedBy = vtpSchoolTransferRequest.UserId;
                                vcTrainer.UpdatedOn = creationDate;
                                vcTrainer.IsActive = true;

                                Context.VCTrainersMap.Add(vcTrainer);

                                // Inactive old VCTrainerMap mapping
                                vcTrainerOld.DateOfResignation = creationDate;
                                vcTrainerOld.UpdatedBy = vtpSchoolTransferRequest.UserId;
                                vcTrainerOld.UpdatedOn = creationDate;
                                vcTrainerOld.IsActive = false;
                                Context.Entry<VCTrainerMap>(vcTrainerOld).State = EntityState.Modified;
                            }

                            // Inactive old VTPCoordinatorsMap mapping
                            // vtpCoordinatorsMapOld.DateOfResignation = creationDate;
                            vtpCoordinatorsMapOld.UpdatedBy = vtpSchoolTransferRequest.UserId;
                            vtpCoordinatorsMapOld.UpdatedOn = creationDate;
                            vtpCoordinatorsMapOld.IsActive = false;
                            Context.Entry<VTPCoordinatorsMap>(vtpCoordinatorsMapOld).State = EntityState.Modified;

                            // Inactive old VCSchoolSector mapping
                            vcSchoolSectorOld.DateOfRemoval = creationDate;
                            vcSchoolSectorOld.UpdatedBy = vtpSchoolTransferRequest.UserId;
                            vcSchoolSectorOld.UpdatedOn = creationDate;
                            vcSchoolSectorOld.IsActive = false;
                            Context.Entry<VCSchoolSector>(vcSchoolSectorOld).State = EntityState.Modified;

                            // Inactive old SchoolVTPSector mapping
                            schoolVTPSectorOld.UpdatedBy = vtpSchoolTransferRequest.UserId;
                            schoolVTPSectorOld.UpdatedOn = creationDate;
                            schoolVTPSectorOld.IsActive = false;
                            Context.Entry<SchoolVTPSector>(schoolVTPSectorOld).State = EntityState.Modified;

                            vtpSchoolItem.Remarks = "Transfer Completed";

                            // Commit all changes for VTP Transfers
                            this.Context.SaveChanges();
                            transaction.Commit();
                           
                        }
                        else if (vcSchoolSectorOld != null)
                        {
                            vtpSchoolItem.Remarks = "VTP Sector/VTP CoordinatorsMap does not exists";
                        }


                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return vtpSchoolTransferRequest;
        }
    }
}