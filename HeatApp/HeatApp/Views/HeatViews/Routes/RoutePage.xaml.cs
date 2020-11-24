using HeatApp.ViewModels.HeatViewModels.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews.Routes
{
    public partial class RoutePage : ContentPage
    {
        public RoutePage()
        {
            InitializeComponent();
            BindingContext = new RouteViewModel(Navigation);
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}