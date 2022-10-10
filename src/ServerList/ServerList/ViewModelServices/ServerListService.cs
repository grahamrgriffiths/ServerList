using Common.Http;
using Core.Models;
using Newtonsoft.Json;

namespace ServerList.ViewModelServices
{
    public class ServerListService : IServerListService
    {
        private IHttpWrapper _httpWrapper;
        public ServerListService(IHttpWrapper httpWrapper)
        {
            _httpWrapper = httpWrapper;
        }

        public async Task<LocationResponse> GetLocationData()
        {
            var httpResponse = await _httpWrapper.HttpGetAsync("https://api.protonvpn.ch/vpn/location", "location.json");
            return JsonConvert.DeserializeObject<LocationResponse>(httpResponse);
        }

        public async Task<IEnumerable<LogicalServer>> GetLogicalServers(LocationResponse filterLocation)
        {
            var httpResponse = await _httpWrapper.HttpGetAsync("https://api.protonvpn.ch/vpn/logicals", "Logicals.json");
            var deserializedLogicals = JsonConvert.DeserializeObject<LogicalsResponse>(httpResponse);

            // The servers should be listed by the distance from the current location in an ascending order.
            var orderedServers = deserializedLogicals.LogicalServers.OrderBy(x => deserializedLogicals.LogicalServers.FindIndex(y => x.Name.Contains(filterLocation.Country)));
            return orderedServers;
        }
    }
}
