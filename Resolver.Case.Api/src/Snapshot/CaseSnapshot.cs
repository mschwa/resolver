using Microsoft.WindowsAzure.Storage.Table;
using Resolver.Case.Api.Models;

namespace Resolver.Case.Api.Snapshot
{
    public class CaseSnapshot : TableEntity
    {
        public CaseSnapshot(string caseId, string year)
        {
            this.PartitionKey = year;
            this.RowKey = caseId;
        }

        public string DisplayName { get; set; }
        public Insurer Insurer { get; set; }
        public Claimant Claimant { get; set; }
    }
}
