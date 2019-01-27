using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resolver.Identity.Api.Models;
using Resolver.Identity.Api.Models.User;

namespace Resolver.Identity.Api.Infrastructure.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, SimpleUser>();
        }
    }
}
