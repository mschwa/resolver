using Microsoft.AspNetCore.Identity;
using Resolver.Identity.Api.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Resolver.Identity.Api.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAccount(ApplicationUser user, string password);
        Task<bool> AccountExists(string username);
        Task<ApplicationUser> GetAccount(string username);
        Task<ApplicationUser> GetAccountById(string id);
        Task<IdentityResult> AddToRole(ApplicationUser user, IdentityRole role);
        Task<IdentityResult> AddClaim(ApplicationUser user, Claim claim);
    }
}
