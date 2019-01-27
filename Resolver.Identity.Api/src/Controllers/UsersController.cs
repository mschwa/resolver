using Microsoft.AspNetCore.Mvc;
using Resolver.Identity.Api.Services;
using System.Threading.Tasks;
using AutoMapper;
using Resolver.Identity.Api.Models.User;

namespace Resolver.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        
        public UsersController(IAccountService accountService, IMapper mapper)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var account = await _accountService.GetAccountById(id);
            var user = _mapper.Map<SimpleUser>(account);

            return Ok(user);
        }
    }
}
