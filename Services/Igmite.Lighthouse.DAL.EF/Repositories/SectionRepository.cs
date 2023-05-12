using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Section entity
    /// </summary>
    public class SectionRepository : GenericRepository<Section>, ISectionRepository
    {
        /// <summary>
        /// Get list of Section
        /// </summary>
        /// <returns></returns>
        public IQueryable<Section> GetSections()
        {
            return this.Context.Sections.AsQueryable();
        }

        /// <summary>
        /// Get list of Section by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Section> GetSectionsByName(string name)
        {
            var sections = (from s in this.Context.Sections
                         where s.Name.Contains(name)
                         select s).AsQueryable();

            return sections;
        }

        /// <summary>
        /// Get Section by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public Section GetSectionById(Guid sectionId)
        {
            return this.Context.Sections.FirstOrDefault(s => s.SectionId == sectionId);
        }

        /// <summary>
        /// Get Section by SectionId using async
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public async Task<Section> GetSectionByIdAsync(Guid sectionId)
        {
            var section = await (from s in this.Context.Sections
                              where s.SectionId == sectionId
                              select s).FirstOrDefaultAsync();

            return section;
        }

        /// <summary>
        /// Insert/Update Section entity
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSectionDetails(Section section)
        {
            if (RequestType.New == section.RequestType)
                Context.Sections.Add(section);
            else
            {
                Context.Entry<Section>(section).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid sectionId)
        {
            Section section = this.Context.Sections.FirstOrDefault(s => s.SectionId == sectionId);

            if (section != null)
            {
                Context.Entry<Section>(section).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Section by Name
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <returns></returns>
        public bool CheckSectionExistByName(SectionModel sectionModel)
        {
            Section section = this.Context.Sections.FirstOrDefault(s => s.Name == sectionModel.Name);

            return section != null;
        }

        /// <summary>}
        /// List of Section with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SectionViewModel> GetSectionsByCriteria(SearchSectionModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SectionViewModels.FromSql<SectionViewModel>("CALL GetSectionsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
