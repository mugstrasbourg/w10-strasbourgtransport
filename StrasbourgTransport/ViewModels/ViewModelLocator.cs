using Autofac;
using GalaSoft.MvvmLight.Views;
using StrasbourgTransport.Services;
using StrasbourgTransport.Views;

namespace StrasbourgTransport.ViewModels
{
    public class ViewModelLocator
    {
        public const string HomePageKey = "HomePage";
        public const string InfoTraficPageKey = "InfoTraficPage";
        public const string FavorisPageKey = "FavorisPage";
        public const string AboutPageKey = "AboutPage";

        private readonly IContainer container;

        public ViewModelLocator()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<HomeViewModel>();
            containerBuilder.RegisterType<InfoTraficViewModel>();
            containerBuilder.RegisterType<FavorisViewModel>();

            DataService service = new DataService();
            containerBuilder.RegisterInstance(service).As<IDataService>();

            NavigationService navigationService = new NavigationService();
            navigationService.Configure(HomePageKey, typeof(HomePage));
            navigationService.Configure(InfoTraficPageKey, typeof(InfoTraficPage));
            navigationService.Configure(FavorisPageKey, typeof(FavorisPage));
            navigationService.Configure(AboutPageKey, typeof(AboutPage));
            containerBuilder.RegisterInstance(navigationService).As<INavigationService>();

            containerBuilder.RegisterType<DialogService>().As<IDialogService>();

            // Create an IContainer instance which can resolve the registered types
            container = containerBuilder.Build();
        }

        public HomeViewModel Home => container.Resolve<HomeViewModel>();

        public InfoTraficViewModel InfoTrafic => container.Resolve<InfoTraficViewModel>();

        public FavorisViewModel Favoris => container.Resolve<FavorisViewModel>();
    }
}
