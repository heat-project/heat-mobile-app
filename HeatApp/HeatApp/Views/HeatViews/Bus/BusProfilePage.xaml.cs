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
        public BusProfilePage()
        {
            InitializeComponent();
        }
        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}