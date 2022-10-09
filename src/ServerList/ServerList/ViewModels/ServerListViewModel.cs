using Core.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ServerList.ViewModels
{
    public class ServerListViewModel : INotifyPropertyChanged
    {
        private List<LogicalServer> _servers;
        public event PropertyChangedEventHandler PropertyChanged;

        public ServerListViewModel()
        {
            
        }

        public List<LogicalServer> Servers
        {
            get => _servers;
            set
            {
                if (_servers != value)
                {
                    _servers = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
