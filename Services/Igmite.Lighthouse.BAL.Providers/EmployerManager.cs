using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the Employer entity
    /// </summary>
    public class EmployerManager : GenericManager<EmployerModel>, IEmployerManager
    {
        private readonly IEmployerRepository employerRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the employer manager.
        /// </summary>
        /// <param name="employerRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public EmployerManager(IEmployerRepository _employerRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.employerRepository = _employerRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Employers
        /// </summary>
        /// <returns></returns>
        public IQueryable<EmployerModel> GetEmployers()
        {
            var employers = this.employerRepository.GetEmployers();

            IList<EmployerModel> employerModels = new List<EmployerModel>();
            employers.ForEach((user) => employerModels.Add(user.ToModel()));

            return employerModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Employers by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<EmployerModel> GetEmployersByName(string employerName)
        {
            var employers = this.employerRepository.GetEmployersByName(employerName);

            IList<EmployerModel> employerModels = new List<EmployerModel>();
            employers.ForEach((user) => employerModels.Add(user.ToModel()));

            return employerModels.AsQueryable();
        }

        /// <summary>
        /// Get Employer by EmployerId
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public EmployerModel GetEmployerById(Guid employerId)
        {
            Employer employer = this.employerRepository.GetEmployerById(employerId);

            return (employer != null) ? employer.ToModel() : null;
        }

        /// <summary>
        /// Get Employer by EmployerId using async
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public async Task<EmployerModel> GetEmployerByIdAsync(Guid employerId)
        {
            var employer = await this.employerRepository.GetEmployerByIdAsync(employerId);

            return (employer != null) ? employer.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Employer entity
        /// </summary>
        /// <param name="employerModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateEmployerDetails(EmployerModel employerModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Employer employer = null;

            //Validate model data
            employerModel = employerModel.GetModelValidationErrors<EmployerModel>();

            if (employerModel.ErrorMessages.Count > 0)
            {
                response.Errors = employerModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (employerModel.RequestType == RequestType.Edit)
            {
                employer = this.employerRepository.GetEmployerById(employerModel.EmployerId);
            }
            else
            {
                employer = new Employer();
                employerModel.EmployerId = Guid.NewGuid();
            }

            if (employerModel.ErrorMessages.Count == 0 && !(string.Equals(employerModel.StateCode.ToLower(), employer.StateCode.StringVal().ToLower()) && Guid.Equals(employerModel.DivisionId, employer.DivisionId) && string.Equals(employerModel.DistrictCode.ToLower(), employer.DistrictCode.StringVal().ToLower()) && string.Equals(employerModel.BusinessType.ToLower(), employer.BusinessType.StringVal().ToLower())))
            {
                bool isEmployerExists = this.employerRepository.CheckEmployerExistByName(employerModel);

                if (isEmployerExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                employer.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                employer = employerModel.FromModel(employer);

                //Save Or Update employer details
                bool isSaved = this.employerRepository.SaveOrUpdateEmployerDetails(employer);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by EmployerId
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid employerId)
        {
            return this.employerRepository.DeleteById(employerId);
        }

        /// <summary>
        /// Check duplicate Employer by Name
        /// </summary>
        /// <param name="employerModel"></param>
        /// <returns></returns>
        public bool CheckEmployerExistByName(EmployerModel employerModel)
        {
            return this.employerRepository.CheckEmployerExistByName(employerModel);
        }

        /// <summary>}
        /// List of Employer with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<EmployerViewModel> GetEmployersByCriteria(SearchEmployerModel searchModel)
        {
            return this.employerRepository.GetEmployersByCriteria(searchModel);
        }
    }
}