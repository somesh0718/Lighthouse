using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the ToolEquipment entity
    /// </summary>
    public interface IToolEquipmentManager : IGenericManager<ToolEquipmentModel>
    {
        /// <summary>
        /// Get list of ToolEquipments
        /// </summary>
        /// <returns></returns>
        IQueryable<ToolEquipmentModel> GetToolEquipments();

        /// <summary>
        /// Get list of ToolEquipments by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<ToolEquipmentModel> GetToolEquipmentsByName(string toolEquipmentName);

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        ToolEquipmentModel GetToolEquipmentById(Guid toolEquipmentId);

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId using async
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        Task<ToolEquipmentModel> GetToolEquipmentByIdAsync(Guid toolEquipmentId);

        /// <summary>
        /// Insert/Update ToolEquipment entity
        /// </summary>
        /// <param name="toolEquipmentModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateToolEquipmentDetails(ToolEquipmentModel toolEquipmentModel);

        /// <summary>
        /// Delete a record by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid toolEquipmentId);

        /// <summary>
        /// Check duplicate ToolEquipment by Name
        /// </summary>
        /// <param name="toolEquipmentModel"></param>
        /// <returns></returns>
        bool CheckToolEquipmentExistByName(ToolEquipmentModel toolEquipmentModel);

        /// <summary>
        /// List of ToolEquipment with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<ToolEquipmentViewModel> GetToolEquipmentsByCriteria(SearchToolEquipmentModel searchModel);

        /// <summary>
        /// Get list of TEAndRMList
        /// </summary>
        /// <returns></returns>
        IQueryable<TEAndRMListModel> GetTEAndRMList();
    }
}