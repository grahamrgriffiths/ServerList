using Core.Models;

namespace ServerList.ViewModelServices
{
    public interface IServerListService
    {
        Task<LocationResponse> GetLocationData();
        Task<IEnumerable<LogicalServer>> GetLogicalServers(LocationResponse filterLocation);
    }
}
