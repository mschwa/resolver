using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Resolver.Case.Api.Services;

namespace Resolver.Case.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventSourceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStorageRepository _storageRepository;

        public EventSourceController(IStorageRepository storageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        [HttpPost("handle")]
        public async Task<IActionResult> HandleEvent(EventData eventData)
        {
            if (eventData.EventType.ToLower() == "addcase")
            {
                var caseBuilder = new CaseBuilder(_storageRepository, _mapper);

                await caseBuilder.ProcessCreateCase(eventData.Subject, eventData.Data);
            }

            return Ok();
        }
    }
}