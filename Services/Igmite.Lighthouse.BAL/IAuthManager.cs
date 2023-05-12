using Igmite.Lighthouse.Models;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Account entity
    /// </summary>
    public interface IAuthManager : IGenericManager<AccountModel>
    {
        /// <summary>
        /// Get Account by LoginId using async
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        Task<AccountModel> GetAccountByLoginIdAsync(string loginId);
    }
}