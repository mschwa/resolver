namespace Resolver.Case.Api.Graph
{
    public class ClaimantVertex : AbstractVertex
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string Name => $"{FirstName} {LastName}";

        public override string ToGremlinForAddingEdgeandVertexToCase(string caseId)
        {
            var qry = $"g.V('{caseId.ToLower()}')" +
                      $".addE('claimant')" +
                      $".to({ToGremlinForAddingVertexToGraph()})";

            return qry;
        }

        public override string ToGremlinForAddingVertexToGraph()
        {
            var qry = $"g.V()" +
                      $".addV('contact')" +
                      $".property('sourceid', '{SourceId}')" +
                      $".property('name', '{FirstName} {LastName}')" +
                      $".property('firstname', '{FirstName}')" +
                      $".property('Lastname', '{LastName}')";

            return qry;
        }
    }
}
