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
    /// Repository of the CourseModule entity
    /// </summary>
    public class CourseModuleRepository : GenericRepository<CourseModule>, ICourseModuleRepository
    {
        /// <summary>
        /// Get list of CourseModule
        /// </summary>
        /// <returns></returns>
        public IQueryable<CourseModule> GetCourseModules()
        {
            return this.Context.CourseModules.AsQueryable();
        }

        /// <summary>
        /// Get list of CourseModule by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<CourseModule> GetCourseModulesByName(string name)
        {
            var courseModules = (from d in this.Context.CourseModules
                                 where d.UnitName.Contains(name)
                                 select d).AsQueryable();

            return courseModules;
        }

        /// <summary>
        /// Get CourseModule by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public CourseModule GetCourseModuleById(Guid courseModuleId)
        {
            return this.Context.CourseModules.FirstOrDefault(d => d.CourseModuleId == courseModuleId);
        }

        /// <summary>
        /// Get CourseModule by CourseModuleId using async
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public async Task<CourseModule> GetCourseModuleByIdAsync(Guid courseModuleId)
        {
            var courseModule = await (from d in this.Context.CourseModules
                                      where d.CourseModuleId == courseModuleId
                                      select d).FirstOrDefaultAsync();

            return courseModule;
        }

        /// <summary>
        /// Insert/Update CourseModule entity
        /// </summary>
        /// <param name="courseModule"></param>
        /// <returns></returns>
        public bool SaveOrUpdateCourseModuleDetails(CourseModule courseModule, IList<UnitSessionModel> unitSessionModels)
        {
            if (RequestType.New == courseModule.RequestType)
                Context.CourseModules.Add(courseModule);
            else
            {
                Context.Entry<CourseModule>(courseModule).State = EntityState.Modified;
            }

            Context.SaveChanges();

            if (unitSessionModels != null && unitSessionModels.Count > 0)
            {
                IList<CourseUnitSession> unitSessions = Context.CourseUnitSessions.Where(v => v.CourseModuleId == courseModule.CourseModuleId).ToList();

                unitSessions.ForEach((unitSessionItem) =>
                {
                    var unitSessionModel = unitSessionModels.FirstOrDefault(s => s.SessionName == unitSessionItem.SessionName);
                    if (unitSessionModel == null)
                    {
                        Context.Entry<CourseUnitSession>(unitSessionItem).State = EntityState.Deleted;
                    }
                });

                foreach (UnitSessionModel sessionItem in unitSessionModels)
                {
                    var unitSessionItem = unitSessions.FirstOrDefault(s => s.SessionName == sessionItem.SessionName);
                    if (unitSessionItem == null)
                    {
                        Context.CourseUnitSessions.Add(new CourseUnitSession
                        {
                            CourseUnitSessionId = Guid.NewGuid(),
                            CourseModuleId = courseModule.CourseModuleId,
                            SessionName = sessionItem.SessionName,
                            DisplayOrder = sessionItem.DisplayOrder,
                            CreatedBy = courseModule.CreatedBy,
                            CreatedOn = courseModule.CreatedOn,
                            IsActive = true
                        });
                    }
                }
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid courseModuleId)
        {
            CourseModule courseModule = this.Context.CourseModules.FirstOrDefault(d => d.CourseModuleId == courseModuleId);

            if (courseModule != null)
            {
                IList<CourseUnitSession> unitSessions = Context.CourseUnitSessions.Where(v => v.CourseModuleId == courseModule.CourseModuleId).ToList();

                unitSessions.ForEach((unitSessionItem) =>
                {
                    Context.Entry<CourseUnitSession>(unitSessionItem).State = EntityState.Deleted;
                });
                Context.SaveChanges();

                Context.Entry<CourseModule>(courseModule).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate CourseModule by Name
        /// </summary>
        /// <param name="courseModuleModel"></param>
        /// <returns></returns>
        public CourseModule CheckCourseModuleExistByName(CourseModuleModel courseModuleModel)
        {
            CourseModule courseModule = this.Context.CourseModules.FirstOrDefault(d => d.SectorId == courseModuleModel.SectorId && d.JobRoleId == courseModuleModel.JobRoleId && d.ClassId == courseModuleModel.ClassId && d.ModuleTypeId == courseModuleModel.ModuleTypeId && d.UnitName == courseModuleModel.UnitName);

            if (courseModule != null)
            {
                courseModule.CourseUnitSessions = this.Context.CourseUnitSessions.Where(c => c.CourseModuleId == courseModule.CourseModuleId).ToList();
            }

            return courseModule;
        }

        /// <summary>
        /// Get UnitSessions by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public IList<UnitSessionModel> GetUnitSessionsById(Guid courseModuleId)
        {
            return this.Context.CourseUnitSessions.Where(c => c.CourseModuleId == courseModuleId).Select(s => new UnitSessionModel { SessionName = s.SessionName, DisplayOrder = s.DisplayOrder }).OrderBy(x => x.DisplayOrder).ToList();
        }

        /// <summary>}
        /// List of CourseModule with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<CourseModuleViewModel> GetCourseModulesByCriteria(SearchCourseModuleModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.CourseModuleViewModels.FromSql<CourseModuleViewModel>("CALL GetCourseModulesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}