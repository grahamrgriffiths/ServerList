using Core.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ServerList.ViewModels
{
    public class ServerListViewModel : BaseViewModel
    {
        const int RefreshDuration = 2;
        bool isRefreshing;

        LogicalServer selectedServer;

        public ObservableCollection<LogicalServer> Servers { get; private set; } = new ObservableCollection<LogicalServer>();

        private LocationResponse _location;
        public LocationResponse Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => new Command(async () => await RefreshDataAsync());
        public ICommand CloseCommand => new Command(Close);

        public ServerListViewModel()
        {
            PopulateServerList();
        }

        private async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            PopulateServerList();
            IsRefreshing = false;
        }

        private void Close()
        {
            Application.Current?.CloseWindow(Application.Current.MainPage.Window);
        }

        private void PopulateServerList()
        {
            // todo: Move to viewModelService class
            var httpClient = new HttpClient();
            string response;

            try
            {
                response = httpClient.GetStringAsync("https://api.protonvpn.ch/vpn/location").Result;
                File.WriteAllText(Path.GetTempPath() + "\\Location.json", response);
            }
            catch (Exception)
            {
                response = File.ReadAllText(Path.GetTempPath() + "\\Location.json");
            }

            var deserializedLocation = JsonConvert.DeserializeObject<LocationResponse>(response);

            Location = deserializedLocation;

            try
            {
                response = httpClient.GetStringAsync("https://api.protonvpn.ch/vpn/logicals").Result;
                File.WriteAllText(Path.GetTempPath() + "\\Logicals.json", response);
            }
            catch (Exception)
            {
                response = File.ReadAllText(Path.GetTempPath() + "\\Logicals.json");
            }

            var deserializedLogicals = JsonConvert.DeserializeObject<LogicalsResponse>(response);

            //Servers = new List<LogicalServer>();
            foreach (var server in deserializedLogicals.LogicalServers)
            {
                if (server.Status == 1)
                    Servers.Add(server);
            }
        }
    }
}
