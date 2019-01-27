using AutoMapper;
using Resolver.Case.Api.Documents;
using Resolver.Case.Api.Graph;
using Resolver.Case.Api.Models;
using Resolver.Case.Api.Snapshot;

namespace Resolver.Case.Api.Infrastructure.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Insurer, InsurerVertex>();
            CreateMap<InsurerVertex, Insurer>();
            CreateMap<Insurer, InsurerDocument>();
            CreateMap<InsurerDocument, Insurer>();

            CreateMap<Claimant, ClaimantVertex>();
            CreateMap<ClaimantVertex, Claimant>();
            CreateMap<Claimant, ClaimantDocument>();
            CreateMap<ClaimantDocument, Claimant>();

            CreateMap<Models.Case, CaseVertex>();
            CreateMap<CaseVertex, Models.Case>();

            CreateMap<Models.Case, CaseSnapshot>();
            CreateMap<CaseSnapshot, Models.Case>();
        }
    }
}
