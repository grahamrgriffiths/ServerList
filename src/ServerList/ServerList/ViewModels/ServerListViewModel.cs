using Core.Models;
using ServerList.ViewModelServices;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ServerList.ViewModels
{
    /// <summary>
    /// View model for server list page
    /// </summary>
    public class ServerListViewModel : BaseViewModel
    {
        private readonly IServerListService _serverListService;
        bool isRefreshing;

        public ObservableCollection<LogicalServer> Servers { get; private set; } = new ObservableCollection<LogicalServer>();

        private string _location;
        public string LocationCountry
        {
            get => _location;
            set => SetProperty(ref _location, value, nameof(LocationCountry));
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

        public ICommand RefreshCommand => new Command(async () => await RefreshDataAsync());
        public ICommand CloseCommand => new Command(Close);

        public ServerListViewModel(IServerListService serverListService)
        {
            _serverListService = serverListService;        
            LocationCountry = "N/A";
            PopulateViewModelData();
        }

        private async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            //await Task.Delay(TimeSpan.FromSeconds(Constants.DEFAULT_REFRESH_DURATION));
            PopulateViewModelData();
            IsRefreshing = false;
        }

        private void Close()
        {
            Application.Current?.CloseWindow(Application.Current.MainPage.Window);
            Application.Current?.Quit();
        }

        private async void PopulateViewModelData()
        {
            // TODO: Location is not updating on the view
            var locationResponse = await _serverListService.GetLocationData();
            LocationCountry = locationResponse.Country;

            var logicalServers = await _serverListService.GetLogicalServers(locationResponse);
            
            foreach (var server in logicalServers.Where(server => server.Status == 1))
            {
                Servers.Add(server);
            }
        }
    }
}
