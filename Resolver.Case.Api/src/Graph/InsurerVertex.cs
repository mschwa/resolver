namespace Resolver.Case.Api.Graph
{
    public class InsurerVertex : AbstractVertex
    {
        public string CompanyName { get; set; }
        public override string Name => $"{CompanyName}";

        public override string ToGremlinForAddingEdgeandVertexToCase(string caseId)
        {
            var qry = $"g.V('{caseId.ToLower()}')" +
                      $".addE('insurer')" +
                      $".to({ToGremlinForAddingVertexToGraph()})";

            return qry;
        }

        public override string ToGremlinForAddingVertexToGraph()
        {
            var qry = $"g.V()" +
                      $".addV('contact')" +
                      $".property('sourceid', '{SourceId}')" +
                      $".property('name', '{CompanyName}')" +
                      $".property('companyname', '{CompanyName}')";

            return qry;
        }
    }
}
