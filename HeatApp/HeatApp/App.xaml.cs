using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Interfaces.Map;
using HeatApp.Interfaces.Routes;
using HeatApp.Services;
using HeatApp.Services.Review;
using HeatApp.Views.Catalog;
using HeatApp.Views.Dashboard;
using HeatApp.Views.Forms;
using HeatApp.Views.HeatViews;
using HeatApp.Views.HeatViews.Common;
using HeatApp.Views.Navigation;
using HeatApp.Views.Profile;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeatApp
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE4NzkyQDMxMzgyZTMyMmUzMEpENWQ0ZXM5WXpWc2hyNW1xNXI0OUYveU9Fd3VWS21KQ0xqZHZBdnRiNWc9");
            SetServices();
            if (IsLogged())
                MainPage = new RootPage();
            else
                MainPage = new NavigationPage(new InitialPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #region AuxiliarMethods
        public void SetServices()
        {
            DependencyService.Register<ISecurityService, SecurityService>();
            DependencyService.Register<IRouteService, RouteService>();
            DependencyService.Register<IBusService, BusService>();
            DependencyService.Register<IGoogleMapsApiService, GoogleMapsApiService>();
            DependencyService.Register<IMapService, MapService>();
            DependencyService.Register<IReviewService, ReviewService>();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsAPIKey);
        }
        private bool IsLogged()
        {
            return !string.IsNullOrEmpty(Settings.Token) && !string.IsNullOrWhiteSpace(Settings.Token);
        }
        #endregion
    }
}
