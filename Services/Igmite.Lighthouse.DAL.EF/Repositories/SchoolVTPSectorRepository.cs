using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the SchoolVTPSector entity
    /// </summary>
    public class SchoolVTPSectorRepository : GenericRepository<SchoolVTPSector>, ISchoolVTPSectorRepository
    {
        /// <summary>
        /// Get list of SchoolVTPSector
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolVTPSector> GetSchoolVTPSectors()
        {
            return this.Context.SchoolVTPSectors.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolVTPSector by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolVTPSector> GetSchoolVTPSectorsByName(string name)
        {
            var schoolVTPSectors = (from s in this.Context.SchoolVTPSectors
                                    select s).AsQueryable();

            return schoolVTPSectors;
        }

        /// <summary>
        /// Get list of SchoolVTPSector by VTP & Sector
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public SchoolVTPSector GetSchoolVTPSectorsBy3Ids(Guid academicYearId, Guid schoolId, Guid vtpId, Guid sectorId)
        {
            var schoolVTPSectors = this.Context.SchoolVTPSectors.FirstOrDefault(s => s.AcademicYearId == academicYearId && s.SchoolId == schoolId && s.VTPId == vtpId && s.SectorId == sectorId && s.IsActive == true);

            return schoolVTPSectors;
        }

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        public SchoolVTPSector GetSchoolVTPSectorById(Guid schoolVTPSectorId)
        {
            return this.Context.SchoolVTPSectors.FirstOrDefault(s => s.SchoolVTPSectorId == schoolVTPSectorId);
        }

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId using async
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        public async Task<SchoolVTPSector> GetSchoolVTPSectorByIdAsync(Guid schoolVTPSectorId)
        {
            var schoolVTPSector = await (from s in this.Context.SchoolVTPSectors
                                         where s.SchoolVTPSectorId == schoolVTPSectorId
                                         select s).FirstOrDefaultAsync();

            return schoolVTPSector;
        }

        /// <summary>
        /// Insert/Update SchoolVTPSector entity
        /// </summary>
        /// <param name="schoolVTPSector"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSchoolVTPSectorDetails(SchoolVTPSector schoolVTPSector)
        {
            try
            {
                if (RequestType.New == schoolVTPSector.RequestType)
                    Context.SchoolVTPSectors.Add(schoolVTPSector);
                else
                {
                    Context.Entry<SchoolVTPSector>(schoolVTPSector).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateSchoolVTPSectorDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid schoolVTPSectorId)
        {
            SchoolVTPSector schoolVTPSector = this.Context.SchoolVTPSectors.FirstOrDefault(s => s.SchoolVTPSectorId == schoolVTPSectorId);

            if (schoolVTPSector != null)
            {
                Context.Entry<SchoolVTPSector>(schoolVTPSector).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate SchoolVTPSector by Name
        /// </summary>
        /// <param name="schoolVTPSector"></param>
        /// <param name="schoolVTPSectorModel"></param>
        /// <returns></returns>
        public string CheckSchoolVTPSectorExistByName(SchoolVTPSector schoolVTPSector, SchoolVTPSectorModel schoolVTPSectorModel)
        {
            if (schoolVTPSectorModel.RequestType == RequestType.New)
            {
                SchoolVTPSector schoolVTPSectorItem = this.Context.SchoolVTPSectors.FirstOrDefault(s => s.AcademicYearId == schoolVTPSectorModel.AcademicYearId && s.SchoolId == schoolVTPSectorModel.SchoolId && s.VTPId == schoolVTPSectorModel.VTPId && s.SectorId == schoolVTPSectorModel.SectorId);

                if (schoolVTPSectorItem != null)
                {
                    return string.Format("School VTP Sector is already exists");
                }
            }
            else if (schoolVTPSectorModel.RequestType == RequestType.Edit)
            {
                // Get list of all asigned VC School Sectors against School VTP Sector
                List<VCSchoolSector> vcSchoolSectors = this.Context.VCSchoolSectors.Where(s => s.AcademicYearId == schoolVTPSectorModel.AcademicYearId && s.SchoolVTPSectorId == schoolVTPSector.SchoolVTPSectorId && s.IsActive == true).ToList();

                if (vcSchoolSectors.Count > 0 && (!Guid.Equals(schoolVTPSector.SchoolId, schoolVTPSectorModel.SchoolId) || !Guid.Equals(schoolVTPSector.VTPId, schoolVTPSectorModel.VTPId) || !Guid.Equals(schoolVTPSector.SectorId, schoolVTPSectorModel.SectorId)))
                {
                    return string.Format("School VTP Sector cannot be changed beacause VC School Sectors are already mapped with this SchoolVTPSector.");
                }
            }

            return string.Empty;
        }

        /// <summary>}
        /// List of SchoolVTPSector with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolVTPSectorViewModel> GetSchoolVTPSectorsByCriteria(SearchSchoolVTPSectorModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[10];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[3] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[4] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[5] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[6] = new MySqlParameter { ParameterName = "isRollover", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.IsRollover };
            sqlParams[7] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[8] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[9] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.SchoolVTPSectorViewModels.FromSql<SchoolVTPSectorViewModel>("CALL GetSchoolVTPSectorsByCriteria (@academicYearId, @vtpId, @sectorId, @schoolId, @status, @name, @isRollover, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}