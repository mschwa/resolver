using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paramore.Brighter;

namespace Resolver.Case.Api.Services
{
    public class EventGridService : IEventGridService
    {
        public async Task SendEventAsync(string eventType, string entityId, object input)
        {
            // TODO: Enter values for <topic-name> and <region>
            string topicEndpoint = "https://resolver-case.westus2-1.eventgrid.azure.net/api/events";

            // TODO: Enter value for <topic-key>
            string topicKey = "xnvJ1ROlpyHhfEhfG/p3f2rUbZBRIblSHv+ra26F1s0=";

            string topicHostname = new Uri(topicEndpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);

            var caseEvent = new EventGridEvent()
            {
                Id = entityId,
                EventType = eventType,
                Data = input,
                EventTime = DateTime.Now,
                Subject = entityId,
                DataVersion = "1.0"
            };

            await client.PublishEventsAsync(topicHostname, new[] {caseEvent});
        }
    }
}
