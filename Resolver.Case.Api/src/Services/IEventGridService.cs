using System.Threading.Tasks;

namespace Resolver.Case.Api.Services
{
    public interface IEventGridService
    {
        Task SendEventAsync(string eventType, string entityId, object input);
    }
}