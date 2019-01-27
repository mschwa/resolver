using System.Collections.Generic;

namespace Resolver.Case.Api.Graph
{
    public class CaseGraph
    {
        public string Name { get; set; }
        public List<string> Insurers { get; set; }
        public List<string> Claimants { get; set; }
    }
}