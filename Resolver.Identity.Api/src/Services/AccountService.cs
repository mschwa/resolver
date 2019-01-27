using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Resolver.Identity.Api.Models;

namespace Resolver.Identity.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _userManager.UserValidators.Clear();
        }

        public async Task<ApplicationUser> GetAccount(string username)
        {
            return await _userManager.FindByEmailAsync(username);
        }

        public async Task<bool> AccountExists(string username)
        {
            return !(await _userManager.FindByEmailAsync(username) == null);
        }

        public async Task<IdentityResult> CreateAccount(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);

        }

        public async Task<IdentityResult> AddToRole(ApplicationUser user, IdentityRole role)
        {
            return await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task<IdentityResult> AddClaim(ApplicationUser user, Claim claim)
        {
            return await _userManager.AddClaimAsync(user, claim);
        }

        public async Task<ApplicationUser> GetAccountById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
