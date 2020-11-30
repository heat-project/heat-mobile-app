using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews.Bus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusProfilePage : ContentPage
    {
        private Action FollowRoute;
        public BusProfilePage(Action followRoute)
        {
            InitializeComponent();
            FollowRoute = followRoute;
        }
        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void SfButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            FollowRoute();
        }
    }
}