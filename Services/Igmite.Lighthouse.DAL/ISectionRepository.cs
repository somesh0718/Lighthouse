using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Section entity
    /// </summary>
    public interface ISectionRepository : IGenericRepository<Section>
    {
        /// <summary>
        /// Get list of Section
        /// </summary>
        /// <returns></returns>
        IQueryable<Section> GetSections();

        /// <summary>
        /// Get list of Section by sectionName
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        IQueryable<Section> GetSectionsByName(string sectionName);

        /// <summary>
        /// Get Section by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Section GetSectionById(Guid sectionId);

        /// <summary>
        /// Get Section by SectionId using async
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task<Section> GetSectionByIdAsync(Guid sectionId);

        /// <summary>
        /// Insert/Update Section entity
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        bool SaveOrUpdateSectionDetails(Section section);

        /// <summary>
        /// Delete a record by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        bool DeleteById(Guid sectionId);

        /// <summary>
        /// Check duplicate Section by Name
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <returns></returns>
        bool CheckSectionExistByName(SectionModel sectionModel);

        /// <summary>
        /// List of Section with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SectionViewModel> GetSectionsByCriteria(SearchSectionModel searchModel);
    }
}
