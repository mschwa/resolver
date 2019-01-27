using System.Threading.Tasks;
using Resolver.Case.Api.Snapshot;

namespace Resolver.Case.Api.Services
{
    public interface IStorageRepository
    {
        Task<Models.Case> GetCaseFromStorageQueueAsync();
        Task<CaseSnapshot> GetCaseSnapshotFromStorageTableAsync(string caseId);
        Task<bool> SubmitCaseSnapshotToStorageTableAsync(CaseSnapshot caseSnapshot);
        Task<bool> ReplaceCaseSnapshotToStorageTableAsync(CaseSnapshot caseSnapshot);
    }
}