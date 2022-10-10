using Common.Http;
using Microsoft.Extensions.Logging;
using ServerList.ViewModels;
using ServerList.ViewModelServices;
using ServerList.Views;

namespace ServerList;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.RegisterAppServices();
		builder.RegisterViewModels();
		builder.RegisterViews();

		builder.Services.AddLogging((loggingBuilder) => loggingBuilder
						.SetMinimumLevel(LogLevel.Trace));
			
        return builder.Build();
	}

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IHttpWrapper, HttpWrapper>();
        mauiAppBuilder.Services.AddSingleton<IServerListService, ServerListService>();

		return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ServerListViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<AppShell>();
        mauiAppBuilder.Services.AddSingleton<AboutPage>();
        mauiAppBuilder.Services.AddSingleton<ServerListPage>();

        return mauiAppBuilder;
    }
}
