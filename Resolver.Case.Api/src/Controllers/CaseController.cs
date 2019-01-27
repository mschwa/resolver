using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Resolver.Case.Api.Documents;
using Resolver.Case.Api.Services;
using Resolver.Case.Api.Models;
using Resolver.Case.Api.Graph;

namespace Resolver.Case.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly IGraphService _graphService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CaseController(IGraphService graphService, IConfiguration configuration, IMapper mapper)
        {
            _graphService = graphService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddCase(Models.Case model)
        {
            var vertex = _mapper.Map<CaseVertex>(model);
            vertex.Id = await _graphService.AddVertex(vertex);

            var eventGrid = new EventGridService();
            await eventGrid.SendEventAsync("AddCase", vertex.Id, vertex);

            return CreatedAtRoute("GetCase", new { caseid = vertex.Id }, model);
        }

        [HttpPost("{caseid}/claimant")]
        public async Task<IActionResult> AddClaimantToCase(string caseId, Claimant model)
        {
            var document = _mapper.Map<ClaimantDocument>(model);

            var contactRepo = new ContactRepository<ClaimantDocument>();
            var item = await contactRepo.CreateItemAsync(document);

            var vertex = _mapper.Map<ClaimantVertex>(model);
            vertex.SourceId = item.Id.ToString();
            
            var graphId = await _graphService.AddEdgeAndVertexToCase(caseId, vertex);

            var eventGrid = new EventGridService();
            await eventGrid.SendEventAsync("AddClaimant", item.ResourceId, model);
            await eventGrid.SendEventAsync("AssignClaimantToCase", caseId, new { caseId, claimantId = item.Id});

            return Ok();
        }

        [HttpPost("{caseid}/insurer")]
        public async Task<IActionResult> AddInsurerToCase(string caseId, Insurer model)
        {
            var document = _mapper.Map<InsurerDocument>(model);

            var contactRepo = new ContactRepository<InsurerDocument>();
            var item = await contactRepo.CreateItemAsync(document);

            var vertex = _mapper.Map<InsurerVertex>(model);
            vertex.SourceId = item.ResourceId.ToString();

            var graphId = await _graphService.AddEdgeAndVertexToCase(caseId, vertex);

            var eventGrid = new EventGridService();
            await eventGrid.SendEventAsync("AddInsurer", item.ResourceId, model);
            await eventGrid.SendEventAsync("AssignInsurerToCase", caseId, new { caseId, claimantId = item.Id });

            return Ok();
        }
        
        [HttpGet("{caseid}", Name = "GetCase")]
        public async Task<IActionResult> GetCaseFromDataSources(string caseId)
        {
            var caseGraph = await _graphService.GetCaseGraph(caseId);

            // READ BEFORE YOU START AGAIN -HAD TO ADD LOWERCASE ID TO MODEL TO QUERY DOCDB. NEED TO SEPERATE MODELS MOVING FORWARD
                        
            var claimantDocument =
                (await new ContactRepository<ClaimantDocument>().GetItemsAsync(d => d.id == caseGraph.Claimants[0])).FirstOrDefault();
            
            var insurerDocument =
                (await new ContactRepository<InsurerDocument>().GetItemsAsync(d => d.id == caseGraph.Insurers[0])).FirstOrDefault();

            var xCase = new Models.Case
            {
                DisplayName = caseGraph.Name,
                Claimant = claimantDocument == null ? null : _mapper.Map<Claimant>(claimantDocument),
                Insurer = insurerDocument == null ? null : _mapper.Map<Insurer>(insurerDocument),
            };

            return Ok(xCase);
        }

        //[HttpGet("{caseid}/fromevents")]
        //public async Task<IActionResult> BuildCaseFromEventQueue(string caseId)
        //{
        //    return Ok();
        //}

        //[HttpGet("{caseid}/fromsnapshot/{snapShotId}")]
        //public async Task<IActionResult> BuildCaseFromSnapshot(string caseId, long snapShotId)
        //{
        //    return Ok();
        //}


    }
}