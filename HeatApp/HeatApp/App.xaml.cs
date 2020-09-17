using HeatApp.Views.Dashboard;
using HeatApp.Views.Forms;
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQxOTY0QDMxMzcyZTMyMmUzMEtFZTVmbkM5UHYyTmpCMTMyaGNyUlMvQ2l2cFlLbkowOW9EQk42WXBZa0U9");

            MainPage = new NavigationPage(new HealthCarePage());
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
    }
}
