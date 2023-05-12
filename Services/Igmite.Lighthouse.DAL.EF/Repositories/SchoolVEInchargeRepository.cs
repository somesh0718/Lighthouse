using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the SchoolVEIncharge entity
    /// </summary>
    public class SchoolVEInchargeRepository : GenericRepository<SchoolVEIncharge>, ISchoolVEInchargeRepository
    {
        /// <summary>
        /// Get list of SchoolVEIncharge
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolVEIncharge> GetSchoolVEIncharges()
        {
            return this.Context.SchoolVEIncharges.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolVEIncharge by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolVEIncharge> GetSchoolVEInchargesByName(string name)
        {
            var schoolVEIncharges = (from s in this.Context.SchoolVEIncharges
                                     where s.FirstName.Contains(name)
                                     select s).AsQueryable();

            return schoolVEIncharges;
        }

        /// <summary>
        /// Get SchoolVEIncharge by VEIId
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        public SchoolVEIncharge GetSchoolVEInchargeById(Guid veiId)
        {
            return this.Context.SchoolVEIncharges.FirstOrDefault(s => s.VEIId == veiId);
        }

        /// <summary>
        /// Get SchoolVEIncharge by VEIId using async
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        public async Task<SchoolVEIncharge> GetSchoolVEInchargeByIdAsync(Guid veiId)
        {
            var schoolVEIncharge = await (from s in this.Context.SchoolVEIncharges
                                          where s.VEIId == veiId
                                          select s).FirstOrDefaultAsync();

            return schoolVEIncharge;
        }

        /// <summary>
        /// Insert/Update SchoolVEIncharge entity
        /// </summary>
        /// <param name="schoolVEIncharge"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSchoolVEInchargeDetails(SchoolVEIncharge schoolVEIncharge)
        {
            try
            {
                if (RequestType.New == schoolVEIncharge.RequestType)
                    Context.SchoolVEIncharges.Add(schoolVEIncharge);
                else
                {
                    Context.Entry<SchoolVEIncharge>(schoolVEIncharge).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateSchoolVEInchargeDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VEIId
        /// </summary>
        /// <param name="veiId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid veiId)
        {
            SchoolVEIncharge schoolVEIncharge = this.Context.SchoolVEIncharges.FirstOrDefault(s => s.VEIId == veiId);

            if (schoolVEIncharge != null)
            {
                Context.Entry<SchoolVEIncharge>(schoolVEIncharge).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate SchoolVEIncharge by Name
        /// </summary>
        /// <param name="schoolVEInchargeModel"></param>
        /// <returns></returns>
        public string CheckSchoolVEInchargeExistByName(SchoolVEInchargeModel schoolVEInchargeModel)
        {
            string errorMessage = string.Empty;

            try
            {
                if (schoolVEInchargeModel.RequestType == RequestType.New)
                {
                    SchoolVEIncharge schoolVEIncharge = this.Context.SchoolVEIncharges.FirstOrDefault(h => h.FullName == schoolVEInchargeModel.FullName);

                    if (schoolVEIncharge != null)
                    {
                        errorMessage = string.Format("School VE Incharge is already exists with {0}", schoolVEIncharge.FullName);
                    }

                    schoolVEIncharge = this.Context.SchoolVEIncharges.FirstOrDefault(h => h.Email == schoolVEInchargeModel.Email);

                    if (schoolVEIncharge != null)
                    {
                        errorMessage = string.Format("School VE Incharge is already exists for {0}", schoolVEIncharge.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckSchoolVEInchargeExistByName", ex);
            }

            return errorMessage;
        }

        /// <summary>}
        /// List of SchoolVEIncharge with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolVEInchargeViewModel> GetSchoolVEInchargesByCriteria(SearchSchoolVEInchargeModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[10];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "hmId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.HMId };
            sqlParams[5] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[6] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[7] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[8] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[9] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SchoolVEInchargeViewModels.FromSql<SchoolVEInchargeViewModel>("CALL GetSchoolVEInchargesByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @hmId, @schoolId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}