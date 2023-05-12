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
    /// Repository of the UserOTPDetail entity
    /// </summary>
    public class UserOTPDetailRepository : GenericRepository<UserOTPDetail>, IUserOTPDetailRepository
    {
        /// <summary>
        /// Get list of UserOTPDetail
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserOTPDetail> GetUserOTPDetails()
        {
            return this.Context.UserOTPDetails.AsQueryable();
        }

        /// <summary>
        /// Get list of UserOTPDetail by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<UserOTPDetail> GetUserOTPDetailsByName(string name)
        {
            var userOTPDetails = (from u in this.Context.UserOTPDetails
                         select u).AsQueryable();

            return userOTPDetails;
        }

        /// <summary>
        /// Get UserOTPDetail by OTPId
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        public UserOTPDetail GetUserOTPDetailById(Guid otpId)
        {
            return this.Context.UserOTPDetails.FirstOrDefault(u => u.OTPId == otpId);
        }

        /// <summary>
        /// Get UserOTPDetail by OTPId using async
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        public async Task<UserOTPDetail> GetUserOTPDetailByIdAsync(Guid otpId)
        {
            var userOTPDetail = await (from u in this.Context.UserOTPDetails
                              where u.OTPId == otpId
                              select u).FirstOrDefaultAsync();

            return userOTPDetail;
        }

        /// <summary>
        /// Insert/Update UserOTPDetail entity
        /// </summary>
        /// <param name="userOTPDetail"></param>
        /// <returns></returns>
        public bool SaveOrUpdateUserOTPDetailDetails(UserOTPDetail userOTPDetail)
        {
            if (RequestType.New == userOTPDetail.RequestType)
                Context.UserOTPDetails.Add(userOTPDetail);
            else
            {
                Context.Entry<UserOTPDetail>(userOTPDetail).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by OTPId
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid otpId)
        {
            UserOTPDetail userOTPDetail = this.Context.UserOTPDetails.FirstOrDefault(u => u.OTPId == otpId);

            if (userOTPDetail != null)
            {
                Context.Entry<UserOTPDetail>(userOTPDetail).State = EntityState.Deleted;

                //IList<Guid> accountOTPIds = userOTPDetail.AccountOTPs.Select(u => u.AccountOTPId).ToList();
                //foreach (Guid accountOTPId in accountOTPIds)
                //{
                //    AccountUserOTP accountOTP = userOTPDetail.AccountOTPs.FirstOrDefault(u => u.AccountOTPId == accountOTPId);
                //    Context.Entry<AccountUserOTP>(accountOTP).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate UserOTPDetail by Name
        /// </summary>
        /// <param name="userOTPDetailModel"></param>
        /// <returns></returns>
        public bool CheckUserOTPDetailExistByName(UserOTPDetailModel userOTPDetailModel)
        {
            UserOTPDetail userOTPDetail = this.Context.UserOTPDetails.FirstOrDefault(u => u.IsRedeemed == userOTPDetailModel.IsRedeemed);

            return userOTPDetail != null;
        }

        /// <summary>}
        /// List of UserOTPDetail with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<UserOTPDetailViewModel> GetUserOTPDetailsByCriteria(SearchUserOTPDetailModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.UserOTPDetailViewModels.FromSql<UserOTPDetailViewModel>("CALL GetUserOTPDetailsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
