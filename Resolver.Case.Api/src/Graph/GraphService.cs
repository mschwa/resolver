using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;

namespace Resolver.Case.Api.Graph
{
    public class GraphService : IGraphService
    {
        // Azure Cosmos DB Configuration variables
        // Replace the values in these variables to your own.
        private static string hostname = "dev-resolver-case.gremlin.cosmosdb.azure.com";
        private static int port = 443;
        private static string authKey = "8PApLOVE9v4PD781SJQILDO0bqLLgsJaxFZU8ftZNOSvSKuN93xsCDb5jbajJDhtoen6lQ4Wf7gsuv9cTxkhJw==";
        private static string database = "Case";
        private static string collection = "CaseGraph";
        
        public async Task<string> AddVertex<T>(T input) where T : AbstractVertex
        {
            using (var gremlinClient = GetClient())
            {
                // Create async task to execute the Gremlin query.
                var result = await SubmitRequestAsync(gremlinClient, input.ToGremlinForAddingVertexToGraph());
                
                var qry = from item in (Dictionary<string, object>)result.ToImmutableArray()[0]
                    where item.Key.ToLower() == "id"
                    select item.Value;

                var id = Convert.ToString(qry.FirstOrDefault());
                
                return Task.FromResult(id).GetAwaiter().GetResult();
            }
        }

        public void RemoveVertexFromCase(string caseId, string vertexId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddEdgeAndVertexToCase<T>(string caseId, T input) where T : AbstractVertex
        {
            using (var gremlinClient = GetClient())
            {
                // Create async task to execute the Gremlin query.
                var result = await SubmitRequestAsync(gremlinClient, input.ToGremlinForAddingEdgeandVertexToCase(caseId));

                var qry = from item in (Dictionary<string, object>)result.ToImmutableArray()[0]
                    where item.Key.ToLower() == "id"
                    select item.Value;

                var id = Convert.ToString(qry.FirstOrDefault());

                return Task.FromResult(id).GetAwaiter().GetResult();
            }
        }

        public async Task<CaseGraph> GetCaseGraph(string id)
        {
            using (var gremlinClient = GetClient())
            {
                var builder = new StringBuilder($"g.V('{id.ToLower()}')" +
                                                $".project('name', 'claimants', 'insurers')" +
                                                $".by('name')" +
                                                $".by(out('claimant').values('sourceid').fold())" +
                                                $".by(out('insurer').values('sourceid').fold())");

                // Create async task to execute the Gremlin query.
                var result = await SubmitRequestAsync(gremlinClient, builder.ToString());

                var json = JsonConvert.SerializeObject(result.ToImmutableArray()[0]);

                return JsonConvert.DeserializeObject<CaseGraph>(json);
            }
        }

        private GremlinClient GetClient()
        {
            var gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                username: "/dbs/" + database + "/colls/" + collection,
                password: authKey);

            return new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), 
                GremlinClient.GraphSON2MimeType);
        }

        private async Task<ResultSet<dynamic>> SubmitRequestAsync(GremlinClient gremlinClient, string query)
        {
            try
            {
                return await gremlinClient.SubmitAsync<dynamic>(query);
            }
            catch (ResponseException e)
            {
                System.Diagnostics.Trace.TraceError(e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
