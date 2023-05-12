using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the ToolEquipment entity
    /// </summary>
    public interface IToolEquipmentRepository : IGenericRepository<ToolEquipment>
    {
        /// <summary>
        /// Get list of ToolEquipment
        /// </summary>
        /// <returns></returns>
        IQueryable<ToolEquipment> GetToolEquipments();

        /// <summary>
        /// Get list of ToolEquipment by toolEquipmentName
        /// </summary>
        /// <param name="toolEquipmentName"></param>
        /// <returns></returns>
        IQueryable<ToolEquipment> GetToolEquipmentsByName(string toolEquipmentName);

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        ToolEquipment GetToolEquipmentById(Guid toolEquipmentId);

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId using async
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        Task<ToolEquipment> GetToolEquipmentByIdAsync(Guid toolEquipmentId);

        /// <summary>
        /// Get RoomDamaged by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        IList<string> GetRoomDamagedById(Guid toolEquipmentId);

        /// <summary>
        /// Get ToolList by ToolEquipmentId
        /// </summary>
        /// <param name="ToolEquipmentId"></param>
        /// <returns></returns>
        IList<TEToolListModel> GetToolListById(Guid toolEquipmentId);

        /// <summary>
        /// Get Rawmaterial by ToolEquipmentId
        /// </summary>
        /// <param name="ToolEquipmentId"></param>
        /// <returns></returns>
        IList<TEMaterialListModel> GetMaterialListById(Guid toolEquipmentId);
        
        /// <summary>
        /// Insert/Update ToolEquipment entity
        /// </summary>
        /// <param name="toolEquipment"></param>
        /// <returns></returns>
        bool SaveOrUpdateToolEquipmentDetails(ToolEquipment toolEquipment, ToolEquipmentModel toolEquipmentModel);

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
        /// VT School Sector is not assign for this VT
        /// </summary>
        /// <param name="toolEquipmentModel"></param>
        /// <returns></returns>
        bool CheckVTSchoolSectorExistByVTId(ToolEquipmentModel toolEquipmentModel);

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
        IQueryable<TEAndRMList> GetTEAndRMList();
    }
}