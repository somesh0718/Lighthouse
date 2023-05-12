using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTStatusOfInductionInserviceTraining entity
    /// </summary>
    public interface IVTStatusOfInductionInserviceTrainingRepository : IGenericRepository<VTStatusOfInductionInserviceTraining>
    {
        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTraining
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStatusOfInductionInserviceTraining> GetVTStatusOfInductionInserviceTrainings();

        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTraining by vtStatusOfInductionInserviceTrainingName
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingName"></param>
        /// <returns></returns>
        IQueryable<VTStatusOfInductionInserviceTraining> GetVTStatusOfInductionInserviceTrainingsByName(string vtStatusOfInductionInserviceTrainingName);

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        VTStatusOfInductionInserviceTraining GetVTStatusOfInductionInserviceTrainingById(Guid vtStatusOfInductionInserviceTrainingId);

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId using async
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        Task<VTStatusOfInductionInserviceTraining> GetVTStatusOfInductionInserviceTrainingByIdAsync(Guid vtStatusOfInductionInserviceTrainingId);

        /// <summary>
        /// Insert/Update VTStatusOfInductionInserviceTraining entity
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTraining"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTStatusOfInductionInserviceTrainingDetails(VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining);

        /// <summary>
        /// Delete a record by VTStatusOfInductionInserviceTrainingId
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtStatusOfInductionInserviceTrainingId);

        /// <summary>
        /// Check duplicate VTStatusOfInductionInserviceTraining by Name
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingModel"></param>
        /// <returns></returns>
        bool CheckVTStatusOfInductionInserviceTrainingExistByName(VTStatusOfInductionInserviceTrainingModel vtStatusOfInductionInserviceTrainingModel);

        /// <summary>
        /// List of VTStatusOfInductionInserviceTraining with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTStatusOfInductionInserviceTrainingViewModel> GetVTStatusOfInductionInserviceTrainingsByCriteria(SearchVTStatusOfInductionInserviceTrainingModel searchModel);
    }
}
