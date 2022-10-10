using Common.Http;
using ServerList.ViewModels;
using ServerList.ViewModelServices;

namespace ServerList.Views;

public partial class ServerListPage : ContentPage
{
    //public ServerListPage(IServerListService serverListService, IHttpWrapper httpWrapper)
    public ServerListPage()
    {
		InitializeComponent();
        // TODO: Can't get the DI to work here
        // //var settingsService = this.Handler.MauiContext.Services.GetServices<IServerListService>();
        //BindingContext = new ServerListViewModel(serverListService);
        BindingContext = new ServerListViewModel(new ServerListService(new HttpWrapper()));

    }
}