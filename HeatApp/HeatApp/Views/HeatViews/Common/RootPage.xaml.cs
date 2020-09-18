using HeatApp.Views.HeatViews.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();
            Master = new MenuPage();
            Detail = new NavigationPage(new MainMapPage(this));
        }
        private void LaunchPageInDetail(Page page)
        {
            if (page != null) { Detail = page; }

            IsPresented = false;
        }
    }
}