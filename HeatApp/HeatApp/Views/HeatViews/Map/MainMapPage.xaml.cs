using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Models;
using HeatApp.Models.HeatModels;
using HeatApp.ViewModels.Heat;
using HeatApp.Views.HeatViews.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Polyline = Xamarin.Forms.GoogleMaps.Polyline;

namespace HeatApp.Views.HeatViews.Common
{
    public partial class MainMapPage : ContentPage
    {
        Entry searchBar = null;
        RootPage rootPage;
        MainMapViewModel viewModel;

        public MainMapPage(RootPage rootPage)
        {
            InitializeComponent();
            viewModel = new MainMapViewModel(Navigation, map);
            BindingContext = viewModel;
            AddMapStyle();
            this.rootPage = rootPage;
        }
        private void SfButton_Clicked(object sender, EventArgs e)
        {
            rootPage.IsPresented = !rootPage.IsPresented;
            //GetCurrentLocation();
        }

        private void AddMapStyle()
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            System.IO.Stream stream = assembly.GetManifestResourceStream($"HeatApp.MapStyle.json");
            string styleFile;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                styleFile = reader.ReadToEnd();
            }

            map.MapStyle = MapStyle.FromJson(styleFile);
        }

        // Do NOT mark async method.
        // Because Xamarin.Forms.GoogleMaps wait synchronously for this callback returns.
        void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            e.Handled = true;

            if (e.Pin.BindingContext is StopDTO)
            {
                if (viewModel.GetStopInfoCommand.CanExecute(e.Pin.BindingContext as StopDTO))
                    viewModel.GetStopInfoCommand.Execute(e.Pin.BindingContext as StopDTO);
            }
            else if (e.Pin.BindingContext is BusDTO)
            {
                if (viewModel.GoToBusProfileCommand.CanExecute(e.Pin.BindingContext as BusDTO))
                    viewModel.GoToBusProfileCommand.Execute(e.Pin.BindingContext as BusDTO);
            }
        }
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as Entry);
            if (lstRoutes.DataSource != null)
            {
                this.lstRoutes.DataSource.Filter = Filter;
                this.lstRoutes.DataSource.RefreshFilter();
            }
        }

        private bool Filter(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var contacts = obj as RouteDTO;
            if (contacts.Description.ToLower().Contains(searchBar.Text.ToLower())
                 || contacts.Description.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else if ((contacts.Code?.ToLower().Contains(searchBar.Text.ToLower())).GetValueOrDefault(false)
                 || (contacts.Code?.ToLower().Contains(searchBar.Text.ToLower())).GetValueOrDefault(false))
                return true;
            else
                return false;
        }
    }
}