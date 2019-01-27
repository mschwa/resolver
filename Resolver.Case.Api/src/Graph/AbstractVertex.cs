using System;
namespace Resolver.Case.Api.Graph
{
    public abstract class AbstractVertex
    {
        public abstract string Name { get; }
        public string SourceId { get; set; }
        public abstract string ToGremlinForAddingEdgeandVertexToCase(string caseId);
        public abstract string ToGremlinForAddingVertexToGraph();
    }
}
