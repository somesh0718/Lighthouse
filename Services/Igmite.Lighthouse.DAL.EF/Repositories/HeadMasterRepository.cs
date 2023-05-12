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
    /// Repository of the HeadMaster entity
    /// </summary>
    public class HeadMasterRepository : GenericRepository<HeadMaster>, IHeadMasterRepository
    {
        /// <summary>
        /// Get list of HeadMaster
        /// </summary>
        /// <returns></returns>
        public IQueryable<HeadMaster> GetHeadMasters()
        {
            return this.Context.HeadMasters.AsQueryable();
        }

        /// <summary>
        /// Get list of HeadMaster by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<HeadMaster> GetHeadMastersByName(string name)
        {
            var headMasters = (from h in this.Context.HeadMasters
                               where h.FirstName.Contains(name)
                               select h).AsQueryable();

            return headMasters;
        }

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="HMId"></param>
        /// <returns></returns>
        public HeadMaster GetHeadMasterById(Guid hmId)
        {
            HeadMaster headMaster = this.Context.HeadMasters.FirstOrDefault(v => v.HMId == hmId);
            if (headMaster != null)
            {
                AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true);
                HMSchoolsMap hmSchools = this.Context.HMSchoolMap.FirstOrDefault(v => v.AcademicYearId == academicYear.AcademicYearId && v.HMId == hmId && v.DateOfResignation == null && v.IsActive == true);

                if (hmSchools != null)
                {
                    headMaster.HMSchool = hmSchools;
                }
            }

            return headMaster;
        }

        /// <summary>
        /// Get HeadMaster by HMId
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="HMId"></param>
        /// <returns></returns>
        public HeadMaster GetHeadMasterById(DataRequest hmRequest)
        {
            Guid academicYearId = Guid.Parse(hmRequest.DataId);
            Guid schoolId = Guid.Parse(hmRequest.DataId1);
            Guid hmId = Guid.Parse(hmRequest.DataId2);

            HeadMaster headMaster = this.Context.HeadMasters.FirstOrDefault(h => h.HMId == hmId);
            if (headMaster != null)
            {
                HMSchoolsMap HMSchools = this.Context.HMSchoolMap.FirstOrDefault(v => v.AcademicYearId == academicYearId && v.SchoolId == schoolId && v.HMId == hmId);

                if (HMSchools != null)
                {
                    headMaster.HMSchool = HMSchools;
                }
            }
            return headMaster;
        }

        /// <summary>
        /// Get HeadMaster by HMId using async
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public async Task<HeadMaster> GetHeadMasterByIdAsync(Guid hmId)
        {
            var headMaster = await (from h in this.Context.HeadMasters
                                    where h.HMId == hmId
                                    select h).FirstOrDefaultAsync();

            return headMaster;
        }

        /// <summary>
        /// Insert/Update HeadMaster entity
        /// </summary>
        /// <param name="headMaster"></param>
        /// <returns></returns>
        public bool SaveOrUpdateHeadMasterDetails(HeadMaster headMaster)
        {
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    if (RequestType.New == headMaster.RequestType)
                    {
                        Context.HeadMasters.Add(headMaster);
                        Context.SaveChanges();

                        AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(ay => ay.IsCurrentAcademicYear == true);
                        headMaster.HMSchool.AcademicYearId = academicYear.AcademicYearId;

                        Context.HMSchoolMap.Add(headMaster.HMSchool);
                        Context.SaveChanges();
                    }
                    else
                    {
                        Context.Entry<HeadMaster>(headMaster).State = EntityState.Modified;
                        Context.Entry<HMSchoolsMap>(headMaster.HMSchool).State = EntityState.Modified;

                        Context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("DAL > SaveOrUpdateHeadMasterDetails", ex);
                }
            }

            return true;
        }

        /// <summary>
        /// Delete a record by HMId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid hmId)
        {
            try
            {
                HeadMaster headMaster = this.Context.HeadMasters.FirstOrDefault(h => h.HMId == hmId);

                if (headMaster != null)
                {
                    Account account = this.Context.Accounts.FirstOrDefault(v => v.LoginId == headMaster.Email);

                    if (account != null)
                    {
                        AccountRole accountRole = this.Context.AccountRoles.FirstOrDefault(v => v.AccountId == account.AccountId);

                        if (accountRole != null)
                        {
                            accountRole.IsActive = false;
                            Context.Entry<AccountRole>(accountRole).State = EntityState.Modified;
                        }

                        account.IsActive = false;
                        Context.Entry<Account>(account).State = EntityState.Modified;
                    }

                    headMaster.IsActive = false;
                    Context.Entry<HeadMaster>(headMaster).State = EntityState.Modified;

                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > DeleteHeadMasterById", ex);
            }

            return true;
        }

        /// <summary>
        /// Check duplicate HeadMaster by Name
        /// </summary>
        /// <param name="headMaster"></param>
        /// <param name="headMasterModel"></param>
        /// <returns></returns>
        public List<string> CheckHeadMasterExistByName(HeadMaster headMaster, HeadMasterModel headMasterModel)
        {
            var errorMessages = new List<string>();

            try
            {
                HeadMaster headMasterItem = null;
                HMSchoolsMap hmSchoolItem = this.Context.HMSchoolMap.FirstOrDefault(h => h.AcademicYearId == headMasterModel.AcademicYearId && h.SchoolId == headMasterModel.SchoolId && h.IsActive == true);

                if (headMasterModel.RequestType == RequestType.New)
                {
                    if (hmSchoolItem != null)
                    {
                        headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.HMId == hmSchoolItem.HMId);
                        errorMessages.Add(string.Format("{0} - Record already exists.", headMasterItem.FullName));
                    }

                    var vtpVCMapping = (from svs in this.Context.SchoolVTPSectors
                                        join vcss in this.Context.VCSchoolSectors on new { a = svs.AcademicYearId, b = svs.SchoolVTPSectorId } equals new { a = vcss.AcademicYearId, b = vcss.SchoolVTPSectorId }
                                        join ay in this.Context.AcademicYears on svs.AcademicYearId equals ay.AcademicYearId
                                        where svs.SchoolId == headMasterModel.SchoolId && svs.IsActive == true && ay.IsCurrentAcademicYear == true
                                        select new LhUserModel
                                        {
                                            AcademicYearId = svs.AcademicYearId,
                                            VTPId = svs.VTPId,
                                            VCId = vcss.VCId
                                        }).FirstOrDefault();

                    if (vtpVCMapping == null)
                    {
                        errorMessages.Add("School does not mapped with VTP and VC");
                    }

                    headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.IsActive == true && h.Email == headMasterModel.Email);
                    if (headMasterItem != null)
                    {
                        errorMessages.Add(string.Format("EmailId is already exists for another Head Master ({0})", headMasterItem.FullName));
                    }

                    headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.IsActive == true && h.Mobile == headMasterModel.Mobile);
                    if (headMasterItem != null)
                    {
                        errorMessages.Add(string.Format("Mobile no is already exists for another Head Master ({0})", headMasterItem.FullName));
                    }

                    HMSchoolsMap HmSchoolMap = this.Context.HMSchoolMap.FirstOrDefault(h => h.AcademicYearId == headMasterModel.AcademicYearId && h.SchoolId == headMasterModel.SchoolId && h.IsActive == true && h.DateOfResignation == null);
                    if (HmSchoolMap != null)
                    {
                        headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.HMId == HmSchoolMap.HMId);
                        errorMessages.Add(string.Format("School is already exists for another Head Master ({0})", headMasterItem.FullName));
                    }

                    Account account = this.Context.Accounts.FirstOrDefault(a => a.LoginId == headMasterModel.Email && a.IsActive == true);
                    if (account != null)
                    {
                        errorMessages.Add(string.Format("Account is already registered with this email ({0})", account.EmailId));
                    }
                }
                else if (headMasterModel.RequestType == RequestType.Edit)
                {
                    headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.HMId == headMasterModel.HMId && h.IsActive == true);

                    if (headMasterItem != null && !Guid.Equals(headMaster.HMSchool.SchoolId, headMasterModel.SchoolId))
                    {
                        errorMessages.Add(string.Format("{0} - Record already exists.", headMasterItem.FullName));
                    }

                    headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.IsActive == true && h.Email == headMasterModel.Email);
                    if (headMasterItem != null && !Guid.Equals(headMaster.HMSchool.SchoolId, headMasterModel.SchoolId) && !string.Equals(headMasterItem.Email, headMaster.Email))
                    {
                        errorMessages.Add(string.Format("EmailId is already exists for another Head Master ({0})", headMasterItem.FullName));
                    }

                    headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.IsActive == true && h.Mobile == headMasterModel.Mobile);
                    if (headMasterItem != null && !Guid.Equals(headMaster.HMSchool.SchoolId, headMasterModel.SchoolId) && !string.Equals(headMasterItem.Mobile, headMaster.Mobile))
                    {
                        errorMessages.Add(string.Format("Mobile no is already exists for another Head Master ({0})", headMasterItem.FullName));
                    }

                    headMasterItem = this.Context.HeadMasters.FirstOrDefault(h => h.IsActive == true && h.HMSchool.SchoolId == headMasterModel.SchoolId && h.HMId != headMasterModel.HMId);
                    if (headMasterItem != null)
                    {
                        errorMessages.Add(string.Format("School is already exists for another Head Master ({0})", headMasterItem.FullName));
                    }
                }

                HMSchoolsMap hmSchoolMap = this.Context.HMSchoolMap.Where(hm => hm.AcademicYearId == headMasterModel.AcademicYearId && hm.SchoolId == headMasterModel.SchoolId && hm.DateOfResignation != null).OrderByDescending(h => h.DateOfResignation).FirstOrDefault();
                if (hmSchoolMap != null && DateTime.Compare(hmSchoolMap.DateOfResignation.Value.Date, headMasterModel.DateOfJoining.Date) > 0)
                {
                    errorMessages.Add("The Date Of Resignation of the previous Head Master is greater then Date Of Joining of the current Head Master.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckHeadMasterExistByName", ex);
            }

            return errorMessages;
        }

        /// <summary>
        /// Inactive HM Related Data When Resigned
        /// </summary>
        /// <param name="headMaster"></param>
        /// <param name="isActivate"></param>
        /// <returns></returns>
        public bool InactiveHMRelatedDataWhenResigned(HeadMaster headMaster)
        {
            AcademicYear academicYears = this.Context.AcademicYears.FirstOrDefault(v => v.IsCurrentAcademicYear == true);

            HMSchoolsMap hmSchoolMap = this.Context.HMSchoolMap.Where(v => v.AcademicYearId == academicYears.AcademicYearId && v.HMId == headMaster.HMId && v.DateOfResignation == null && v.IsActive == true).OrderByDescending(x => x.UpdatedOn).FirstOrDefault();

            if (hmSchoolMap != null)
            {
                if (!headMaster.IsActive && headMaster.HMSchool.DateOfResignation.HasValue)
                {
                    hmSchoolMap.DateOfResignation = headMaster.HMSchool.DateOfResignation;
                }
                else if (headMaster.IsActive && !headMaster.HMSchool.DateOfResignation.HasValue)
                {
                    hmSchoolMap.DateOfResignation = null;
                }

                hmSchoolMap.UpdatedBy = headMaster.UpdatedBy;
                hmSchoolMap.UpdatedOn = Constants.GetCurrentDateTime;
                hmSchoolMap.IsActive = headMaster.IsActive;

                Context.Entry<HMSchoolsMap>(hmSchoolMap).State = EntityState.Modified;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// List of HeadMaster with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<HeadMasterViewModel> GetHeadMastersByCriteria(SearchHeadMasterModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[12];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[5] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "jobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[8] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[9] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[10] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[11] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.HeadMasterViewModels.FromSql<HeadMasterViewModel>("CALL GetHeadMastersByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @schoolId, @sectorId, @jobRoleId, @status, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}