using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Section entity
    /// </summary>
    public interface ISectionManager : IGenericManager<SectionModel>
    {
        /// <summary>
        /// Get list of Sections
        /// </summary>
        /// <returns></returns>
        IQueryable<SectionModel> GetSections();

        /// <summary>
        /// Get list of Sections by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SectionModel> GetSectionsByName(string sectionName);

        /// <summary>
        /// Get Section by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        SectionModel GetSectionById(Guid sectionId);

        /// <summary>
        /// Get Section by SectionId using async
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task<SectionModel> GetSectionByIdAsync(Guid sectionId);

        /// <summary>
        /// Insert/Update Section entity
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSectionDetails(SectionModel sectionModel);

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
