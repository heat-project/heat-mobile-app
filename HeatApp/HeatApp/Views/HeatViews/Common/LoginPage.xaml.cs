using HeatApp.ViewModels.HeatViewModels.Security;
using HeatApp.Views.HeatViews.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new RootPage();
        }
    }
}