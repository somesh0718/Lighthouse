using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Business logic of the Generic Manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericManager<T> : IGenericManager<T> where T : class
    {
        private IGenericRepository<T> repository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the Generic Manager class.
        /// </summary>
        public GenericManager()
        {
        }

        /// <summary>
        /// Initializes the Generic Manager class.
        /// </summary>
        public GenericManager(IHttpContextAccessor _httpContextAccessor)
        {
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Initializes the Generic Manager class with Repository parameter .
        /// </summary>
        /// <param name="_repository"></param>
        public GenericManager(IGenericRepository<T> _repository)
        {
            this.repository = _repository;
        }

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        public void Add(T entity)
        {
            repository.Add(entity);
        }

        /// <summary>
        /// Edit entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to edit</param>
        /// <returns>The edit entity</returns>
        public void Edit(T entity)
        {
            repository.Edit(entity);
        }

        /// <summary>
        /// Delete entity within the the repository
        /// </summary>
        /// <param name="entity">the entity to delete</param>
        /// <returns>The delete entity</returns>
        public void Delete(T entity)
        {
            repository.Delete(entity);
        }

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public T Save(T entity)
        {
            repository.Save(entity);
            return entity;
        }

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = this.repository.GetAll();

            return query;
        }

        /// <summary>
        /// Get all the element of this repository by condition
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = this.repository.FindBy(predicate);

            return query;
        }

        /// <summary>
        /// Check entity already exists
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Exists(Func<T, bool> predicate)
        {
            return this.repository.Exists(predicate);
        }

        public static string GetUserId(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = tokenHandler.ReadToken(authToken);
            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)securityToken;

            return authToken;
        }

        public StringBuilder GetNewUserCreationTemplate(Account account)
        {
            StringBuilder sbNewUserTemplate = new StringBuilder();
            sbNewUserTemplate.AppendFormat("<p>Hi {0},</p>\n", account.UserName);
            sbNewUserTemplate.AppendFormat("<p>Welcome to Lighthouse {0}. Please find your login details below.</p>\n", Constants.StateCode);
            sbNewUserTemplate.AppendFormat("<p><b>Username:</b> {0}</p>\n", account.LoginId);
            sbNewUserTemplate.AppendFormat("<p><b>Password:</b> {0}</p>\n", Constants.DefaultAppPwd);
            sbNewUserTemplate.AppendFormat("<p><b>Click here: </b><a href=\"https://gujarat.lighthouse.net.in\">To Start Using Web application now</a></p>\n");
            sbNewUserTemplate.AppendLine("<p>Thanks,</p>");
            sbNewUserTemplate.AppendLine("<p>Lighthouse Team</p>");

            return sbNewUserTemplate;
        }

        public StringBuilder GetNewComplaintRegistrationTemplate(ComplaintRegistrationModel complaintRegistrationModel)
        {
            StringBuilder sbNewUserTemplate = new StringBuilder();
            sbNewUserTemplate.AppendFormat("<p>Hi {0},</p>\n", complaintRegistrationModel.UserName);
            sbNewUserTemplate.AppendFormat("<p>{0} has registered new complaint.</p>\n", complaintRegistrationModel.EmailId);

            sbNewUserTemplate.AppendFormat("<p><b>User Type:</b> {0}</p>\n", complaintRegistrationModel.UserType);
            sbNewUserTemplate.AppendFormat("<p><b>Subject:</b> {0}</p>\n", complaintRegistrationModel.Subject);
            sbNewUserTemplate.AppendFormat("<p><b>IssueDetails:</b> {0}</p>\n", complaintRegistrationModel.IssueDetails);

            sbNewUserTemplate.AppendLine("<p>Thanks,</p>");
            sbNewUserTemplate.AppendLine("<p>Lighthouse Team</p>");

            return sbNewUserTemplate;
        }

        public StringBuilder GetConductingGuestLectureTemplate(VocationalTrainer vocationalTrainer, VTGuestLectureConducted guestLecture)
        {
            StringBuilder sbNewUserTemplate = new StringBuilder();
            sbNewUserTemplate.AppendFormat("<p>Hi {0},</p>\n", guestLecture.GLName);

            sbNewUserTemplate.AppendFormat("<p>Thank you to conducting the Guest Lecture with a feedback form for recording their observation.</p>\n");
            sbNewUserTemplate.AppendFormat("<p>VT Name: {0}</p>\n", vocationalTrainer.FullName);
            sbNewUserTemplate.AppendFormat("<p>GL Conducted Date: {0}</p>\n", guestLecture.ReportingDate.ToString("dd/MM/yyyy hh:mm tt"));
            sbNewUserTemplate.AppendFormat("<p>GL Topic: {0}</p>\n", guestLecture.GLTopic);
            sbNewUserTemplate.AppendFormat("<p>GL Address: {0}</p>\n", guestLecture.GLAddress);

            sbNewUserTemplate.AppendLine("<p>Thanks,</p>");
            sbNewUserTemplate.AppendLine("<p>Lighthouse Team</p>");

            return sbNewUserTemplate;
        }

        public StringBuilder GetConductingFieldIndustryVisitTemplate(VocationalTrainer vocationalTrainer, VTFieldIndustryVisitConducted fieldVisit)
        {
            StringBuilder sbNewUserTemplate = new StringBuilder();
            sbNewUserTemplate.AppendFormat("<p>Hi {0},</p>\n", vocationalTrainer.FullName);

            sbNewUserTemplate.AppendFormat("<p>Thank you to conducting the Field Industry Visit with a feedback form for recording their observation.</p>\n");
            sbNewUserTemplate.AppendFormat("<p>VT Name: {0}</p>\n", vocationalTrainer.FullName);
            sbNewUserTemplate.AppendFormat("<p>FV Conducted Date: {0}</p>\n", fieldVisit.ReportingDate.ToString("dd/MM/yyyy hh:mm tt"));
            sbNewUserTemplate.AppendFormat("<p>FV Theme: {0}</p>\n", fieldVisit.FieldVisitTheme);
            sbNewUserTemplate.AppendFormat("<p>FV Activities: {0}</p>\n", fieldVisit.FieldVisitActivities);
            sbNewUserTemplate.AppendFormat("<p>FV Address: {0}</p>\n", fieldVisit.FVOrganisationAddress);

            sbNewUserTemplate.AppendLine("<p>Thanks,</p>");
            sbNewUserTemplate.AppendLine("<p>Lighthouse Team</p>");

            return sbNewUserTemplate;
        }
    }
}