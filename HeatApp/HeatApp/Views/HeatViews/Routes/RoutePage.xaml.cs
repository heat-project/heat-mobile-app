using HeatApp.Models.HeatModels;
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
        SearchBar searchBar = null;

        public RoutePage()
        {
            InitializeComponent();
            BindingContext = new RouteViewModel(Navigation);
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (listView.DataSource != null)
            {
                this.listView.DataSource.Filter = FilterContacts;
                this.listView.DataSource.RefreshFilter();
            }
        }

        private bool FilterContacts(object obj)
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