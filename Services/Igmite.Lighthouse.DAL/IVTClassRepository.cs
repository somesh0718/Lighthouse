using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTClass entity
    /// </summary>
    public interface IVTClassRepository : IGenericRepository<VTClass>
    {
        /// <summary>
        /// Get list of VTClass
        /// </summary>
        /// <returns></returns>
        IQueryable<VTClass> GetVTClasses();

        /// <summary>
        /// Get list of VTClass by vtClassName
        /// </summary>
        /// <param name="vtClassName"></param>
        /// <returns></returns>
        IQueryable<VTClass> GetVTClassesByName(string vtClassName);

        /// <summary>
        /// Get VTClass by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        VTClass GetVTClassById(Guid vtClassId);

        /// <summary>
        /// Get VTClass by VTClass
        /// </summary>
        /// <param name="vtClassModel"></param>
        /// <returns></returns>
        VTClass GetVTClassByClass(VTClassModel vtClassModel);

        /// <summary>
        /// Get VTClass by VTClassId using async
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        Task<VTClass> GetVTClassByIdAsync(Guid vtClassId);

        /// <summary>
        /// Insert/Update VTClass entity
        /// </summary>
        /// <param name="vtClass"></param>
        /// <param name="vtClassModel"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTClassDetails(VTClass vtClass, VTClassModel vtClassModel);

        /// <summary>
        /// Delete a record by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtClassId);

        /// <summary>
        /// Check duplicate VTClass by Name
        /// </summary>
        /// <param name="vtClass"></param>
        /// <param name="vtClassModel"></param>
        /// <returns></returns>
        string CheckVTClassExistByName(VTClass vtClass, VTClassModel vtClassModel);

        /// <summary>
        /// Check VT Class can be inactive
        /// </summary>
        /// <param name="vtClass"></param>
        /// <returns></returns>
        bool CheckUserCanInactiveVTClassById(VTClass vtClass);

        /// <summary>
        /// Get SectionIds by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        IList<Guid> GetVTClassSectionsById(Guid vtClassId);

        /// <summary>
        /// List of VTClass with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTClassViewModel> GetVTClassesByCriteria(SearchVTClassModel searchModel);
    }
}