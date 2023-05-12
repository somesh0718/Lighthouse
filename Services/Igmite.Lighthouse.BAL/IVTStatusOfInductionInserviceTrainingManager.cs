using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTStatusOfInductionInserviceTraining entity
    /// </summary>
    public interface IVTStatusOfInductionInserviceTrainingManager : IGenericManager<VTStatusOfInductionInserviceTrainingModel>
    {
        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTrainings
        /// </summary>
        /// <returns></returns>
        IQueryable<VTStatusOfInductionInserviceTrainingModel> GetVTStatusOfInductionInserviceTrainings();

        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTrainings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTStatusOfInductionInserviceTrainingModel> GetVTStatusOfInductionInserviceTrainingsByName(string vtStatusOfInductionInserviceTrainingName);

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        VTStatusOfInductionInserviceTrainingModel GetVTStatusOfInductionInserviceTrainingById(Guid vtStatusOfInductionInserviceTrainingId);

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId using async
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        Task<VTStatusOfInductionInserviceTrainingModel> GetVTStatusOfInductionInserviceTrainingByIdAsync(Guid vtStatusOfInductionInserviceTrainingId);

        /// <summary>
        /// Insert/Update VTStatusOfInductionInserviceTraining entity
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTStatusOfInductionInserviceTrainingDetails(VTStatusOfInductionInserviceTrainingModel vtStatusOfInductionInserviceTrainingModel);

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
