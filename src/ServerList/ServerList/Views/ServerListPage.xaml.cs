using ServerList.ViewModels;

namespace ServerList.Views;

public partial class ServerListPage : ContentPage
{
	public ServerListPage()
	{
		InitializeComponent();
		BindingContext = new ServerListViewModel();
	}

    private void CloseButton_Clicked(object sender, EventArgs e)
	{
		

    }

	private void RefreshButton_Clicked(object sender, EventArgs e)
	{

	}
}