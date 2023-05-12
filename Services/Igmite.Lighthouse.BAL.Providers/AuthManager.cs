using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.Cryptography;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.EmailServices;
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
public class AuthManager : GenericManager<AccountModel>, IAuthManager
    {
        private readonly IAccountRepository accountRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the account manager.
        /// </summary>
        /// <param name="accountRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public AuthManager(IAccountRepository _accountRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.accountRepository = _accountRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get Account by LoginId using async
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public async Task<AccountModel> GetAccountByLoginIdAsync(string loginId)
        {
            Account account = await this.accountRepository.GetAccountByLoginIdAsync(loginId);

            return (account != null) ? account.ToModel() : null;
        }
    }
}
