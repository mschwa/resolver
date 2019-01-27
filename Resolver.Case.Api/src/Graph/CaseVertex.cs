using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Resolver.Case.Api.Graph
{
    public class CaseVertex : AbstractVertex
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public override string Name => DisplayName;

        public override string ToGremlinForAddingEdgeandVertexToCase(string caseId)
        {
            throw new NotImplementedException();
        }

        public override string ToGremlinForAddingVertexToGraph()
        {
            return $"g.addV('case').property('name', '{Name}')";
        }
    }
}
