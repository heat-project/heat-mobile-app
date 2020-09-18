using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews.Common
{
    public partial class MainMapPage : ContentPage
    {
        RootPage rootPage;
        public MainMapPage(RootPage rootPage)
        {
            InitializeComponent();
            Position position = new Position(36.9628066, -122.0194722);
            Pin pin = new Pin
            {
                Label = "Casa",
                Address = "Esta es mi casa",
                Type = PinType.Place,
                Position = position
            };
            map.Pins.Add(pin);
            this.rootPage = rootPage;
            //rootPage.IsPresentedChanged += SfButton_Clicked;
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            this.rootPage.IsPresented = !this.rootPage.IsPresented;
        }
    }
}