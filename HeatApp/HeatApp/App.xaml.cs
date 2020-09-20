using HeatApp.Views.Dashboard;
using HeatApp.Views.Forms;
using HeatApp.Views.HeatViews;
using HeatApp.Views.HeatViews.Common;
using HeatApp.Views.Navigation;
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
    }
}
