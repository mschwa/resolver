using System;
using System.Threading.Tasks;

namespace Resolver.Case.Api.Graph
{
    public interface IGraphService : IDisposable
    {
        Task<string> AddVertex<T>(T input) where T : AbstractVertex;
        Task<string>  AddEdgeAndVertexToCase<T>(string caseId, T input) where T : AbstractVertex;
        void RemoveVertexFromCase(string caseId, string vertexId);
        Task<CaseGraph> GetCaseGraph(string id);
    }
}
