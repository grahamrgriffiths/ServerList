using Common.Http;
using Core.Models;
using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;

namespace ServerList.ViewModelServices
{
    /// <summary>
    /// Logic for service list view model
    /// </summary>
    public class ServerListService : IServerListService
    {
        private readonly IHttpWrapper _httpWrapper;
        public ServerListService(IHttpWrapper httpWrapper)
        {
            _httpWrapper = httpWrapper;
        }

        public async Task<LocationResponse> GetLocationData()
        {
            // TODO: Move URI to config
            var httpResponse = await _httpWrapper.HttpGetAsync("https://api.protonvpn.ch/vpn/location", "location.json");
            return JsonConvert.DeserializeObject<LocationResponse>(httpResponse);
        }

        public async Task<IEnumerable<LogicalServer>> GetLogicalServers(LocationResponse filterLocation)
        {
            // TODO: Move URI to config
            var httpResponse = await _httpWrapper.HttpGetAsync("https://api.protonvpn.ch/vpn/logicals", "Logicals.json");
            var deserializedLogicals = JsonConvert.DeserializeObject<LogicalsResponse>(httpResponse);

            // The servers should be listed by the distance from the current location in an ascending order.
            var currentLocation = new Location { Latitude = filterLocation.Lat, Longitude = filterLocation.Long };           

            var orderedServers = deserializedLogicals.LogicalServers.OrderBy(x => Location.CalculateDistance(
                currentLocation,
                new Location(x.Location.Lat, x.Location.Long),
                DistanceUnits.Miles
                ));

            return orderedServers;
        }
    }
}
